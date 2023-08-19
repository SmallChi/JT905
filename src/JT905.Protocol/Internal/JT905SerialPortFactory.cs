using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JT905.Protocol.Interfaces;
using JT905.Protocol.SerialPort;

namespace JT905.Protocol.Internal
{
    internal class JT905SerialPortFactory : IJT905SerialPortFactory
    {
        IDictionary<ushort, object> UpMap { get; } = new Dictionary<ushort, object>();
        IDictionary<ushort, object> DownMap { get; } = new Dictionary<ushort, object>();
        IDictionary<ushort, object> IJT905SerialPortFactory.Map => UpMap;

        public JT905SerialPortFactory()
        {
            Register(Assembly.GetExecutingAssembly());
        }

        public void Register(Assembly externalAssembly)
        {
            foreach (var item in externalAssembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.BaseType?.GenericTypeArguments?.Contains(x) == true && x.BaseType?.GetGenericTypeDefinition() == typeof(Body<>)))
            {
                var instance = Activator.CreateInstance(item) as IBody;
                if (instance.Type == IBody.Types.Up)
                    UpMap.Add(instance.MessageId, instance);
                else if (instance.Type == IBody.Types.Down)
                    DownMap.Add(instance.MessageId, instance);
            }
        }

        public IJT905SerialPortFactory SetMap<T>() where T : IBody<T>
        {
            return this;
        }

        public bool TryGetValue(ushort id, IBody.Types type, out IBody value)
        {
            object instance = default;
            value = default;
            var result = type switch
            {
                IBody.Types.Up => UpMap.TryGetValue(id, out instance),
                IBody.Types.Down => DownMap.TryGetValue(id, out instance),
                _ => false
            };
            if (instance != null)
                value = instance as IBody;
            return result;
        }
    }
}