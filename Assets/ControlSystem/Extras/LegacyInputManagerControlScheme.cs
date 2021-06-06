#if (ENABLE_LEGACY_INPUT_MANAGER)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyInputManagerControlScheme : MonoBehaviour, IControlScheme
{
    private bool _controlsEnabled = false;
    public bool controlsEnabled
    {
        get { return this._controlsEnabled; }
    }

    private List<LegacyInputTypeRawAxis> rawAxisInputs = new List<LegacyInputTypeRawAxis>();
    private List<LegacyInputTypeKeyDown> keyDownInputs = new List<LegacyInputTypeKeyDown>();

    void AddRawAxisInput(string axisX, string axisY, Action<Vector2> callback)
    {
        this.rawAxisInputs.Add(new LegacyInputTypeRawAxis(axisX, axisY, callback));
    }

    void AddKeyDownInput(string key, Action callback)
    {
        this.keyDownInputs.Add(new LegacyInputTypeKeyDown(key, callback));
    }

    void Update()
    {
        foreach (var rawAxis in this.rawAxisInputs)
        {
            Vector2 vec2 = new Vector2(Input.GetAxisRaw(rawAxis.axisX), Input.GetAxisRaw(rawAxis.axisY));
            rawAxis.callback(vec2);
        }
        foreach (var keyDown in this.keyDownInputs)
        {
            if (Input.GetKeyDown(keyDown.key))
            {
                keyDown.callback();
            }
        }
    }

    public void Enable()
    {
        this._controlsEnabled = true;
    }

    public void Disable()
    {
        this._controlsEnabled = false;
    }
}

#endif