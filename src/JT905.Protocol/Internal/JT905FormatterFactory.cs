using JT905.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JT905.Protocol.Internal
{
    internal class JT905FormatterFactory : IJT905FormatterFactory
    {
        public IDictionary<Guid, object> FormatterDict { get; }

        public JT905FormatterFactory()
        {
            FormatterDict = new Dictionary<Guid, object>();
            Init(Assembly.GetExecutingAssembly());
        }

        private void Init(Assembly assembly)
        {
           foreach(var type in assembly.GetTypes().Where(w=>w.GetInterfaces().Contains(typeof(IJT905Formatter))))
           {
                var implTypes = type.GetInterfaces();
                if(implTypes!=null && implTypes .Length>1)
                {
                    var firstType = implTypes.FirstOrDefault(f=>f.Name== typeof(IJT905MessagePackFormatter<>).Name);
                    var genericImplType = firstType.GetGenericArguments().FirstOrDefault();
                    if (genericImplType != null)
                    {
                        if (!FormatterDict.ContainsKey(genericImplType.GUID))
                        {
                            FormatterDict.Add(genericImplType.GUID, Activator.CreateInstance(genericImplType));
                        }
                    }
                }
            }
        }

        public void Register(Assembly externalAssembly)
        {
            Init(externalAssembly);
        }

        public IJT905FormatterFactory SetMap<TIJT905Formatter>() where TIJT905Formatter : IJT905Formatter
        {
            Type type = typeof(TIJT905Formatter);
            if (!FormatterDict.ContainsKey(type.GUID))
            {
                FormatterDict.Add(type.GUID, Activator.CreateInstance(type));
            }
            return this;
        }
    }
}
