using System.Text.Json;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// ACCON汇报距离间隔，单位为米(m)
    /// 0x8103_=0x002B
    /// </summary>
    public class JT905_0x8103_0x002B : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x002B>, IJT905Analyze
    {
        /// <summary>
        /// 0x002B
        /// </summary>
        public override uint ParamId { get; set; } = JT905Constants.JT905_0x8103_0x002B;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// ACCON汇报距离间隔，单位为米(m)
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// ACCON汇报距离间隔，单位为米(m)
        /// 0x8103_0x002B
        /// 解析数据
        /// </summary>
        /// <param name="reader">JT905消息读取器</param>
        /// <param name="writer">消息写入</param>
        /// <param name="config">JT905接口配置</param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x002B JT905_0x8103_0x002B = new JT905_0x8103_0x002B();
            JT905_0x8103_0x002B.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x002B.ParamLength = reader.ReadByte();
            JT905_0x8103_0x002B.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ JT905_0x8103_0x002B.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x002B.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x002B.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x002B.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x002B.ParamValue.ReadNumber()}]参数值[ACCON汇报距离间隔，单位为米(m)]", JT905_0x8103_0x002B.ParamValue);
        }
        /// <summary>
        /// ACCON汇报距离间隔，单位为米(m)
        /// 0x8103_0x002B
        /// 消息反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x002B Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x002B JT905_0x8103_0x002B = new JT905_0x8103_0x002B();
            JT905_0x8103_0x002B.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x002B.ParamLength = reader.ReadByte();
            JT905_0x8103_0x002B.ParamValue = reader.ReadUInt32();
            return JT905_0x8103_0x002B;
        }
        /// <summary>
        /// ACCON汇报距离间隔，单位为米(m)
        /// 0x8103_0x002B
        /// 消息序列化
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x002B value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}

                    
