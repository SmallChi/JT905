using JT905.Protocol.Interfaces;
using JT905.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JT905.Protocol.Internal
{
    class JT905_0x0200_Custom_Factory : IJT905_0x0200_Custom_Factory
    {
        public IDictionary<byte, object> Map { get; }

        public IDictionary<ushort, object> Map2 { get; }

        public IDictionary<ushort, object> Map3 { get; }

        public IDictionary<byte, object> Map4 { get; }

        public JT905_0x0200_Custom_Factory()
        {
            Map = new Dictionary<byte, object>();
            Map2 = new Dictionary<ushort, object>();
            Map3 = new Dictionary<ushort, object>();
            Map4 = new Dictionary<byte, object>();
        }

        public void Register(Assembly externalAssembly)
        {
            var types = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT905_0x0200_CustomBodyBase)).ToList();
            foreach(var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var attachid = (byte)type.GetProperty(nameof(JT905_0x0200_CustomBodyBase.AttachInfoId)).GetValue(instance);
                if (Map.ContainsKey(attachid))
                {
                    throw new ArgumentException($"{type.FullName} {attachid} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(attachid, instance);
                }
            }

            var types2 = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT905_0x0200_CustomBodyBase2)).ToList();
            foreach (var type in types2)
            {
                var instance = Activator.CreateInstance(type);
                var attachid = (ushort)type.GetProperty(nameof(JT905_0x0200_CustomBodyBase2.AttachInfoId)).GetValue(instance);
                if (Map2.ContainsKey(attachid))
                {
                    throw new ArgumentException($"{type.FullName} {attachid} An element with the same key already exists.");
                }
                else
                {
                    Map2.Add(attachid, instance);
                }
            }

            var types3 = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT905_0x0200_CustomBodyBase3)).ToList();
            foreach (var type in types3)
            {
                var instance = Activator.CreateInstance(type);
                var attachid = (ushort)type.GetProperty(nameof(JT905_0x0200_CustomBodyBase3.AttachInfoId)).GetValue(instance);
                if (Map3.ContainsKey(attachid))
                {
                    throw new ArgumentException($"{type.FullName} {attachid} An element with the same key already exists.");
                }
                else
                {
                    Map3.Add(attachid, instance);
                }
            }

            var types4 = externalAssembly.GetTypes().Where(w => w.BaseType == typeof(JT905_0x0200_CustomBodyBase4)).ToList();
            foreach (var type in types4)
            {
                var instance = Activator.CreateInstance(type);
                var attachid = (byte)type.GetProperty(nameof(JT905_0x0200_CustomBodyBase4.AttachInfoId)).GetValue(instance);
                if (Map4.ContainsKey(attachid))
                {
                    throw new ArgumentException($"{type.FullName} {attachid} An element with the same key already exists.");
                }
                else
                {
                    Map4.Add(attachid, instance);
                }
            }
        }

        public IJT905_0x0200_Custom_Factory SetMap<TJT905_0x0200_CustomBody>() where TJT905_0x0200_CustomBody : JT905_0x0200_CustomBodyBase
        {
            Type type = typeof(TJT905_0x0200_CustomBody);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (byte)type.GetProperty(nameof(JT905_0x0200_CustomBodyBase.AttachInfoId)).GetValue(instance);
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

        public IJT905_0x0200_Custom_Factory SetMap2<TJT905_0x0200_CustomBody2>() where TJT905_0x0200_CustomBody2 : JT905_0x0200_CustomBodyBase2
        {
            Type type = typeof(TJT905_0x0200_CustomBody2);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (ushort)type.GetProperty(nameof(JT905_0x0200_CustomBodyBase2.AttachInfoId)).GetValue(instance);
            if (Map2.ContainsKey(attachInfoId))
            {
                throw new ArgumentException($"{type.FullName} {attachInfoId} An element with the same key already exists.");
            }
            else
            {
                Map2.Add(attachInfoId, instance);
            }
            return this;
        }

        public IJT905_0x0200_Custom_Factory SetMap3<TJT905_0x0200_CustomBody3>() where TJT905_0x0200_CustomBody3 : JT905_0x0200_CustomBodyBase3
        {
            Type type = typeof(TJT905_0x0200_CustomBody3);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (ushort)type.GetProperty(nameof(JT905_0x0200_CustomBodyBase3.AttachInfoId)).GetValue(instance);
            if (Map3.ContainsKey(attachInfoId))
            {
                throw new ArgumentException($"{type.FullName} {attachInfoId} An element with the same key already exists.");
            }
            else
            {
                Map3.Add(attachInfoId, instance);
            }
            return this;
        }

        public IJT905_0x0200_Custom_Factory SetMap4<TJT905_0x0200_CustomBody4>() where TJT905_0x0200_CustomBody4 : JT905_0x0200_CustomBodyBase4
        {
            Type type = typeof(TJT905_0x0200_CustomBody4);
            var instance = Activator.CreateInstance(type);
            var attachInfoId = (byte)type.GetProperty(nameof(JT905_0x0200_CustomBodyBase4.AttachInfoId)).GetValue(instance);
            if (Map4.ContainsKey(attachInfoId))
            {
                throw new ArgumentException($"{type.FullName} {attachInfoId} An element with the same key already exists.");
            }
            else
            {
                Map4.Add(attachInfoId, instance);
            }
            return this;
        }
    }
}
