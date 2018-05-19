using System;
using System.Threading.Tasks;
using ViveTracker.Treadmill.ServiceCore.Enums;
using ViveTracker.Treadmill.VirtualGamePad.Helper;

namespace ViveTracker.Treadmill.ServiceCore
{


    public class GamepadServer
    {
        private ServerState _state = ServerState.NotRunning;
        internal void SetServerState(ServerState state)
        {
            _state = state;
        }

        public ServerState GetServerState()
        {
            return _state;
        }

        //TODO: TO REMOVE, JUST FOR INPUT TESt
        private bool waitingStop = true;
        Task manageEvent = null;

        public void Run()
        {
            if (!GamepadInfo.IsGamepadDriverInstalled())
            {
                SetServerState(ServerState.NoDriver);
                Console.WriteLine("Gamepad Server failed to start: No SCP Driver found!");
                return;
            }

            GamepadControl.PlugGamepad();

            Console.WriteLine("Gamepad Server started!");

            manageEvent = Task.Run(async () =>
            {
                while (waitingStop)
                {
                    GamepadControl.SendForward();
                    await Task.Delay(10);
                }
            });

            //TODO: Do something
        }

        public void Stop()
        {
            waitingStop = false;
            Task.Delay(50).GetAwaiter().GetResult();
            GamepadControl.Shutdown();
            Console.WriteLine("Gamepad Server stopped!");
            //TODO: Do something
        }
    }
}
