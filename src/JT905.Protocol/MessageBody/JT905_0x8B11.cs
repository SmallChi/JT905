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
    /// 中心巡检设备
    /// </summary>
    public class JT905_0x8B11 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8B11>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.中心巡检设备;

        public override string Description => "中心巡检设备";

        /// <summary>
        /// 巡检设备类型代码
        /// </summary>
        public List<Enums.JT905DeviceType> TypeID { get; set; }




        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8B11 value = new JT905_0x8B11();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                //value.TypeID = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                //writer.WriteString($"[{value.TypeID.ToHexString()}]巡检设备类型代码", value.TypeID.ToHexString());
                //var TypeID = new List<Enums.JT905DeviceType>();
                while (reader.ReadCurrentRemainContentLength() > 0)
                {
                    byte deviceType = reader.ReadByte();
                    writer.WriteString($"[{deviceType.ReadNumber()}]巡检设备类型代码", ((Enums.JT905DeviceType)deviceType).ToString());
                }
            }
            else
            {
                writer.WriteString("[]巡检设备类型代码", "");
            }

        }

        public JT905_0x8B11 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8B11 value = new JT905_0x8B11();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                var TypeID = new List<Enums.JT905DeviceType>();
                while (reader.ReadCurrentRemainContentLength() > 0)
                {
                   // Enums.JT905DeviceType deviceType = (Enums.JT905DeviceType)reader.ReadByte();
                    TypeID.Add((Enums.JT905DeviceType)reader.ReadByte());
                }
                value.TypeID = TypeID;
            }


            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8B11 value, IJT905Config config)
        {
            if (value.TypeID != null && value.TypeID.Count > 0)
            {
                foreach (var item in value.TypeID)
                {
                    writer.WriteByte((byte)item);
                }
                
            }

        }
    }
}



