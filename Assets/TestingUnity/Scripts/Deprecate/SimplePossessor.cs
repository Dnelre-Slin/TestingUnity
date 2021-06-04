using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePossessor : MonoBehaviour
{
    // private Camera pawnCamera;

    // [SerializeField]
    // private SimplePossessor otherPossessor;

    // // [SerializeField]
    // // private InputActionAsset actionAsset;

    // [SerializeField]
    // private string actionMapName;

    // private InputActionMap pawnMap;
    // private InputAction interactAction;

    // private ControllableManager controllableManager;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     this.controllableManager = GameObject.FindObjectOfType<ControllableManager>();

    //     this.pawnCamera = GetComponentInChildren<Camera>();

    //     // this.pawnMap = actionAsset.FindActionMap(actionMapName);

    //     this.interactAction = pawnMap.FindAction("Interact");

    //     this.interactAction.performed += ctx => OnInteract(ctx);
    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    // public void OnInteract(InputAction.CallbackContext context)
    // {
    //     if (this.enabled)
    //     {
    //         Debug.Log("Hello!");
    //         this.controllableManager.PossessGameobject(1, "BoxShip");

    //         // Test
    //         Camera cam = GameObject.Find("BoxShip").GetComponentInChildren<Camera>();
    //         Debug.Log("Test: " + cam.enabled);
    //         //
    //     }
    //     // Debug.Log(context);
    //     // if (this.otherPossessor != null)
    //     // {
    //     //     this.pawnMap.Disable();
    //     //     if (this.pawnCamera != null)
    //     //     {
    //     //         this.pawnCamera.enabled = false;
    //     //     }
    //     //     this.otherPossessor.OnPossess();
    //     // }
    // }

    // public void OnPossess()
    // {
    //     Debug.Log("Nugget!");
    //     // this.pawnMap.Enable();
    //     // if (this.pawnCamera != null)
    //     // {
    //     //     this.pawnCamera.enabled = true;
    //     // }
    // }

}
