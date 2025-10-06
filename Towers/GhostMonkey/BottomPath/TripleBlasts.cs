using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

namespace Halloween2025.Towers.GhostMonkey.BottomPath;

public class TripleBlasts : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        
        weapon.SetEmission(new ArcEmissionModel("ArcEmissionModel_", 3, 0, 45, null, false, false));
    }

    public override string Description => "Ghost monkey shoots out three bolts! The more bolts the better, right?";

    public override int Path => Bottom;
    public override int Tier => 2;
    public override int Cost => 675;
}