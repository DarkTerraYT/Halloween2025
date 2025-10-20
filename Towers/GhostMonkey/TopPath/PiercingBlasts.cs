using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Data.Behaviors.Projectiles;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

namespace Halloween2025.Towers.GhostMonkey.MiddlePath;

public class PiercingBlasts : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;
        projectile.pierce += 2;
        weapon.rate *= 0.9f;
    }

    public override string Description => "Soul bolts can pierce through 2 more bloons, and are shot out 10% faster.";

    public override int Path => Top;
    public override int Tier => 1;
    public override int Cost => 350;
}