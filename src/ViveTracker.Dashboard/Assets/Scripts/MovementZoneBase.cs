using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unity_Overlay))]
public abstract class MovementZoneBase : MonoBehaviour
{
    protected Unity_Overlay overlay = null;

    private float _movementZoneRatio = MovementConst.DefaultMovementZoneRatio;
    public float MovementZoneRatio
    {
        get
        {
            return _movementZoneRatio;
        }
    }

    protected virtual void Awake()
    {
        overlay = GetComponent<Unity_Overlay>();
        if (overlay == null)
        {
            //Something is wrong
            Debug.LogError("Unable to find the required Unity_Overlay on " + this.GetType().Name + " type");
        }
    }

    public void SetMovementZoneRatio(float value)
    {
        if (value < MovementConst.MinMovementZoneRatio)
        {
            //Assuming minimum value
            value = MovementConst.MinMovementZoneRatio;
        }

        if (value > MovementConst.MaxMovementZoneRatio)
        {
            //Assuming max value
            value = MovementConst.MaxMovementZoneRatio;
        }

        _movementZoneRatio = value;
        overlay.widthInMeters = _movementZoneRatio;
    }
}
