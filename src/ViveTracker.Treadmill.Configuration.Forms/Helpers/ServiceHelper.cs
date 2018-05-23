using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Services;
using ViveTracker.Treadmill.Configuration.Forms.Models;
using ViveTracker.Treadmill.Configuration.Forms.Services;

namespace ViveTracker.Treadmill.Configuration.Forms.Helpers
{
    public static class ServiceHelper
    {
        public static void RegisterServices()
        {
            DependencyService.Register<IMessageBox>(new CustomMessageBox());
            DependencyService.Register<IGamepadService>(new GamepadService());
        }

        public static void ConnectProcessToServices(PipedProcess Gamepad)
        {
            var gamepadService = DependencyService.Get<IGamepadService>();
            var messageBoxService = DependencyService.Get<IMessageBox>();

            gamepadService?.AddPipe(Gamepad.GetSendPipe());
            messageBoxService?.AddPipe(Gamepad.GetSendPipe());

            var gamepadOk = gamepadService.OpenGamepadCanal().GetAwaiter().GetResult();
            if (!gamepadOk)
            {
                MessageBox.Show("Unable to launch Gamepad background service. Application will now exit");
                Application.Exit();
            }

            gamepadService.StartGamepadListener();
        }
    }
}
