using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;

namespace Halloween2025.Towers.GhostMonkey.BottomPath;

public class DualBlasts : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();

        if (towerModel.tiers[1] < 3 && towerModel.tiers[0] < 3)
        {
            weapon.SetEmission(new ArcEmissionModel("ArcEmissionModel_", 2, 0, 10, null, false, false));
        }
        else
        {
            BurstWeaponBehaviorModel burst = new("BurstWeaponBehaviorModel_", 0.33f, 2, true);
            weapon.AddBehavior(burst);
        }
    }

    public override string Description => "Ghost monkey shoots out twice the bolts! \"No, the bolts aren't dueling.\" If Ghost Monkey is a Banshee or Jiangshi, attacks twice in a burst attack";

    public override int Path => Bottom;
    public override int Tier => 1;
    public override int Cost => 415;
}