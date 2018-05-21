using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.NugetToUnity.Models;

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

        private Task receiveHandler = null;

        public PipedProcess() : base()
        {
            _clientReceive = new AnonymousPipeServerStream(
                PipeDirection.In,
                HandleInheritability.Inheritable);

            _clientSend = new AnonymousPipeServerStream(
                PipeDirection.Out,
                HandleInheritability.Inheritable);

            _clientReceive.ReadMode = PipeTransmissionMode.Byte;
            _clientSend.ReadMode = PipeTransmissionMode.Byte;
        }

        public new bool Start()
        {
            var success = ((Process)this).Start();
            _clientReceive.DisposeLocalCopyOfClientHandle();
            _clientSend.DisposeLocalCopyOfClientHandle();

            if (success == false)
                return false;

            WaitForInputIdle(1000);

            //TODO: Add some form of Timeout on this line below

            //SERVER: First Read, Then Write
            //CHILD: First Write, Then Read

            //We should now receive the PipeHandle of the Child
            sr = new StreamReader(_clientReceive);
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
                receiveHandler = ReceiveHandler();
                return true;
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

                    var mp = MethodProxy.GetFromJson(content);
                    mp.Invoke();
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

        public void DisposeClientPipe()
        {
            //TODO: Send disconnection to client
            _clientSend.Dispose();
            _clientReceive.Dispose();
        }
    }
}
