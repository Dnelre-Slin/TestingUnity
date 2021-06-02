using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdvancedCharacterController), typeof(AdvancedPlayerCameraController))]
public class AdvancedPlayerControllerSimplified : MonoBehaviour
{
    [SerializeField]
    private string inputHorizontalAxis = "Horizontal";
    [SerializeField]
    private string inputVerticalAxis = "Vertical";
    [SerializeField]
    private string inputMouseXAxis = "Mouse X";
    [SerializeField]
    private string inputMouseYAxis = "Mouse Y";
    [SerializeField]
    private string inputJumpButton = "space";

    private AdvancedCharacterController controller;
    private AdvancedPlayerCameraController cameraController;
    void Start()
    {
        this.controller = GetComponent<AdvancedCharacterController>();
        this.cameraController = GetComponent<AdvancedPlayerCameraController>();
    }

    void Update()
    {
        if (this.inputHorizontalAxis != "" && this.inputVerticalAxis != "")
        {
            Vector2 playerMove = new Vector2(Input.GetAxisRaw(this.inputHorizontalAxis), Input.GetAxisRaw(this.inputVerticalAxis));
            this.controller.Move(playerMove);
        }
        if (this.inputMouseXAxis != "" && this.inputMouseYAxis != "")
        {
            Vector2 playerLook = new Vector2(Input.GetAxisRaw(this.inputMouseXAxis), Input.GetAxisRaw(this.inputMouseYAxis));
            this.cameraController.Look(playerLook);
        }
        if (this.inputJumpButton != "")
        {
            if (Input.GetKeyDown(this.inputJumpButton))
            {
                this.controller.Jump();
            }
        }
    }
}
