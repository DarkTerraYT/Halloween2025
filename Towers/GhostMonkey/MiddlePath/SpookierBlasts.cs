using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.GhostMonkey.TopPath;

public class SpookierBlasts : ModUpgrade<GhostMonkey>
{
    public override string Description =>
        "Soul bolts slow bloons down 33% more. Also shot out faster. \"Sends shivers down your spine!\"";

    public override int Path => Middle;
    public override int Tier => 2;
    public override int Cost => 820;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;
        weapon.rate *= 0.9f;
        projectile.GetBehavior<SlowModel>().multiplier = 0.5f;
    }
}