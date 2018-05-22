using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViveTracker.Treadmill.Common.Interop;
using ViveTracker.Treadmill.Common.Serialization;

namespace ViveTracker.Treadmill.Common.Services
{
    public static class ProcessToProcessDispatcher
    {
        public static void Send(StreamWriter sr, MethodProxy methodProxy)
        {
            string csharpProxy = BridgeSerializer.Serialize(methodProxy);
            sr.WriteLine(csharpProxy);
        }

        public static void Receive(MethodProxy resultProxy)
        {
            if (resultProxy == null)
                return;

            var taskToReturn = MethodDispatcher.GetTaskDispatcher(resultProxy.TaskIdentity);
            MethodDispatcher.SetTaskResult(resultProxy.TaskIdentity, resultProxy);

            if (taskToReturn == null)
                return;

            taskToReturn.RunSynchronously();

            MethodDispatcher.ClearTask(resultProxy.TaskIdentity);
        }
    }
}
