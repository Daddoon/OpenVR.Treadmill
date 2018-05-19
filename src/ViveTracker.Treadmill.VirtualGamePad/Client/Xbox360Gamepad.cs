using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViveTracker.Treadmill.VirtualGamePad.Client
{
    public class Xbox360Gamepad : Xbox360Controller
    {
        public Xbox360Gamepad(ViGEmClient client) : base(client)
        {
        }

        public Xbox360Gamepad(ViGEmClient client, ushort vendorId, ushort productId)
            : base(client, vendorId, productId)
        {

        }

        public override void Connect()
        {
            base.Connect();
        }

        public override void Disconnect()
        {
            try
            {
                base.Disconnect();
            }
            catch (Exception)
            {
            }
        }
    }
}
