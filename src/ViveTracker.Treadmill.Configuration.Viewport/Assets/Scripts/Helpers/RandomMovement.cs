using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveTracker.Treadmill.VirtualGamePad.Helper;

public class RandomMovement : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        GamepadControl.PlugGamepad();
    }
	
	// Update is called once per frame
	void Update () {
        GamepadControl.SendForward();
	}

    void OnApplicationQuit()
    {
        GamepadControl.UnPlugGamepad();
    }
}
