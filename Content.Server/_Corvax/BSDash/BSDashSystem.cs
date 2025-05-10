using System.Numerics;
using Content.Server.Shuttles.Components;
using Content.Shared._Corvax.BSDash;
using Robust.Server.GameObjects;
using Robust.Shared.Physics.Components;
using Robust.Shared.Timing;

namespace Content.Server._Corvax.BSDash;

public sealed class BSDashSystem : EntitySystem
{
    [Dependency] private readonly PhysicsSystem _physics = default!;
    [Dependency] private readonly GameTiming _gameTiming = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeNetworkEvent<BSDashEvent>(OnBSDash);
    }

    private void OnBSDash(BSDashEvent args)
    {
        if (TryComp<ShuttleComponent>(_gameTiming))
        {

        }
        var shuttle = GetEntity(args.Shuttle);
        var xform = Transform(shuttle);
        Vector2 direction = xform.LocalRotation.ToWorldVec();

        if (TryComp<PhysicsComponent>(shuttle, out var physics))
        {
            float forceMagnitude = 50f; // Настройка силы
            Vector2 force = -direction * forceMagnitude;
            _physics.ApplyLinearImpulse(shuttle, force, body: physics);
        }
    }
}