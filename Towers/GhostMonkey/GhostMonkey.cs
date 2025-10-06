using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace Halloween2025.Towers.GhostMonkey;

public class GhostMonkey : ModTower<HalloweenTowers>
{
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;

        weapon.rate = 2;

        projectile.pierce = 5;
        projectile.GetDamageModel().damage = 2;
        var travelStraitModel = projectile.GetBehavior<TravelStraitModel>();
        travelStraitModel.lifespan *= 3;
        travelStraitModel.speed /= 3;

        var damageModiferForStateModel =
            new DamageModifierForBloonStateModel("DamageModifierForBloonStateModel_Spooked", "Spooked", 1.25f, 1, true,
                true, false);
        projectile.AddBehavior(damageModiferForStateModel);
        
        projectile.ApplyDisplay<GhostBolt>();

        towerModel.areaTypes = new Il2CppStructArray<AreaType>([
            AreaType.unplaceable, AreaType.ice, AreaType.land, AreaType.water, AreaType.waterMermonkey,
            AreaType.removable
        ]);
    }

    public override string Description => "Soul of a long forgotten monkey. Shoots out slow moving soul bolts";

    public override string BaseTower => "DartMonkey";
    public override int Cost => 700;
    public override string Icon => Portrait;
}