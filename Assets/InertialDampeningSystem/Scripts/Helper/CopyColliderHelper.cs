using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CopyColliderHelper
{
    public static BoxCollider CopyBoxCollider(BoxCollider originalCollider, GameObject copyGameObject)
    {
        BoxCollider copyCollider = copyGameObject.AddComponent<BoxCollider>();

        // Copy box properties
        copyCollider.isTrigger = originalCollider.isTrigger;
        copyCollider.sharedMaterial = originalCollider.sharedMaterial;
        copyCollider.center = originalCollider.center;
        copyCollider.size = originalCollider.size;

        return copyCollider;
    }
    public static SphereCollider CopySphereCollider(SphereCollider originalCollider, GameObject copyGameObject)
    {
        SphereCollider copyCollider = copyGameObject.AddComponent<SphereCollider>();

        // Copy sphere properties
        copyCollider.isTrigger = originalCollider.isTrigger;
        copyCollider.sharedMaterial = originalCollider.sharedMaterial;
        copyCollider.center = originalCollider.center;
        copyCollider.radius = originalCollider.radius;

        return copyCollider;
    }
    public static CapsuleCollider CopyCapsuleCollider(CapsuleCollider originalCollider, GameObject copyGameObject)
    {
        CapsuleCollider copyCollider = copyGameObject.AddComponent<CapsuleCollider>();

        // Copy capsule properties
        copyCollider.isTrigger = originalCollider.isTrigger;
        copyCollider.sharedMaterial = originalCollider.sharedMaterial;
        copyCollider.center = originalCollider.center;
        copyCollider.radius = originalCollider.radius;
        copyCollider.height = originalCollider.height;
        copyCollider.direction = copyCollider.direction;

        return copyCollider;
    }
    public static MeshCollider CopyMeshCollider(MeshCollider originalCollider, GameObject copyGameObject)
    {
        MeshCollider copyCollider = copyGameObject.AddComponent<MeshCollider>();

        // Copy capsule properties
        copyCollider.convex = originalCollider.convex;
        copyCollider.isTrigger = originalCollider.isTrigger;
        copyCollider.cookingOptions = originalCollider.cookingOptions;
        copyCollider.sharedMaterial = originalCollider.sharedMaterial;
        copyCollider.sharedMesh = originalCollider.sharedMesh;

        return copyCollider;
    }
    public static TerrainCollider CopyTerrainCollider(TerrainCollider originalCollider, GameObject copyGameObject)
    {
        TerrainCollider copyCollider = copyGameObject.AddComponent<TerrainCollider>();

        // Copy capsule properties
        copyCollider.isTrigger = originalCollider.isTrigger;
        copyCollider.sharedMaterial = originalCollider.sharedMaterial;
        copyCollider.terrainData = originalCollider.terrainData;

        return copyCollider;
    }

    public static T CopyCollider<T>(T originalCollider, GameObject copyGameObject) where T : Collider
    {
        if (originalCollider is BoxCollider)
        {
            return CopyColliderHelper.CopyBoxCollider(originalCollider as BoxCollider, copyGameObject) as T;
        }
        else if (originalCollider is SphereCollider)
        {
            return CopyColliderHelper.CopySphereCollider(originalCollider as SphereCollider, copyGameObject) as T;
        }
        else if (originalCollider is CapsuleCollider)
        {
            return CopyColliderHelper.CopyCapsuleCollider(originalCollider as CapsuleCollider, copyGameObject) as T;
        }
        else if (originalCollider is MeshCollider)
        {
            return CopyColliderHelper.CopyMeshCollider(originalCollider as MeshCollider, copyGameObject) as T;
        }
        else if (originalCollider is MeshCollider)
        {
            return CopyColliderHelper.CopyMeshCollider(originalCollider as MeshCollider, copyGameObject) as T;
        }
        throw new UnityException("Type : " + typeof(T) + " is not currently supported");
    }

    public static GameObject CopyCollidersToFromGameObject(GameObject originalGameObject, GameObject copyGameObject,
                                                            List<Collider> collidersOfInterest = null, List<Collider> copiesOfInterestedColliders = null, bool skipTrigger = true)
    {
        Collider[] originalColliders = originalGameObject.GetComponents<Collider>();
        foreach (var orgCollider in originalColliders)
        {
            if (!skipTrigger || !orgCollider.isTrigger)
            {
                Collider copyCollider = CopyColliderHelper.CopyCollider(orgCollider, copyGameObject);
                if (collidersOfInterest != null && copiesOfInterestedColliders != null && collidersOfInterest.Contains(orgCollider))
                {
                    copiesOfInterestedColliders.Add(copyCollider);
                }
            }
        }
        return copyGameObject;
    }

    public static Transform CopyTransformProperties(Transform originalTransform, Transform copyTransform)
    {
        copyTransform.localPosition = originalTransform.localPosition;
        copyTransform.localRotation = originalTransform.localRotation;
        copyTransform.localScale = originalTransform.localScale;
        return copyTransform;
    }

    public static GameObject CreateCopyGameObjectWithColliders(GameObject originalGameObject, Transform copyParent = null, string copyExtensionName = "_Copy",
                                                                List<Collider> collidersOfInterest = null, List<Collider> copiesOfInterestedColliders = null, bool skipTrigger = true)
    {
        GameObject copyGameObject = new GameObject(originalGameObject.name + copyExtensionName);
        copyGameObject.SetActive(originalGameObject.activeSelf);
        copyGameObject.transform.parent = copyParent;
        CopyColliderHelper.CopyTransformProperties(originalGameObject.transform, copyGameObject.transform);
        CopyColliderHelper.CopyCollidersToFromGameObject(originalGameObject, copyGameObject, collidersOfInterest, copiesOfInterestedColliders, skipTrigger);
        foreach (Transform childObject in originalGameObject.transform)
        {
            CopyColliderHelper.CreateCopyGameObjectWithColliders(childObject.gameObject, copyGameObject.transform, copyExtensionName, collidersOfInterest, copiesOfInterestedColliders, skipTrigger);
        }
        return copyGameObject;
    }
}
