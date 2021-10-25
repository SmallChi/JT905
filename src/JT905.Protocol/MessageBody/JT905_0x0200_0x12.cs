using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 进出区域/路线报警附加信息
    /// </summary>
    public class JT905_0x0200_0x12 : JT905_0x0200_BodyBase, IJT905MessagePackFormatter<JT905_0x0200_0x12>, IJT905Analyze
    {
        /// <summary>
        /// 位置类型
        /// 1：圆形区域；
        /// 2：矩形区域；
        /// 3：多边形区域；
        /// 4：路段
        /// </summary>
        public JT905PositionType JT905PositionType { get; set; }

        /// <summary>
        /// 区域或路段 ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 方向 
        /// 0：进
        /// 1：出
        /// </summary>
        public JT905DirectionType Direction { get; set; }
        /// <summary>
        /// JT905_0x0200_0x12
        /// </summary>
        public override byte AttachInfoId { get; set; } = JT905Constants.JT905_0x0200_0x12;
        /// <summary>
        /// 6 byte
        /// </summary>
        public override byte AttachInfoLength { get; set; } = 6;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0200_0x12 value = new JT905_0x0200_0x12();
            value.AttachInfoId = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoId.ReadNumber()}]附加信息Id", value.AttachInfoId);
            value.AttachInfoLength = reader.ReadByte();
            writer.WriteNumber($"[{value.AttachInfoLength.ReadNumber()}]附加信息长度", value.AttachInfoLength);
            value.JT905PositionType = (JT905PositionType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.JT905PositionType).ReadNumber()}]位置类型-{value.JT905PositionType.ToString()}", (byte)value.JT905PositionType);
            value.AreaId = reader.ReadInt32();
            writer.WriteNumber($"[{value.AreaId.ReadNumber()}]区域或路段ID", value.AreaId);
            value.Direction = (JT905DirectionType)reader.ReadByte();
            writer.WriteNumber($"[{((byte)value.Direction).ReadNumber()}]方向-{value.Direction.ToString()}", (byte)value.Direction);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0200_0x12 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0200_0x12 value = new JT905_0x0200_0x12();
            value.AttachInfoId = reader.ReadByte();
            value.AttachInfoLength = reader.ReadByte();
            value.JT905PositionType = (JT905PositionType)reader.ReadByte();
            value.AreaId = reader.ReadInt32();
            value.Direction = (JT905DirectionType)reader.ReadByte();
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0200_0x12 value, IJT905Config config)
        {
            writer.WriteByte(value.AttachInfoId);
            writer.WriteByte(value.AttachInfoLength);
            writer.WriteByte((byte)value.JT905PositionType);
            writer.WriteInt32(value.AreaId);
            writer.WriteByte((byte)value.Direction);
        }
    }
}
