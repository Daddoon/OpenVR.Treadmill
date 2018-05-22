using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Interop;

namespace ViveTracker.Treadmill.Common.Models
{
    public class TaskDispatch
    {
        public int TaskId { get; set; }

        public Task ResultAction { get; set; }

        public CancellationTokenSource CancelTokenSource { get; set; }

        public CancellationToken CancelToken { get; set; }

        public MethodProxy ResultData { get; set; }

        public void CancelTask()
        {
            CancelTokenSource.Cancel();
        }
    }
}
