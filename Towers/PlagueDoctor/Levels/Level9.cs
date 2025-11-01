using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level9 : HeroLevel
{
    public override string Description => "Amog throws three needles instead of one.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetWeapon().SetEmission(new ArcEmissionModel("ArcEmissionModel", 3, 0, 45, null, false, false));
    }
}