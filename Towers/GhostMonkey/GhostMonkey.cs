using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common;
using Il2CppSystem.Collections.Generic;

namespace Halloween2025.Towers.GhostMonkey;

public class GhostMonkey : ModTower<HalloweenTowers>
{
    public override void ModifyBaseTowerModel(TowerModel towerModel)
    {
        towerModel.ignoreBlockers = true;
        towerModel.IncreaseRange(15);
        
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;

        towerModel.GetAttackModel().attackThroughWalls = true;

        weapon.rate = 2;
        weapon.name = "Ghost";

        projectile.pierce = 5;
        projectile.ignoreBlockers = true;
        projectile.GetDamageModel().damage = 2;
        var travelStraitModel = projectile.GetBehavior<TravelStraitModel>();
        travelStraitModel.lifespan *= 3;
        travelStraitModel.speed /= 3;

        var damageModiferForStateModel =
            new DamageModifierForBloonStateModel("DamageModifierForBloonStateModel_Spooked", "Spooked", 1.25f, 1, false, true, false);
        projectile.AddBehavior(damageModiferForStateModel);
        var damageModiferForStateModel2 =
            new DamageModifierForBloonStateModel("DamageModifierForBloonStateModel_Spooked2", "Spooked2",2, 3, false, true, false);
        projectile.AddBehavior(damageModiferForStateModel2);
        
        projectile.ApplyDisplay<GhostBolt>();

        towerModel.areaTypes = new Il2CppStructArray<AreaType>([
            AreaType.unplaceable, AreaType.ice, AreaType.land, AreaType.water, AreaType.waterMermonkey,
            AreaType.removable
        ]);
    }

    public override string Description => "Soul of a long forgotten monkey. Shoots out slow moving soul bolts. Ghost monkeys can be placed anywhere (except on other monkeys or the track) and can attack through walls.";

    public override string BaseTower => "DartMonkey";
    public override int Cost => 700;
    public override string Icon => Portrait;

    public override bool IsValidCrosspath(int[] tiers) =>
        ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
}