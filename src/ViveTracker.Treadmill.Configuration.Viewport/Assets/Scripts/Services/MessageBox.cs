using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViveTracker.Treadmill.NugetToUnity.Interface;
using ViveTracker.Treadmill.NugetToUnity.Models;

public class MessageBox : IMessageBox
{
    public void ShowAlert(string message)
    {
        PipeHelpers.Send(new MethodProxy(typeof(IMessageBox), nameof(ShowAlert), new object[] { message }));
    }
}
