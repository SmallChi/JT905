using JT905.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 设置终端参数消息工厂
    /// </summary>
    public interface IJT905_0x8103_Factory: IJT905ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<uint, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT905_0x8103_Body"></typeparam>
        /// <returns></returns>
        IJT905_0x8103_Factory SetMap<TJT905_0x8103_Body>() where TJT905_0x8103_Body : JT905_0x8103_BodyBase;
    }
}
