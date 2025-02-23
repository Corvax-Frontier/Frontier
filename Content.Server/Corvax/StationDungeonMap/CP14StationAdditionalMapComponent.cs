using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Server._CP14.StationDungeonMap;

/// <summary>
/// Loads additional maps from the list at the start of the round.
/// </summary>
[RegisterComponent, Access(typeof(CP14StationAdditionalMapSystem))]
public sealed partial class CP14StationAdditionalMapComponent : Component
{
    /// <summary>
    /// A map paths to load on a new map.
    /// </summary>
    [DataField(required: true)]
    public List<ResPath> MapPaths = new();

    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField(required: true)]
    public string Name { get; private set; } = "";

    [DataField]
    [AlwaysPushInheritance]
    public ComponentRegistry AddComponents { get; set; } = new();

}
