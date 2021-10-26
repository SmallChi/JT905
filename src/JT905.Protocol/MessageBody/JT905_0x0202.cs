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
    /// 位置跟踪信息汇报
    /// </summary>
    public class JT905_0x0202 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0202>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.位置跟踪信息汇报;

        public override string Description => "位置跟踪信息汇报";


        /// <summary>
        /// 位置信息汇报(0x0200) 消息体
        /// </summary>
        public JT905_0x0200 Position { get; set; }

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0202 value = new JT905_0x0202();
            writer.WriteStartObject("位置基本信息");
            config.GetAnalyze<JT905_0x0200>().Analyze(ref reader, writer, config);
            writer.WriteEndObject();
        }

        public JT905_0x0202 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0202 value = new JT905_0x0202();
            value.Position = config.GetMessagePackFormatter<JT905_0x0200>().Deserialize(ref reader,config);
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0202 value, IJT905Config config)
        {
            config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer, value.Position, config);
        }
    }
}
