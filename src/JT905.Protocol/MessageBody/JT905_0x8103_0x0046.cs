using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 每次最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
    /// </summary>
    public class JT905_0x8103_0x0046 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0046>, IJT905Analyze
    {
        /// <summary>
        /// 0x0046
        /// </summary>
        public override uint ParamId { get; set; } = 0x0046;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 每次最长通话时间，单位为秒（s），0 为不允许通话，0xFFFFFFFF 为不限制
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x0046 JT905_0x8103_0x0046 = new JT905_0x8103_0x0046();
            JT905_0x8103_0x0046.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0046.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0046.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ JT905_0x8103_0x0046.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0046.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0046.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0046.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0046.ParamValue.ReadNumber()}]参数值[每次最长通话时间s]", JT905_0x8103_0x0046.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0046 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0046 JT905_0x8103_0x0046 = new JT905_0x8103_0x0046();
            JT905_0x8103_0x0046.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0046.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0046.ParamValue = reader.ReadUInt32();
            return JT905_0x8103_0x0046;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0046 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
