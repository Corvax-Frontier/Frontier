using Robust.Shared.Serialization;

namespace Content.Shared._Corvax.BSDash;

[Serializable, NetSerializable]
public sealed class BSDashEvent : EntityEventArgs
{
    public NetEntity Shuttle { get; }

    public BSDashEvent(NetEntity shuttle)
    {
        Shuttle = shuttle;
    }
}