using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnGround : MonoBehaviour {

    public GameObject ParentObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ParentObject == null)
            return;

        transform.position = new Vector3(ParentObject.transform.position.x, transform.position.y, ParentObject.transform.position.z);
	}
}
