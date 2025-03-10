// Port from Nuclear 14 Corvax

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

namespace Content.Server.Corvax.StationDungeonMap;

public sealed partial class ExpansionMapSystem : EntitySystem
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
        SubscribeLocalEvent<ExpansionMapComponent, StationPostInitEvent>(OnStationPostInit);
    }
    private void OnStationPostInit(Entity<ExpansionMapComponent> addMap, ref StationPostInitEvent args)
    {
        if (!TryComp(addMap, out StationDataComponent? dataComp))
            return;

        foreach (var path in addMap.Comp.MapPaths)
        {
            var mapUid = _map.CreateMap(out var mapId);
            Log.Info($"Created map {mapId} for ExpansionMap system");
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
