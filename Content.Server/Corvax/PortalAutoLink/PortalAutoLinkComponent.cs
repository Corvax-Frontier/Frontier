using Robust.Shared.Map;

namespace Content.Server._N14.PortalAutoLink;

[RegisterComponent, Access(typeof(PortalAutoLinkSystem))]
public sealed partial class PortalAutoLinkComponent : Component
{
    [DataField]
    public string? LinkKey { get; set; } = "ReplaceMe";
}
