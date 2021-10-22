using System.Text.Json;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 当天累计驾驶时间门限，单位为秒(s)
    /// 0x8103_=0x005A
    /// </summary>
    public class JT905_0x8103_0x005A : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x005A>, IJT905Analyze
    {
        /// <summary>
        /// 参数ID
        ///当天累计驾驶时间门限，单位为秒(s)
        /// 0x005A
        /// </summary>
        public override ushort ParamId { get; set; } = JT905Constants.JT905_0x8103_0x005A;
        /// <summary>
        /// 数据长度
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
    
        public override string Description => "当天累计驾驶时间门限，单位为秒(s)";
        
        /// <summary>
        /// 当天累计驾驶时间门限，单位为秒(s)
        /// </summary>
        public uint ParamValue { get; set; }
        
        
        /// <summary>
        /// 解析数据
        /// 当天累计驾驶时间门限，单位为秒(s)
        /// 0x8103_0x005A        
        /// </summary>
        /// <param name="reader">JT905消息读取器</param>
        /// <param name="writer">消息写入</param>
        /// <param name="config">JT905接口配置</param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x005A JT905_0x8103_0x005A = new JT905_0x8103_0x005A();
            JT905_0x8103_0x005A.ParamId = reader.ReadUInt16();
            JT905_0x8103_0x005A.ParamLength = reader.ReadByte();            
            writer.WriteNumber($"[{JT905_0x8103_0x005A.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x005A.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x005A.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x005A.ParamLength);
            JT905_0x8103_0x005A.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{JT905_0x8103_0x005A.ParamValue.ReadNumber()}]参数值[当天累计驾驶时间门限，单位为秒(s)]", JT905_0x8103_0x005A.ParamValue);
            
        }
        /// <summary>
        /// 消息反序列化
        /// 当天累计驾驶时间门限，单位为秒(s)
        /// 0x8103_0x005A        
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x005A Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x005A JT905_0x8103_0x005A = new JT905_0x8103_0x005A();
            JT905_0x8103_0x005A.ParamId = reader.ReadUInt16();
            JT905_0x8103_0x005A.ParamLength = reader.ReadByte();
            JT905_0x8103_0x005A.ParamValue = reader.ReadUInt32();
            
            return JT905_0x8103_0x005A;
        }
        /// <summary>
        /// 消息序列化
        /// 当天累计驾驶时间门限，单位为秒(s)
        /// 0x8103_0x005A        
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x005A value, IJT905Config config)
        {
                       
            writer.WriteUInt16(value.ParamId);
            writer.WriteByte(value.ParamLength); 
            writer.WriteUInt32(value.ParamValue);
        }
    }
}

                    
