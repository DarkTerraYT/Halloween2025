using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.GhostMonkey.BottomPath;

public class Retribution : ModUpgrade<GhostMonkey>
{
    public override string Description =>
        "Bloon souls can spawn more bloon souls after hitting a bloon, and these souls do double damage.";

    public override int Path => Bottom;
    public override int Tier => 5;
    public override int Cost => 820;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        var projectile = weapon.projectile;

        projectile.GetBehavior<CreateProjectileOnContactModel>().projectile
            .AddBehavior(projectile.GetBehavior<CreateProjectileOnContactModel>().Duplicate());
        projectile.GetBehavior<CreateProjectileOnContactModel>().projectile
            .GetBehavior<CreateProjectileOnContactModel>().projectile.name = "BloonSoul_DoubleDamage";
    }
}