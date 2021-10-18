using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据输出频率，定义如下：
    /// 0x00：500ms；0x01：1000ms（默认值）；
    /// 0x02：2000ms；0x03：3000ms；
    /// 0x04：4000ms。
    /// </summary>
    public class JT905_0x8103_0x0092 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0092>, IJT905Analyze
    {
        /// <summary>
        /// 0x0092
        /// </summary>
        public override uint ParamId { get; set; } = 0x0092;
        /// <summary>
        /// 数据长度
        /// 1 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 1;
        /// <summary>
        /// GNSS 模块详细定位数据输出频率，定义如下：
        /// 0x00：500ms；0x01：1000ms（默认值）；
        /// 0x02：2000ms；0x03：3000ms；
        /// 0x04：4000ms。
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
            JT905_0x8103_0x0092 JT905_0x8103_0x0092 = new JT905_0x8103_0x0092();
            JT905_0x8103_0x0092.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0092.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0092.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ JT905_0x8103_0x0092.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0092.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0092.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0092.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0092.ParamValue.ReadNumber()}]GNSS模块详细定位数据输出频率ms", JT905_0x8103_0x0092.ParamValue==0?500: JT905_0x8103_0x0092.ParamValue*1000);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0092 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0092 JT905_0x8103_0x0092 = new JT905_0x8103_0x0092();
            JT905_0x8103_0x0092.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0092.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0092.ParamValue = reader.ReadByte();
            return JT905_0x8103_0x0092;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0092 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
