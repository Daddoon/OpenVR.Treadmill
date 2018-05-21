using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO.Pipes;
using System.IO;
using Newtonsoft.Json;
using ViveTracker.Treadmill.NugetToUnity.Models;
using ViveTracker.Treadmill.NugetToUnity.Service;
using ViveTracker.Treadmill.NugetToUnity.Interface;

public static class PipeHelpers {

    private static AnonymousPipeClientStream _pipeReceive = null;
    private static StreamReader sr = null;

    private static AnonymousPipeClientStream _pipeSend = null;
    private static StreamWriter sw = null;

    public static void GetPipeHandlers(out string pipeIn, out string pipeOut)
    {
        pipeIn = null;
        pipeOut = null; ;

        pipeIn = InitScript.Arguments.FirstOrDefault(p => p.StartsWith("-pipeInHandle"));
        if (pipeIn == null)
            return;

        pipeIn = pipeIn.Replace("-pipeInHandle", string.Empty).TrimEnd().Trim('\n').Trim('\r');
        Debug.Log($"Received PipeIn handle: {pipeIn}");

        pipeOut = InitScript.Arguments.FirstOrDefault(p => p.StartsWith("-pipeOutHandle"));

        pipeOut = pipeOut.Replace("-pipeOutHandle", string.Empty).TrimEnd().Trim('\n').Trim('\r');
        Debug.Log($"Received PipeOut handle: {pipeOut}");

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

        GetPipeHandlers(out pipeIn, out pipeOut);

        //Actually name are inverted related to the parent

        _pipeSend = new AnonymousPipeClientStream(PipeDirection.Out, pipeIn);
        _pipeSend.ReadMode = PipeTransmissionMode.Byte;

        _pipeReceive = new AnonymousPipeClientStream(PipeDirection.In, pipeOut);
        _pipeReceive.ReadMode = PipeTransmissionMode.Byte;

        sr = new StreamReader(_pipeReceive);

        sw = new StreamWriter(_pipeSend);
        sw.AutoFlush = true;
        sw.WriteLine("OK");

        _pipeSend.WaitForPipeDrain();

        string result = sr.ReadLine();
        if (result == "OK")
            sw.WriteLine("OK");
        else
            sw.WriteLine("KO");

        //TEST TO REMOVE
        DependencyService.Get<IMessageBox>().ShowAlert("Mon alert depuis Unity");
    }

    public static void Send(MethodProxy methodCall)
    {
        sw.WriteLine(methodCall.GetAsJsonMessage());
    }

    public static void Receive()
    {
        //TODO: Add a Task.Run and execute only when dispatched,
        //as Microsoft implementation is pretty stupid and don't
        //allow non-blocking read by default
        //TODO: Add a cancelation token ?
    }
}
