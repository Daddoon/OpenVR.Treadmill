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

    public Text PitchLabel;

    [HideInInspector]
    public float Pitch;

    public GameObject ObjectToFollow;

    public bool IsLeftFoot = false;

    [HideInInspector]
    public bool HasForwardAngle = true; 

    protected void Awake()
    {
        overlay = GetComponent<Unity_Overlay>();
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

	// Update is called once per frame
	void Update () {

        if (ObjectToFollow != null && GetFootIndex() != OpenVR.k_unTrackedDeviceIndexInvalid)
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

            if (IsLeftFoot)
            {
                Debug.Log("Pitch: " + Pitch);
            }
            

            ComputeForwardAngle();

            UpdatePitchLabel();
        }
        else
        {
            Pitch = MovementCalibration.PitchNeutral;

            ComputeForwardAngle();

            UpdatePitchLabel();

            if (overlay.isVisible)
                overlay.isVisible = false;
        }
    }
}
