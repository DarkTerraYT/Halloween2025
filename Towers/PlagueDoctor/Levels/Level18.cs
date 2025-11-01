using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level18 : HeroLevel
{
    public override string Description => "Plague does double DPS.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().GetBehavior<DamageOverTimeModel>()
            .interval *= 0.5f;
    }
}