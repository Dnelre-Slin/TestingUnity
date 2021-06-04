using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CopyColliderHelper
{
    // public static BoxCollider CopyBoxCollider(BoxCollider originalCollider, GameObject copyGameObject)
    // {
    //     return copyGameObject.AddComponent<BoxCollider>(originalCollider);
    // }

    public static T CopyCollider<T>(T originalCollider, GameObject copyGameObject) where T : Collider
    {
        return copyGameObject.AddComponent<T>(originalCollider);
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
