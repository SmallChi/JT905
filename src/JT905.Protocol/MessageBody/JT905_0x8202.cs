using JT905.Protocol;
using JT905.Protocol.Enums;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 位置跟踪控制
    /// </summary>
    public class JT905_0x8202 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8202>, IJT905Analyze
    {
        /// <summary>
        /// 0x8201
        /// </summary>
        public override ushort MsgId { get; } = (ushort)Enums.JT905MsgId.位置跟踪控制;
        /// <summary>
        /// 位置跟踪控制
        /// </summary>
        public override string Description => "位置跟踪控制";
        /// <summary>
        /// 属性
        /// 对后面两个字段进行标示： 
        /// 0x00：按时间间隔、持续时间
        /// 0x11：按距离间隔、持续距离
        /// 0x01：按时间间隔、持续距离
        /// 0x10：按距离间隔、持续时间
        /// 0xFF：停止当前跟踪(ISU 忽略后面字段)
        /// </summary>

        public JT905ControlType ControlType { get; set; }

        /// <summary>
        /// 时间间隔或距离间隔
        /// 时间单位为秒( s)，距离单位为米( m)
        /// </summary>

        public ushort Interval { get; set; } = 0;
        /// <summary>
        /// 持续时间或持续距离
        /// 时间单位为秒( s)，距离单位为米( m)
        /// </summary>

        public uint DurationOrDistance { get; set; } = 0;

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {

            try
            {
                var ControlType = reader.ReadByte();
                string interval="", durationOrDistance = "";

                JT905ControlType controlType = (JT905ControlType)ControlType;
                switch (controlType)
                {
                    case JT905ControlType.按时间间隔_持续时间:
                        interval = "时间间隔(s)";
                        durationOrDistance = "持续时间(s)";
                        break;
                    case JT905ControlType.按时间间隔_持续距离:
                        interval = "时间间隔(s)";
                        durationOrDistance = "持续距离(m)";
                        break;
                    case JT905ControlType.按距离间隔_持续距离:
                        interval = "距离间隔(m)";
                        durationOrDistance = "持续距离(m)";
                        break;
                    case JT905ControlType.按距离间隔_持续时间:
                        interval = "距离间隔(m)";
                        durationOrDistance = "持续时间(s)";
                        break;                    
                }
                writer.WriteString($"[{ControlType}]属性", ((JT905ControlType)ControlType).ToString());
                var Interval = reader.ReadUInt16();
                writer.WriteNumber($"[{Interval}]{interval}", Interval);
                var DurationOrDistance = reader.ReadUInt32();
                writer.WriteNumber($"[{DurationOrDistance}]{durationOrDistance}", DurationOrDistance);

            }
            catch { }
        }

        public JT905_0x8202 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8202 value = new JT905_0x8202();
            value.ControlType = (JT905ControlType)reader.ReadByte();
            value.Interval = reader.ReadUInt16();
            value.DurationOrDistance = reader.ReadUInt32();
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8202 value, IJT905Config config)
        {
            writer.WriteByte((byte)value.ControlType);
            writer.WriteUInt16(value.Interval);
            writer.WriteUInt32(value.DurationOrDistance);
        }
    }
}
