using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MovementCalibration
{
    public const float FeetDefaultInitialLocalRotationX = 0.0f;
    public const float FeetDefaultInitialLocalRotationY = 180.0f;
    public const float FeetDefaultInitialLocalRotationZ = 0.0f;

    private static float _leftFeetBaseRotationDeltaX = 0.0f;
    private static float _rightFeetBaseRotationDeltaX = 0.0f;

    private static float _leftFeetBaseRotationDeltaZ = 0.0f;
    private static float _rightFeetBaseRotationDeltaZ = 0.0f;

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
}
