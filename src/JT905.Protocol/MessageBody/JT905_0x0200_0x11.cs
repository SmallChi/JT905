using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 超速报警附加信息
    /// </summary>
    public class JT905_0x0200_0x11 : JT905_0x0200_BodyBase, IJT905MessagePackFormatter<JT905_0x0200_0x11>, IJT905Analyze
    {
        /// <summary>
        /// 超速报警附加信息
        /// 0：无特定位置；
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT905PositionType JT905PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// 若位置类型为 0，无该字段
        /// </summary>
        public uint AreaId { get; set; }
        /// <summary>
        /// JT905_0x0200_0x11
        /// </summary>
        public override byte AttachInfoId { get; set; } = JT905Constants.JT905_0x0200_0x11;
        /// <summary>
        /// 1或5 byte
        /// </summary>
        public override byte AttachInfoLength
        {
            get
            {
                if (JT905PositionType != JT905PositionType.无特定位置)
                {
                    return 5;
                }
                else
                {
                    return 1;
                }
            }
            set { }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0200_0x11 value = new JT905_0x0200_0x11();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.JT905PositionType = (JT905PositionType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.JT905PositionType).ReadNumber()}]超速报警附加信息-{value.JT905PositionType.ToString()}", (byte)value.JT905PositionType);
            if (value.JT905PositionType != JT905PositionType.无特定位置)
            {
                value.AreaId = reader.ReadUInt32();
                writer.WriteNumber($"[{value.AreaId.ReadNumber()}]区域或路段ID", value.AreaId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0200_0x11 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0200_0x11 value = new JT905_0x0200_0x11();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.JT905PositionType = (JT905PositionType)reader.ReadByte();
            if (value.JT905PositionType != JT905PositionType.无特定位置)
            {
                value.AreaId = reader.ReadUInt32();
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0200_0x11 value, IJT905Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte((byte)value.JT905PositionType);
            if (value.JT905PositionType != JT905PositionType.无特定位置)
            {
                writer.WriteUInt32(value.AreaId);
            }
        }
    }
}
