using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionTypeHandler
{
    [System.Flags]
    public enum ActionType
    {
        None = 0,
        Started = 1,
        Performed = 2,
        Canceled = 4
    }

    static public bool IsStarted(ActionType type)
    {
        return (ActionType.Started & type) == ActionType.Started;
    }
    static public bool IsPerformed(ActionType type)
    {
        return (ActionType.Performed & type) == ActionType.Performed;
    }
    static public bool IsCanceled(ActionType type)
    {
        return (ActionType.Canceled & type) == ActionType.Canceled;
    }
}

public class Controllable : MonoBehaviour
{
    public static event Action<Controllable> OnNewControlled;

    [SerializeField]
    private InputActionAsset actionAsset = null;
    [SerializeField]
    private Camera playerCamera = null;

    private Dictionary<string, InputActionMap> actionMapDict = new Dictionary<string, InputActionMap>();
    private Dictionary<string, InputAction> actionDict = new Dictionary<string, InputAction>();
    private Dictionary<string, Action<InputAction.CallbackContext>> actionSubscriptions = new Dictionary<string, Action<InputAction.CallbackContext>>();

    private Controllable next = null;
    private Controllable prev = null;

    private bool _isControlled = false;
    public bool isControlled
    {
        get { return this._isControlled; }
    }

    public void AddActionMap(string map)
    {
        InputActionMap actionMap = this.actionAsset.FindActionMap(map);
        this.actionMapDict[map] = actionMap;
    }

    public void AddAction(string map, string action, ActionTypeHandler.ActionType actionType, Action<InputAction.CallbackContext> callback)
    {
        if (!this.actionMapDict.ContainsKey(map))
        {
            AddActionMap(map);
        }
        if (!this.actionDict.ContainsKey(action))
        {
            InputAction inputAction = this.actionMapDict[map].FindAction(action);
            this.actionDict[action] = inputAction;
        }
        if (ActionTypeHandler.IsStarted(actionType))
        {
            Action<InputAction.CallbackContext> useCallback = ctx => { if (this.isControlled) {callback(ctx);} };
            this.actionDict[action].started += useCallback;
            string subKey = map + action + ActionTypeHandler.ActionType.Started;
            this.actionSubscriptions[subKey] = useCallback;
        }
        if (ActionTypeHandler.IsPerformed(actionType))
        {
            Action<InputAction.CallbackContext> useCallback = ctx => { if (this.isControlled) {callback(ctx);} };
            this.actionDict[action].performed += useCallback;
            string subKey = map + action + ActionTypeHandler.ActionType.Performed;
            this.actionSubscriptions[subKey] = useCallback;
        }
        if (ActionTypeHandler.IsCanceled(actionType))
        {
            Action<InputAction.CallbackContext> useCallback = ctx => { if (this.isControlled) {callback(ctx);} };
            this.actionDict[action].canceled += useCallback;
            string subKey = map + action + ActionTypeHandler.ActionType.Canceled;
            this.actionSubscriptions[subKey] = useCallback;
        }
    }

    public void RemoveAction(string map, string action, ActionTypeHandler.ActionType actionType)
    {
        if (ActionTypeHandler.IsStarted(actionType))
        {
            string subKey = map + action + ActionTypeHandler.ActionType.Started;
            this.actionDict[action].started -= this.actionSubscriptions[subKey];
            this.actionSubscriptions.Remove(subKey);
        }
        if (ActionTypeHandler.IsPerformed(actionType))
        {
            string subKey = map + action + ActionTypeHandler.ActionType.Performed;
            this.actionDict[action].performed -= this.actionSubscriptions[subKey];
            this.actionSubscriptions.Remove(subKey);
        }
        if (ActionTypeHandler.IsCanceled(actionType))
        {
            string subKey = map + action + ActionTypeHandler.ActionType.Canceled;
            this.actionDict[action].canceled -= this.actionSubscriptions[subKey];
            this.actionSubscriptions.Remove(subKey);
        }
    }

    // Used to set this controllable as the initial controllable. Should only be called be ControllableManagment
    public void InitControll()
    {
        this.Controll();
    }

    public void Possess(Controllable newNext)
    {
        if (newNext == this)
        {
            throw new ArgumentException("Cannot possess itself");
        }
        if (this.next != null)
        {
            throw new UnityException("Not the head of a possesion chain");
        }
        this.next = newNext;
        this.UnControll();
        newNext.HandlePossess(this);
    }

    private void HandlePossess(Controllable newPrev)
    {
        if (this.prev != null || this.next != null)
        {
            throw new UnityException("Already in a possesion chain");
        }
        this.prev = newPrev;
        this.Controll();
    }

    public void Unpossess()
    {
        if (this.next != null)
        {
            throw new UnityException("Not the head of a possesion chain");
        }
        if (this.prev == null)
        {
            throw new UnityException("Cannot unpossess root chain node");
        }
        this.UnControll();
        this.prev.HandleUnPossess();
        this.prev = null;
    }

    private void HandleUnPossess()
    {
        if (this.next == null)
        {
            throw new UnityException("Control returned to a node that does not have a child");
        }
        this.next = null;
        this.Controll();
    }

    private void Controll()
    {
        if (!this.isControlled)
        {
            if (this.playerCamera != null)
            {
                this.playerCamera.enabled = true;
            }

            foreach (var entry in this.actionMapDict)
            {
                entry.Value.Enable();
            }

            this._isControlled = true;
            OnNewControlled(this);
        }
    }

    private void UnControll()
    {
        if (this.isControlled)
        {
            this._isControlled = false;

            foreach (var entry in this.actionMapDict)
            {
                entry.Value.Disable();
            }

            if (this.playerCamera != null)
            {
                this.playerCamera.enabled = false;
            }
        }
    }

    public void SetPlayerCamera(Camera newPlayerCamera)
    {
        this.playerCamera = newPlayerCamera;
    }
}
