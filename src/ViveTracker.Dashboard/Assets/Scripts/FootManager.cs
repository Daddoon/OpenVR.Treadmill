using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Unity_Overlay))]
public class FootManager : MonoBehaviour {

    [HideInInspector]
    public Unity_Overlay overlay;
    public Unity_SteamVR_Handler handler;

    [HideInInspector]
    public float Pitch;

    public GameObject ObjectToFollow;

    public bool IsLeftFoot = false;

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

            Debug.Log("Pich: " + Pitch);
        }
        else
        {
            Pitch = MovementCalibration.PitchNeutral;

            if (overlay.isVisible)
                overlay.isVisible = false;
        }
    }
}
