extern alias Task35;
using ITaskString = Task35::System.Threading.Tasks.Task<string>;
using ITaskBool = Task35::System.Threading.Tasks.Task<bool>;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Models;
using ViveTracker.Treadmill.Common.Services;
using System.Reflection;

namespace ViveTracker.Treadmill.Configuration.Forms.Services
{
    public class GamepadService : PipedEntity, IGamepadService
    {
        public ITaskBool CloseGamepadCanal()
        {
            throw new NotImplementedException();
        }

        public ITaskString GetConnectedDevices()
        {
            return MethodDispatcher.CallMethodAsync<string>(GetPipe()[0], MethodBase.GetCurrentMethod());
        }

        public Stream GetGamepadReader()
        {
            throw new NotImplementedException();
        }

        public Stream GetGamepadWriter()
        {
            throw new NotImplementedException();
        }

        public ITaskBool OpenGamepadCanal()
        {
            throw new NotImplementedException();
        }
    }
}
