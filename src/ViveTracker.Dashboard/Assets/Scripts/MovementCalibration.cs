using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementCalibration
{
    public const float FeetDefaultInitialLocalRotationX = 0.0f;
    public const float FeetDefaultInitialLocalRotationY = 180.0f;
    public const float FeetDefaultInitialLocalRotationZ = 0.0f;

    public const float PitchNeutral = 90.0f;

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

    public static float GetXAngle(Transform transform)
    {
        float xAngle;

        // Get X
        if (transform.eulerAngles.y > 180f)
        {
            //if (dragAlphaArrow.eulerAngles.x > 256f)
            if (transform.eulerAngles.x > 256f)
                xAngle = (transform.eulerAngles.x * -1f) + 360f;
            else
                xAngle = -transform.eulerAngles.x;
        }
        else
        {

            if (transform.eulerAngles.x > 256f)
                xAngle = transform.eulerAngles.x - 180f;
            else
                xAngle = ((transform.eulerAngles.x * -1f) + 180f) * -1f;
        }

        return xAngle;
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
