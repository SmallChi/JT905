using JT905.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT905.Protocol.Interfaces
{
    class JT905_0x8103_Custom_Factory : IJT905_0x8103_Custom_Factory
    {
        public JT905_0x8103_Custom_Factory()
        {
            Map = new Dictionary<uint, object>();
        }

        public IDictionary<uint, object> Map { get; }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT905_0x8103_CustomBodyBase)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var paramId = (uint)type.GetProperty(nameof(JT905_0x8103_CustomBodyBase.ParamId)).GetValue(instance);
                if (Map.ContainsKey(paramId))
                {
                    throw new ArgumentException($"{type.FullName} {paramId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(paramId, instance);
                }
            }
        }

        public IJT905_0x8103_Custom_Factory SetMap<TJT905_0x8103_CustomBody>() where TJT905_0x8103_CustomBody : JT905_0x8103_CustomBodyBase
        {
            Type type = typeof(TJT905_0x8103_CustomBody);
            var instance = Activator.CreateInstance(type);
            var paramId = (uint)type.GetProperty(nameof(JT905_0x8103_CustomBodyBase.ParamId)).GetValue(instance);
            if (Map.ContainsKey(paramId))
            {
                throw new ArgumentException($"{type.FullName} {paramId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(paramId, instance);
            }
            return this;
        }
    }
}
