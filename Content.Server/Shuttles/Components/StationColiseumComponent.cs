using Robust.Shared.Map;
using Robust.Shared.Utility;

namespace Content.Server.Shuttles.Components;

[RegisterComponent]
public sealed partial class StationColiseumComponent : Component
{
    [DataField]
    public float ShuttleIndex;

    [DataField]
    public ResPath Map = new("/Maps/Corvax/POI/coliseum.yml");

    [DataField]
    public EntityUid? Entity;

    [DataField]
    public EntityUid? MapEntity;
}
