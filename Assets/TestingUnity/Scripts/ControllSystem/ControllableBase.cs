// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// abstract public class ControllableBase : MonoBehaviour, IControllable
// {
//     [SerializeField]
//     protected Camera playerCamera = null;

//     protected ControllableManagment controllableManagment = null;
//     protected PossessionSystem possessionSystem = null;

//     public bool isControlled
//     {
//         get { return this.possessionSystem.isControlled; }
//     }

//     public virtual void Awake()
//     {
//         this.controllableManagment = GameObject.FindObjectOfType<ControllableManagment>();
//     }

//     // Used to set this controllable as the initial controllable. Should only be called be ControllableManagment
//     public void InitControll()
//     {
//         this.possessionSystem.InitControll();
//     }

//     public void AquirePossession()
//     {
//         if (this.controllableManagment != null)
//         {
//             Controllable currentControllable = this.controllableManagment.GetCurrentControlledControllable();
//             currentControllable.Possess(this);
//         }
//     }

//     public void Possess(IControllable newNext)
//     {
//         this.possessionSystem.Possess(((ControllableBase)newNext).possessionSystem);
//     }

//     public void Unpossess()
//     {
//         this.possessionSystem.Unpossess();
//     }

//     public void SetPlayerCamera(Camera newPlayerCamera)
//     {
//         this.playerCamera = newPlayerCamera;
//         this.possessionSystem.SetPlayerCamera(newPlayerCamera);
//     }
// }
