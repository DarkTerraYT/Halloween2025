using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;

namespace Halloween2025.Towers.GhostMonkey;

public class GhostMonkey : ModTower<HalloweenTowers>
{
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;

        weapon.rate = 2;

        projectile.pierce = 8;
        projectile.GetDamageModel().damage = 2;
        var travelStraitModel = projectile.GetBehavior<TravelStraitModel>();
        travelStraitModel.lifespan *= 3;
        travelStraitModel.speed /= 3;
        
        projectile.ApplyDisplay<GhostBolt>();
    }

    public override string Description => "Soul of a long forgotten monkey. Shoots out slow moving soul bolts";

    public override string BaseTower => "DartMonkey";
    public override int Cost => 700;
    public override string Icon => Portrait;
}