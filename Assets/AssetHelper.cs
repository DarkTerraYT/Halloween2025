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

    public static Shader GetShader(string name)
    {
        return Get<Shader>(name);
    }

    public static Material GetMaterial(string name)
    {
        return Get<Material>(name);
    }

    public static void PrepareAssetBundle()
    {
        if (!bundle) bundle = ResourceHandler.Bundles["Halloween2025-halloween2025"];
    }

    public static T Get<T>(string name) where T : Object
    {
        PrepareAssetBundle();
        return bundle.LoadAssetAsync<T>(name).asset.Cast<T>();
    }

    public static Object Get(string name)
    {
        PrepareAssetBundle();
        return bundle.LoadAssetAsync(name).asset;
    }
}