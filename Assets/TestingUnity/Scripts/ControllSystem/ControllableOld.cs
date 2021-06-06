// #if (ENABLE_INPUT_SYSTEM)
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class Controllable : ControllableBase, IControllable
// {
//     public static event Action<Controllable> OnNewControlled;

//     [SerializeField]
//     private InputActionAsset actionAsset = null;

//     private InputSystemControlScheme inputSystemControlScheme = null;

//     public override void Awake()
//     {
//         base.Awake();
//         this.inputSystemControlScheme = new InputSystemControlScheme();
//         this.possessionSystem = new PossessionSystem(this.controllableManagment, this.playerCamera, this.inputSystemControlScheme, this.OnNewControlledCallback);
//     }

//     protected void OnNewControlledCallback(PossessionSystem possessionSystem)
//     {
//         OnNewControlled(this);
//     }

//     public void AddActionMap(string map)
//     {
//         if (this.actionAsset != null)
//         {
//             this.inputSystemControlScheme.AddActionMap(actionAsset, map);
//         }
//     }

//     public void AddAction(string map, string action, ActionTypeHandler.ActionType actionType, Action<InputAction.CallbackContext> callback)
//     {
//         if (this.actionAsset != null)
//         {
//             this.inputSystemControlScheme.AddAction(actionAsset, map, action, actionType, callback);
//         }
//     }

//     public void RemoveAction(string map, string action, ActionTypeHandler.ActionType actionType)
//     {
//         if (this.actionAsset != null)
//         {
//             this.inputSystemControlScheme.RemoveAction(map, action, actionType);
//         }
//     }
// }
// #endif