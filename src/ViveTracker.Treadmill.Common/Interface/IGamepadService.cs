using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Attributes;
using ViveTracker.Treadmill.Common.Interface.Base;

namespace ViveTracker.Treadmill.Common.Interface
{
    [ProxyInterface]
    public interface IGamepadService : IPipedEntity
    {
        #region BOTH

        Stream GetGamepadReader();

        Stream GetGamepadWriter();

        #endregion

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

        #endregion
    }
}
