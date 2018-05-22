using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViveTracker.Treadmill.Common.Models
{
    public enum Tracker : int
    {
        Invalid,
        Tracker1,
        Tracker2,
        Tracker3,
        Tracker4,
        Tracker5,
        Tracker6,
        Tracker7,
        Tracker8,
        Tracker9,
        Tracker10,
        Tracker11,
        Tracker12,
        Tracker13,
    }

    public enum DeviceType : int
    {
        HMD = 0,
        Hand = 1,
        Tracker = 2
    }

    public class DeviceState
    {
        public DeviceType deviceType { get; set; }
        public Tracker trackerRole { get; set; }
        public uint deviceIndex { get; set; }
        public string serialNumber { get; set; }
        public string modelNumber { get; set; }
        public string renderModelName { get; set;  }
        public string deviceClass { get; set; }
        public string deviceModel { get; set;  }
    }
}
