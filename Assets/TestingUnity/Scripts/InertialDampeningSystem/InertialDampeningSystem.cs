using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertialDampeningSystem : MonoBehaviour
{
    public delegate void CreateSourceAction(GameObject sourceObject, GameObject projectedObject);
    public delegate void RemoveSourceAction(GameObject sourceObject);
    public delegate void TransitionEnterAction(GameObject sourceObject, Transform interiorZone, Transform exteriorZone);
    public delegate void TransitionExitAction(GameObject sourceObject, Transform interiorZone, Transform exteriorZone);
    public static event CreateSourceAction OnCreateSource;
    public static event RemoveSourceAction OnRemoveSource;
    public static event TransitionEnterAction OnTransitionEnter;
    public static event TransitionExitAction OnTransitionExit;

    [SerializeField]
    private Collider triggerZone = null;
    [SerializeField]
    private float order = 1f;
    [SerializeField]
    private List<GameObject> interiors = null;

    private int positionIndex = 0;
    private IDSExternal _idsExternal = null;

    public IDSExternal idsExternal
    {
        get { return this._idsExternal; }
    }

    public void TriggerOnCreateSource(GameObject sourceObject, GameObject projectedObject)
    {
        OnCreateSource(sourceObject, projectedObject);
    }
    public void TriggerOnRemoveSource(GameObject sourceObject)
    {
        OnRemoveSource(sourceObject);
    }
    public void TriggerOnTransitionEnter(GameObject sourceObject, Transform interiorZone, Transform exteriorZone)
    {
        OnTransitionEnter(sourceObject, interiorZone, exteriorZone);
    }
    public void TriggerOnTransitionExit(GameObject sourceObject, Transform interiorZone, Transform exteriorZone)
    {
        OnTransitionExit(sourceObject, interiorZone, exteriorZone);
    }

    void Start()
    {
        // Collider[] thisComponents = this.GetComponents<Collider>();
        if (this.triggerZone == null)
        {
            Debug.LogError("Trigger zone not set. This will result in unwanted behaviour");
        }

        IDSLevelScript idsLevelScript = GameObject.FindObjectOfType<IDSLevelScript>();
        if (idsLevelScript == null)
        {
            GameObject idsLevelScriptObject = new GameObject();
            idsLevelScript = idsLevelScriptObject.AddComponent<IDSLevelScript>();
        }

        this.positionIndex = idsLevelScript.GetPositionIndex(this.gameObject);

        GameObject interiorShip = new GameObject(this.name + "_interior");
        interiorShip.transform.position = idsLevelScript.GetPositionFromIndex(this.positionIndex);

        this.CopyColliderComponents(this.gameObject, interiorShip);

        this.CreateClone(this.gameObject, interiorShip);

        GameObject exteriorZone = new GameObject(this.name + "_exteriorZone");
        GameObject interiorZone = new GameObject(interiorShip.name + "_interiorZone");

        RuntimeComponentCopier.AddComponent(exteriorZone, this.triggerZone);

        exteriorZone.transform.SetParent(this.transform, false);
        interiorZone.transform.SetParent(interiorShip.transform, false);
        // exteriorZone.transform.parent = this.transform;
        // interiorZone.transform.parent = interiorShip.transform;

        Destroy(this.triggerZone);

        this._idsExternal = exteriorZone.AddComponent<IDSExternal>();
        IDSInternal idsInternal = interiorShip.AddComponent<IDSInternal>();

        Rigidbody externalRigidbody = this.GetComponent<Rigidbody>();

        this._idsExternal.InitSetup(exteriorZone.transform, idsInternal, this.order);
        idsInternal.InitSetup(this._idsExternal, interiorZone.transform, idsLevelScript, this, externalRigidbody);

        // foreach (var comp in thisComponents)
        // {
        //     RuntimeComponentCopier.AddComponent(interiorShip, comp);
        //     // System.Type compType = comp.GetType();
        //     // Component newComp = interiorShip.AddComponent(compType);
        //     // RuntimeComponentCopier.GetCopyOf(newComp, comp);
        //     // Debug.Log(compType.IsSubclassOf(typeof(Collider)));
        // }
        // if (thisCollider != null)
        // {
        //     interiorShip.AddComponent(typeof(BoxCollider));
        // }
    }

    void CreateClone(GameObject org, GameObject clone)
    {
        foreach (Transform child in org.transform)
        {
            if (this.interiors.Contains(child.gameObject))
            {
                GameObject cloneChild = GameObject.Instantiate(child.gameObject, clone.transform);
                cloneChild.name = child.name + "_interior";
            }
            else if (child.gameObject.activeInHierarchy)
            {
                GameObject cloneChild = new GameObject();
                cloneChild.transform.parent = clone.transform;
                cloneChild.name = child.name + "_interior";
                this.CopyTransform(child.gameObject, cloneChild);
                this.CopyColliderComponents(child.gameObject, cloneChild);
                this.CreateClone(child.gameObject, cloneChild);
            }
        }
    }

    void CopyTransform(GameObject org, GameObject clone)
    {
        clone.transform.localPosition = org.transform.localPosition;
        clone.transform.localRotation = org.transform.localRotation;
        clone.transform.localScale = org.transform.localScale;
    }

    void CopyColliderComponents(GameObject org, GameObject clone)
    {
        foreach (var comp in org.GetComponents<Collider>())
        {
            RuntimeComponentCopier.AddComponent(clone, comp);
        }
    }
}
