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
    /// 摄像头图像上传
    /// </summary>
    public class JT905_0x0800 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0800>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.摄像头图像上传;

        public override string Description => "摄像头图像上传";
        
        /// <summary>
        /// 上传原因
        /// </summary>
        public ushort UploadReason { get; set; }
        /// <summary>
        /// 图像
        /// </summary>
        public uint Image { get; set; }
        /// <summary>
        /// 摄像头ID
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 位置图像数据大小
        /// </summary>
        public uint ImageDataSize { get; set; }
        /// <summary>
        /// 起始地址
        /// </summary>
        public uint InitialAddress { get; set; }
        /// <summary>
        /// 位置图像数据包
        /// </summary>
        public byte[] PositionImagePacket { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0800 value = new JT905_0x0800();
            value.UploadReason = reader.ReadUInt16();        
            writer.WriteNumber($"[{value.UploadReason.ReadNumber()}]上传原因", value.UploadReason);
            value.Image = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.Image.ReadNumber()}]图像", value.Image);
            value.ChannelId = reader.ReadByte();        
            writer.WriteNumber($"[{value.ChannelId.ReadNumber()}]摄像头ID", value.ChannelId);      
            value.ImageDataSize = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.ImageDataSize.ReadNumber()}]位置图像数据大小", value.ImageDataSize);
            value.InitialAddress = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.InitialAddress.ReadNumber()}]起始地址", value.InitialAddress);
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.PositionImagePacket = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                writer.WriteString($"[{value.PositionImagePacket.ToHexString()}]位置图像数据包", value.PositionImagePacket.ToHexString());
            }
            else
            {
                writer.WriteString("[]位置图像数据包", "");
            }

        }

        public JT905_0x0800 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0800 value = new JT905_0x0800();
            value.UploadReason = reader.ReadUInt16();  
            value.Image = reader.ReadUInt32();    
            value.ChannelId = reader.ReadByte(); 
            value.ImageDataSize = reader.ReadUInt32();    
            value.InitialAddress = reader.ReadUInt32();    
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.PositionImagePacket = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            }
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0800 value, IJT905Config config)
        {
            writer.WriteUInt16(value.UploadReason);
            writer.WriteUInt32(value.Image);
            writer.WriteByte(value.ChannelId);
            writer.WriteUInt32(value.ImageDataSize);
            writer.WriteUInt32(value.InitialAddress);
            if(value.PositionImagePacket != null && value.PositionImagePacket.Length > 0)
            {
                writer.WriteArray(value.PositionImagePacket);
            }
            
        }
    }
}


                    
