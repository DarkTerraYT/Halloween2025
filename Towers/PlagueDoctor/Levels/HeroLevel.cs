using BTD_Mod_Helper.Api.Towers;

namespace Halloween2025.Towers.PlagueDoctor.Levels;

public abstract class HeroLevel : ModHeroLevel<PlagueDoctor>
{
    public override int Level => int.Parse(GetType().Name.Replace("Level", ""));
}