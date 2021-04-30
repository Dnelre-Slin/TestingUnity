using System;
using System.Collections;
using UnityEngine;

// public static class CopyHelper
// {
//     public static T AddComponent<T> ( this GameObject gameObject, T duplicate ) where T : Component
//     {
//         T target = gameObject.AddComponent<T> ();
//         foreach (PropertyInfo x in typeof ( T ).GetProperties ())
//             if (x.CanWrite)
//                 x.SetValue ( target, x.GetValue ( duplicate ) );
//         return target;
//     }
// }

public class TestShallowCopy : MonoBehaviour
{
    [SerializeField]
    private GameObject original = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject copy = new GameObject();
        copy.transform.position = original.transform.position + new Vector3(3f, 0, 0);

        RuntimeComponentCopier.CopyAllCosmeticComponents(original, copy);

        copy.name = original.name + "_copy";

        // MeshFilter mf = original.GetComponent<MeshFilter>();
        // MeshRenderer mr = original.GetComponent<MeshRenderer>();

        // MeshFilter mf2 = copy.AddComponent<MeshFilter>();
        // MeshRenderer mr2 = copy.AddComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    // void CopyComponents(GameObject source, GameObject target)
    // {
    //     MeshFilter comp = source.GetComponent<MeshFilter>();
    //     foreach(var component in source.GetComponents<Component>())
    //     {
    //         var componentType = component.GetType();
    //         if (componentType == typeof(MeshFilter))
    //         {
    //             // UnityEditorInternal.ComponentUtility.CopyComponent(component);
    //             // UnityEditorInternal.ComponentUtility.PasteComponentAsNew(target);
    //             RuntimeComponentCopier.AddComponent<MeshFilter>(target, component as MeshFilter);
    //         }
    //         else if (componentType == typeof(MeshRenderer))
    //         {
    //             RuntimeComponentCopier.AddComponent<MeshRenderer>(target, component as MeshRenderer);
    //         }
    //     }
    // }
    // void CopyComponents(GameObject source, GameObject target)
    // {
    //     foreach(var component in source.GetComponents<Component>())
    //     {
    //         var componentType = component.GetType();
    //         if (componentType != typeof(Transform))
    //         {
    //             // UnityEditorInternal.ComponentUtility.CopyComponent(component);
    //             // UnityEditorInternal.ComponentUtility.PasteComponentAsNew(target);
    //             RuntimeComponentCopier.AddComponent<MeshFilter>(target, component as MeshFilter);
    //         }
    //     }
    // }

}
