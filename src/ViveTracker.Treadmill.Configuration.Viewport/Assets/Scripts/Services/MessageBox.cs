using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Models;
using ViveTracker.Treadmill.Common.Services;

public class MessageBox : PipedEntity, IMessageBox
{
    public void ShowAlert(string message)
    {
        MethodDispatcher.CallVoidMethod(GetPipe()[0], MethodBase.GetCurrentMethod(), message);
    }
}
