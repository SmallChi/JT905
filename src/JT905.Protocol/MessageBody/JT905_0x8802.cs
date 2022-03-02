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
    /// 存储图像检索
    /// </summary>
    public class JT905_0x8802 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8802>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.存储图像检索;

        public override string Description => "存储图像检索";
        
        /// <summary>
        /// 摄像头ID
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 拍照原因
        /// </summary>
        public JT905PhotoReason PhotoReason { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8802 value = new JT905_0x8802();
            value.ChannelId = reader.ReadByte();        
            writer.WriteNumber($"[{value.ChannelId.ReadNumber()}]摄像头ID", value.ChannelId);      
            value.PhotoReason = (JT905PhotoReason)reader.ReadByte();        
            writer.WriteString($"[{((byte)value.PhotoReason).ReadNumber()}]拍照原因", value.PhotoReason.ToString());      
            value.StartTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.StartTime:yyMMddHHmmss}]起始时间", value.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));        
            value.EndTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.EndTime:yyMMddHHmmss}]结束时间", value.EndTime.ToString("yyyy-MM-dd HH:mm:ss"));        

        }

        public JT905_0x8802 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8802 value = new JT905_0x8802();
            value.ChannelId = reader.ReadByte(); 
            value.PhotoReason = (JT905PhotoReason)reader.ReadByte(); 
            value.StartTime = reader.ReadDateTime6();        
            value.EndTime = reader.ReadDateTime6();        
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8802 value, IJT905Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteByte((byte)value.PhotoReason);
            writer.WriteDateTime6(value.StartTime);
            writer.WriteDateTime6(value.EndTime);
        }
    }
}


                    
