using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (ENABLE_INPUT_SYSTEM)
using UnityEngine.InputSystem;
#endif

public class Controllable : MonoBehaviour, IControllable
{
    public static event Action<Controllable> OnNewControlled;

    #if (ENABLE_INPUT_SYSTEM)
    [SerializeField]
    private InputActionAsset actionAsset = null;
    #endif
    [SerializeField]
    private Camera playerCamera = null;

    private ControllableManagment controllableManagment = null;

    private PossessionSystem possessionSystem = null;
    private InputSystemControlScheme inputSystemControlScheme = null;

    public bool isControlled
    {
        get { return this.possessionSystem.isControlled; }
    }

    void Awake()
    {
        this.controllableManagment = GameObject.FindObjectOfType<ControllableManagment>();
        this.inputSystemControlScheme = new InputSystemControlScheme();
        this.possessionSystem = new PossessionSystem(this.controllableManagment, this.playerCamera, this.inputSystemControlScheme, this.OnNewControlledCallback);
    }

    public void AddActionMap(string map)
    {
        #if (ENABLE_INPUT_SYSTEM)
        if (this.actionAsset != null)
        {
            this.inputSystemControlScheme.AddActionMap(actionAsset, map);
        }
        #elif (ENABLE_LEGACY_INPUT_MANAGER)
        throw new UnityException("AddActionMap can only be used with new InputSystem");
        #endif
    }

    public void AddAction(string map, string action, ActionTypeHandler.ActionType actionType, Action<InputAction.CallbackContext> callback)
    {
        #if (ENABLE_INPUT_SYSTEM)
        if (this.actionAsset != null)
        {
            this.inputSystemControlScheme.AddAction(actionAsset, map, action, actionType, callback);
        }
        #elif (ENABLE_LEGACY_INPUT_MANAGER)
        throw new UnityException("AddAction can only be used with new InputSystem");
        #endif
    }

    public void RemoveAction(string map, string action, ActionTypeHandler.ActionType actionType)
    {
        #if (ENABLE_INPUT_SYSTEM)
        if (this.actionAsset != null)
        {
            this.inputSystemControlScheme.RemoveAction(map, action, actionType);
        }
        #elif (ENABLE_LEGACY_INPUT_MANAGER)
        throw new UnityException("RemoveAction can only be used with new InputSystem");
        #endif
    }

    private void OnNewControlledCallback(PossessionSystem possessionSystem)
    {
        OnNewControlled(this);
    }

    // Used to set this controllable as the initial controllable. Should only be called be ControllableManagment
    public void InitControll()
    {
        this.possessionSystem.InitControll();
    }

    public void AquirePossession()
    {
        if (this.controllableManagment != null)
        {
            Controllable currentControllable = this.controllableManagment.GetCurrentControlledControllable();
            currentControllable.Possess(this);
        }
    }

    public void Possess(IControllable newNext)
    {
        this.possessionSystem.Possess(((Controllable)newNext).possessionSystem);
    }

    public void Unpossess()
    {
        this.possessionSystem.Unpossess();
    }

    public void SetPlayerCamera(Camera newPlayerCamera)
    {
        this.playerCamera = newPlayerCamera;
        this.possessionSystem.SetPlayerCamera(newPlayerCamera);
    }
}
