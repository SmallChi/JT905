using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// ISU心跳
    /// </summary>
    public class JT905_0x0002 : JT905Bodies
    {
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public override bool SkipSerialization { get; set; } = true;
        public override ushort MsgId => Enums.JT905MsgId.ISU心跳.ToUInt16Value();

        public override string Description => "ISU心跳";
        
    }
}
