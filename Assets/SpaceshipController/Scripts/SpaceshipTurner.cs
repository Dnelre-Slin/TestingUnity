using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipTurner : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform = null;
    [SerializeField]
    float cursorTravelDistance = 140.0f;
    [SerializeField]
    float deadzone = 15.0f;
    [SerializeField]
    float cursorSensitivity = 20.0f;

    Vector2 cursorPos = Vector2.zero;
    Vector2 mouseInput = Vector2.zero;

    public void TurnPitchYaw(Vector2 mouseInput)
    {
        this.mouseInput = mouseInput;
    }

    public void ClearInputs()
    {
        this.mouseInput = Vector2.zero;
        this.cursorPos = Vector2.zero;
    }

    void Update()
    {
        this.cursorPos += this.mouseInput * this.cursorSensitivity * Time.deltaTime;

        float magnitude = this.cursorPos.magnitude;

        if (magnitude > this.cursorTravelDistance)
        {
            this.cursorPos = this.cursorPos * (this.cursorTravelDistance / magnitude);
        }

        if (this.rectTransform != null)
        {
            this.rectTransform.localPosition = this.cursorPos;
        }

        Vector3 rotation = new Vector3(-this.cursorPos.y, this.cursorPos.x, 0.0f);

        if (this.cursorPos.magnitude > this.deadzone)
        {
            this.transform.Rotate(rotation * Time.deltaTime);
        }
    }
}
