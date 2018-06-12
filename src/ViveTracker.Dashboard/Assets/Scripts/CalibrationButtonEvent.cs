using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationButtonEvent : MonoBehaviour {

    private Button calibrateBtn;
    public GameObject leftFootOverlay;
    public GameObject rightFootOverlay;

    public FollowGridPosition gridPosition;

    // Use this for initialization
    void Start () {
        calibrateBtn = GetComponent<Button>();
        calibrateBtn.onClick.AddListener(TaskCalibrate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CalibrateFeets()
    {
        //Positioning the joystick zone as we don't want to alterate the experience if the head move afterward calibration
        if (gridPosition != null)
        {
            gridPosition.SetJoystickPosition();
        }

        for (int i = 0; i < 3; i++)
        {
            if (leftFootOverlay != null)
            {
                var deltaX = Mathf.DeltaAngle(leftFootOverlay.transform.rotation.eulerAngles.x, 0.0f);
                Debug.Log("Left DeltaX is: " + deltaX);

                MovementCalibration.LeftFeetBaseRotationDeltaX = deltaX + 50;

                //var deltaZ = Mathf.DeltaAngle(leftFootOverlay.transform.rotation.eulerAngles.z, 0.0f);
                //Debug.Log("Left DeltaZ is: " + deltaZ);

                //MovementCalibration.LeftFeetBaseRotationDeltaZ = deltaZ + 45;
            }

            if (rightFootOverlay != null)
            {
                var deltaX = Mathf.DeltaAngle(rightFootOverlay.transform.rotation.eulerAngles.x, 0.0f);
                Debug.Log("Right DeltaX is: " + deltaX);

                MovementCalibration.RightFeetBaseRotationDeltaX = deltaX + 50;

                //var deltaZ = Mathf.DeltaAngle(rightFootOverlay.transform.rotation.eulerAngles.z, 0.0f);
                //Debug.Log("Right DeltaZ is: " + deltaZ);

                //MovementCalibration.RightFeetBaseRotationDeltaZ = deltaX + 45;
            }

            yield return new WaitForSeconds(0.250f);
        }

        Debug.Log("You have calibrated the Trackers!");
        yield return null;
    }

    void TaskCalibrate()
    {
        StartCoroutine(CalibrateFeets());
    }
}
