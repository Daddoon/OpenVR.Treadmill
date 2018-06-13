using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CalibrationButtonEvent : MonoBehaviour {

    private Button calibrateBtn;
    public GameObject leftFootOverlay;
    public GameObject rightFootOverlay;
    public GameObject HMD;

    public FollowGridPosition gridPosition;

    // Use this for initialization
    void Start () {
        calibrateBtn = GetComponent<Button>();
        calibrateBtn.onClick.AddListener(TaskCalibrate);
	}

    #region DEBUG

    public static Vector3 RadianToDegree(Vector3 rad)
    {
        return new Vector3(rad.x * Mathf.Rad2Deg, rad.y * Mathf.Rad2Deg, rad.z * Mathf.Rad2Deg);
    }

    public static Vector3 AngleFromQ2(Quaternion q1)
    {
        float sqw = q1.w * q1.w;
        float sqx = q1.x * q1.x;
        float sqy = q1.y * q1.y;
        float sqz = q1.z * q1.z;
        float unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
        float test = q1.x * q1.w - q1.y * q1.z;
        Vector3 v;

        if (test > 0.4995f * unit)
        { // singularity at north pole
            v.y = Convert.ToSingle(2.0 * Math.Atan2(q1.y, q1.x));
            v.x = Convert.ToSingle(Math.PI / 2.0);
            v.z = 0;
            return NormalizeAngles(RadianToDegree(v));
        }
        if (test < -0.4995f * unit)
        { // singularity at south pole
            v.y = Convert.ToSingle(-2.0 * Math.Atan2(q1.y, q1.x));
            v.x = Convert.ToSingle(-Math.PI / 2.0);
            v.z = 0;
            return NormalizeAngles(RadianToDegree(v));
        }
        Quaternion q = new Quaternion(q1.w, q1.z, q1.x, q1.y);
        v.y = (float)Math.Atan2(2f * q.x * q.w + 2.0 * q.y * q.z, 1 - 2.0 * (q.z * q.z + q.x * q.w));     // Yaw
        v.x = (float)Math.Asin(2f * (q.x * q.z - q.w * q.y));                             // Pitch
        v.z = (float)Math.Atan2(2f * q.x * q.y + 2.0 * q.z * q.w, 1 - 2.0 * (q.y * q.y + q.z * q.z));      // Roll
        return NormalizeAngles(RadianToDegree(v));
    }

    static Vector3 NormalizeAngles(Vector3 angles)
    {
        angles.x = NormalizeAngle(angles.x);
        angles.y = NormalizeAngle(angles.y);
        angles.z = NormalizeAngle(angles.z);
        return angles;
    }

    static float NormalizeAngle(float angle)
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;
        return angle;
    }

    #endregion

    void RotationDebug()
    {
        //Debug.Log("Current RotationX: " + MovementCalibration.GetXAngle(leftFootOverlay.transform.parent));
        //Debug.Log("Current RotationY: " + (leftFootOverlay.transform.parent.rotation.eulerAngles.y));
        //Debug.Log("Current RotationZ: " + (leftFootOverlay.transform.parent.rotation.eulerAngles.z));

        return;

        #region X

        Quaternion newRotation = Quaternion.identity; // Create new quaternion with no rotation
        newRotation.x = leftFootOverlay.transform.parent.rotation.x; // Get only the X rotation from the controllers quaternion

        // Output the x rotation. It should be a value between -1 and 1
        //Debug.Log("newRotation.x: " + newRotation.x);
        // Convert to a value between 0 and 360
        float xEuler = (newRotation.x + 1) * 180;
         // Output the x rotation as a Euler angle
         Debug.Log("xEuler: " + xEuler);

        #endregion X

        #region Y

        Quaternion newRotationY = Quaternion.identity; // Create new quaternion with no rotation
        newRotationY.y = leftFootOverlay.transform.parent.rotation.y; // Get only the X rotation from the controllers quaternion

        // Output the x rotation. It should be a value between -1 and 1
        //Debug.Log("newRotation.x: " + newRotation.x);
        // Convert to a value between 0 and 360
        float yEuler = (newRotationY.y + 1) * 180;
        // Output the x rotation as a Euler angle
        Debug.Log("yEuler: " + yEuler);

        #endregion Y

        #region Z

        Quaternion newRotationZ = Quaternion.identity; // Create new quaternion with no rotation
        newRotationZ.z = leftFootOverlay.transform.parent.rotation.z; // Get only the X rotation from the controllers quaternion

        // Output the x rotation. It should be a value between -1 and 1
        //Debug.Log("newRotation.x: " + newRotation.x);
        // Convert to a value between 0 and 360
        float zEuler = (newRotationZ.z + 1) * 180;
        // Output the x rotation as a Euler angle
        Debug.Log("zEuler: " + zEuler);

        #endregion Z

        //Debug.Log("X: " + leftFootOverlay.transform.parent.rotation);
    }

    // Update is called once per frame
    void FixedUpdate () {
        RotationDebug();
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

                MovementCalibration.PitchBaseRotationDelta = MovementCalibration.PitchNeutral - MovementCalibration.GetXAngle(leftFootOverlay.transform.parent);

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
