using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputSystemControlScheme))]
public class UiCursorController : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform = null;
    [SerializeField]
    float cursorTravelDistance = 150.0f;
    [SerializeField]
    float deadzone = 70.0f;
    [SerializeField]
    float cursorSensitivity = 5.0f;

    InputSystemControlScheme controlScheme = null;

    Vector2 cursorPos = Vector2.zero;
    Vector2 mouseInput = Vector2.zero;

    void Start()
    {
        this.controlScheme = this.GetComponent<InputSystemControlScheme>();

        this.controlScheme.AddActionMap("Spaceship");
        this.controlScheme.AddAction("Spaceship", "TurnPitchYaw", ActionTypeHandler.ActionType.Performed | ActionTypeHandler.ActionType.Canceled, this.OnTurnPitchYaw);
    }

    void OnTurnPitchYaw(InputAction.CallbackContext context)
    {
        if (this.rectTransform != null)
        {
            this.mouseInput = context.ReadValue<Vector2>();
            // this.cursorPos += context.ReadValue<Vector2>() * this.cursorSensitivity * Time.deltaTime;

            // float magnitude = this.cursorPos.magnitude;

            // if (magnitude > this.cursorTravelDistance)
            // {
            //     this.cursorPos = this.cursorPos * (this.cursorTravelDistance / magnitude);
            // }

            // Vector3 newPos = this.cursorPos;

            // this.rectTransform.localPosition = newPos;
        }
    }

    void Update()
    {
        if (this.rectTransform != null)
        {
            this.cursorPos += this.mouseInput * this.cursorSensitivity * Time.deltaTime;

            float magnitude = this.cursorPos.magnitude;

            if (magnitude > this.cursorTravelDistance)
            {
                this.cursorPos = this.cursorPos * (this.cursorTravelDistance / magnitude);
            }

            this.rectTransform.localPosition = this.cursorPos;

            Vector3 rotation = new Vector3(-this.cursorPos.y, this.cursorPos.x, 0.0f);

            if (this.cursorPos.magnitude > this.deadzone)
            {
                this.transform.Rotate(rotation * Time.deltaTime);
            }
        }
    }
}
