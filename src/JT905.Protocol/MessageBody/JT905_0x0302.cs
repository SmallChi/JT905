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
    /// 提问应答
    /// </summary>
    public class JT905_0x0302 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0302>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.提问应答;

        public override string Description => "提问应答";


        /// <summary>
        /// 问题ID
        /// </summary>
        public uint IssueId { get; set; }

        /// <summary>
        /// 答案ID
        /// </summary>
        public byte AnswerId { get; set; }
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            var IssueId = reader.ReadUInt32();
            writer.WriteNumber($"[{IssueId.ReadNumber()}]问题ID", IssueId);
            var AnswerId = reader.ReadByte();
            writer.WriteNumber($"[{AnswerId.ReadNumber()}]答案ID", AnswerId);
        }

        public JT905_0x0302 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0302 value = new JT905_0x0302();
            value.IssueId = reader.ReadUInt32();
            value.AnswerId = reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0302 value, IJT905Config config)
        {
            //config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer, value.Positions, config);
            writer.WriteUInt32(value.IssueId);
            writer.WriteByte(value.AnswerId);
        }
    }
}
