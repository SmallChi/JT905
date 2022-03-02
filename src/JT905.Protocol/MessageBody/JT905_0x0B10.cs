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
    /// 外围设备指令上行透传
    /// </summary>
    public class JT905_0x0B10 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0B10>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.外围设备指令上行透传;

        public override string Description => "外围设备指令上行透传";
        
        /// <summary>
        /// 设备类型代码
        /// </summary>
        public Enums.JT905DeviceType TypeID { get; set; }
        /// <summary>
        /// 厂商标识
        /// </summary>
        public byte VendorID { get; set; }
        /// <summary>
        /// 命令类型
        /// </summary>
        public ushort CommandType { get; set; }
        /// <summary>
        /// 数据包
        /// </summary>
        public byte[] DataPacket  { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0B10 value = new JT905_0x0B10();
            value.TypeID = (Enums.JT905DeviceType)reader.ReadByte();        
            writer.WriteString($"[{((byte)value.TypeID).ReadNumber()}]设备类型代码", value.TypeID.ToString());      
            value.VendorID = reader.ReadByte();        
            writer.WriteNumber($"[{value.VendorID.ReadNumber()}]厂商标识", value.VendorID);      
            value.CommandType = reader.ReadUInt16();        
            writer.WriteNumber($"[{value.CommandType.ReadNumber()}]命令类型", value.CommandType);
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.DataPacket  = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                writer.WriteString($"[{value.DataPacket .ToHexString()}]数据包", value.DataPacket .ToHexString());
            }
            else
            {
                writer.WriteString("[]数据包", "");
            }

        }

        public JT905_0x0B10 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B10 value = new JT905_0x0B10();
            value.TypeID = (Enums.JT905DeviceType)reader.ReadByte(); 
            value.VendorID = reader.ReadByte(); 
            value.CommandType = reader.ReadUInt16();  
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.DataPacket  = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            }
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B10 value, IJT905Config config)
        {
            writer.WriteByte((byte)value.TypeID);
            writer.WriteByte(value.VendorID);
            writer.WriteUInt16(value.CommandType);
            if(value.DataPacket  != null && value.DataPacket .Length > 0)
            {
                writer.WriteArray(value.DataPacket );
            }
            
        }
    }
}


                    
