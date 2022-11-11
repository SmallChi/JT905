using JT905.Protocol.Interfaces;
using JT905.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT905.Protocol.Internal
{
    class JT905_0x0B11_Factory : IJT905_0x0B11_Factory
    {
        public IDictionary<byte, object> Map { get; set; }
        public JT905_0x0B11_Factory() {
            Map = new Dictionary<byte, object>();
            InitMap(Assembly.GetExecutingAssembly());
        }

        private void InitMap(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(w => w.BaseType == typeof(JT905_0x0B11_TLV)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var attachInfoId = (byte)type.GetProperty(nameof(JT905_0x0B11_TLV.DeviceType)).GetValue(instance);
                if (Map.ContainsKey(attachInfoId))
                {
                    throw new ArgumentException($"{type.FullName} {attachInfoId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(attachInfoId, instance);
                }

            }
        }

        public void Register(Assembly externalAssembly)
        {
            InitMap(externalAssembly);
        }

        public IJT905_0x0B11_Factory SetMap<IJT905_0x0B11_TLV>() where IJT905_0x0B11_TLV : JT905_0x0B11_TLV
        {
            Type type = typeof(IJT905_0x0B11_TLV);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (byte)type.GetProperty(nameof(JT905_0x0B11_TLV.DeviceType)).GetValue(instance);
            if (Map.ContainsKey(attachInfoId))
            {
                throw new ArgumentException($"{type.FullName} {attachInfoId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(attachInfoId, instance);
            }
            return this;
        }
    }
}
