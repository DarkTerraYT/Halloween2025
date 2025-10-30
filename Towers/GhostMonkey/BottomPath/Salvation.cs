using System;
using System.IO;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets;
using Halloween2025.Assets.Towers;
using HarmonyLib;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppSystem.Collections.Generic;
using MelonLoader.Utils;
using UnityEngine;

namespace Halloween2025.Towers.GhostMonkey.BottomPath;

public class Salvation : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        weapon.rate *= 0.45f;
        var projectile = weapon.projectile;
        projectile.id = "h25_salvation_1";
        
        var createProj = new CreateProjectileOnContactModel("CreateProjectileOnContactModel_", projectile.Duplicate(), new SingleEmissionModel("SingleEmissionModel_", null), false, false, false);
        var projectile2 = createProj.projectile;
        projectile2.RemoveBehavior<TravelStraitModel>();
        projectile2.RemoveBehavior<TrackTargetModel>();
        projectile2.AddBehavior(new TravelAlongPathModel("TravelAlongPathModel", 300f, 1000, false, true, 1));
        projectile2.ApplyDisplay<BloonSoul>();
        projectile2.id = "h25_salvation_2";
        projectile.AddBehavior(createProj);
    }

    [HarmonyPatch(typeof(CreateProjectileOnContact), nameof(CreateProjectileOnContact.Collide))]
    private static class CreateProjectileOnContact_Collide
    {
        public static HashSet<ObjectId> ModifiedBloons = new();
        
        public static bool Prefix(CreateProjectileOnContact __instance, Bloon bloon)
        {
            if (!__instance.projectile.projectileModel.id.StartsWith("h25_salvation_") ||
                ModifiedBloons.Contains(bloon.Id))
            {
                return false;
            }
            
            var projectile = __instance.createProjectileOnContactModel.projectile;
            projectile.GetBehavior<TravelAlongPathModel>().speed = bloon.bloonModel.speed;
            if (bloon.bloonModel.isMoab)
            {
                projectile.ApplyDisplay<MoabBloonSoul>();
                projectile.GetBehavior<TravelAlongPathModel>().disableRotateWithPathDirection = false;
            }
            else
            {
                projectile.ApplyDisplay<BloonSoul>();
                projectile.GetBehavior<TravelAlongPathModel>().disableRotateWithPathDirection = true;
            }
            projectile.GetDamageModel().damage = 3 * Mathf.Pow(bloon.bloonModel.danger, 2.5f);
            if (projectile.name == "BloonSoul_DoubleDamage")
            {
                projectile.GetDamageModel().damage *= 2;
            }
            ModifiedBloons.Add(bloon.Id);

            var udn = bloon.GetUnityDisplayNode();
            if (udn)
            {
                foreach (var spriteRenderer in udn.GetRenderers<SpriteRenderer>())
                {
                    spriteRenderer.material.shader = AssetHelper.GetShader("Desaturate");
                }

                foreach (var meshRenderer in udn.GetMeshRenderers())
                {
                    var newMat = meshRenderer.material.Duplicate();
                    newMat.shader = AssetHelper.GetShader("Desaturate");
                }
            }
            
            return true;
        }
    }
    public override string Description => "Bloons hit by the spectre release their soul, their strength tied to the strength of the bloon. Also does more damage, and attacks faster.";

    public override int Path => Bottom;
    public override int Tier => 4;
    public override int Cost => 820;
}
