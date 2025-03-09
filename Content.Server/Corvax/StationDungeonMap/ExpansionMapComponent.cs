//Port from Nuclear 14 Corvax

using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Server.Corvax.StationDungeonMap;

/// <summary>
/// Loads additional maps from the list at the start of the round.
/// </summary>
[RegisterComponent, Access(typeof(ExpansionMapSystem))]
public sealed partial class ExpansionMapComponent : Component
{
    /// <summary>
    /// A map paths to load on a new map.
    /// </summary>
    [DataField(required: true)]
    public List<ResPath> MapPaths = new();

}
