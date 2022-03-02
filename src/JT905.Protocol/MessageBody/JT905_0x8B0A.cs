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
    /// 中心确认报警
    /// </summary>
    public class JT905_0x8B0A : JT905Bodies
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.中心确认报警;

        public override string Description => "中心确认报警";


        public override bool SkipSerialization { get; set; } = true;
    }
}


                    
