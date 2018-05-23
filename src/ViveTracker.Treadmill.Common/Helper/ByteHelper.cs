using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ViveTracker.Treadmill.Common.Helper
{
    public static class ByteHelper
    {
        public static byte[] GetBytes<T>(T str)
        {
            var type = typeof(T);

            int size = Marshal.SizeOf(str);

            byte[] arr = new byte[size];

            GCHandle h = default(GCHandle);

            try
            {
                h = GCHandle.Alloc(arr, GCHandleType.Pinned);

                Marshal.StructureToPtr(str, h.AddrOfPinnedObject(), false);
            }
            finally
            {
                if (h.IsAllocated)
                {
                    h.Free();
                }
            }

            return arr;
        }

        public static T FromBytes<T>(byte[] arr) where T : struct
        {
            var type = typeof(T);

            T str = default(T);

            GCHandle h = default(GCHandle);

            try
            {
                h = GCHandle.Alloc(arr, GCHandleType.Pinned);

                str = (T)Marshal.PtrToStructure(h.AddrOfPinnedObject(), type);

            }
            finally
            {
                if (h.IsAllocated)
                {
                    h.Free();
                }
            }

            return str;
        }
    }
}
