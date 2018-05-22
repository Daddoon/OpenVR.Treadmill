using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Interface.Base;

namespace ViveTracker.Treadmill.Common.Models
{
    /// <summary>
    /// All theses Pipe only concern the Sending pipe
    /// </summary>
    public abstract class PipedEntity : IPipedEntity
    {
        public PipedEntity()
        {
            PipeList = new List<StreamWriter>();
            DisabledPipeList = new List<StreamWriter>();
        }

        public IList<StreamWriter> PipeList { get; set; }

        public IList<StreamWriter> DisabledPipeList { get; set; }

        public IEnumerable<StreamWriter> GetPipe()
        {
            return PipeList;
        }

        public void AddPipe(StreamWriter pipe)
        {
            if (!PipeList.Contains(pipe) && !DisabledPipeList.Contains(pipe))
            {
                PipeList.Add(pipe);
            }
        }

        public void RemovePipe(StreamWriter pipe)
        {
            PipeList.Remove(pipe);
            DisabledPipeList.Remove(pipe);
        }

        public void RemoveAllPipe()
        {
            PipeList.Clear();
            DisabledPipeList.Clear();
        }

        public void DisablePipe(StreamWriter pipe)
        {
            if (PipeList.Contains(pipe))
            {
                PipeList.Remove(pipe);
                if (!DisabledPipeList.Contains(pipe))
                {
                    DisabledPipeList.Add(pipe);
                }
            }
        }

        public void DisableAllPipe()
        {
            foreach (var pipe in PipeList)
            {
                if (!DisabledPipeList.Contains(pipe))
                {
                    DisabledPipeList.Add(pipe);
                }
            }

            PipeList.Clear();
        }

        public void EnablePipe(StreamWriter pipe)
        {
            if (DisabledPipeList.Contains(pipe))
            {
                DisabledPipeList.Remove(pipe);
                if (!PipeList.Contains(pipe))
                {
                    PipeList.Add(pipe);
                }
            }
        }

        public void EnableAllPipe()
        {
            foreach (var pipe in DisabledPipeList)
            {
                if (!PipeList.Contains(pipe))
                {
                    PipeList.Add(pipe);
                }
            }

            DisabledPipeList.Clear();
        }
    }
}
