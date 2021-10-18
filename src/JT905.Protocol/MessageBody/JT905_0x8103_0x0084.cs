using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 车牌颜色，按照 JT/T415-2006 的 5.4.12
    /// </summary>
    public class JT905_0x8103_0x0084 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0084>,  IJT905Analyze
    {
        /// <summary>
        /// 0x0084
        /// </summary>
        public override uint ParamId { get; set; } = 0x0084;
        /// <summary>
        /// 数据长度
        /// n byte
        /// </summary>
        public override byte ParamLength { get; set; } = 1;
        /// <summary>
        /// 车牌颜色，按照 JT/T415-2006 的 5.4.12
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
            JT905_0x8103_0x0084 JT905_0x8103_0x0084 = new JT905_0x8103_0x0084();
            JT905_0x8103_0x0084.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0084.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0084.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ JT905_0x8103_0x0084.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0084.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0084.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0084.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0084.ParamValue.ReadNumber()}]参数值[车牌颜色,按照 JT/T415-2006 的 5.4.12]", JT905_0x8103_0x0084.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0084 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0084 JT905_0x8103_0x0084 = new JT905_0x8103_0x0084();
            JT905_0x8103_0x0084.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0084.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0084.ParamValue = reader.ReadByte();
            return JT905_0x8103_0x0084;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0084 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
