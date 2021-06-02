using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 5f;

    private Camera playerCamera;
    private float pitchRotation = 0f;
    private Vector2 inputLook;
    void Start()
    {
        this.playerCamera = GetComponentInChildren<Camera>();
        if (this.playerCamera == null)
        {
            Debug.LogError("Needs to be a camera on gameobject or in a child gameobject");
        }
    }

    void LateUpdate()
    {
        HandleCameraMovement();
    }

    public void Look(Vector2 look)
    {
        this.inputLook = look;
    }

    void HandleCameraMovement()
    {
        float mouseX = this.inputLook.x * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = this.inputLook.y * mouseSensitivity * Time.fixedDeltaTime;

        this.transform.Rotate(Vector3.up * mouseX);

        pitchRotation -= mouseY;
        pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(pitchRotation, 0, 0);
    }
}
