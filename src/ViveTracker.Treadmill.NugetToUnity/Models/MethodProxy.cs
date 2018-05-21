using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.NugetToUnity.Serialization;
using ViveTracker.Treadmill.NugetToUnity.Service;

namespace ViveTracker.Treadmill.NugetToUnity.Models
{
    public class MethodProxy
    {
        //The implementation is Weak for retrieving method at the moment (no generic etc.)
        //But we don't need more at the moment

        public TypeProxy InterfaceType = null;
        public string MethodName = null;
        public object[] Parameters { get; set; }

        public MethodProxy()
        {

        }

        public static MethodProxy GetFromJson(string json)
        {
            return BridgeSerializer.Deserialize<MethodProxy>(json);
        }

        public MethodProxy(Type interfaceName, string MethodName, object[] args)
        {
            InterfaceType = new TypeProxy(interfaceName);
            this.MethodName = MethodName;
            Parameters = args;
        }

        public string GetAsJsonMessage()
        {
            return BridgeSerializer.Serialize(this);
        }

        public bool Invoke()
        {
            var interfaceType = InterfaceType.ResolvedType();
            if (interfaceType == null)
                return false;

            var instance = DependencyService.Get(interfaceType);
            if (instance == null)
                return false;

            var method = instance.GetType().GetMethod(MethodName);
            if (method == null)
                return false;

            try
            {
                method.Invoke(instance, Parameters);
            }
            catch (Exception)
            {
                return false;
            }
            

            return true;
        }
    }
}
