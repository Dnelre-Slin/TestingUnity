using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class FloatMathFunctions
{
    static public float NormalizeFloat(float x, float oldMin, float oldMax, float newMin, float newMax)
    {
        return FloatMathFunctions.NormalizeFloat01(x, oldMin, oldMax) * (newMax - newMin);
    }

    static public float NormalizeFloat01(float x, float oldMin, float oldMax)
    {
        return (x - oldMin) / (oldMax - oldMin);
    }
}
