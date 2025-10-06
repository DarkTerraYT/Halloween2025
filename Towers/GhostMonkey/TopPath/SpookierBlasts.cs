using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Halloween2025.Assets;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.Effects;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppNinjaKiwi.Common;

namespace Halloween2025.Towers.GhostMonkey.TopPath;

public class SpookierBlasts : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;
        projectile.pierce += 2;
        weapon.rate *= 0.9f;
    }

    public override string Description => "Soul bolts now spook bloons slowing them down for a short amount of time. All Soul bolts from any ghost monkey do more damage to spooked bloons.";

    public override int Path => Top;
    public override int Tier => 2;
    public override int Cost => 820;
}