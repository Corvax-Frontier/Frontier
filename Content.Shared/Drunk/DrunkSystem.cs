using Content.Shared.Speech.EntitySystems;
using Content.Shared.StatusEffect;
using Content.Shared.Traits.Assorted;

namespace Content.Shared.Drunk
{
    /// <summary>
    /// Система для управления эффектами алкогольного опьянения у сущностей.
    /// </summary>
    public abstract class SharedDrunkSystem : EntitySystem
    {
        [ValidatePrototypeId<StatusEffectPrototype>]
        public const string DrunkKey = "Drunk";

        [Dependency] private readonly StatusEffectsSystem _statusEffectsSystem = default!;
        [Dependency] private readonly SharedSlurredSystem _slurredSystem = default!;

        /// <summary>
        /// Применяет эффект опьянения к сущности, учитывая модификаторы и опционально добавляя эффект невнятной речи.
        /// </summary>
        /// <param name="uid">Идентификатор сущности.</param>
        /// <param name="boozePower">
        /// Базовая сила или длительность опьянения в секундах. При наличии компонента <see cref="LightweightDrunkComponent"/>
        /// значение умножается на его <c>BoozeStrengthMultiplier</c>.
        /// </param>
        /// <param name="applySlur">Флаг, указывающий, нужно ли применять эффект невнятной речи.</param>
        /// <param name="status">Необязательный компонент для работы со статус-эффектами.</param>
        public void TryApplyDrunkenness(EntityUid uid, float boozePower, bool applySlur = true, StatusEffectsComponent? status = null)
        {
            if (!Resolve(uid, ref status, false))
            {
                return;
            }

            // Корректируем силу опьянения, если сущность имеет соответствующий компонент
            if (TryComp<LightweightDrunkComponent>(uid, out var trait))
            {
                boozePower *= trait.BoozeStrengthMultiplier;
            }

            var effectDuration = TimeSpan.FromSeconds(boozePower);

            // Применяем эффект невнятной речи, если требуется
            if (applySlur)
            {
                _slurredSystem.DoSlur(uid, effectDuration, status);
            }

            // Если эффект опьянения отсутствует, добавляем его, иначе увеличиваем оставшееся время действия
            if (!_statusEffectsSystem.HasStatusEffect(uid, DrunkKey, status))
            {
                _statusEffectsSystem.TryAddStatusEffect<DrunkComponent>(uid, DrunkKey, effectDuration, true, status);
            }
            else
            {
                _statusEffectsSystem.TryAddTime(uid, DrunkKey, effectDuration, status);
            }
        }

        /// <summary>
        /// Удаляет эффект опьянения с сущности.
        /// </summary>
        /// <param name="uid">Идентификатор сущности.</param>
        public void TryRemoveDrunkenness(EntityUid uid)
        {
            _statusEffectsSystem.TryRemoveStatusEffect(uid, DrunkKey);
        }

        /// <summary>
        /// Уменьшает время действия эффекта опьянения на заданное количество секунд.
        /// </summary>
        /// <param name="uid">Идентификатор сущности.</param>
        /// <param name="timeRemoved">Количество секунд, которое необходимо убрать из оставшегося времени эффекта.</param>
        public void TryRemoveDrunkennessTime(EntityUid uid, double timeRemoved)
        {
            var reductionDuration = TimeSpan.FromSeconds(timeRemoved);
            _statusEffectsSystem.TryRemoveTime(uid, DrunkKey, reductionDuration);
        }
    }
}
