using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerIDSTransitions : MonoBehaviour
{
    [SerializeField]
    private GameObject playerGameObject = null;
    private AdvancedCharacterController advancedCharacterController = null;
    private Controllable controllable = null;
    private Camera playerCamera = null;

    void OnEnable()
    {
        InertialDampeningSystemFancy.OnCreateSource += this.CreatePlayerSource;
        InertialDampeningSystemFancy.OnRemoveSource += this.RemovePlayerSource;
        InertialDampeningSystemFancy.OnTransitionEnter += this.PlayerTransitionEnter;
        InertialDampeningSystemFancy.OnTransitionExit += this.PlayerTransitionExit;
    }

    void OnDisable()
    {
        InertialDampeningSystemFancy.OnCreateSource -= this.CreatePlayerSource;
        InertialDampeningSystemFancy.OnRemoveSource -= this.RemovePlayerSource;
        InertialDampeningSystemFancy.OnTransitionEnter -= this.PlayerTransitionEnter;
        InertialDampeningSystemFancy.OnTransitionExit -= this.PlayerTransitionExit;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.advancedCharacterController = this.playerGameObject.GetComponent<AdvancedCharacterController>();
        if (advancedCharacterController == null)
        {
            Debug.LogError("PlayerGameObject does not have a advancedCharacterController. This will result in unwanted behaviour");
        }
        this.controllable = this.playerGameObject.GetComponent<Controllable>();
        this.playerCamera = this.playerGameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreatePlayerSource(GameObject sourceObject, GameObject projectedObject)
    {
        if (sourceObject == this.playerGameObject && this.controllable != null)
        {
            Camera projectedCamera = projectedObject.GetComponentInChildren<Camera>();
            this.controllable.SetPlayerCamera(projectedCamera);
        }
    }

    void RemovePlayerSource(GameObject sourceObject)
    {
        if (sourceObject == this.playerGameObject && this.controllable != null && this.playerCamera != null)
        {
            this.controllable.SetPlayerCamera(this.playerCamera);
        }
    }

    void PlayerTransitionEnter(GameObject sourceObject, Transform interiorZone, Transform exteriorZone)
    {
        if (sourceObject == this.playerGameObject)
        {
            // advancedCharacterController.RotateGravityVelocity(Quaternion.Inverse(exteriorZone.rotation));
        }
    }

    void PlayerTransitionExit(GameObject sourceObject, Transform interiorZone, Transform exteriorZone)
    {
        if (sourceObject == this.playerGameObject)
        {
            // advancedCharacterController.RotateGravityVelocity(exteriorZone.rotation);
        }
    }
}
