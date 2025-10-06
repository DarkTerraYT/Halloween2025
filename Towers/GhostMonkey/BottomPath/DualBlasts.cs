using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

namespace Halloween2025.Towers.GhostMonkey.BottomPath;

public class DualBlasts : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapon();
        
        weapon.SetEmission(new ArcEmissionModel("ArcEmissionModel_", 2, 0, 10, null, false, false));
    }

    public override string Description => "Ghost monkey shoots out twice the bolts! No, the bolts aren't dueling.";

    public override int Path => Bottom;
    public override int Tier => 1;
    public override int Cost => 415;
}