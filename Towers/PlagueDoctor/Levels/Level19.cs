using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level19 : HeroLevel
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAbility(1).GetBehavior<ActivateAttackModel>().lifespan = 2.95f;
    }

    public override string Description => "Plague bringer spawns 3 bloons instead of two.";
}