using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level12 : HeroLevel
{
    public override string Description => "Plague does 25% more damage to MOAB-class bloons.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetWeapon().projectile
            .AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs", 1.25f, 0, false,
                true));
    }
}