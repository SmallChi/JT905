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
    /// 位置信息查询应答
    /// </summary>
    public class JT905_0x0201 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0201>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.位置信息查询应答;

        public override string Description => "位置信息查询应答";

        /// <summary>
        /// 应答流水号汇报
        /// 对应的位置查询消息的流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }

        /// <summary>
        /// 位置信息汇报(0x0200) 消息体
        /// </summary>
        public JT905_0x0200 Position { get; set; }

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0201 value = new JT905_0x0201();
            value.ReplyMsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.ReplyMsgNum.ReadNumber()}]应答流水号汇报", value.ReplyMsgNum);
            writer.WriteStartObject("位置基本信息");
            config.GetAnalyze<JT905_0x0200>().Analyze(ref reader, writer, config);
            writer.WriteEndObject();
        }

        public JT905_0x0201 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0201 value = new JT905_0x0201();
            value.ReplyMsgNum = reader.ReadUInt16();
            value.Position = config.GetMessagePackFormatter<JT905_0x0200>().Deserialize(ref reader,config);
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0201 value, IJT905Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer, value.Position, config);
        }
    }
}
