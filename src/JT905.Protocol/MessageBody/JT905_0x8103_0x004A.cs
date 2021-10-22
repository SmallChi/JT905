using System.Text.Json;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// ISU设备维护密码
    /// 0x8103_=0x004A
    /// </summary>
    public class JT905_0x8103_0x004A : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x004A>, IJT905Analyze
    {
        /// <summary>
        /// 参数ID
        ///ISU设备维护密码
        /// 0x004A
        /// </summary>
        public override ushort ParamId { get; set; } = JT905Constants.JT905_0x8103_0x004A;
        /// <summary>
        /// 数据长度
        /// </summary>
        public override byte ParamLength { get; set; }
    
        public override string Description => "ISU设备维护密码";
        
        /// <summary>
        /// ISU设备维护密码
        /// </summary>
        public string ParamValue { get; set; }
        
        
        /// <summary>
        /// 解析数据
        /// ISU设备维护密码
        /// 0x8103_0x004A        
        /// </summary>
        /// <param name="reader">JT905消息读取器</param>
        /// <param name="writer">消息写入</param>
        /// <param name="config">JT905接口配置</param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x004A JT905_0x8103_0x004A = new JT905_0x8103_0x004A();
            JT905_0x8103_0x004A.ParamId = reader.ReadUInt16();
            JT905_0x8103_0x004A.ParamLength = reader.ReadByte();            
            writer.WriteNumber($"[{JT905_0x8103_0x004A.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x004A.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x004A.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x004A.ParamLength);
            var paramValue = reader.ReadVirtualArray(JT905_0x8103_0x004A.ParamLength);
            JT905_0x8103_0x004A.ParamValue = reader.ReadString(JT905_0x8103_0x004A.ParamLength);
            writer.WriteString($"[{paramValue.ToArray().ToHexString()}]参数值[ISU设备维护密码]", JT905_0x8103_0x004A.ParamValue);
            
        }
        /// <summary>
        /// 消息反序列化
        /// ISU设备维护密码
        /// 0x8103_0x004A        
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x004A Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x004A JT905_0x8103_0x004A = new JT905_0x8103_0x004A();
            JT905_0x8103_0x004A.ParamId = reader.ReadUInt16();
            JT905_0x8103_0x004A.ParamLength = reader.ReadByte();
            JT905_0x8103_0x004A.ParamValue = reader.ReadString(JT905_0x8103_0x004A.ParamLength);
            
            return JT905_0x8103_0x004A;
        }
        /// <summary>
        /// 消息序列化
        /// ISU设备维护密码
        /// 0x8103_0x004A        
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x004A value, IJT905Config config)
        {
                       
            writer.WriteUInt16(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}

                    
