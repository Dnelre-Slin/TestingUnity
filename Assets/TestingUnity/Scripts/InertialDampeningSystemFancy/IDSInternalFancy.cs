using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class IDSInternalFancy : MonoBehaviour
{
    [SerializeField]
    private IDSExternalFancy idsExternalReal = null;
    [SerializeField]
    private Transform internalZone = null;
    [SerializeField]
    private IDSLevelScript idsLevelScript = null;

    private InertialDampeningSystemFancy inertialDampeningSystem = null;

    // private Transform projectionZone = null;
    public IDSExternalFancy idsExternal
    {
        get { return this.idsExternalReal; }
    }
    public Transform projectionZone
    {
        get { return this.thisProjection ?? this.externalZone; }
    }
    private Transform externalZoneReal = null;
    public Transform externalZone
    {
        get { return this.externalZoneReal; }
        set { this.externalZoneReal = value; }
    }

    private IDSInternalFancy parentIds = null;
    private Transform thisProjection = null;
    private Dictionary<GameObject, bool> unenteredGameobjects;

    private Rigidbody externalRigidbody = null;

    public void InitSetup(IDSExternalFancy idsExternal, Transform internalZone, IDSLevelScript idsLevelScript, InertialDampeningSystemFancy inertialDampeningSystem, Rigidbody externalRigidbody)
    {
        this.idsExternalReal = idsExternal;
        this.internalZone = internalZone;
        this.idsLevelScript = idsLevelScript;
        this.inertialDampeningSystem = inertialDampeningSystem;
        this.externalRigidbody = externalRigidbody;
    }

    // Start is called before the first frame update
    void Start()
    {
        // this.idsLevelScript = GameObject.FindObjectOfType<IDSLevelScript>();

        // this.idsExternal.SetupTriggerArea(this.GetComponent<BoxCollider>());
        // this.idsExternal.SetInternal(this);
        unenteredGameobjects = new Dictionary<GameObject, bool>();
        this.idsExternal.idsInternal = this;

        this.externalZoneReal = this.idsExternal.projectionZone;

        // this.projectionArea = this.idsExternal.GetProjectionZone().transform;
        // this.SetProjectionArea(null);
        // this.SetupAddInitObjects();
    }

    void FixedUpdate()
    {
        if (this.unenteredGameobjects.Count > 0)
        {
            List<GameObject> deleteKeys = new List<GameObject>(this.unenteredGameobjects.Count);
            // foreach (var item in this.unenteredGameobjects)
            foreach (var key in this.unenteredGameobjects.Keys.ToList())
            {
                if (!this.unenteredGameobjects[key]) // Skip first frame, so the physics engine has a chance to detect the collision
                {
                    this.unenteredGameobjects[key] = true;
                }
                else // If still not entered trigger zone on second frame, remove it.
                {
                    deleteKeys.Add(key);
                }
            }
            foreach (var key in deleteKeys)
            {
                Debug.Log("Force exit: " + key);
                this.SourceObjectExit(key);
                this.unenteredGameobjects.Remove(key);
            }
        }
    }

    // public void SetProjectionArea(Transform newProjectionArea)
    // {
    //     if (newProjectionArea == null)
    //     {
    //         this.projectionZone = this.externalZone;
    //     }
    //     else
    //     {
    //         this.projectionZone = newProjectionArea;
    //     }
    // }

    // void SetupAddInitObjects()
    // {
    //     for (int i = 0; i < this.transform.childCount; i++)
    //     {
    //         this.AddGameObject(this.transform.GetChild(i).gameObject);
    //     }
    // }

    public void AddNewSourceObject(GameObject sourceObject, IDSExternalFancy idsSourceObject)
    {
        // sourceObject.transform.parent = this.internalZone;
        // List<Transform> childProjections = null;
        if (idsSourceObject != null)
        {
            idsSourceObject.idsInternal.externalZone.parent = null; // Remove from tree temporarily, so that projections of projections will not be created.
            // childProjections = IDSProjectionCreator.ExtractChildTransforms(idsSourceObject.idsInternal.externalZone); // Remove from tree temporarily, so that projections of projections will not be created.
        }
        List<CameraInfo> cameraInfos = new List<CameraInfo>(2);
        IDSProjection idsProjection = IDSProjectionCreator.CreateProjections(sourceObject, cameraInfos, this.projectionZone);

        IDSProjectionCreator.SwitchCameraInfosToProjection(cameraInfos);

        // List<Camera> cameras = IDSHandleSourceObject.FindAndDisableCameras(sourceObject);
        // List<AudioListener> audioListeners = IDSHandleSourceObject.FindAndDisableAudioListeners(sourceObject);

        this.idsLevelScript.AddSourceObjectData(sourceObject, new SourceObjectData(this, idsProjection, cameraInfos));

        if (idsSourceObject != null)
        {
            Transform newProjectionZone = IDSProjectionCreator.MoveProjectionsEnter(idsSourceObject.idsInternal, idsProjection);
            idsSourceObject.idsInternal.parentIds = this;
            idsSourceObject.idsInternal.thisProjection = newProjectionZone;
            // IDSProjectionCreator.InsertChildTransforms(idsProjection.transform, childProjections);
        }
        if (this.inertialDampeningSystem != null)
        {
            this.inertialDampeningSystem.TriggerOnCreateSource(sourceObject, idsProjection.gameObject);
        }
    }

    public void RemoveSourceObject(GameObject sourceObject, SourceObjectData sourceObjectData, IDSExternalFancy idsSourceObject)
    {
        if (idsSourceObject != null)
        {
            IDSProjectionCreator.MoveProjectionsExit(idsSourceObject.idsInternal);
            idsSourceObject.idsInternal.parentIds = null;
            idsSourceObject.idsInternal.thisProjection = null;
        }

        IDSProjectionCreator.SwitchCameraInfosToReal(sourceObjectData.cameraInfos);
        GameObject.Destroy(sourceObjectData.idsProjection.gameObject);
        // IDSHandleSourceObject.ReenableCameras(sourceObjectData.cameras);
        // IDSHandleSourceObject.ReenableAudioListeners(sourceObjectData.audioListeners);
        this.idsLevelScript.RemoveSourceObjectData(sourceObject);

        // if (idsSourceObject != null)
        // {
        //     // IDSProjectionCreator.MoveProjectionsExit(idsSourceObject.idsInternal);

        //     idsSourceObject.idsInternal.parentIds = null;
        //     idsSourceObject.idsInternal.thisProjection = null;
        // }
        if (this.inertialDampeningSystem != null)
        {
            this.inertialDampeningSystem.TriggerOnRemoveSource(sourceObject);
        }
    }

    public void SourceObjectEnter(GameObject sourceObject, Rigidbody sourceRgbd, IDSExternalFancy idsSourceObject)
    {
        // IDSTransformations.TransformEnter(sourceObject.transform, sourceRgbd, this.internalZone, this.projectionZone);
        // sourceObject.transform.parent = this.internalZone;

        this.unenteredGameobjects[sourceObject] = false; // Used to check if object enters internal zone
        SourceObjectData? sourceObjectData = this.idsLevelScript.GetSourceObjectData(sourceObject);
        if (sourceObjectData.HasValue)
        {
            SourceObjectData sod = sourceObjectData.Value;
            if (sod.idsInternal == this)
            {
                // All is good. Handle enter skip double frame fixy fix
            }
            else // Transfer from one ids to another
            {
                IDSTransformations.TransformEnter(sourceObject.transform, sourceRgbd, this.externalRigidbody, this.internalZone, this.externalZone);
                sourceObject.transform.parent = this.internalZone;
                if (this.inertialDampeningSystem != null)
                {
                    this.inertialDampeningSystem.TriggerOnTransitionEnter(sourceObject, this.internalZone, this.externalZone);
                }
                // IDSExternal idsSourceObject = sourceObject.GetComponent<IDSExternal>();

                sod.idsProjection.transform.parent = this.projectionZone;

                sod.idsInternal = this;
                this.idsLevelScript.ModifySourceObjectData(sourceObject, sod);

                if (idsSourceObject != null)
                {
                    idsSourceObject.idsInternal.parentIds = this;
                }
            }
        }
        else // Create new
        {
            IDSTransformations.TransformEnter(sourceObject.transform, sourceRgbd, this.externalRigidbody, this.internalZone, this.externalZone);
            sourceObject.transform.parent = this.internalZone;
            if (this.inertialDampeningSystem != null)
            {
                this.inertialDampeningSystem.TriggerOnTransitionEnter(sourceObject, this.internalZone, this.externalZone);
            }
            // IDSExternal idsSourceObject = sourceObject.GetComponent<IDSExternal>();
            this.AddNewSourceObject(sourceObject, idsSourceObject);
        }

    }
    public void SourceObjectEnter(GameObject sourceObject, Rigidbody rgbd)
    {
        IDSExternalFancy idsSourceObject = sourceObject.GetComponent<IDSExternalFancy>();
        SourceObjectEnter(sourceObject, rgbd, idsSourceObject);
    }
    public void SourceObjectEnter(GameObject sourceObject)
    {
        Rigidbody rgbd = sourceObject.GetComponent<Rigidbody>();
        IDSExternalFancy idsSourceObject = sourceObject.GetComponent<IDSExternalFancy>();
        SourceObjectEnter(sourceObject, rgbd, idsSourceObject);
    }

    public void SourceObjectExit(GameObject sourceObject, Rigidbody sourceRgbd, IDSExternalFancy idsSourceObject)
    {
        // IDSTransformations.TransformExit(sourceObject.transform, sourceRgbd, this.internalZone, this.externalZone);
        // sourceObject.transform.parent = null;

        SourceObjectData? sourceObjectData = this.idsLevelScript.GetSourceObjectData(sourceObject);
        if (sourceObjectData.HasValue)
        {
            SourceObjectData sod = sourceObjectData.Value;
            if (sod.idsInternal == this)
            {
                IDSTransformations.TransformExit(sourceObject.transform, sourceRgbd, this.externalRigidbody, this.internalZone, this.externalZone);
                if (this.inertialDampeningSystem != null)
                {
                    this.inertialDampeningSystem.TriggerOnTransitionExit(sourceObject, this.internalZone, this.externalZone);
                }
                // IDSExternal idsSourceObject = sourceObject.GetComponent<IDSExternal>();

                if (this.parentIds == null) // Remove from ids-system
                {
                    sourceObject.transform.parent = null;
                    this.RemoveSourceObject(sourceObject, sod, idsSourceObject);
                }
                else // Transfer from one ids to another
                {
                    sourceObject.transform.parent = this.parentIds.internalZone;
                    sod.idsProjection.transform.parent = this.parentIds.projectionZone;

                    sod.idsInternal = this.parentIds;
                    this.idsLevelScript.ModifySourceObjectData(sourceObject, sod);

                    if (idsSourceObject != null)
                    {
                        idsSourceObject.idsInternal.parentIds = this.parentIds;
                    }
                }
            }
            else
            {
                // Probably entered child ids-system
            }
        }
        else
        {
            // Should never happen
        }
    }
    public void SourceObjectExit(GameObject sourceObject, Rigidbody rgbd)
    {
        IDSExternalFancy idsSourceObject = sourceObject.GetComponent<IDSExternalFancy>();
        SourceObjectExit(sourceObject, rgbd, idsSourceObject);
    }
    public void SourceObjectExit(GameObject sourceObject)
    {
        Rigidbody rgbd = sourceObject.GetComponent<Rigidbody>();
        IDSExternalFancy idsSourceObject = sourceObject.GetComponent<IDSExternalFancy>();
        SourceObjectExit(sourceObject, rgbd, idsSourceObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        // Debug.Log("Exiting (" + this + ") : " + collider.gameObject.name);

        Rigidbody rgbd = collider.gameObject.GetComponent<Rigidbody>();

        if (rgbd != null) // Only gameobject with a rigidbody will be handled
        {
            Debug.Log("Entering (" + this + ") : " + collider.gameObject.name);
            if (this.unenteredGameobjects.ContainsKey(collider.gameObject))
            {
                this.unenteredGameobjects.Remove(collider.gameObject);
            }
            // IDSExternal idsSourceObject = collider.gameObject.GetComponent<IDSExternal>();
            // if (idsSourceObject == null)
            // {
            //     this.SourceObjectExit(collider.gameObject, rgbd);
            // }
            // else if (idsSourceObject.order < this.idsExternal.order)
            // {
            //     this.SourceObjectExit(collider.gameObject, rgbd, idsSourceObject);
            // }
        }
    }
    void OnTriggerExit(Collider collider)
    {
        // Debug.Log("Exiting (" + this + ") : " + collider.gameObject.name);

        Rigidbody rgbd = collider.gameObject.GetComponent<Rigidbody>();

        if (rgbd != null) // Only gameobject with a rigidbody will be handled
        {
            Debug.Log("Exiting (" + this + ") : " + collider.gameObject.name);
            IDSExternalFancy idsSourceObject = collider.gameObject.GetComponent<IDSExternalFancy>();
            if (idsSourceObject == null)
            {
                this.SourceObjectExit(collider.gameObject, rgbd);
            }
            else if (idsSourceObject.order < this.idsExternal.order)
            {
                this.SourceObjectExit(collider.gameObject, rgbd, idsSourceObject);
            }
        }
    }
}
