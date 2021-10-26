using JT905.Protocol;
using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 文本信息下发
    /// </summary>
    public class JT905_0x8300 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8300>, IJT905Analyze
    {
        /// <summary>
        /// 0x8300
        /// </summary>
        public override ushort MsgId { get; } = (ushort)Enums.JT905MsgId.文本信息下发;
        /// <summary>
        /// 文本信息下发
        /// </summary>
        public override string Description => "文本信息下发";
        /// <summary>
        /// 文本信息标志位含义见表 28
        /// </summary>

        public byte TextFlag { get; set; }

        /// <summary>
        /// 文本信息
        /// 最长为 499byte
        /// </summary>

        public string TextInfo { get; set; }

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8300 value = new JT905_0x8300();
            try
            {
                var TextFlag = reader.ReadByte();

                string textInfoHex = reader.ReadVirtualArraryEndChar0();
                var TextInfo = reader.ReadStringEndChar0();
                writer.WriteNumber($"[{TextFlag.ReadBinary().ToString()}]标志", TextFlag);
                ReadOnlySpan<char> textFlagBits = TextFlag.ReadBinary();
                writer.WriteStartObject("文本信息标志位");
                writer.WriteString("[bit5~7]保留", textFlagBits.Slice(0, 3).ToString());
                writer.WriteString($"[bit4]广告屏显示", $"{textFlagBits[3]}");
                writer.WriteString($"[bit3]语音合成播读", $"{textFlagBits[4]}");
                writer.WriteString($"[bit2]显示装置显示", $"{textFlagBits[5]}");
                writer.WriteString($"[bit1]保留", $"{textFlagBits[6]}");
                writer.WriteString($"[bit0]紧急", $"{textFlagBits[7]}");
                writer.WriteEndObject();
                writer.WriteString($"[{textInfoHex}]文本信息", TextInfo);

            }
            catch { }
        }

        public JT905_0x8300 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8300 value = new JT905_0x8300();
            value.TextFlag = reader.ReadByte();
            value.TextInfo = reader.ReadStringEndChar0();
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8300 value, IJT905Config config)
        {
            writer.WriteByte((byte)value.TextFlag);
            writer.WriteStringEndChar0(value.TextInfo);
        }
    }
}
