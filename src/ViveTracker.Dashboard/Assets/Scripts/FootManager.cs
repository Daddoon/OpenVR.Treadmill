using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

[RequireComponent(typeof(Unity_Overlay))]
public class FootManager : MonoBehaviour {

    [HideInInspector]
    public Unity_Overlay overlay;
    public Unity_SteamVR_Handler handler;

    public GameObject HMD;

    public Text PitchLabel;

    [HideInInspector]
    public float Pitch;

    public GameObject ObjectToFollow;

    public bool IsLeftFoot = false;

    [HideInInspector]
    public bool HasForwardAngle = true;

    public StepInfo Step;

    private float currentStepDuration = 0.0f;

    protected void Awake()
    {
        overlay = GetComponent<Unity_Overlay>();
        StartCoroutine(GetFootDirection());
    }

    // Use this for initialization
    void Start () {
		if (handler == null)
        {
            Debug.LogError("Unable to find SteamVR handler in scene");
        }

        //SetDeviceToTrack();
    }

    public uint GetFootIndex()
    {
        if (IsLeftFoot)
            return handler.poseHandler.leftFootIndex;
        else
            return handler.poseHandler.rightFootIndex;
    }
	
    void UpdatePitchLabel()
    {
        if (PitchLabel != null)
        {
            string footName = string.Empty;

            if (!IsLeftFoot)
            {
                footName = "Right foot pitch: ";
            }
            else
            {
                footName = "Left foot pitch: ";
            }

            PitchLabel.text = footName + string.Format("{0:F3}", Pitch) + ", Direction: " + (HasForwardAngle ? "Forward" : "Backward");
        }
    }

    void ComputeForwardAngle()
    {
        if (Pitch >= -45.0f && Pitch <= 20.0f)
        {
            HasForwardAngle = false;
        }
        else
        {
            HasForwardAngle = true;
        }
    }

    float StepCounter = 0.0f;
    //float LastForwardDistance = 0.0f;
    //float LastBackwardDistance = 0.0f;
    float LastDistance = 0.0f;

    void OnStepChange()
    {
        Debug.Log("Step: " + Step.State.ToString() + ", Amplitude:" + Step.Amplitude);
    }

    IEnumerator GetFootDirection()
    {
        var footTransform = ObjectToFollow.transform;
        var hmdTransform = HMD.transform;

        while (true)
        {
            if (!ValidFoot())
            {
                Step = new StepInfo();
                yield return null;
            }

            currentStepDuration += Time.deltaTime;

            if (currentStepDuration < MovementConst.FootDetectionRate)
            {
                yield return null;
            }

            currentStepDuration = 0.0f;

            bool notifyStepChange = false;

            float distance = Vector3.Distance(hmdTransform.position, footTransform.position);

            var distanceDiff = Mathf.Abs(distance - LastDistance);
            if (distanceDiff <= MovementConst.FootStepIdle)
            {
                var amplitude = distanceDiff;

                if (Step.State == StepState.Neutral)
                {
                    amplitude = 0.0f;
                }
                else
                {
                    notifyStepChange = true;
                }

                Step = new StepInfo()
                {
                    State = StepState.Neutral,
                    Amplitude = amplitude
                };
            }
            else
            {
                var amplitude = distanceDiff;

                if (distance > LastDistance)
                {
                    //Same Step state, we must add amplitude
                    if (Step.State == StepState.StepIn)
                    {
                        amplitude += Step.Amplitude;
                    }
                    else
                    {
                        notifyStepChange = true;
                    }

                    Step = new StepInfo()
                    {
                        State = StepState.StepIn,
                        Amplitude = amplitude
                    };
                }
                else
                {
                    //Same Step state, we must add amplitude
                    if (Step.State == StepState.StepOut)
                    {
                        amplitude += Step.Amplitude;
                    }
                    else
                    {
                        notifyStepChange = true;
                    }

                    Step = new StepInfo()
                    {
                        State = StepState.StepOut,
                        Amplitude = amplitude
                    };
                }
            }

            LastDistance = distance;
            if (notifyStepChange)
            {
                OnStepChange();
            }

            yield return null;
        }
    }

    bool ValidFoot()
    {
        return ObjectToFollow != null && GetFootIndex() != OpenVR.k_unTrackedDeviceIndexInvalid;
    }

	// Update is called once per frame
	void Update () {

        if (ValidFoot())
        {
            if (!overlay.isVisible)
                overlay.isVisible = true;

            float RotationX = 0.0f;
            float RotationY = 0.0f;
            float RotationZ = 0.0f;

            if (IsLeftFoot)
            {
                RotationX = MovementCalibration.ComputedLeftFeetBaseRotationX;
                RotationY = MovementCalibration.ComputedLeftFeetBaseRotationY;
                RotationZ = MovementCalibration.ComputedLeftFeetBaseRotationZ;
            }
            else
            {
                RotationX = MovementCalibration.ComputedRightFeetBaseRotationX;
                RotationY = MovementCalibration.ComputedRightFeetBaseRotationY;
                RotationZ = MovementCalibration.ComputedRightFeetBaseRotationZ;
            }
            
            this.gameObject.transform.localRotation = Quaternion.Euler(RotationX, RotationY, RotationZ);

            Pitch = MovementCalibration.BoundsDisplacement(MovementCalibration.GetXAngle(ObjectToFollow.transform), MovementCalibration.PitchBaseRotationDelta) + MovementCalibration.PitchBaseRotationDelta;

            //if (IsLeftFoot)
            //{
            //    Debug.Log("Pitch: " + Pitch);
            //}
            

            ComputeForwardAngle();

            GetFootDirection();

            UpdatePitchLabel();
        }
        else
        {
            Pitch = MovementCalibration.PitchNeutral;

            ComputeForwardAngle();

            GetFootDirection();

            UpdatePitchLabel();

            if (overlay.isVisible)
                overlay.isVisible = false;
        }
    }
}
