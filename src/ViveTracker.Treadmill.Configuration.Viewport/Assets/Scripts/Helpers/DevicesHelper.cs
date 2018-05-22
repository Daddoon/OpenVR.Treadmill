using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using HTC.UnityPlugin.Utility;
using HTC.UnityPlugin.VRModuleManagement;
using ViveTracker.Treadmill.Common.Models;

public static class DevicesHelper {

    private static DeviceState GetDeviceInformation(TrackerRole role)
    {
        var deviceIndex = ViveRole.GetDeviceIndexEx(role);
        if (!VRModule.IsValidDeviceIndex(deviceIndex))
            return null;

        var deviceState = VRModule.GetDeviceState(deviceIndex);

        var ds = new DeviceState()
        {
            deviceType = ViveTracker.Treadmill.Common.Models.DeviceType.Tracker,
            trackerRole = (Tracker)((int)role),
            deviceIndex = deviceIndex,
            serialNumber = deviceState.serialNumber,
            deviceClass = deviceState.deviceClass.ToString(),
            deviceModel = deviceState.deviceModel.ToString()
        };

        return ds;
    }

    public static List<DeviceState> GetActiveDevices()
    {
        List<DeviceState> activeDevices = new List<DeviceState>()
        {
            GetDeviceInformation(TrackerRole.Tracker1),
            GetDeviceInformation(TrackerRole.Tracker2),
            GetDeviceInformation(TrackerRole.Tracker3),
            GetDeviceInformation(TrackerRole.Tracker4),
            GetDeviceInformation(TrackerRole.Tracker5),
            GetDeviceInformation(TrackerRole.Tracker6),
            GetDeviceInformation(TrackerRole.Tracker7),
            GetDeviceInformation(TrackerRole.Tracker8),
            GetDeviceInformation(TrackerRole.Tracker9),
            GetDeviceInformation(TrackerRole.Tracker10),
            GetDeviceInformation(TrackerRole.Tracker11),
            GetDeviceInformation(TrackerRole.Tracker12),
            GetDeviceInformation(TrackerRole.Tracker13),
        };

        activeDevices.RemoveAll(p => p == null);

        return activeDevices;
    }

}
