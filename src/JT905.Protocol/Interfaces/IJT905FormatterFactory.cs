using JT905.Protocol.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 序列化工厂
    /// </summary>
    public interface IJT905FormatterFactory: IJT905ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<Guid,object> FormatterDict { get;}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIJT905Formatter"></typeparam>
        /// <returns></returns>
        IJT905FormatterFactory SetMap<TIJT905Formatter>()
                    where TIJT905Formatter : IJT905Formatter;
    }
}
