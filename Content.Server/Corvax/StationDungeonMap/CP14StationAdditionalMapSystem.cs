using Content.Server._NF.GameRule;
using Content.Server.GameTicking;
using Content.Server.Maps;
using Content.Server.Parallax;
using Content.Server.Station.Components;
using Content.Server.Station.Events;
using Content.Server.Station.Systems;
using Content.Shared.Teleportation.Systems;
using Robust.Server.GameObjects;
using Robust.Server.Maps;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using System.Numerics;

namespace Content.Server._CP14.StationDungeonMap;

public sealed partial class CP14StationAdditionalMapSystem : EntitySystem
{
    [Dependency] private readonly IPrototypeManager _proto = default!;
    [Dependency] private readonly MapSystem _map = default!;
    [Dependency] private readonly LinkedEntitySystem _linkedEntity = default!;
    [Dependency] private readonly StationSystem _station = default!;
    [Dependency] private readonly MapLoaderSystem _mapLoader = default!;
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly MetaDataSystem _meta = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CP14StationAdditionalMapComponent, StationPostInitEvent>(OnStationPostInit);
    }

    public void InitializeStation(MapId mapUid, CP14StationAdditionalMapComponent proto, Vector2 offset, out EntityUid? gridUid, string? overrideName = null)
    {
        gridUid = null;

        foreach (var mapPath in proto.MapPaths)
        {
            if (_mapLoader.TryLoad(mapUid, mapPath.ToString(), out var mapUids, // Используем конкретный путь
                new MapLoadOptions
                {
                    Offset = offset,
                    Rotation = _random.NextAngle()
                }))
            {
                string stationName = string.IsNullOrEmpty(overrideName) ? proto.Name : overrideName;

                EntityUid? stationUid = null;
                if (_proto.TryIndex<GameMapPrototype>(proto.ID, out var stationProto))
                {
                    stationUid = _station.InitializeNewStation(stationProto.Stations[proto.ID], mapUids, stationName);
                }

                foreach (var grid in mapUids)
                {
                    var meta = EnsureComp<MetaDataComponent>(grid);
                    _meta.SetEntityName(grid, stationName, meta);
                    EntityManager.AddComponents(grid, proto.AddComponents);
                }
            }
        }

    }
    private void OnStationPostInit(Entity<CP14StationAdditionalMapComponent> addMap, ref StationPostInitEvent args)
    {
        if (!TryComp(addMap, out StationDataComponent? dataComp))
            return;

        foreach (var path in addMap.Comp.MapPaths)
        {
            var mapUid = _map.CreateMap(out var mapId);
            Log.Info($"Created map {mapId} for StationAdditionalMap system");
            var options = new MapLoadOptions { LoadMap = true };
            if (!_mapLoader.TryLoad(mapId, path.ToString(), out var roots, options))
            {
                Log.Error($"Failed to load map from {path}!");
                Del(mapUid);
                return;
            }
        }
    }
}
