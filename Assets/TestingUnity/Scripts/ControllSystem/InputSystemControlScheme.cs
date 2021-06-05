using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemControlScheme : IControllScheme
{
    private Dictionary<string, InputActionMap> actionMapDict = new Dictionary<string, InputActionMap>();
    private Dictionary<string, InputAction> actionDict = new Dictionary<string, InputAction>();
    private Dictionary<string, Action<InputAction.CallbackContext>> actionSubscriptions = new Dictionary<string, Action<InputAction.CallbackContext>>();

    private bool _enabled = false;
    public bool enabled
    {
        get { return this._enabled; }
    }

    public void AddActionMap(InputActionAsset actionAsset, string map)
    {
        InputActionMap actionMap = actionAsset.FindActionMap(map);
        this.actionMapDict[map] = actionMap;
    }

    public void AddAction(InputActionAsset actionAsset, string map, string action, ActionTypeHandler.ActionType actionType, Action<InputAction.CallbackContext> callback)
    {
        if (!this.actionMapDict.ContainsKey(map))
        {
            AddActionMap(actionAsset, map);
        }
        if (!this.actionDict.ContainsKey(action))
        {
            InputAction inputAction = this.actionMapDict[map].FindAction(action);
            this.actionDict[action] = inputAction;
        }
        if (ActionTypeHandler.IsStarted(actionType))
        {
            Action<InputAction.CallbackContext> useCallback = ctx => { if (this.enabled) { callback(ctx); } };
            this.actionDict[action].started += useCallback;
            string subKey = map + action + ActionTypeHandler.ActionType.Started;
            this.actionSubscriptions[subKey] = useCallback;
        }
        if (ActionTypeHandler.IsPerformed(actionType))
        {
            Action<InputAction.CallbackContext> useCallback = ctx => { if (this.enabled) { callback(ctx); } };
            this.actionDict[action].performed += useCallback;
            string subKey = map + action + ActionTypeHandler.ActionType.Performed;
            this.actionSubscriptions[subKey] = useCallback;
        }
        if (ActionTypeHandler.IsCanceled(actionType))
        {
            Action<InputAction.CallbackContext> useCallback = ctx => { if (this.enabled) { callback(ctx); } };
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

    public void Enable()
    {
        this._enabled = true;
        foreach (var actionMap in this.actionMapDict.Values)
        {
            actionMap.Enable();
        }
    }

    public void Disable()
    {
        this._enabled = false;
        foreach (var actionMap in this.actionMapDict.Values)
        {
            actionMap.Disable();
        }
    }
}
