using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    // private ControllableManager controllableManager;
    // private Camera currentCamera = null;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     controllableManager = FindObjectOfType<ControllableManager>();
    //     controllableManager.SubscribeToPossession(OnPossession);

    //     // Turn off all cameras on start
    //     foreach (var cam in GameObject.FindObjectsOfType<Camera>())
    //     {
    //         cam.enabled = false;
    //     }

    //     string gameObjectName = controllableManager.GetPossessedGameObjectName();
    //     if (gameObjectName.Length != 0)
    //     {
    //         // Will turn on the camera of the possessed gameobject
    //         OnPossession(gameObjectName);
    //     }

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    // void OnPossession(string gameObjectName)
    // {
    //     Debug.Log("Camera possessed: " + gameObjectName);

    //     // if (this.currentCamera != null)
    //     // {
    //     //     this.currentCamera.enabled = false;
    //     // }

    //     Camera oldCam = this.currentCamera;

    //     this.currentCamera = GameObject.Find(gameObjectName).GetComponentInChildren<Camera>();
    //     // Debug.Log(this.currentCamera);
    //     this.currentCamera.enabled = true;
    //     // Debug.Log(this.currentCamera.enabled);

    //     if (oldCam != null)
    //     {
    //         oldCam.enabled = false;
    //     }
    // }
}
