using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 禁行路段行驶报警附加信息
    /// </summary>
    public class JT905_0x0200_0x14 : JT905_0x0200_BodyBase, IJT905MessagePackFormatter<JT905_0x0200_0x14>, IJT905Analyze
    {
        /// <summary>
        /// JT905_0x0200_0x14
        /// </summary>
        public override byte AttachInfoId { get; set; } = JT905Constants.JT905_0x0200_0x14;
        /// <summary>
        /// 4 byte
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 4;

        /// <summary>
        /// 路段 ID
        /// </summary>
        public uint DrivenRouteId { get; set; }

        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0200_0x14 value = new JT905_0x0200_0x14();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.DrivenRouteId = reader.ReadUInt32();
            writer.WriteNumber($"[{((byte)value.DrivenRouteId).ReadNumber()}]路段ID", value.DrivenRouteId);
                       
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0200_0x14 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0200_0x14 value = new JT905_0x0200_0x14();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.DrivenRouteId = reader.ReadUInt32();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0200_0x14 value, IJT905Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.DrivenRouteId);
        }
    }
}
