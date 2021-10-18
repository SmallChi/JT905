using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 定位模式，定义如下：
    /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
    /// bit1，0：禁用北斗定位， 1：启用北斗定位；
    /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
    /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
    /// </summary>
    public class JT905_0x8103_0x0090 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0090>, IJT905Analyze
    {
        /// <summary>
        /// 0x0090
        /// </summary>
        public override uint ParamId { get; set; } = 0x0090;
        /// <summary>
        /// 数据长度
        /// 1 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 1;
        /// <summary>
        /// GNSS 定位模式，定义如下：
        /// bit0，0：禁用 GPS 定位， 1：启用 GPS 定位；
        /// bit1，0：禁用北斗定位， 1：启用北斗定位；
        /// bit2，0：禁用 GLONASS 定位， 1：启用 GLONASS 定位；
        /// bit3，0：禁用 Galileo 定位， 1：启用 Galileo 定位。
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
            JT905_0x8103_0x0090 JT905_0x8103_0x0090 = new JT905_0x8103_0x0090();
            JT905_0x8103_0x0090.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0090.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0090.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ JT905_0x8103_0x0090.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0090.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0090.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0090.ParamLength);
            writer.WriteStartArray($"[{ JT905_0x8103_0x0090.ParamValue.ReadNumber()}]参数值[GNSS定位模式]");
            writer.WriteStringValue((JT905_0x8103_0x0090.ParamValue & 01) > 0 ? "启用GPS定位" : "禁用GPS定位");
            writer.WriteStringValue((JT905_0x8103_0x0090.ParamValue & 02) > 0 ? "启用北斗定位" : "禁用北斗定位");
            writer.WriteStringValue((JT905_0x8103_0x0090.ParamValue & 04) > 0 ? "启用GLONASS定位" : "禁用GLONASS定位");
            writer.WriteStringValue((JT905_0x8103_0x0090.ParamValue & 08) > 0 ? "启用Galileo定位" : "禁用Galileo定位");
            writer.WriteEndArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0090 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0090 JT905_0x8103_0x0090 = new JT905_0x8103_0x0090();
            JT905_0x8103_0x0090.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0090.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0090.ParamValue = reader.ReadByte();
            return JT905_0x8103_0x0090;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0090 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
