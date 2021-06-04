using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRenderCloner : MonoBehaviour
{
    // [SerializeField]
    // private GameObject inputObject;
    private TestRenderInput testRenderInput = null;

    // void OnValidate()
    // {
    //     this.RemoveAllChildren();
    //     this.SetupOutputContent();
    // }

    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < this.inputObject.transform.childCount; i++)
        // {
        //     GameObject otherGameObject = this.inputObject.transform.GetChild(i).gameObject;
        //     GameObject newGameObject = new GameObject();

        //     newGameObject.name = otherGameObject.name;

        //     MeshRenderer[] meshRenderers = otherGameObject.GetComponents<MeshRenderer>();
        //     foreach (var meshRenderer in meshRenderers)
        //     {
        //         MeshRenderer meshRend = newGameObject.AddComponent<MeshRenderer>();
        //         meshRend.materials = meshRenderer.materials;
        //     }
        //     MeshFilter[] meshFilters = otherGameObject.GetComponents<MeshFilter>();
        //     foreach (var meshFilter in meshFilters)
        //     {
        //         MeshFilter meshFilt = newGameObject.AddComponent<MeshFilter>();
        //         meshFilt.mesh = meshFilter.mesh;
        //     }

        //     newGameObject.transform.parent = this.transform;
        // }
        // this.RemoveAllChildren();
        // this.SetupOutputContent();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Input pos " + this.testRenderInput.transform.position);
        // Debug.Log("Output pos " + this.transform.position);
    }

    public void SetInputSection(TestRenderInput input)
    {
        this.testRenderInput = input;
    }

    public void SetupTriggerArea(BoxCollider trigger)
    {
        BoxCollider triggerClone = this.gameObject.AddComponent<BoxCollider>();
        triggerClone.isTrigger = trigger.isTrigger;
        triggerClone.center = trigger.center;
        triggerClone.size = trigger.size; // - new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void AddGameObject(GameObject otherGameObject)
    {
        // GameObject clone = GameObject.Instantiate(otherGameObject);

        // this.DestroyUnwantedComponents(clone);
        // this.CreateCloneUpdater(otherGameObject, clone);

        // this.DestroyUnwantedAndCreateUpdaterForChildren(otherGameObject, clone);

        // clone.transform.parent = this.transform;
        // clone.name = otherGameObject.name;
        GameObject clone = this.SetupOutputContent(otherGameObject, this.transform);
        // this.SetupChildren(otherGameObject, clone);
    }

    public GameObject GetClone(GameObject otherGameObject)
    {
        foreach(Transform child in this.transform)
        {
            if (child.gameObject.name == otherGameObject.name)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    public void RemoveGameObject(GameObject cloneGameObject)
    {
        GameObject.Destroy(cloneGameObject);
    }

    // void DestroyUnwantedComponents(GameObject newClone)
    // {
    //     foreach (var comp in newClone.GetComponents<MonoBehaviour>())
    //     {
    //         GameObject.Destroy(comp);
    //     }
    //     foreach (var comp in newClone.GetComponents<Collider>())
    //     {
    //         GameObject.Destroy(comp);
    //     }
    //     foreach (var comp in newClone.GetComponents<Rigidbody>())
    //     {
    //         GameObject.Destroy(comp);
    //     }
    // }

    // void DestroyUnwantedAndCreateUpdaterForChildren(GameObject otherGameObject, GameObject newClone)
    // {
    //     for (int i = 0; i < newClone.transform.childCount; i++)
    //     {
    //         Debug.Log("Clone : " + newClone.transform.GetChild(i).gameObject.name);
    //         Debug.Log("org : " + otherGameObject.transform.GetChild(i).gameObject.name);
    //         this.DestroyUnwantedComponents(newClone.transform.GetChild(i).gameObject);
    //         this.CreateCloneUpdater(otherGameObject.transform.GetChild(i).gameObject, newClone.transform.GetChild(i).gameObject);
    //     }

    //     // foreach (Transform child in newClone.transform)
    //     // {
    //     //     this.DestroyUnwantedComponents(child.gameObject);
    //     //     // this.CreateCloneUpdater(otherGameObject, child.gameObject);
    //     // }
    // }

    // public void UpdateRendering()
    // {
    //     this.RemoveAllChildrenEdit();
    //     this.SetupOutputContent();
    // }

    // void RemoveAllChildren()
    // {
    //     foreach (Transform child in this.transform)
    //     {
    //         GameObject.Destroy(child.gameObject);
    //     }
    // }

    // void RemoveAllChildrenEdit()
    // {
    //     foreach (Transform child in this.transform)
    //     {
    //         GameObject.DestroyImmediate(child.gameObject);
    //     }
    // }

    // void SetupOutputContent(GameObject otherGameObject)
    // {
    //     for (int i = 0; i < otherGameObject.transform.childCount; i++)
    //     {
    //         GameObject otherGameObject = otherGameObject.transform.GetChild(i).gameObject;
    //         GameObject newGameObject = new GameObject();

    //         newGameObject.transform.parent = this.transform;

    //         newGameObject.name = otherGameObject.name;

    //         newGameObject.transform.localPosition = otherGameObject.transform.localPosition;
    //         newGameObject.transform.localRotation = otherGameObject.transform.localRotation;
    //         newGameObject.transform.localScale = otherGameObject.transform.localScale;

    //         this.CreateMeshes(otherGameObject, newGameObject);
    //         this.CreateCloneUpdater(otherGameObject, newGameObject);
    //     }
    // }
    GameObject SetupOutputContent(GameObject otherGameObject, Transform parent)
    {
        GameObject newGameObject = new GameObject();

        newGameObject.transform.localPosition = otherGameObject.transform.localPosition;
        newGameObject.transform.localRotation = otherGameObject.transform.localRotation;
        newGameObject.transform.localScale = otherGameObject.transform.localScale;

        this.CreateMeshes(otherGameObject, newGameObject);
        this.CreateCameras(otherGameObject, newGameObject);
        this.CreateCloneUpdater(otherGameObject, newGameObject);

        newGameObject.name = otherGameObject.name;
        newGameObject.transform.parent = parent;

        SetupChildren(otherGameObject, newGameObject);

        return newGameObject;
    }

    void SetupChildren(GameObject otherGameObject, GameObject clone)
    {
        foreach (Transform child in otherGameObject.transform)
        {
            SetupOutputContent(child.gameObject, clone.transform);

        }
    }

    void CreateMeshes(GameObject otherObject, GameObject newObject)
    {
        MeshRenderer[] meshRenderers = otherObject.GetComponents<MeshRenderer>();
        foreach (var meshRenderer in meshRenderers)
        {
            MeshRenderer meshRend = newObject.AddComponent<MeshRenderer>();
            meshRend.materials = meshRenderer.materials;
        }
        MeshFilter[] meshFilters = otherObject.GetComponents<MeshFilter>();
        foreach (var meshFilter in meshFilters)
        {
            MeshFilter meshFilt = newObject.AddComponent<MeshFilter>();
            meshFilt.mesh = meshFilter.mesh;
        }
    }

    void CreateCameras(GameObject otherObject, GameObject newObject)
    {
        Camera[] cameras = otherObject.GetComponents<Camera>();
        foreach (var camera in cameras)
        {
            Camera cam = newObject.AddComponent<Camera>();
        }
        AudioListener[] audioListeners = otherObject.GetComponents<AudioListener>();
        foreach (var audioListener in audioListeners)
        {
            AudioListener cam = newObject.AddComponent<AudioListener>();
        }
    }

    void CreateCloneUpdater(GameObject otherObject, GameObject newObject)
    {
        TestRenderCloneUpdater cloneUpdater = newObject.AddComponent<TestRenderCloneUpdater>();
        cloneUpdater.realTransform = otherObject.transform;
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Entering (" + this + ") : " + collider.gameObject.name);

        Rigidbody rgbd = collider.gameObject.GetComponent<Rigidbody>();

        if (rgbd != null) // Only gameobject with a rigidbody will be handled
        {
            Debug.Log("Input pos " + this.testRenderInput.transform.position);
            Debug.Log("Output pos " + this.transform.position);
            Debug.Log("Player pos " + collider.gameObject.transform.position);
            // Position
            Vector3 deltaPosition = this.testRenderInput.transform.position - this.transform.position;
            Vector3 localPosition = collider.gameObject.transform.position - this.transform.position;
            localPosition = Quaternion.Inverse(this.transform.rotation) * localPosition;
            // localPosition = this.testRenderInput.transform.rotation * localPosition; // Needs to be uncommented if the 'source' is ever to be rotated.
            collider.gameObject.transform.position = localPosition + this.testRenderInput.transform.position;

            Debug.Log("After pos " + collider.gameObject.transform.position);

            // Rotation
            collider.gameObject.transform.rotation = Quaternion.Inverse(this.transform.rotation) * collider.gameObject.transform.rotation;
            // collider.gameObject.transform.rotation = this.testRenderInput.transform.rotation * collider.gameObject.transform.rotation; // Needs to be uncommented if the 'source' is ever to be rotated.

            // Velocity
            rgbd.velocity = Quaternion.Inverse(this.transform.rotation) * rgbd.velocity;
            // rgbd.velocity = this.testRenderInput.transform.rotation * rgbd.velocity; // Needs to be uncommented if the 'source' is ever to be rotated.

            //// AdvancedCharacterController. (Try to think of a better solution to this one)
            // AdvancedCharacterController acc = collider.gameObject.GetComponent<AdvancedCharacterController>();
            // if (acc != null)
            // {
            //     acc.RotateGravityVelocity(Quaternion.Inverse(this.transform.rotation));
            // }

            this.testRenderInput.AddGameObject(collider.gameObject);
        }

    }
}
