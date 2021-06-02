using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IControllable
{
    void AddActionMap(string map);
    void AddAction(string map, string action, ActionTypeHandler.ActionType actionType, Action<InputAction.CallbackContext> callback);
    void RemoveAction(string map, string action, ActionTypeHandler.ActionType actionType);
}
