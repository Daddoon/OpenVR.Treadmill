using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO.Pipes;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using ViveTracker.Treadmill.Common.Models;
using ViveTracker.Treadmill.Common.Services;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Interop;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Helper;

public static class PipeHelpers {

    private static AnonymousPipeClientStream _pipeReceive = null;
    private static StreamReader sr = null;

    private static AnonymousPipeClientStream _pipeSend = null;
    private static StreamWriter sw = null;

    private static AnonymousPipeClientStream _pipeGamepad = null;
    private static StreamWriter gpsw = null;

    public static void GetPipeHandlers(out string pipeIn, out string pipeOut, out string pipeGamepad)
    {
        pipeIn = null;
        pipeOut = null;
        pipeGamepad = null;

        pipeIn = InitScript.Arguments.FirstOrDefault(p => p.StartsWith("-pipeInHandle"));
        if (pipeIn == null)
            return;

        pipeIn = pipeIn.Replace("-pipeInHandle", string.Empty).TrimEnd().Trim('\n').Trim('\r');

        pipeOut = InitScript.Arguments.FirstOrDefault(p => p.StartsWith("-pipeOutHandle"));

        pipeOut = pipeOut.Replace("-pipeOutHandle", string.Empty).TrimEnd().Trim('\n').Trim('\r');

        pipeGamepad = InitScript.Arguments.FirstOrDefault(p => p.StartsWith("-pipeGamepadHandle"));

        pipeGamepad = pipeOut.Replace("-pipeGamepadHandle", string.Empty).TrimEnd().Trim('\n').Trim('\r');

        return;

    }

    public static void OpenPipes()
    {
        //Faking it is open
        if (!InitScript.CanSupportPipe)
            return;

        if (_pipeSend != null)
            return;

        string pipeIn = null;
        string pipeOut = null;
        string pipeGamepad = null;

        GetPipeHandlers(out pipeIn, out pipeOut, out pipeGamepad);

        Debug.Log("PipeIn: " + pipeIn);
        Debug.Log("pipeOut: " + pipeOut);
        Debug.Log("pipeGamepad: " + pipeGamepad);

        //Actually name are inverted related to the parent

        _pipeSend = new AnonymousPipeClientStream(PipeDirection.Out, pipeIn);
        _pipeSend.ReadMode = PipeTransmissionMode.Byte;

        _pipeReceive = new AnonymousPipeClientStream(PipeDirection.In, pipeOut);
        _pipeReceive.ReadMode = PipeTransmissionMode.Byte;

        _pipeGamepad = new AnonymousPipeClientStream(PipeDirection.Out, pipeGamepad);
        _pipeGamepad.ReadMode = PipeTransmissionMode.Byte;

        sr = new StreamReader(_pipeReceive);

        sw = new StreamWriter(_pipeSend);
        sw.AutoFlush = true;

        gpsw = new StreamWriter(_pipeGamepad);
        gpsw.AutoFlush = true;

        sw.WriteLine("OK");

        _pipeSend.WaitForPipeDrain();

        string result = sr.ReadLine();
        if (result == "OK")
        {
            sw.WriteLine("OK");
            _pipeSend.WaitForPipeDrain();
            gpsw.WriteLine("OK");
        }
        else
            sw.WriteLine("KO");

        ServiceHelper.ConnectProcessToServices(sw);
        ReceiveHandler();
    }

    private static Task _receiveTask = null;

    private static Task ReceiveHandler()
    {
        if (_receiveTask != null)
            return _receiveTask;

        _receiveTask = Task.Run(() =>
        {
            while (true)
            {
                var content = sr.ReadLine();
                if (string.IsNullOrEmpty(content))
                    break;

                PipeHelper.HandleMethodCalls(sw, content);
            }
        });

        return _receiveTask;
    }
}
