using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level8 : HeroLevel
{
    public override string Description =>
        "Bloons can spread infections to bloons 20% away. DOT ticks another 35% faster";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().GetBehavior<CarryProjectileModel>()
            .projectile.radius += 2;
        towerModel.GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().GetBehavior<DamageOverTimeModel>()
            .interval *= 0.65f;
    }
}