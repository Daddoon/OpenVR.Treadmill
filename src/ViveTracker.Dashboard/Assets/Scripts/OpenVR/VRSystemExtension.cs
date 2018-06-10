
using Valve.VR;

public static class VRSystemExtension
{
    public static uint GetTrackedDeviceIndexForTrackerRole(this CVRSystem VRSystem, EViveTrackerControllerRole unDeviceType)
    {
        uint deviceOccurence = 0;

        for (uint deviceId = 0; deviceId < OpenVR.k_unMaxTrackedDeviceCount; deviceId++)
        {
            ETrackedDeviceClass @class =
            VRSystem.GetTrackedDeviceClass(deviceId);

            if (@class == ETrackedDeviceClass.GenericTracker)
            {
                if (deviceOccurence == (uint)unDeviceType)
                {
                    if (!VRSystem.IsTrackedDeviceConnected(deviceId))
                        return OpenVR.k_unTrackedDeviceIndexInvalid;

                    return deviceId;
                }

                deviceOccurence++;
            }
        }

        return OpenVR.k_unTrackedDeviceIndexInvalid;
    }
}