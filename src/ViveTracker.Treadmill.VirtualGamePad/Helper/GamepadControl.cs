using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System;
using System.Collections.Generic;
using System.Text;
using ViveTracker.Treadmill.VirtualGamePad.Client;

namespace ViveTracker.Treadmill.VirtualGamePad.Helper
{
    public static class GamepadControl
    {
        private static GamepadClient client = null;
        private static Xbox360Gamepad gamepad = null;

        public static void Shutdown()
        {
            UnPlugGamepad();
        }

        public static void UnPlugGamepad()
        {
            //TODO: Separate Disconnecting from disposing (like mute control)
            gamepad.Disconnect();
            gamepad?.Dispose();
            client?.Dispose();
            client = null;
            gamepad = null;
        }

        public static void PlugGamepad()
        {
            if (client == null)
            {
                client = new GamepadClient();
                gamepad = new Xbox360Gamepad(client);
                gamepad.Connect();
            }
        }

        private static Random rand = new Random();

        public static void SendForward()
        {
            var report = new Xbox360Report();
            report.SetAxis(Xbox360Axes.LeftThumbX, (short)rand.Next(short.MinValue + 10, short.MaxValue - 10));
            report.SetAxis(Xbox360Axes.RightThumbX, (short)rand.Next(short.MinValue + 10, short.MaxValue - 10));

            report.SetAxis(Xbox360Axes.LeftThumbY, (short)rand.Next(short.MinValue + 10, short.MaxValue - 10));
            report.SetAxis(Xbox360Axes.RightThumbY, (short)rand.Next(short.MinValue + 10, short.MaxValue - 10));

            gamepad.SendReport(report);
            

            //// Get a reference to the state of the first GamePad:
            //var simPad = SimGamePad.Instance;
            //var state = SimGamePad.Instance.State[wiredGamepad];
            //// Pull the left trigger halfway back:
            //state.LeftTrigger = 127;
            //// Move the right analog stick three quarters of the way to the left:
            //state.RightStickX = short.MinValue * 3 / 4;
            //state.RightStickY = short.MinValue * 3 / 4;

            //state.LeftStickX = short.MinValue * 3 / 4;
            //state.LeftStickY = short.MinValue * 3 / 4;
            //// Add the RightBumber to the set of held buttons:
            //state.Buttons |= GamePadControl.RightShoulder;
            //// Update the driver's simulated state with the above state changes:
            //simPad.Update(wiredGamepad);
        }
    }
}
