using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class VectorCalculations
{
    // The acceleration in the following functions, should always be in absolute value (always positive), or behaviour is unpredicable

    // Gradually go from one float to another, based on the acceleration. acceleraction should be in absulote value (always positive), or behaviour is unpredictable
    static public float GradualFloatChange(float from, float to, float acceleration)
    {
        float result = 0.0f;
        float delta = to - from;
        if (Mathf.Abs(delta) <= acceleration)
        {
            result = to;
        }
        else
        {
            result = from + Mathf.Sign(delta) * acceleration;
        }

        return result;
    }

    // Gradually go from one vector to another, based on the acceleration. Will be component-wise. acceleraction should be in absulote value (always positive), or behaviour is unpredictable
    static public Vector3 GradualVector3Change(Vector3 from, Vector3 to, Vector3 acceleration)
    {
        Vector3 result = Vector3.zero;

        result.x = VectorCalculations.GradualFloatChange(from.x, to.x, acceleration.x);
        result.y = VectorCalculations.GradualFloatChange(from.y, to.y, acceleration.y);
        result.z = VectorCalculations.GradualFloatChange(from.z, to.z, acceleration.z);

        return result;
    }

    // Gradually go from one vector to another, based on the acceleration. acceleraction should be in absulote value (always positive), or behaviour is unpredictable
    static public Vector3 GradualVector3Change(Vector3 from, Vector3 to, float acceleration)
    {
        Vector3 result = Vector3.zero;

        result.x = VectorCalculations.GradualFloatChange(from.x, to.x, acceleration);
        result.y = VectorCalculations.GradualFloatChange(from.y, to.y, acceleration);
        result.z = VectorCalculations.GradualFloatChange(from.z, to.z, acceleration);

        return result;
    }
}
