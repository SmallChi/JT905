using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据采集频率，单位为秒，默认为 1。
    /// </summary>
    public class JT905_0x8103_0x0093 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0093>, IJT905Analyze
    {
        /// <summary>
        /// 0x0093
        /// </summary>
        public override uint ParamId { get; set; } = 0x0093;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// GNSS 模块详细定位数据采集频率，单位为秒，默认为 1。
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
            JT905_0x8103_0x0093 JT905_0x8103_0x0093 = new JT905_0x8103_0x0093();
            JT905_0x8103_0x0093.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0093.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0093.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ JT905_0x8103_0x0093.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0093.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0093.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0093.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0093.ParamValue.ReadNumber()}]参数值[GNSS模块详细定位数据采集频率s]", JT905_0x8103_0x0093.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0093 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0093 JT905_0x8103_0x0093 = new JT905_0x8103_0x0093();
            JT905_0x8103_0x0093.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0093.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0093.ParamValue = reader.ReadUInt32();
            return JT905_0x8103_0x0093;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0093 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
