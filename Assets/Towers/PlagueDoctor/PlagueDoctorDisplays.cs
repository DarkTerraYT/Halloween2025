using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2Cpp;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using MelonLoader;
using UnityEngine;

namespace Halloween2025.Assets.Towers.PlagueDoctor;

[RegisterTypeInIl2Cpp(false)]
public class FreezeRotation : MonoBehaviour
{
    public Vector3 rotation = new(-90, 0, 90);

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(rotation);
    }
}

public class PlagueDoctorLevel1 : ModTowerCustomDisplay<global::Halloween2025.Towers.PlagueDoctor.PlagueDoctor>
{
    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => "PlagueDoctor";

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[0] < 7;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.GetMeshRenderers())
        {
            renderer.ApplyOutlineShader();
            switch (renderer.name)
            {
                case "TopHat":
                    break;
                case "Mask":
                    renderer.SetOutlineColor(new Color(0.85f, 0.85f, 0.85f));
                    break;
                default:
                    renderer.SetOutlineColor(new Color32(120, 59, 26, 255));
                    break;
            }
        }
    }
}

public class PlagueDoctorLevel7 : ModTowerCustomDisplay<global::Halloween2025.Towers.PlagueDoctor.PlagueDoctor>
{
    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => "PlagueDoctor2";

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[0] >= 7 && tiers[0] < 11;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.GetMeshRenderers())
        {
            renderer.ApplyOutlineShader();
            switch (renderer.name)
            {
                case "TopHat":
                    break;
                case "Robe":
                    foreach (var material in renderer.materials) material.ApplyOutlineShader();
                    break;
                case "BeltBuckle":
                    renderer.SetOutlineColor(new Color32(39, 25, 12, 255));
                    break;
                case "Mask":
                    renderer.SetOutlineColor(new Color(0.85f, 0.85f, 0.85f));
                    break;
                default:
                    renderer.SetOutlineColor(new Color32(120, 59, 26, 255));
                    break;
            }
        }
    }
}

public class PlagueDoctorLevel11 : ModTowerCustomDisplay<global::Halloween2025.Towers.PlagueDoctor.PlagueDoctor>
{
    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => "PlagueDoctor3";

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[0] >= 11 && tiers[0] < 20;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.GetMeshRenderers())
        {
            renderer.ApplyOutlineShader();
            switch (renderer.name)
            {
                case "BeltBuckle":
                    renderer.SetOutlineColor(new Color32(39, 25, 12, 255));
                    break;
                case "Buttons":
                    renderer.SetOutlineColor(new Color32(39, 25, 12, 255));
                    break;
                case "Mask":
                    renderer.SetOutlineColor(new Color(0.85f, 0.85f, 0.85f));
                    break;
                case "Body":
                    renderer.SetOutlineColor(new Color(0.85f, 0.85f, 0.85f));
                    break;
                case "Robe":
                    foreach (var material in renderer.materials) material.ApplyOutlineShader();
                    break;
            }
        }
    }
}

public class PlagueDoctorLevel20 : ModTowerCustomDisplay<global::Halloween2025.Towers.PlagueDoctor.PlagueDoctor>
{
    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => "PlagueDoctor4";

    public override bool UseForTower(params int[] tiers)
    {
        return tiers[0] >= 20;
    }

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        foreach (var renderer in node.GetMeshRenderers())
        {
            renderer.ApplyOutlineShader();
            switch (renderer.name)
            {
                case "BeltBuckle":
                    renderer.SetOutlineColor(new Color32(39, 25, 12, 255));
                    break;
                case "Buttons":
                    renderer.SetOutlineColor(new Color32(39, 25, 12, 255));
                    break;
                case "Mask":
                    renderer.SetOutlineColor(new Color(0.85f, 0.85f, 0.85f));
                    break;
                case "Body":
                    renderer.SetOutlineColor(new Color(0.85f, 0.85f, 0.85f));
                    break;
                case "Lantern":
                    renderer.gameObject.AddComponent<FreezeRotation>();
                    break;
                case "Robe":
                    foreach (var material in renderer.materials) material.ApplyOutlineShader();
                    break;
            }
        }
    }
}

public class Needle : ModDisplay
{
    public override string BaseDisplay => Generic2dDisplay;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        Set2DTexture(node, Name);
    }
}

public class Poison : ModBloonOverlay
{
    public override string BaseOverlay =>
        Game.instance.model.GetTower("MortarMonkey", 0, 0, 2).GetDescendant<AddBehaviorToBloonModel>().overlayType;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        node.gameObject.DestroyAllChildren();
        if (node.gameObject.HasComponent<SpriteRenderer>()) node.gameObject.RemoveComponent<SpriteRenderer>();
        switch (OverlayClass)
        {
            case BloonOverlayClass.Moab:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonMoab");
                break;
            case BloonOverlayClass.Bfb:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonBfb");
                break;
            case BloonOverlayClass.Zomg:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonZomg");
                break;
            case BloonOverlayClass.Ddt:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonDdt");
                break;
            case BloonOverlayClass.Bad:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonBad");
                break;
            default:
                var particles = Object.Instantiate(AssetHelper.GetObject("PoisonBloon"), node.transform);
                var renderer = particles.GetComponent<ParticleSystemRenderer>();
                renderer.sortingLayerID = 2;
                break;
        }

        node.genericRendererLayers = new Il2CppStructArray<int>(1);
        node.genericRenderers = new Il2CppReferenceArray<Renderer>([node.GetComponentInChildren<Renderer>()]);

        node.RecalculateGenericRenderers();
    }
}

public class PlagueBloon : ModCustomDisplay
{
    public override string AssetBundleName => "halloween2025";
    public override string PrefabName => Name;

    public override Il2CppAssets.Scripts.Simulation.SMath.Vector3 PositionOffset => new(0, 0, 10);

    public override DisplayCategory DisplayCategory => DisplayCategory.Bloon;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        node.GetRenderer<SpriteRenderer>().flipY = true;
    }
}

public class MegaPoison : ModBloonOverlay
{
    public override string BaseOverlay =>
        Game.instance.model.GetTower("MortarMonkey", 0, 0, 2).GetDescendant<AddBehaviorToBloonModel>().overlayType;

    public override void ModifyDisplayNode(UnityDisplayNode node)
    {
        node.gameObject.DestroyAllChildren();
        if (node.gameObject.HasComponent<SpriteRenderer>()) node.gameObject.RemoveComponent<SpriteRenderer>();
        switch (OverlayClass)
        {
            case BloonOverlayClass.Moab:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonMoab2");
                break;
            case BloonOverlayClass.Bfb:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonBfb2");
                break;
            case BloonOverlayClass.Zomg:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonZomg2");
                break;
            case BloonOverlayClass.Ddt:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonDdt2");
                break;
            case BloonOverlayClass.Bad:
                node.GetMeshRenderer().material = AssetHelper.GetMaterial("PoisonBad2");
                break;
            default:
                var particles = Object.Instantiate(AssetHelper.GetObject("PoisonBloon2"), node.transform);
                var renderer = particles.GetComponent<ParticleSystemRenderer>();
                renderer.sortingLayerID = 2;
                break;
        }

        node.genericRendererLayers = new Il2CppStructArray<int>(1);
        node.genericRenderers = new Il2CppReferenceArray<Renderer>([node.GetComponentInChildren<Renderer>()]);

        node.RecalculateGenericRenderers();
    }
}