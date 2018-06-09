using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using UnityEngine;
using ViveTracker.Treadmill.Common.Events;
using ViveTracker.Treadmill.Common.Helper;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Models;
using ViveTracker.Treadmill.Common.Services;

public class GamepadService : PipedEntity, IGamepadService
{
    private AnonymousPipeClientStream _clientSend { get; set; }
    private StreamWriter _gpw = null;

    public event GamepadEventHandler Trigger;

    /// <summary>
    /// This should be invoked from the parent only, or if somehow the client handle is known by another source
    /// </summary>
    /// <param name="clientHandle"></param>
    /// <returns></returns>
    public Task<bool> AnswerOpenGamepadCanal(string clientHandle)
    {
        try
        {
            DependencyService.Get<IMessageBox>().ShowAlert("Client Handle: " + clientHandle);

            _clientSend = new AnonymousPipeClientStream(PipeDirection.Out, clientHandle);
            _clientSend.ReadMode = PipeTransmissionMode.Byte;

            _gpw = new StreamWriter(_clientSend);
            _gpw.AutoFlush = true;

            Task.Delay(1000).GetAwaiter().GetResult();

            //_gpw.WriteLine("OK");

            DependencyService.Get<IMessageBox>().ShowAlert("Client is connected: " + _clientSend.IsConnected);

            return Task.FromResult(true);
        }
        catch (System.Exception ex)
        {
            DependencyService.Get<IMessageBox>().ShowAlert("Gamepad.exe: " + ex.Message + " " + ex.StackTrace);
            return Task.FromResult(false);
        }
    }

    public Task<bool> CloseGamepadCanal()
    {
        try
        {
            if (_clientSend != null)
            {
                _clientSend.Dispose();
                _gpw.Dispose();

                _clientSend = null;
                _gpw = null;
            }
        }
        catch (System.Exception)
        {
        }

        return Task.FromResult(true);
    }

    public Task<string> GetConnectedDevices()
    {
        return Task.FromResult("Lorem ipsum!!");
    }

    /// <summary>
    /// Should be called from parent
    /// </summary>
    /// <returns></returns>
    public Task<bool> OpenGamepadCanal()
    {
        throw new System.NotImplementedException();
    }

    public StreamReader GetGamepadReader()
    {
        throw new System.NotImplementedException();
    }

    public StreamWriter GetGamepadWriter()
    {
        return _gpw;
    }

    public void SendTrigger(short leftThumbX, short leftThumbY, short rightThumbX, short rightThumbY)
    {
        var sw = GetGamepadWriter();
        if (sw != null)
        {
            GamepadEventStruct es = new GamepadEventStruct()
            {
                leftThumbX = leftThumbX,
                leftThumbY = leftThumbY,
                rightThumbX = rightThumbX,
                rightThumbY = rightThumbY
            };
            var result = ByteHelper.GetBytes(es);
            sw.Write(result);
        }
    }

    public void StartGamepadListener()
    {
        throw new System.NotImplementedException();
    }

    public void StopGamepadListener()
    {
        throw new System.NotImplementedException();
    }
}
