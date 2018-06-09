using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Configuration.Forms.Models;

namespace ViveTracker.Treadmill.Configuration.Forms.Helpers
{
    public static class ProcessHelper
    {
        private static List<PipedProcess> processList = new List<PipedProcess>();

        /// <summary>
        /// Get new Viewport process (not started)
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static PipedProcess GetNewViewportProcess(int handle)
        {
            var process = new PipedProcess();
            process.StartInfo.FileName = ApplicationModel.Apps.Viewport.GetAppExecutableAbsolutePath();
            process.StartInfo.Arguments = "-parentHWND " + handle + " " + Environment.CommandLine;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            processList.Add(process);

            return process;
        }

        public static PipedProcess GetGamepadProcess()
        {
            var process = new PipedProcess();
            process.StartInfo.FileName = ApplicationModel.Apps.Gamepad.GetAppExecutableAbsolutePath();
            process.StartInfo.Arguments = "-batchmode -nographics -pipeInHandle" + process.GetReceivePipeHandler() + " -pipeOutHandle" + process.GetSendPipeHandler() + " -pipeGamepadHandle" + process.GetGamepadPipeHandler() + " " + Environment.CommandLine;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            processList.Add(process);

            return process;
        }

        public static void StopAllChildProcess()
        {
            foreach (PipedProcess process in processList)
            {
                if (process.HasExited)
                    continue;

                process.DisposeClientPipe();
                Thread.Sleep(100);

                process.CloseMainWindow();
                Thread.Sleep(1000);
                while (process.HasExited == false)
                    process.Kill();
            }
        }
    }
}
