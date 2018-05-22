using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViveTracker.Treadmill.Common.Serialization;

namespace ViveTracker.Treadmill.Common.Models
{
    public class TypeProxy
    {
        public TypeProxy()
        {

        }

        public TypeProxy(Type type)
        {
            SerializedData = BridgeSerializer.Serialize(type);
        }

        public string SerializedData { get; set; }

        /// <summary>
        /// Get resolved type from given AssemblyName & TypeName
        /// </summary>
        /// <returns></returns>
        public Type ResolvedType()
        {
            try
            {
                if (string.IsNullOrEmpty(SerializedData))
                    return null;

                var type = BridgeSerializer.Deserialize<Type>(SerializedData);
                return type;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
