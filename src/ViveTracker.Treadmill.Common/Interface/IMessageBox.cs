using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Attributes;
using ViveTracker.Treadmill.Common.Interface.Base;

namespace ViveTracker.Treadmill.Common.Interface
{
    [ProxyInterface]
    public interface IMessageBox : IPipedEntity
    {
        void ShowAlert(string message);
    }
}
