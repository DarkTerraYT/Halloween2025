using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers.PlagueDoctor;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level20 : HeroLevel
{
    public override string Description => "Normal needles now apply the mega plague.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAbility(1).GetBehavior<ActivateAttackModel>().attacks[0].GetDescendant<DamageOverTimeModel>()
            .damage *= 1.25f;
        towerModel.GetAbility(1).GetBehavior<ActivateAttackModel>().attacks[0].GetDescendant<DamageOverTimeModel>()
            .interval /= 1.25f;

        var addBehaviors =
            towerModel.GetWeapon().projectile
                .GetBehavior<AddBehaviorToBloonModel>();
        addBehaviors.RemoveBehavior<DamageOverTimeModel>();
        addBehaviors.AddBehavior(towerModel.GetAbility(1).GetBehavior<ActivateAttackModel>()
            .GetDescendant<DamageOverTimeModel>().Duplicate());
        addBehaviors.ApplyOverlay<MegaPoison>();
        addBehaviors.GetBehavior<CarryProjectileModel>().projectile.GetBehavior<AddBehaviorToBloonModel>()
            .RemoveBehavior<DamageOverTimeModel>();
        addBehaviors.GetBehavior<CarryProjectileModel>().projectile.GetBehavior<AddBehaviorToBloonModel>().AddBehavior(
            towerModel.GetAbility(1).GetBehavior<ActivateAttackModel>().GetDescendant<DamageOverTimeModel>()
                .Duplicate());
    }
}