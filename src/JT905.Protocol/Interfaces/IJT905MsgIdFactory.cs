using System;
using System.Collections.Generic;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// JT905消息工厂接口
    /// </summary>
    public interface IJT905MsgIdFactory:IJT905ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<ushort, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryGetValue(ushort msgId, out object instance);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT905Bodies"></typeparam>
        /// <returns></returns>
        IJT905MsgIdFactory SetMap<TJT905Bodies>() where TJT905Bodies : JT905Bodies;
    }
}
