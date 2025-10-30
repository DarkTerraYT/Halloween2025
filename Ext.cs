using System;
using System.Linq;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;

namespace Halloween2025;

public static class Ext
{
    private const string DefaultShader = "NinjaKiwi/SimpleUnlitOutline";
    private static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
    
    public static void ApplyOutlineShader(this Material material)
    {
        var shader = Resources.FindObjectsOfTypeAll<Shader>().FirstOrDefault(shader => shader.name == DefaultShader);
        if (shader == null)
        {
            var dummyDisplay = Game.instance.model.GetTowerWithName(TowerType.DartMonkey).display;
            Game.instance.GetDisplayFactory().FindAndSetupPrototypeAsync(dummyDisplay, DisplayCategory.Default,
                new Action<UnityDisplayNode>(udn => material.shader = udn.GetRenderers().First().material.shader));
        }
        else
        {
            material.shader = shader;
        }
    }
    
    public static void SetOutlineColor(this Material material, Color color)
    {
        material.SetColor(OutlineColor, color);
        material.SetShaderKeywords(Array.Empty<string>());
    }
}