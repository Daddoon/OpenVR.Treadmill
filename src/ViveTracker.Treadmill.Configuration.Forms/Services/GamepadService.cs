extern alias Task35;
using ITask = Task35::System.Threading.Tasks.Task;
using ITaskString = Task35::System.Threading.Tasks.Task<string>;
using ITaskBool = Task35::System.Threading.Tasks.Task<bool>;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Models;
using ViveTracker.Treadmill.Common.Services;
using System.Reflection;
using System.IO.Pipes;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Helper;
using ViveTracker.Treadmill.Common.Events;
using ViveTracker.Treadmill.VirtualGamePad.Helper;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace ViveTracker.Treadmill.Configuration.Forms.Services
{
    public class GamepadService : PipedEntity, IGamepadService
    {
        private AnonymousPipeServerStream _clientReceive { get; set; }
        private StreamReader _gpr = null;

        #region EVENT

        public event GamepadEventHandler Trigger;

        protected virtual void OnTrigger(GamepadEventArgs e)
        {
            if (Trigger != null)
                Trigger(this, e);
        }

        #endregion

        public ITaskBool CloseGamepadCanal()
        {
            try
            {
                //Not fatal
                GamepadControl.Shutdown();
            }
            catch (Exception)
            {
            }
            

            return MethodDispatcher.CallMethodAsync<bool>(GetPipe()[0], MethodBase.GetCurrentMethod());
        }

        public ITaskString GetConnectedDevices()
        {
            return MethodDispatcher.CallMethodAsync<string>(GetPipe()[0], MethodBase.GetCurrentMethod());
        }

        public StreamReader GetGamepadReader()
        {
            return _gpr;
        }

        public StreamWriter GetGamepadWriter()
        {
            throw new NotImplementedException("This method must be called on the Gamepad Process");
        }

        private Task gamepadListener = null;
        private CancellationTokenSource cts = null;

        public void StartGamepadListener()
        {
            if (gamepadListener != null)
                return;

            cts = new CancellationTokenSource();
            var token = cts.Token;

            var structSize = Marshal.SizeOf(typeof(GamepadEventStruct));

            gamepadListener = Task.Run(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    var sr = GetGamepadReader();
                    if (sr != null)
                    {
                        byte[] buffer = new byte[structSize];
                        byte[] temp = new byte[structSize];
                        int alreadyRead = 0;

                        while (alreadyRead < structSize)
                        {
                            int read = sr.BaseStream.Read(temp, 0, structSize);
                            if (read <= 0)
                            {
                                //TODO: Notify failure if needed

                                //Pipe closed
                                return;
                            }

                            if (read == structSize)
                            {
                                buffer = temp;
                                break;
                            }

                            Array.Copy(temp, 0, buffer, alreadyRead, read);
                            alreadyRead += read;
                        }

                        var gamepadStruct = ByteHelper.FromBytes<GamepadEventStruct>(buffer);
                        OnTrigger(new GamepadEventArgs(gamepadStruct.leftThumbX, gamepadStruct.leftThumbY, gamepadStruct.rightThumbX, gamepadStruct.rightThumbY));
                    }
                }
            }, token);
        }

        public void StopGamepadListener()
        {
            if (gamepadListener == null)
                return;

            try
            {
                cts.Cancel();
            }
            catch (Exception)
            {
            }

            gamepadListener = null;
            cts = null;
        }

        public ITaskBool OpenGamepadCanal()
        {
            //We first try to open VirtualGamepad locally
            try
            {
                GamepadControl.PlugGamepad();
            }
            catch (Exception)
            {
                //TODO: CATCH DRIVER VIGEM BUS MISSING
                return ITask.FromResult(false);
            }

            _clientReceive = new AnonymousPipeServerStream(
                PipeDirection.In,
                HandleInheritability.Inheritable);
            _clientReceive.ReadMode = PipeTransmissionMode.Byte;

            _gpr = new StreamReader(_clientReceive);

            string clientHandle = _clientReceive.GetClientHandleAsString();
            _clientReceive.DisposeLocalCopyOfClientHandle();

            return MethodDispatcher.CallMethodAsync<bool>(
                GetPipe()[0],
                MethodInfoHelper.GetMethodInfo(() => AnswerOpenGamepadCanal(clientHandle)),
                clientHandle);
        }

        public ITaskBool AnswerOpenGamepadCanal(string clientHandle)
        {
            throw new NotImplementedException();
        }

        public void SendTrigger(short leftThumbX, short leftThumbY, short rightThumbX, short rightThumbY)
        {
            OnTrigger(new GamepadEventArgs(leftThumbX, leftThumbY, rightThumbX, rightThumbY));
        }
    }
}
