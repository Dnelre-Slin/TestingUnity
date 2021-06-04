using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TestRenderInput : MonoBehaviour
{
    [SerializeField]
    private TestRenderCloner testRenderCloner = null;

    private Dictionary<GameObject, Camera[]> cameras;
    // Start is called before the first frame update
    void Start()
    {
        cameras = new Dictionary<GameObject, Camera[]>();

        this.testRenderCloner.SetupTriggerArea(this.GetComponent<BoxCollider>());
        this.testRenderCloner.SetInputSection(this);
        this.SetupAddInitObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetupAddInitObjects()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.AddGameObject(this.transform.GetChild(i).gameObject);
        }
    }

    public void AddGameObject(GameObject newGameObject)
    {
        newGameObject.transform.parent = this.transform;
        this.testRenderCloner.AddGameObject(newGameObject);
        this.FindAndDisableCameras(newGameObject);
    }

    void FindAndDisableCameras(GameObject newGameObject)
    {
        Camera[] cams = gameObject.GetComponentsInChildren<Camera>(false);
        foreach (Camera cam in cams)
        {
            cam.gameObject.SetActive(false);
        }

        this.cameras[newGameObject] = cams;
    }

    void EnableCameras(GameObject gameObject)
    {
        foreach (Camera cam in this.cameras[gameObject])
        {
            cam.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Exiting (" + this + ") : " + collider.gameObject.name);

        Rigidbody rgbd = collider.gameObject.GetComponent<Rigidbody>();

        if (rgbd != null) // Only gameobject with a rigidbody will be handled
        {
            GameObject cloneGameObject = this.testRenderCloner.GetClone(collider.gameObject);

            // Position
            collider.gameObject.transform.position = cloneGameObject.transform.position;

            // Rotation
            collider.gameObject.transform.rotation = cloneGameObject.transform.rotation;

            // Velocity
            // rgbd.velocity = Quaternion.Inverse(this.transform.rotation) * rgbd.velocity; // Needs to be uncommented if the 'source' is ever to be rotated.
            rgbd.velocity = this.testRenderCloner.transform.rotation * rgbd.velocity;

            //// AdvancedCharacterController. (Try to think of a better solution to this one)
            // AdvancedCharacterController acc = collider.gameObject.GetComponent<AdvancedCharacterController>();
            // if (acc != null)
            // {
            //     acc.RotateGravityVelocity(this.testRenderCloner.transform.rotation);
            // }

            this.testRenderCloner.RemoveGameObject(cloneGameObject);
            this.EnableCameras(collider.gameObject);
            collider.gameObject.transform.parent = null;
        }
    }
}
