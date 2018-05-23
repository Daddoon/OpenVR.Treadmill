using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Attributes;
using ViveTracker.Treadmill.Common.Events;
using ViveTracker.Treadmill.Common.Interface.Base;

namespace ViveTracker.Treadmill.Common.Interface
{
    [ProxyInterface]
    public interface IGamepadService : IPipedEntity
    {
        #region EVENT

        event GamepadEventHandler Trigger;

        #endregion

        void SendTrigger(short leftThumbX, short leftThumbY, short rightThumbX, short rightThumbY);

        #region BOTH

        StreamReader GetGamepadReader();

        StreamWriter GetGamepadWriter();

        #endregion

        void StartGamepadListener();

        void StopGamepadListener();

        #region PARENT

        /// <summary>
        /// Call from parent
        /// </summary>
        /// <returns></returns>
        Task<bool> OpenGamepadCanal();

        /// <summary>
        /// Call from parent
        /// </summary>
        /// <returns></returns>
        Task<bool> CloseGamepadCanal();

        Task<string> GetConnectedDevices();

        #endregion

        #region CHILD

        Task<bool> AnswerOpenGamepadCanal(string clientHandle);

        #endregion
    }
}
