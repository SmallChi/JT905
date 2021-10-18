using System;
using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 波特率，定义如下：
    /// 0x00：4800；0x01：9600；
    /// 0x02：19200；0x03：38400；
    /// 0x04：57600；0x05：115200。
    /// </summary>
    public class JT905_0x8103_0x0091 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0091>, IJT905Analyze
    {
        /// <summary>
        /// 0x0091
        /// </summary>
        public override uint ParamId { get; set; } = 0x0091;
        /// <summary>
        /// 数据长度
        /// 1 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 1;
        /// <summary>
        /// GNSS 波特率，定义如下：
        /// 0x00：4800；0x01：9600；
        /// 0x02：19200；0x03：38400；
        /// 0x04：57600；0x05：115200。
        /// </summary>
        public byte ParamValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x0091 JT905_0x8103_0x0091 = new JT905_0x8103_0x0091();
            JT905_0x8103_0x0091.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0091.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0091.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ JT905_0x8103_0x0091.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0091.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0091.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0091.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0091.ParamValue.ReadNumber()}]参数值[GNSS波特率]",Math.Pow(4800, JT905_0x8103_0x0091.ParamValue));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0091 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0091 JT905_0x8103_0x0091 = new JT905_0x8103_0x0091();
            JT905_0x8103_0x0091.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0091.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0091.ParamValue = reader.ReadByte();
            return JT905_0x8103_0x0091;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0091 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
