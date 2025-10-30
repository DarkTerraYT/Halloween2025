using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level2 : HeroLevel
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetDescendant<AddBehaviorToBloonModel>().lifespan = 4;
        towerModel.GetWeapon().projectile.pierce++;
    }

    public override string Description => "DOT lasts for one more second and needles pierce one bloon.";
}