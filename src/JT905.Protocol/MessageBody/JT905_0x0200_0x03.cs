using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Runtime.Serialization;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 海拔高度，INT16，单位为米( m)
    /// </summary>
    public class JT905_0x0200_0x03 : JT905_0x0200_BodyBase, IJT905MessagePackFormatter<JT905_0x0200_0x03>, IJT905Analyze
    {
        /// <summary>
        /// 海拔高度，INT16，单位为米( m)
        /// </summary>
        public ushort Altitude { get; set; }
        
        /// <summary>
        /// JT905_0x0200_0x02
        /// </summary>
        public override byte AttachInfoId { get; set; } = JT905Constants.JT905_0x0200_0x03;
        /// <summary>
        /// 2 byte
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0200_0x02 value = new JT905_0x0200_0x02();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.Oil = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Oil.ReadNumber()}]海拔高度，INT16，单位为米( m)", value.Oil);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0200_0x03 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0200_0x03 value = new JT905_0x0200_0x03();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.Altitude = reader.ReadUInt16();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0200_0x03 value, IJT905Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt16(value.Altitude);
        }
    }
}
