using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementZoneManager : MovementZoneBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start () {
        //TODO: Load profile info (like movement zone ratio) at startup	
        SetMovementZoneRatio(MovementConst.DefaultMovementZoneRatio);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
