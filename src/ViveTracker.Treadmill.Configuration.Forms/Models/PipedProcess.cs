﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Helper;
using ViveTracker.Treadmill.Common.Interop;
using ViveTracker.Treadmill.Common.Models;
using ViveTracker.Treadmill.Common.Serialization;
using ViveTracker.Treadmill.Common.Services;

namespace ViveTracker.Treadmill.Configuration.Forms.Models
{
    public class PipedProcess : Process
    {
        //This pipe send
        private AnonymousPipeServerStream _clientSend { get; set; }
        private StreamWriter sw = null;

        //This pipe receive
        private AnonymousPipeServerStream _clientReceive { get; set; }
        private StreamReader sr = null;

        //This pipe receive
        private AnonymousPipeServerStream _clientGamepad { get; set; }
        private StreamReader srgp = null;

        private Task receiveHandler = null;

        public PipedProcess() : base()
        {
            _clientReceive = new AnonymousPipeServerStream(
                PipeDirection.In,
                HandleInheritability.Inheritable);

            _clientGamepad = new AnonymousPipeServerStream(
                PipeDirection.In,
                HandleInheritability.Inheritable);

            _clientSend = new AnonymousPipeServerStream(
                PipeDirection.Out,
                HandleInheritability.Inheritable);

            _clientReceive.ReadMode = PipeTransmissionMode.Byte;
            _clientSend.ReadMode = PipeTransmissionMode.Byte;
            _clientGamepad.ReadMode = PipeTransmissionMode.Byte;
        }

        public StreamWriter GetSendPipe()
        {
            return sw;
        }

        public StreamReader GetGamepadPipe()
        {
            return srgp;
        }

        public new bool Start()
        {
            var success = ((Process)this).Start();
            _clientReceive.DisposeLocalCopyOfClientHandle();
            _clientSend.DisposeLocalCopyOfClientHandle();
            _clientGamepad.DisposeLocalCopyOfClientHandle();

            if (success == false)
                return false;

            WaitForInputIdle(1000);

            //TODO: Add some form of Timeout on this line below

            //SERVER: First Read, Then Write
            //CHILD: First Write, Then Read

            //We should now receive the PipeHandle of the Child
            sr = new StreamReader(_clientReceive);
            srgp = new StreamReader(_clientGamepad);
            string handle = sr.ReadLine();

            if (handle != "OK")
            {
                throw new InvalidOperationException();
            }

            sw = new StreamWriter(_clientSend);
            sw.AutoFlush = true;
            sw.WriteLine("OK");
            _clientSend.WaitForPipeDrain();

            //TODO: Should set Timeout
            //Should receive OK a second time
            var result = sr.ReadLine();
            if (result == "OK")
            {
                result = srgp.ReadLine();
                if (result == "OK")
                {
                    receiveHandler = ReceiveHandler();
                    return true;
                }

                return false;
            }

            return false;
        }

        private Task ReceiveHandler()
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    var content = sr.ReadLine();
                    if (string.IsNullOrEmpty(content))
                        break;

                    PipeHelper.HandleMethodCalls(sw, content);
                }
            });
        }

        public string GetReceivePipeHandler()
        {
            return _clientReceive.GetClientHandleAsString();
        }

        public string GetSendPipeHandler()
        {
            return _clientSend.GetClientHandleAsString();
        }

        public string GetGamepadPipeHandler()
        {
            return _clientGamepad.GetClientHandleAsString();
        }

        public void DisposeClientPipe()
        {
            //TODO: Send disconnection to client
            _clientSend?.Dispose();
            _clientReceive?.Dispose();
            _clientGamepad?.Dispose();
        }
    }
}
