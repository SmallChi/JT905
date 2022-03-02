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
    /// 驾驶员取消订单
    /// </summary>
    public class JT905_0x0B08 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0B08>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.驾驶员取消订单;

        public override string Description => "驾驶员取消订单";
        
        /// <summary>
        /// 业务ID 对应订单业务下发(0x8B00) 消息中的业务ID
        /// </summary>
        public uint BusinessId { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public JT905CancellationReasons CancellationReasons { get; set; } = JT905CancellationReasons.其他;

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0B08 value = new JT905_0x0B08();
            value.BusinessId = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.BusinessId.ReadNumber()}]业务ID 对应订单业务下发(0x8B00) 消息中的业务ID", value.BusinessId);
            value.CancellationReasons = (JT905CancellationReasons)reader.ReadByte();        
            writer.WriteString($"[{((byte)value.CancellationReasons).ReadNumber()}]取消原因", value.CancellationReasons.ToString());      

        }

        public JT905_0x0B08 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B08 value = new JT905_0x0B08();
            value.BusinessId = reader.ReadUInt32();    
            value.CancellationReasons = (JT905CancellationReasons)reader.ReadByte(); 
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B08 value, IJT905Config config)
        {
            writer.WriteUInt32(value.BusinessId);
            writer.WriteByte((byte)value.CancellationReasons);
            
        }
    }
}


                    
