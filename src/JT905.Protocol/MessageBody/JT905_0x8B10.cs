using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 外围设备指令下行透传
    /// </summary>
    public class JT905_0x8B10 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8B10>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.外围设备指令下行透传;

        public override string Description => "外围设备指令下行透传";

        /// <summary>
        /// 设备类型代码
        /// </summary>
        public Enums.JT905DeviceType TypeID { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public ushort DataType { get; set; }
        /// <summary>
        /// 数据包
        /// </summary>
        public byte[] DataPacket { get; set; }




        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8B10 value = new JT905_0x8B10();
            value.TypeID = (Enums.JT905DeviceType)reader.ReadByte();
            writer.WriteString($"[{((byte)value.TypeID).ReadNumber()}]设备类型代码", value.TypeID.ToString());
            value.DataType = reader.ReadUInt16();
            writer.WriteNumber($"[{value.DataType.ReadBinary().ToString()}]数据类型", value.DataType);
            var alarmFlagBits = Convert.ToString(value.DataType, 2).PadLeft(16, '0').AsSpan();
            writer.WriteStartObject("数据类型对象");
            writer.WriteString("[bit4~15]保留:", alarmFlagBits.Slice(0, 11).ToString());
            writer.WriteString($"[bit3]数据加密:{ alarmFlagBits[12]}", alarmFlagBits[12]=='0'?"明文":"密文");
            switch (alarmFlagBits.Slice(13, 3).ToString())
            {
                case "000":
                    writer.WriteString($"[bit0~2]压缩算法:{alarmFlagBits.Slice(13, 3).ToString()}", "数据无压缩");
                    break;
                case "001":
                    writer.WriteString($"[bit0~2]压缩算法:{alarmFlagBits.Slice(13, 3).ToString()}", "gz压缩");
                    break;
                default:
                    writer.WriteString($"[bit0~2]压缩算法:{alarmFlagBits.Slice(13, 3).ToString()}", "其他");
                    break;
            }
            //writer.WriteString($"[bit0~2]压缩算法:{alarmFlagBits.Slice(13, 3).ToString()}", alarmFlagBits.Slice(13, 3).ToString());
            writer.WriteEndObject();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.DataPacket = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                writer.WriteString($"[{value.DataPacket.ToHexString()}]数据包", value.DataPacket.ToHexString());
            }
            else
            {
                writer.WriteString("[]数据包", "");
            }

        }

        public JT905_0x8B10 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8B10 value = new JT905_0x8B10();
            value.TypeID = (Enums.JT905DeviceType)reader.ReadByte();
            value.DataType = reader.ReadUInt16();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.DataPacket = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            }


            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8B10 value, IJT905Config config)
        {
            writer.WriteByte((byte)value.TypeID);
            writer.WriteUInt16(value.DataType);
            if (value.DataPacket != null && value.DataPacket.Length > 0)
            {
                writer.WriteArray(value.DataPacket);
            }

        }
    }
}



