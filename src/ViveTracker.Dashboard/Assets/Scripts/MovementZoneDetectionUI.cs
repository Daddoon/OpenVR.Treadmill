using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementZoneDetectionUI : MovementZoneBase
{
    private MovementZoneManager parentZoneManager = null;

    private SphereCollider MaxCollider;

    protected override void Awake()
    {
        base.Awake();
        parentZoneManager = this.transform.parent.gameObject.GetComponent<MovementZoneManager>();
        MaxCollider = GetComponent<SphereCollider>();
    }

    // Use this for initialization
    void Start () {
        SetMovementZoneRatio(MovementConst.DefaultMovementZoneRatio);
    }
	
	// Update is called once per frame
	void Update () {
        //DetectionUpscaleSimulationLoop();
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Activated!");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit!");
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
