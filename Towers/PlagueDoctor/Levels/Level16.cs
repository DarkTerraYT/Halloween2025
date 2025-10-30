using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers.PlagueDoctor;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level16 : HeroLevel
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAbility(1).GetBehavior<ActivateAttackModel>().attacks[0].GetDescendant<DamageOverTimeModel>()
            .damage *= 1.25f;
        towerModel.GetAbility(1).GetBehavior<ActivateAttackModel>().attacks[0].GetDescendant<DamageOverTimeModel>()
            .interval /= 1.25f;

        var frayAddBehaviors =
            towerModel.GetAbility().GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile
                .GetBehavior<AddBehaviorToBloonModel>();
        frayAddBehaviors.RemoveBehavior<DamageOverTimeModel>();
        frayAddBehaviors.AddBehavior(towerModel.GetAbility(1).GetBehavior<ActivateAttackModel>().GetDescendant<DamageOverTimeModel>().Duplicate());
        frayAddBehaviors.ApplyOverlay<MegaPoison>();
    }

    public override string Description => "Mega plague does 50% more DPS. Fray's projectiles now also apply this mega plague.";
}