using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveTracker.Treadmill.Common.Interface.Base
{
    public interface IPipedEntity
    {
        void AddPipe(StreamWriter pipe);
        void RemovePipe(StreamWriter pipe);
        void RemoveAllPipe();
        void DisablePipe(StreamWriter pipe);
        void DisableAllPipe();
        void EnablePipe(StreamWriter pipe);
        void EnableAllPipe();
    }
}
