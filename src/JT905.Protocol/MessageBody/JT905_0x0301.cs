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
    /// 事件报告
    /// </summary>
    public class JT905_0x0301 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0301>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.事件报告;

        public override string Description => "事件报告";


        /// <summary>
        /// 事件ID
        /// </summary>
        public byte EventId { get; set; }

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            var EventId = reader.ReadByte();
            writer.WriteNumber($"[{EventId.ReadNumber()}]事件ID", EventId);
        }

        public JT905_0x0301 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0301 value = new JT905_0x0301();
            value.EventId = reader.ReadByte();           
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0301 value, IJT905Config config)
        {
            //config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer, value.Positions, config);
            writer.WriteByte(value.EventId);
        }
    }
}
