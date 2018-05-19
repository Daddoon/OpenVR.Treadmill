using System;

namespace ViveTracker.Treadmill.VirtualGamePad.Helper
{
    public static class GamepadInfo
    {
        public static bool IsGamepadDriverInstalled()
        {
            //TODO: Proper Driver install of ViGem. Assuming manual installation
            return true;
        }

        public static bool InstallGamepadDriver()
        {
            try
            {
                //TODO: Proper Driver install of ViGem. Assuming manual installation
                return true;
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}
