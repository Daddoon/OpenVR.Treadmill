using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ViveTracker.Treadmill.Common.Interop;
using ViveTracker.Treadmill.Common.Attributes;
using ViveTracker.Treadmill.Common.Serialization;
using System.IO;

namespace ViveTracker.Treadmill.Common.Services
{
    public static class MethodDispatcher
    {
        /// <summary>
        /// Fake type just to return void value wrapped around a simple Task type
        /// </summary>
        internal class IgnoredType
        {

        }

        #region VOID CALL

        public static Task CallVoidMethod(IEnumerable<StreamWriter> pipe, MethodBase method)
        {
            return InternalCallMethod<IgnoredType>(pipe, method, null, null);
        }

        public static Task CallVoidMethod(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<IgnoredType>(pipe, method, null, args);
        }

        #region SYNTACTIC SUGAR HELPER

        //Just a Generic argument signature for easier signature call

        public static Task CallVoidMethod<TGenericArg>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<IgnoredType>(
                pipe,
                method,
                new Type[] { typeof(TGenericArg) }
                , args);
        }

        public static Task CallVoidMethod<TGenericArg1, TGenericArg2>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<IgnoredType>(
                pipe,
                method,
                new Type[] { typeof(TGenericArg1), typeof(TGenericArg2) }
                , args);
        }

        public static Task CallVoidMethod<TGenericArg1, TGenericArg2, TGenericArg3>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<IgnoredType>(
                pipe,
                method,
                new Type[] { typeof(TGenericArg1), typeof(TGenericArg2), typeof(TGenericArg3) }
                , args);
        }

        public static Task CallVoidMethod<TGenericArg1, TGenericArg2, TGenericArg3, TGenericArg4>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<IgnoredType>(
                pipe,
                method,
                new Type[] { typeof(TGenericArg1), typeof(TGenericArg2), typeof(TGenericArg4) }
                , args);
        }

        #endregion

        public static Task CallVoidMethod(IEnumerable<StreamWriter> pipe, MethodBase method, Type[] genericParameters, params object[] args)
        {
            return InternalCallMethod<IgnoredType>(pipe, method, genericParameters, args);
        }

        #endregion


        #region WITH RETURN VALUE

        public static Task<TReturnType> CallMethod<TReturnType>(IEnumerable<StreamWriter> pipe, MethodBase method)
        {
            return InternalCallMethod<TReturnType>(pipe, method, null, null);
        }

        public static Task<TReturnType> CallMethod<TReturnType>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<TReturnType>(pipe, method, null, args);
        }

        #region SYNTACTIC SUGAR HELPER

        //Just a Generic argument signature for easier signature call

        public static Task<TReturnType> CallMethod<TReturnType, TGenericArg>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<TReturnType>(
                pipe,
                method,
                new Type[] { typeof(TGenericArg) }
                , args);
        }

        public static Task<TReturnType> CallMethod<TReturnType, TGenericArg1, TGenericArg2>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<TReturnType>(
                pipe,
                method,
                new Type[] { typeof(TGenericArg1), typeof(TGenericArg2) }
                , args);
        }

        public static Task<TReturnType> CallMethod<TReturnType, TGenericArg1, TGenericArg2, TGenericArg3>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<TReturnType>(
                pipe,
                method,
                new Type[] { typeof(TGenericArg1), typeof(TGenericArg2), typeof(TGenericArg3) }
                , args);
        }

        public static Task<TReturnType> CallMethod<TReturnType, TGenericArg1, TGenericArg2, TGenericArg3, TGenericArg4>(IEnumerable<StreamWriter> pipe, MethodBase method, params object[] args)
        {
            return InternalCallMethod<TReturnType>(
                pipe,
                method,
                new Type[] { typeof(TGenericArg1), typeof(TGenericArg2), typeof(TGenericArg4) }
                , args);
        }

        #endregion

        public static Task<TReturnType> CallMethod<TReturnType>(IEnumerable<StreamWriter> pipe, MethodBase method, Type[] genericParameters, params object[] args)
        {
            return InternalCallMethod<TReturnType>(pipe, method, genericParameters, args);
        }

        private static Task<TReturnType> InternalCallMethod<TReturnType>(IEnumerable<StreamWriter> pipe, MethodBase methodBase, Type[] genericParameters, object[] args)
        {
            MethodInfo method = (MethodInfo)methodBase;

            if (genericParameters == null)
            {
                genericParameters = new Type[0];
            }

            if (args == null)
            {
                args = new object[0];
            }

            #region Invokation

            MethodProxy methodProxy = new MethodProxy();

            var iface = methodBase.DeclaringType.GetInterfaces().FirstOrDefault(p => p.GetCustomAttribute<ProxyInterfaceAttribute>() != null);
            if (iface == null)
            {
                throw new ArgumentException("Given class method does not contain any possible ProxyInterface. Be sure to add [ProxyInterface] attribute on top of your interface definition");
            }

            //As we are using a DispatchProxy, the targetMethod should be the interface MethodInfo (and not a class MethodInfo)
            int index = MethodProxyHelper.GetInterfaceMethodIndex(methodBase.DeclaringType, method, iface);

            if (index == -1)
                return default(Task<TReturnType>);

            methodProxy.MethodIndex = index;
            methodProxy.GenericTypes = genericParameters.Select(p => new TypeProxy(p)).ToArray();

            //On this case, the DeclaringType should be the Interface type
            methodProxy.InterfaceType = new TypeProxy(iface);

            //We will let the serializer do the thing for RunTime values
            methodProxy.Parameters = args;

            var jsonMethodProxy = BridgeSerializer.Serialize(methodProxy);


            #endregion

            foreach (var currentPipe in pipe)
            {
                currentPipe.WriteLine(jsonMethodProxy);
            }

            return null;
        }

        #endregion
    }
}
