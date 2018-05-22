using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using ViveTracker.Treadmill.Common.Interface;
using ViveTracker.Treadmill.Common.Models;

public class GamepadService : PipedEntity, IGamepadService
{
    public Task<bool> CloseGamepadCanal()
    {
        throw new System.NotImplementedException();
    }

    public Task<string> GetConnectedDevices()
    {
        return Task.FromResult("Lorem ipsum!!");
    }

    public Stream GetGamepadReader()
    {
        throw new System.NotImplementedException();
    }

    public Stream GetGamepadWriter()
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> OpenGamepadCanal()
    {
        throw new System.NotImplementedException();
    }
}
