using System.IO;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Helpers;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets.Towers;
using HarmonyLib;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppSystem.Collections.Generic;
using MelonLoader.Utils;

namespace Halloween2025.Towers.GhostMonkey.BottomPath;

public class Retribution : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;
        
        projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.AddBehavior(projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
        projectile.GetBehavior<CreateProjectileOnContactModel>().projectile
            .GetBehavior<CreateProjectileOnContactModel>().projectile.name = "BloonSoul_DoubleDamage";
    }
    public override string Description => "Bloon souls can spawn more bloon souls after hitting a bloon, and these souls do double damage.";

    public override int Path => Bottom;
    public override int Tier => 5;
    public override int Cost => 820;
}