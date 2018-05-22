using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Interface.Base;

namespace ViveTracker.Treadmill.Common.Services
{
    public static class DependencyService
    {
        private static Dictionary<Type, object> _service = new Dictionary<Type, object>();

        public static void Register<T>(T instance) where T : class, IPipedEntity
        {
            if (instance == null)
                throw new NullReferenceException("instance is not set");

            //Must be Interface type
            var type = typeof(T);

            if (!type.IsInterface)
            {
                throw new InvalidOperationException("Generic type must be an interface");
            }

            if (!_service.ContainsKey(type))
                _service.Add(type, null);

            _service[type] = instance;
        }

        public static T Get<T>() where T : class
        {
            if (!_service.ContainsKey(typeof(T)))
                return null;

            return (T)_service[typeof(T)];
        }

        public static object Get(Type type)
        {
            if (type == null || !_service.ContainsKey(type))
                return null;

            return _service[type];
        }
    }
}
