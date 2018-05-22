using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViveTracker.Treadmill.Common.Interop;
using ViveTracker.Treadmill.Common.Serialization;
using ViveTracker.Treadmill.Common.Services;

namespace ViveTracker.Treadmill.Common.Helper
{
    public static class PipeHelper
    {
        //Auto mange In/Out call of Pipes methods
        public static void HandleMethodCalls(StreamWriter sw, string content)
        {
            var methodProxy = BridgeSerializer.Deserialize<MethodProxy>(content);

            switch (methodProxy.Direction)
            {
                case CallbackDirection.In:
                    var returnValue = ContextBridge.Receive(methodProxy);
                    returnValue.Direction = CallbackDirection.Out;
                    string jsonReturnValue = ContextBridge.GetJSONReturnValue(returnValue);

                    sw.WriteLine(jsonReturnValue);
                    break;
                case CallbackDirection.Out:
                default:
                    ProcessToProcessDispatcher.Receive(methodProxy);
                    break;
            }
        }
    }
}
