using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private IGamepadService gamepadService = null;

    // Use this for initialization
    void Start () {

        DontDestroyOnLoad(this);

        if (!_init)
        {
            Arguments = Environment.GetCommandLineArgs();
            Application.targetFrameRate = 60;

            ServiceHelper.RegisterServices();

            gamepadService = DependencyService.Get<IGamepadService>();

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

    private static System.Random rand = new System.Random();

    public bool first = true;


    private float dta = 0.0f;
    // Update is called once per frame
    void Update () {

        return;

        if (first == true)
        {
            if (gamepadService.GetGamepadWriter() == null)
                return;

            dta += Time.deltaTime;
            if (dta < 2.0f)
                return;

            try
            {
                var sw = gamepadService.GetGamepadWriter();
                sw.WriteLine("DumbValue");
            }
            catch (Exception ex)
            {
                DependencyService.Get<IMessageBox>().ShowAlert("Gamepad.exe: " + ex.Message + " " + ex.StackTrace);
            }



            //gamepadService.SendTrigger(
            //(short)rand.Next(short.MinValue + 10, short.MaxValue - 10),
            //(short)rand.Next(short.MinValue + 10, short.MaxValue - 10),
            //(short)rand.Next(short.MinValue + 10, short.MaxValue - 10),
            //(short)rand.Next(short.MinValue + 10, short.MaxValue - 10));

            first = false;
        }
	}
}
