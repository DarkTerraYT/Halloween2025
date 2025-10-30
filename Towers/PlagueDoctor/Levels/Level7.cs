using System.Collections.Generic;
using System.Linq;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using HarmonyLib;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppNinjaKiwi.Common.ResourceUtils;
using Newtonsoft.Json;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level7 : HeroLevel
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        ProjectileModel proj = towerModel.GetWeapon().projectile.Duplicate();
            proj.id = "Plague_Spread";
            proj.radius = 10;
            proj.display = new();
            proj.pierce = 99999;
            proj.AddFilter(new FilterOutTagModel("FilterOutTagModel_Amog_Plague", "Amog_Plague", null));
            proj.RemoveBehavior<TravelStraitModel>();
            proj.AddBehavior(new AgeModel("AgeModel_", 0.1f, 1, false, null));
        CarryProjectileModel projectileOverTimeModel = new("ProjectileOverTimeModel_Plague", proj,
            new SingleEmissionModel("SingleEmissionModel_", null), ObjectId.Invalid);
        towerModel.GetWeapon().projectile.GetBehavior<AddBehaviorToBloonModel>().AddBehavior(projectileOverTimeModel);
    }

    [HarmonyPatch(typeof(CarryProjectile), nameof(CarryProjectile.Emit))]
    private static class ProjectileOverTime_CreateProjectile
    {
        public static bool Prefix(CarryProjectile __instance)
        {
            Tower? amog = InGame.instance.GetTowers().Find(t => t.towerModel.baseId == TowerID<PlagueDoctor>());
            if (amog == null || amog.towerModel.tiers[0] < 7 || !__instance.carryProjectileModel.name.Contains("ProjectileOverTimeModel_Plague"))
            {
                return true;
            }

            if (__instance.Sim.time.elapsed %
                 (amog.towerModel.tiers[0] < 11 ? 3 : amog.towerModel.tiers[0] < 15 ? 2 : 1) == 0)
            {
                return true;
            }

            return false;
        }
    }

    public override string Description => "Every 3 seconds, bloons nearby already infected bloons also get infected.";
}