using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level13 : HeroLevel
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAbility(1).cooldown = 50;
    }

    public override string Description => "Plague bringer ability charges 17% faster.";
}