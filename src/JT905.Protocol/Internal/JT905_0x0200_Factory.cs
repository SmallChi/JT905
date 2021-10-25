using JT905.Protocol.Interfaces;
using JT905.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT905.Protocol.Internal
{
    class JT905_0x0200_Factory : IJT905_0x0200_Factory
    {
        public IDictionary<byte, object> Map { get; set; }

        public JT905_0x0200_Factory()
        {
            Map = new Dictionary<byte, object>();
            InitMap(Assembly.GetExecutingAssembly());
            //Map.Add(JT905Constants.JT905_0x0200_0x01, new JT905_0x0200_0x01());
            //Map.Add(JT905Constants.JT905_0x0200_0x02, new JT905_0x0200_0x02());
            ////todo:
            //Map.Add(JT905Constants.JT905_0x0200_0x03, new JT905_0x0200_0x03());
            //Map.Add(JT905Constants.JT905_0x0200_0x04, new JT905_0x0200_0x04());
            //Map.Add(JT905Constants.JT905_0x0200_0x05, new JT905_0x0200_0x05());
            //Map.Add(JT905Constants.JT905_0x0200_0x06, new JT905_0x0200_0x06());
            //Map.Add(JT905Constants.JT905_0x0200_0x07, new JT905_0x0200_0x07());
            //Map.Add(JT905Constants.JT905_0x0200_0x11, new JT905_0x0200_0x11());
            //Map.Add(JT905Constants.JT905_0x0200_0x12, new JT905_0x0200_0x12());
            //Map.Add(JT905Constants.JT905_0x0200_0x13, new JT905_0x0200_0x13());
            //Map.Add(JT905Constants.JT905_0x0200_0x25, new JT905_0x0200_0x25());
            //Map.Add(JT905Constants.JT905_0x0200_0x2A, new JT905_0x0200_0x2A());
            //Map.Add(JT905Constants.JT905_0x0200_0x2B, new JT905_0x0200_0x2B());
            //Map.Add(JT905Constants.JT905_0x0200_0x30, new JT905_0x0200_0x30());
            //Map.Add(JT905Constants.JT905_0x0200_0x31, new JT905_0x0200_0x31());
        }

        public IJT905_0x0200_Factory SetMap<TJT905_0x0200_Body>() where TJT905_0x0200_Body : JT905_0x0200_BodyBase
        {
            Type type = typeof(TJT905_0x0200_Body);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (byte)type.GetProperty(nameof(JT905_0x0200_BodyBase.AttachInfoId)).GetValue(instance);
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

        public void Register(Assembly externalAssembly)
        {
            InitMap(externalAssembly);
        }

        private void InitMap(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT905_0x0200_BodyBase)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var attachid = (byte)type.GetProperty(nameof(JT905_0x0200_BodyBase.AttachInfoId)).GetValue(instance);
                if (Map.ContainsKey(attachid))
                {
                    throw new ArgumentException($"{type.FullName} {attachid} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(attachid, instance);
                }
            }
        }
    }
}
