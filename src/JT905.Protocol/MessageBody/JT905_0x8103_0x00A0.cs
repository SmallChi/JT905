using System.Text.Json;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// ISU录音模式(0x01：全程录音；0x02：翻牌录音)
    /// 0x8103_=0x00A0
    /// </summary>
    public class JT905_0x8103_0x00A0 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x00A0>, IJT905Analyze
    {
        /// <summary>
        ///Byte
        ///System.Byte
        /// 0x00A0
        /// </summary>
        public override ushort ParamId { get; set; } = JT905Constants.JT905_0x8103_0x00A0;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 1;
        
        /// <summary>
        /// ISU录音模式(0x01：全程录音；0x02：翻牌录音)
        /// </summary>
        public byte ParamValue { get; set; }
        
        /// <summary>
        /// 解析数据
        /// ISU录音模式(0x01：全程录音；0x02：翻牌录音)
        /// 0x8103_0x00A0        
        /// </summary>
        /// <param name="reader">JT905消息读取器</param>
        /// <param name="writer">消息写入</param>
        /// <param name="config">JT905接口配置</param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x00A0 JT905_0x8103_0x00A0 = new JT905_0x8103_0x00A0();
            JT905_0x8103_0x00A0.ParamId = reader.ReadUInt16();
            JT905_0x8103_0x00A0.ParamLength = reader.ReadByte();            
            writer.WriteNumber($"[{JT905_0x8103_0x00A0.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x00A0.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x00A0.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x00A0.ParamLength);
            JT905_0x8103_0x00A0.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{JT905_0x8103_0x00A0.ParamValue.ReadNumber()}]参数值[ISU录音模式(0x01：全程录音；0x02：翻牌录音)]", JT905_0x8103_0x00A0.ParamValue);
            
        }
        /// <summary>
        /// 消息反序列化
        /// ISU录音模式(0x01：全程录音；0x02：翻牌录音)
        /// 0x8103_0x00A0        
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x00A0 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x00A0 JT905_0x8103_0x00A0 = new JT905_0x8103_0x00A0();
            JT905_0x8103_0x00A0.ParamId = reader.ReadUInt16();
            JT905_0x8103_0x00A0.ParamLength = reader.ReadByte();
            JT905_0x8103_0x00A0.ParamValue = reader.ReadByte();
            
            return JT905_0x8103_0x00A0;
        }
        /// <summary>
        /// 消息序列化
        /// ISU录音模式(0x01：全程录音；0x02：翻牌录音)
        /// 0x8103_0x00A0        
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x00A0 value, IJT905Config config)
        {
                       
            writer.WriteUInt16(value.ParamId);
            writer.WriteByte(value.ParamLength); 
            writer.WriteByte(value.ParamValue);
        }
    }
}

                    
