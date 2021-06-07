using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedIndicatorUI : MonoBehaviour
{
    [SerializeField]
    private Slider currentMainThrustSpeedIndicator = null;
    [SerializeField]
    private Slider desiredMainThrustSpeedIndicator = null;
    [SerializeField]
    private Text numberIndicator = null;

    public void SetCurrentMainThrustSpeedIndicator(float currentMainThrustSpeedNormalized)
    {
        if (this.currentMainThrustSpeedIndicator != null)
        {
            this.currentMainThrustSpeedIndicator.value = currentMainThrustSpeedNormalized;
        }
    }

    public void SetDesiredMainThrustSpeedIndicator(float desiredMainThrustSpeedNormalized)
    {
        if (this.desiredMainThrustSpeedIndicator != null)
        {
            this.desiredMainThrustSpeedIndicator.value = desiredMainThrustSpeedNormalized;
        }
    }

    public void SetNumberIndicator(float currentMainThrusterSpeed)
    {
        if (this.currentMainThrustSpeedIndicator != null)
        {
            this.numberIndicator.text = currentMainThrusterSpeed.ToString("F0") + " m/s";
        }
    }
}
