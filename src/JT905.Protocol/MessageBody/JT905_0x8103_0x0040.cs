using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 监控平台电话号码
    /// </summary>
    public class JT905_0x8103_0x0040 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0040>, IJT905Analyze
    {
        /// <summary>
        /// 0x0040
        /// </summary>
        public override uint ParamId { get; set; } = 0x0040;
        /// <summary>
        /// 数据长度
        /// n byte
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 监控平台电话号码
        /// </summary>
        public string ParamValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x0040 JT905_0x8103_0x0040 = new JT905_0x8103_0x0040();
            JT905_0x8103_0x0040.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0040.ParamLength = reader.ReadByte();
            var paramValue = reader.ReadVirtualArray(JT905_0x8103_0x0040.ParamLength);
            JT905_0x8103_0x0040.ParamValue = reader.ReadString(JT905_0x8103_0x0040.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0040.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0040.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0040.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0040.ParamLength);
            writer.WriteString($"[{paramValue.ToArray().ToHexString()}]参数值[监控平台电话号码]", JT905_0x8103_0x0040.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0040 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0040 JT905_0x8103_0x0040 = new JT905_0x8103_0x0040();
            JT905_0x8103_0x0040.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0040.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0040.ParamValue = reader.ReadString(JT905_0x8103_0x0040.ParamLength);
            return JT905_0x8103_0x0040;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0040 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}


