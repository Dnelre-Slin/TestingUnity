using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InertialDampeningSystemDirect : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> externalObjects = new List<GameObject>();
    [SerializeField]
    private List<GameObject> internalObjects = new List<GameObject>();
    [SerializeField]
    private List<Collider> externalDoorColliders = new List<Collider>();

    void Start()
    {
        List<Collider> internalDoorColliders = new List<Collider>(this.externalDoorColliders.Count);
        this.SetParentOfObjects(this.externalObjects, null);
        this.SetParentOfObjects(this.internalObjects, null);
        GameObject internalObject = CopyColliderHelper.CreateCopyGameObjectWithColliders(this.gameObject, this.transform.parent, "_Internal", this.externalDoorColliders, internalDoorColliders);
        this.SetParentOfObjects(this.externalObjects, this.transform);
        this.SetParentOfObjects(this.internalObjects, internalObject.transform);

        IdsDoorUpdater idsDoorUpdater = new IdsDoorUpdater(internalDoorColliders.Count);
        for (int i = 0; i < internalDoorColliders.Count; i++)
        {
            idsDoorUpdater.AddDoorPair(this.externalDoorColliders[i].transform, internalDoorColliders[i].transform);
        }

        Rigidbody externalRigidbody = this.GetComponent<Rigidbody>();
        Collider[] externalColliders = this.GetComponentsInChildren<Collider>();

        IDSInternalDirect idsInternalDirect = internalObject.AddComponent<IDSInternalDirect>();
        idsInternalDirect.Setup(this.transform, externalRigidbody, externalColliders, idsDoorUpdater);
        Rigidbody internalRigidbody = internalObject.AddComponent<Rigidbody>();
        internalRigidbody.isKinematic = true;
    }

    void SetParentOfObjects(IEnumerable<GameObject> gameObjects, Transform parent)
    {
        foreach (var gameObject in gameObjects)
        {
            gameObject.transform.parent = parent;
        }
    }


}
