using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;

namespace Halloween2025.Towers.GhostMonkey.MiddlePath;

public class SuperPiercingBlasts : ModUpgrade<GhostMonkey>
{
    public override string Description =>
        "Soul bolts can pierce through another 2 bloons, and are shot out another 10% faster.";

    public override int Path => Top;
    public override int Tier => 2;
    public override int Cost => 550;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;
        projectile.pierce += 2;
        weapon.rate *= 0.9f;
    }
}