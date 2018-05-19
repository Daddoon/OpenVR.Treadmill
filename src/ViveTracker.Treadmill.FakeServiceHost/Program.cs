using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViveTracker.Treadmill.ServiceCore;

namespace ViveTracker.Treadmill.FakeServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                exitEvent.Set();
            };

            var server = new GamepadServer();
            server.Run();

            exitEvent.WaitOne();
            server.Stop();
        }
    }
}
