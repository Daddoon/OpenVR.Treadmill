using System;
using System.Collections.Generic;
using System.Reflection;
using ViveTracker.Treadmill.Common.Serialization;
using ViveTracker.Treadmill.Common.Services;
using System.Linq;

namespace ViveTracker.Treadmill.Common.Interop
{
    public static class ContextBridge
    {
        private static object GetDefault(Type type)
        {
            if (type == typeof(void))
            {
                return null;
            }

            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        public static void SendReturnValue(MethodProxy methodResult)
        {

        }

        public static MethodProxy Receive(string methodProxyJson)
        {
            object defaultValue = default(object);
            MethodProxy methodProxy = null;

            try
            {
                methodProxy = BridgeSerializer.Deserialize<MethodProxy>(methodProxyJson);

                Type iface = methodProxy.InterfaceType.ResolvedType();
                object concreteService = DependencyService.Get(iface);

                MethodInfo baseMethod = MethodProxyHelper.GetClassMethodInfo(concreteService.GetType(), iface, methodProxy.MethodIndex);

                //In case of failure, getting Default Return Type
                defaultValue = GetDefault(baseMethod.ReturnType);

                if (methodProxy.GenericTypes != null && methodProxy.GenericTypes.Length > 0)
                {
                    Type[] genericTypes = methodProxy.GenericTypes.Select(p => p.ResolvedType()).ToArray();

                    methodProxy.ReturnValue = baseMethod.MakeGenericMethod(genericTypes).Invoke(concreteService, methodProxy.Parameters);
                    methodProxy.TaskSuccess = true;
                }
                else
                {
                    methodProxy.ReturnValue = baseMethod.Invoke(concreteService, methodProxy.Parameters);
                    methodProxy.TaskSuccess = true;
                }
            }
            catch (Exception)
            {
                methodProxy.ReturnValue = defaultValue;
                methodProxy.TaskSuccess = false;
            }

            return methodProxy;
        }
    }
}
