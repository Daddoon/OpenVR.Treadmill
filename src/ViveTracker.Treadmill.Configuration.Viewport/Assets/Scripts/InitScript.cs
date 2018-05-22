using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Services;

public class InitScript : MonoBehaviour {

    private bool _init = false;
    public static string[] Arguments = new string[0];

    public static bool CanSupportPipe {
        get
        {
#if DEBUG
            return false;
#else
            return true;
#endif
        }
    }

	// Use this for initialization
	void Start () {

        DontDestroyOnLoad(this);

        if (!_init)
        {
            Arguments = Environment.GetCommandLineArgs();
            Application.targetFrameRate = 60;

            DependencyService.Register<IMessageBox>(new MessageBox());

#if RELEASE

            //Impossible case
            if (Arguments.Length <= 0)
            {
                Application.Quit();
            }
#endif

            PipeHelpers.OpenPipes();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
