using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StepState
{
    Neutral,
    StepIn,
    StepOut
}

public struct StepInfo
{
    public StepState State;
    public float Amplitude;
}