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
    /// 摄像头立即拍摄命令
    /// </summary>
    public class JT905_0x8801 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8801>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.摄像头立即拍摄命令;

        public override string Description => "摄像头立即拍摄命令";
        
        /// <summary>
        /// 摄像头ID
        /// </summary>
        public byte ChannelId { get; set; }
        /// <summary>
        /// 拍摄命令 
        /// 0 表示停止拍摄；0xFFFF 表示录像；
        /// 其它表示拍照张数
        /// </summary>
        public ushort ShootingCommand { get; set; }
        /// <summary>
        /// 拍照间隔/录像时间 
        /// 秒，0 表示按最小间隔拍照或一直录像
        /// </summary>
        public ushort VideoTime { get; set; }
        /// <summary>
        /// 保存标志 1：保存；0：实时上传
        /// </summary>
        public byte SaveFlag { get; set; }
        /// <summary>
        /// 分辨率
        /// 0：320 × 240；
        /// 1：640 × 480；
        /// 2：800 × 600； 
        /// 其他:保留
        /// </summary>
        public byte Resolution { get; set; }
        /// <summary>
        /// 图像/视频质量
        /// 1-10，1 代表质量损失最小，10 表示压缩比最大
        /// </summary>
        public byte VideoQuality { get; set; }
        /// <summary>
        /// 亮度
        /// 0-255
        /// </summary>
        public byte Lighting { get; set; }
        /// <summary>
        /// 对比度
        /// 0-127
        /// </summary>
        public byte Contrast { get; set; }
        /// <summary>
        /// 饱和度
        /// 0-127
        /// </summary>
        public byte Saturability { get; set; }
        /// <summary>
        /// 色度
        /// 0-127
        /// </summary>
        public byte Chroma { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8801 value = new JT905_0x8801();
            value.ChannelId = reader.ReadByte();        
            writer.WriteNumber($"[{value.ChannelId.ReadNumber()}]摄像头ID", value.ChannelId);      
            value.ShootingCommand = reader.ReadUInt16();        
            writer.WriteNumber($"[{value.ShootingCommand.ReadNumber()}]拍摄命令 0 表示停止拍摄；0xFFFF 表示录像；其它表示拍照张数", value.ShootingCommand);
            value.VideoTime = reader.ReadUInt16();        
            writer.WriteNumber($"[{value.VideoTime.ReadNumber()}]拍照间隔/录像时间 秒，0 表示按最小间隔拍照或一直录像", value.VideoTime);
            value.SaveFlag = reader.ReadByte();        
            writer.WriteNumber($"[{value.SaveFlag.ReadNumber()}]保存标志 1：保存；0：实时上传", value.SaveFlag);      
            value.Resolution = reader.ReadByte();        
            writer.WriteNumber($"[{value.Resolution.ReadNumber()}]分辨率 0：320 × 240；1：640 × 480；2：800 × 600； 其他:保留", value.Resolution);      
            value.VideoQuality = reader.ReadByte();        
            writer.WriteNumber($"[{value.VideoQuality.ReadNumber()}]图像/视频质量 1-10，1 代表质量损失最小，10 表示压缩比最大", value.VideoQuality);      
            value.Lighting = reader.ReadByte();        
            writer.WriteNumber($"[{value.Lighting.ReadNumber()}]亮度", value.Lighting);      
            value.Contrast = reader.ReadByte();        
            writer.WriteNumber($"[{value.Contrast.ReadNumber()}]对比度", value.Contrast);      
            value.Saturability = reader.ReadByte();        
            writer.WriteNumber($"[{value.Saturability.ReadNumber()}]饱和度", value.Saturability);      
            value.Chroma = reader.ReadByte();        
            writer.WriteNumber($"[{value.Chroma.ReadNumber()}]色度", value.Chroma);      

        }

        public JT905_0x8801 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8801 value = new JT905_0x8801();
            value.ChannelId = reader.ReadByte(); 
            value.ShootingCommand = reader.ReadUInt16();  
            value.VideoTime = reader.ReadUInt16();  
            value.SaveFlag = reader.ReadByte(); 
            value.Resolution = reader.ReadByte(); 
            value.VideoQuality = reader.ReadByte(); 
            value.Lighting = reader.ReadByte(); 
            value.Contrast = reader.ReadByte(); 
            value.Saturability = reader.ReadByte(); 
            value.Chroma = reader.ReadByte(); 
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8801 value, IJT905Config config)
        {
            writer.WriteByte(value.ChannelId);
            writer.WriteUInt16(value.ShootingCommand);
            writer.WriteUInt16(value.VideoTime);
            writer.WriteByte(value.SaveFlag);
            writer.WriteByte(value.Resolution);
            writer.WriteByte(value.VideoQuality);
            writer.WriteByte(value.Lighting);
            writer.WriteByte(value.Contrast);
            writer.WriteByte(value.Saturability);
            writer.WriteByte(value.Chroma);
            
        }
    }
}


                    
