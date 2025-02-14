﻿using Content.Shared.Examine;
using Content.Shared.Tag;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype; //Corvax-Frontier

namespace Content.Shared.Construction.Steps
{
    public abstract partial class ArbitraryInsertConstructionGraphStep : EntityInsertConstructionGraphStep
    {
        [DataField("name")] public string Name { get; private set; } = string.Empty;

        [DataField("icon")] public SpriteSpecifier? Icon { get; private set; }

        [DataField("tag", customTypeSerializer: typeof(PrototypeIdSerializer<TagPrototype>))] //Corvax-Frontier
        public string? Tag { get; private set; } //Corvax-Frontier

        public override void DoExamine(ExaminedEvent examinedEvent)
        {
            if (string.IsNullOrEmpty(Name))
                return;

            examinedEvent.PushMarkup(Loc.GetString("construction-insert-arbitrary-entity", ("stepName", Name)));
        }

        public override ConstructionGuideEntry GenerateGuideEntry()
        {
            var prototypeManager = IoCManager.Resolve<IPrototypeManager>(); //Corvax-Frontier start
            var entityManager = IoCManager.Resolve<IEntityManager>();

            string? nameLocale = null;
            if (Tag is not null && prototypeManager.TryIndex<TagPrototype>(Tag, out var tag))
            {
                var entities = prototypeManager.EnumeratePrototypes<EntityPrototype>();

                foreach (var item in entities)
                {
                    if (item.TryGetComponent<TagComponent>(out var entityTag) && entityManager.System<TagSystem>().HasTag(entityTag, Tag))
                    {
                        nameLocale = item.Name;
                        break;
                    }
                }
            } //Corvax-Frontier end
            return new ConstructionGuideEntry
            {
                Localization = "construction-presenter-arbitrary-step",
                Arguments = new (string, object)[] { ("name", Loc.TryGetString($"step-{Name.Replace(' ', '-')}", out var translatedname) ? translatedname : nameLocale ?? Name) }, //Corvax-Frontier
                Icon = Icon,
            };
        }
    }
}
