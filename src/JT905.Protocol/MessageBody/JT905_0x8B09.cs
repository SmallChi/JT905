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
    /// 中心取消订单
    /// </summary>
    public class JT905_0x8B09 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8B09>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.中心取消订单;

        public override string Description => "中心取消订单";
        
        /// <summary>
        /// 业务ID 对应订单业务下发(0x8B00) 消息中的业务ID
        /// </summary>
        public uint BusinessId { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8B09 value = new JT905_0x8B09();
            value.BusinessId = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.BusinessId.ReadNumber()}]业务ID 对应订单业务下发(0x8B00) 消息中的业务ID", value.BusinessId);

        }

        public JT905_0x8B09 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8B09 value = new JT905_0x8B09();
            value.BusinessId = reader.ReadUInt32();    
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8B09 value, IJT905Config config)
        {
            writer.WriteUInt32(value.BusinessId);
            
        }
    }
}


                    
