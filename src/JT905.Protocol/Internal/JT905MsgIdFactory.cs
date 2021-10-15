using JT905.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JT905.Protocol.Internal
{
    internal class JT905MsgIdFactory: IJT905MsgIdFactory
    {
        public IDictionary<ushort, object> Map { get; }

        internal JT905MsgIdFactory()
        {
            Map = new Dictionary<ushort, object>();
            InitMap(Assembly.GetExecutingAssembly());
        }

        private void InitMap(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(w => w.BaseType == typeof(JT905Bodies)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                ushort msgId = 0;
                try
                {
                    msgId = (ushort)type.GetProperty(nameof(JT905Bodies.MsgId)).GetValue(instance);
                }
                catch (Exception ex)
                {
                    continue;
                }
                if (Map.ContainsKey(msgId))
                {
                    throw new ArgumentException($"{type.FullName} {msgId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(msgId, instance);
                }
            }
        }

        public bool TryGetValue(ushort msgId, out object instance)
        {
            return Map.TryGetValue(msgId, out instance);
        }

        public IJT905MsgIdFactory SetMap<TJT905Bodies>() where TJT905Bodies : JT905Bodies
        {
            Type type = typeof(TJT905Bodies);
            var instance = Activator.CreateInstance(type);
            var msgId = (ushort)type.GetProperty(nameof(JT905Bodies.MsgId)).GetValue(instance);
            if (Map.ContainsKey(msgId))
            {
                throw new ArgumentException($"{type.FullName} {msgId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(msgId, instance);
            }
            return this;
        }

        public void Register(Assembly externalAssembly)
        {
            InitMap(externalAssembly);
        }
    }
}
