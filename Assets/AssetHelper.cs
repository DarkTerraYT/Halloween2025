using BTD_Mod_Helper.Api.Internal;
using UnityEngine;

namespace Halloween2025.Assets;

internal static class AssetHelper
{
    public static AssetBundle bundle;

    public static GameObject GetObject(string name)
    {   
        return Get<GameObject>(name);
    }

    public static void PrepareAssetBundle()
    {
        if (!bundle)
        {
            bundle = ResourceHandler.Bundles["Halloween2025-halloween2025"];
        }
    }
    
    public static T Get<T>(string name) where T : Il2CppSystem.Object 
    {
        PrepareAssetBundle();
        return bundle.LoadAssetAsync<T>(name).asset.Cast<T>();
    }
}