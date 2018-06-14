using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class MovementCalibration
{
    public const float FeetDefaultInitialLocalRotationX = 0.0f;
    public const float FeetDefaultInitialLocalRotationY = 180.0f;
    public const float FeetDefaultInitialLocalRotationZ = 0.0f;

    public const float PitchNeutral = -90.0f;

    public static float _pitchRotationDelta = 0.0f;

    private static float _leftFeetBaseRotationDeltaX = 0.0f;
    private static float _rightFeetBaseRotationDeltaX = 0.0f;

    private static float _leftFeetBaseRotationDeltaZ = 0.0f;
    private static float _rightFeetBaseRotationDeltaZ = 0.0f;

    public static float PitchBaseRotationDelta
    {
        get
        {
            return _pitchRotationDelta;
        }
        set
        {
            _pitchRotationDelta = value;
            ComputedBasePitch = PitchNeutral + _pitchRotationDelta;
        }
    }

    public static float LeftFeetBaseRotationDeltaX
    {
        get
        {
            return _leftFeetBaseRotationDeltaX;
        }
        set
        {
            _leftFeetBaseRotationDeltaX = value;
            ComputedLeftFeetBaseRotationX = FeetDefaultInitialLocalRotationX + _leftFeetBaseRotationDeltaX;
        }
    }

    public static float RightFeetBaseRotationDeltaX
    {
        get
        {
            return _rightFeetBaseRotationDeltaX;
        }
        set
        {
            _rightFeetBaseRotationDeltaX = value;
            ComputedRightFeetBaseRotationX = FeetDefaultInitialLocalRotationX + _rightFeetBaseRotationDeltaX;
        }
    }

    public static float LeftFeetBaseRotationDeltaZ
    {
        get
        {
            return _leftFeetBaseRotationDeltaZ;
        }
        set
        {
            _leftFeetBaseRotationDeltaZ = value;
            ComputedLeftFeetBaseRotationZ = FeetDefaultInitialLocalRotationZ + _leftFeetBaseRotationDeltaZ;
        }
    }

    public static float RightFeetBaseRotationDeltaZ
    {
        get
        {
            return _rightFeetBaseRotationDeltaZ;
        }
        set
        {
            _rightFeetBaseRotationDeltaZ = value;
            ComputedRightFeetBaseRotationZ = FeetDefaultInitialLocalRotationZ + _rightFeetBaseRotationDeltaZ;
        }
    }

    public static float ComputedLeftFeetBaseRotationY = FeetDefaultInitialLocalRotationY;
    public static float ComputedRightFeetBaseRotationY = FeetDefaultInitialLocalRotationY;

    public static float ComputedLeftFeetBaseRotationX = FeetDefaultInitialLocalRotationX;
    public static float ComputedRightFeetBaseRotationX = FeetDefaultInitialLocalRotationX;

    public static float ComputedLeftFeetBaseRotationZ = FeetDefaultInitialLocalRotationZ;
    public static float ComputedRightFeetBaseRotationZ = FeetDefaultInitialLocalRotationZ;

    public static float ComputedBasePitch = PitchNeutral;

    private static MethodInfo GetLocalEulerAnglesMethod = null;
    private static PropertyInfo rotationOrderProperty = null;

    public static float GetXAngle(Transform transform)
    {
        Vector3 vect3 = Vector3.zero;

        if (GetLocalEulerAnglesMethod == null)
        {
            GetLocalEulerAnglesMethod = typeof(Transform).GetMethod("GetLocalEulerAngles", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        if (rotationOrderProperty == null)
        {
            rotationOrderProperty = typeof(Transform).GetProperty("rotationOrder", BindingFlags.Instance | BindingFlags.NonPublic);
        }
       

        object rotationOrder = null;
        if (rotationOrderProperty != null)
        {
            rotationOrder = rotationOrderProperty.GetValue(transform, null);
        }
        if (GetLocalEulerAnglesMethod != null)
        {
            object retVector3 = GetLocalEulerAnglesMethod.Invoke(transform, new object[] { rotationOrder });
            vect3 = (Vector3)retVector3;
            //Debug.Log("Get Inspector Euler:" + vect3);
        }

        float result = vect3.x;

        if (vect3.z > 150 || vect3.z < -150)
        {
            if (vect3.x < 0)
            {
                result = (180f + vect3.x) * -1f;
            }
            else if (vect3.x > 0)
            {
                result = (-180f + vect3.x) * -1f;
            }
        }

        return result;
    }

    public static float BoundsDisplacement(float angle, float displacement)
    {
        float maxDisplacement = 180.0f - displacement;
        float minDisplacement = -180.0f - displacement;

        if (angle > maxDisplacement)
        {
            return angle - maxDisplacement;
        }
        else if (angle < minDisplacement)
        {
            return maxDisplacement - (minDisplacement - angle);
        }

        return angle;
    }
}
