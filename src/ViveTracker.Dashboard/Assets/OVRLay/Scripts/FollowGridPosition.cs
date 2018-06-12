using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGridPosition : MonoBehaviour {

    public GameObject ParentObject;

	// Use this for initialization
	void Start () {
        SetJoystickPosition();
    }
	
    public void SetJoystickPosition()
    {
        if (ParentObject == null)
            return;

        transform.position = new Vector3(ParentObject.transform.position.x, transform.position.y, ParentObject.transform.position.z);
    }

	// Update is called once per frame
	void Update () {

	}
}
