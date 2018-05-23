using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViveTracker.Treadmill.Common.Events
{
    public class GamepadEventArgs : EventArgs
    {
        private short _leftThumbX = 0;
        private short _leftThumbY = 0;
        private short _rightThumbX = 0;
        private short _rightThumbY = 0;

        public GamepadEventArgs(short leftThumbX, short leftThumbY, short rightThumbX, short rightThumbY)
        {
            _leftThumbX = leftThumbX;
            _leftThumbY = leftThumbY;
            _rightThumbX = rightThumbX;
            _rightThumbY = rightThumbY;
        }

        public short LeftThumbX { get { return _leftThumbX; } }
        public short LeftThumbY { get { return _leftThumbY; } }
        public short RightThumbX { get { return _rightThumbX; } }
        public short RightThumbY { get { return _rightThumbY; } }
    }
}
