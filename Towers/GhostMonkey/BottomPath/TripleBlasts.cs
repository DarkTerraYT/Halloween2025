using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;

namespace Halloween2025.Towers.GhostMonkey.BottomPath;

public class TripleBlasts : ModUpgrade<GhostMonkey>
{
    public override string Description =>
        "Ghost monkey shoots out three bolts! \"The more bolts the better, right?\" If a Jiangshi or Banshee attacks three times in a burst attack.";

    public override int Path => Bottom;
    public override int Tier => 2;
    public override int Cost => 675;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();

        if (towerModel.tiers[Top] < 3 && towerModel.tiers[Middle] < 3)
        {
            var emission = weapon.emission.Cast<ArcEmissionModel>();
            emission.count = 3;
            emission.angle = 45;
        }
        else
        {
            var burst = weapon.GetBehavior<BurstWeaponBehaviorModel>();
            burst.count = 3;
        }
    }
}