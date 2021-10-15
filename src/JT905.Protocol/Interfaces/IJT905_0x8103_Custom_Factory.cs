using JT905.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 自定义设置终端参数消息工厂
    /// </summary>
    public interface IJT905_0x8103_Custom_Factory : IJT905ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<uint, object> Map { get;}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT905_0x8103_CustomBody"></typeparam>
        /// <returns></returns>

        IJT905_0x8103_Custom_Factory SetMap<TJT905_0x8103_CustomBody>() where TJT905_0x8103_CustomBody : JT905_0x8103_CustomBodyBase;
    }
}
