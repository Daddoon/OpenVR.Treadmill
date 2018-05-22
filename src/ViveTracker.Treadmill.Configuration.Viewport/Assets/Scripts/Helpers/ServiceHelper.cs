using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Services;

public static class ServiceHelper
{
    public static void RegisterServices()
    {
        DependencyService.Register<IMessageBox>(new MessageBox());
        DependencyService.Register<IGamepadService>(new GamepadService());
    }

    public static void ConnectProcessToServices(StreamWriter Gamepad)
    {
        var gamepadService = DependencyService.Get<IGamepadService>();
        var messageBoxService = DependencyService.Get<IMessageBox>();

        if (gamepadService != null)
        {
            gamepadService.AddPipe(Gamepad);
            
        }

        if (messageBoxService != null)
        {
            messageBoxService.AddPipe(Gamepad);
        }
    }
}