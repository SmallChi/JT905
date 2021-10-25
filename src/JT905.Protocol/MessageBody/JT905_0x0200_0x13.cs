using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 路段行驶时间不足/过长报警附加信息
    /// </summary>
    public class JT905_0x0200_0x13 : JT905_0x0200_BodyBase, IJT905MessagePackFormatter<JT905_0x0200_0x13>, IJT905Analyze
    {
        /// <summary>
        /// JT905_0x0200_0x13
        /// </summary>
        public override byte AttachInfoId { get; set; } = JT905Constants.JT905_0x0200_0x13;
        /// <summary>
        /// 7 byte
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 7;



        /// <summary>
        /// 路段 ID
        /// </summary>
        public uint DrivenRouteId { get; set; }

        /// <summary>
        /// 路段行驶时间
        /// 单位为秒（s)
        /// </summary>
        public ushort Time { get; set; }

        /// <summary>
        ///  结果 0：不足；1：过长
        /// </summary>
        public JT905DrivenRouteType DrivenRoute { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0200_0x13 value = new JT905_0x0200_0x13();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.DrivenRouteId = reader.ReadUInt32();
            writer.WriteNumber($"[{((byte)value.DrivenRouteId).ReadNumber()}]路段ID", value.DrivenRouteId);
            value.Time = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Time.ReadNumber()}]路段行驶时间", value.Time);
            value.DrivenRoute = (JT905DrivenRouteType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.DrivenRoute).ReadNumber()}]结果-{value.DrivenRoute.ToString()}", (byte)value.DrivenRoute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0200_0x13 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0200_0x13 value = new JT905_0x0200_0x13();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.DrivenRouteId = reader.ReadUInt32();
            value.Time = reader.ReadUInt16();
            value.DrivenRoute = (JT905DrivenRouteType)reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0200_0x13 value, IJT905Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteUInt32(value.DrivenRouteId);
            writer.WriteUInt16(value.Time);
            writer.WriteByte((byte)value.DrivenRoute);
        }
    }
}
