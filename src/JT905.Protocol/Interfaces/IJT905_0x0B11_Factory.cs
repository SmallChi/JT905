using JT905.Protocol.MessageBody;
using System.Collections.Generic;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 设备巡检应答消息工厂
    /// </summary>
    public interface IJT905_0x0B11_Factory : IJT905ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<byte, object> Map { get; }

        IJT905_0x0B11_Factory SetMap<IJT905_0x0B11_TLV>() where IJT905_0x0B11_TLV : JT905_0x0B11_TLV;
    }
}