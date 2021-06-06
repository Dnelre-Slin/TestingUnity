using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LegacyInputTypeRawAxis
{
    public string axisX;
    public string axisY;
    public Action<Vector2> callback;

    public LegacyInputTypeRawAxis(string axisX, string axisY, Action<Vector2> callback)
    {
        this.axisX = axisX;
        this.axisY = axisY;
        this.callback = callback;
    }
}

public struct LegacyInputTypeKeyDown
{
    public string key;
    public Action callback;

    public LegacyInputTypeKeyDown(string key, Action callback)
    {
        this.key = key;
        this.callback = callback;
    }
}
