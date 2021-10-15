using JT905.Protocol.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 0x0200附加信息工厂
    /// </summary>
   public interface IJT905_0x0200_Factory: IJT905ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<byte, object> Map { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT905_0x0200_Body"></typeparam>
        /// <returns></returns>
        IJT905_0x0200_Factory SetMap<TJT905_0x0200_Body>() where TJT905_0x0200_Body : JT905_0x0200_BodyBase;
    }
}
