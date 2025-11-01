using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace Halloween2025.Towers.GhostMonkey.MiddlePath;

public class DeathlyAura : ModUpgrade<GhostMonkey>
{
    public override string Description =>
        "DOT does 10x more damage at a 4x faster tick rate. All nearby jiangsu do extra damage and every jiangsu attacks faster.";

    public override int Path => Top;
    public override int Tier => 5;
    public override int Cost => 50000;

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var weapon = towerModel.GetWeapons().Find(w => w.name == "Jiangshi")!;
        var projectile = weapon.projectile;

        var DOT = projectile.GetDescendant<DamageOverTimeModel>();
        DOT.damage *= 10;
        DOT.interval /= 4f;

        var filter = new Il2CppReferenceArray<TowerFilterModel>([
            new FilterInTowerTiersModel(TowerID<GhostMonkey>(), 3, 5, 0, 5, 0, 5)
        ]);

        DamageSupportModel supportModel = new("DeathlyAura_Buff", true, 5, "DeathlyAura_Buff",
            filter, false, false, 0);
        towerModel.AddBehavior(supportModel);

        RateSupportModel rateSupport =
            new("DeathlyAura_Buff2", 0.75f, true, "DeathlyAura_Buff2", true, 1, filter, "", "");
        rateSupport.ApplyBuffIcon<DeathlyAuraBuffIcon>();
        towerModel.AddBehavior(rateSupport);
        var buffIcon2 =
            new RateSupportModel("", 1, true, "", false, 0, filter, "",
                ""); // Buff Icon on DamageSupportModel doesn't work for whatever reason
        buffIcon2.ApplyBuffIcon<DeathlyAuraDamageBuffIcon>();
        towerModel.AddBehavior(buffIcon2);
    }

    public class DeathlyAuraDamageBuffIcon : ModBuffIcon
    {
        protected override int Order => 1;
        public override string Icon => "DeathlyAuraDamageBuffIcon";
        public override int MaxStackSize => 1;
    }

    public class DeathlyAuraBuffIcon : ModBuffIcon
    {
        protected override int Order => 2;
        public override string Icon => "DeathlyAuraBuffIcon";
        public override int MaxStackSize => 1;
    }
}