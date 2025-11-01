using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level17 : HeroLevel
{
    public override string Description => "Amog flicks 2 more needles.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetWeapon().SetEmission(new ArcEmissionModel("ArcEmissionModel_", 5, 0, 45, null, false, false));
    }
}