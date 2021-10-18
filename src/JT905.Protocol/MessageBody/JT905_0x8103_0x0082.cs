using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 车辆所在的市域 ID
    /// </summary>
    public class JT905_0x8103_0x0082 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0082>, IJT905Analyze
    {
        /// <summary>
        /// 0x0082
        /// </summary>
        public override uint ParamId { get; set; } = 0x0082;
        /// <summary>
        /// 数据长度
        /// 2 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 2;
        /// <summary>
        /// 车辆所在的市域 ID
        /// </summary>
        public ushort ParamValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x0082 JT905_0x8103_0x0082 = new JT905_0x8103_0x0082();
            JT905_0x8103_0x0082.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0082.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0082.ParamValue = reader.ReadUInt16();
            writer.WriteNumber($"[{ JT905_0x8103_0x0082.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0082.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0082.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0082.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0082.ParamValue.ReadNumber()}]参数值[车辆所在的市域ID]", JT905_0x8103_0x0082.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0082 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0082 JT905_0x8103_0x0082 = new JT905_0x8103_0x0082();
            JT905_0x8103_0x0082.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0082.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0082.ParamValue = reader.ReadUInt16();
            return JT905_0x8103_0x0082;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0082 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt16(value.ParamValue);
        }
    }
}
