using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public class Level5 : HeroLevel
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetWeapon().rate *= 0.85f;

        towerModel.IncreaseRange(5);
        towerModel.GetWeapon().projectile.GetBehavior<TravelStraitModel>().lifespan *= 1.1f;
        towerModel.GetWeapon().projectile.pierce++;
    }

    public override int Level => 5;

    public override string Description => "Amog flicks darts 15% faster in a further range. Needles pierce an extra bloon";
}