using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementZoneDetectionUI : MovementZoneBase
{
    private MovementZoneManager parentZoneManager = null;

    protected override void Awake()
    {
        base.Awake();
        parentZoneManager = this.transform.parent.gameObject.GetComponent<MovementZoneManager>();
    }

    // Use this for initialization
    void Start () {
        SetMovementZoneRatio(MovementConst.DefaultMovementZoneRatio);
    }
	
	// Update is called once per frame
	void Update () {
        //DetectionUpscaleSimulationLoop();
	}

    void DetectionUpscaleSimulationLoop()
    {
        if (MovementZoneRatio < parentZoneManager.MovementZoneRatio)
        {
            SetMovementZoneRatio(MovementZoneRatio + 0.05f);
        }
        else
        {
            SetMovementZoneRatio(MovementConst.MinMovementZoneRatio);
        }
    }
}
