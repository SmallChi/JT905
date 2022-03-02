using JT905.Protocol.Enums;
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
    /// 存储图像/音视频上传命令
    /// </summary>
    public class JT905_0x8803 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8803>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.存储图像_音视频上传命令;

        public override string Description => "存储图像/音视频上传命令";
        
        /// <summary>
        /// 类型
        /// </summary>
        public JT905MultimediaType MultimediaType { get; set; }
        /// <summary>
        /// 文件 ID
        /// </summary>
        public uint FileId { get; set; }
        /// <summary>
        /// 起始位置 本包数据在整个位置图像数据中的偏移量， 第一包数据为 0
        /// </summary>
        public uint InitialPosition { get; set; } = 0;

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8803 value = new JT905_0x8803();
            value.MultimediaType = (JT905MultimediaType)reader.ReadByte();        
            writer.WriteString($"[{((byte)value.MultimediaType).ReadNumber()}]类型", value.MultimediaType.ToString());      
            value.FileId = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.FileId.ReadNumber()}]文件 ID", value.FileId);
            value.InitialPosition = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.InitialPosition.ReadNumber()}]起始位置 本包数据在整个位置图像数据中的偏移量， 第一包数据为 0", value.InitialPosition);

        }

        public JT905_0x8803 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8803 value = new JT905_0x8803();
            value.MultimediaType = (JT905MultimediaType)reader.ReadByte(); 
            value.FileId = reader.ReadUInt32();    
            value.InitialPosition = reader.ReadUInt32();    
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8803 value, IJT905Config config)
        {
            writer.WriteByte((byte)value.MultimediaType);
            writer.WriteUInt32(value.FileId);
            writer.WriteUInt32(value.InitialPosition);
            
        }
    }
}


                    
