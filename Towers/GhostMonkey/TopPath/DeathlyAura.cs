using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;

namespace Halloween2025.Towers.GhostMonkey.MiddlePath;

public class DeathlyAura : ModUpgrade<GhostMonkey>
{
    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapons().Find(w => w.name == "Jiangshi")!;
        var projectile = weapon.projectile;
    }

    public override string Description => "Soul bolts can pierce through 2 more bloons, and are shot out 10% faster.";

    public override int Path => Top;
    public override int Tier => 5;
    public override int Cost => 50000;
}