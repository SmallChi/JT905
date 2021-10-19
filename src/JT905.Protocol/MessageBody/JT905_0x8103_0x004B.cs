using System.Text.Json;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// ISU的语音播报音量控制：0～9(0为静音，9为最高)
    /// 0x8103_=0x004B
    /// </summary>
    public class JT905_0x8103_0x004B : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x004B>, IJT905Analyze
    {
        /// <summary>
        /// 0x004B
        /// </summary>
        public override uint ParamId { get; set; } = JT905Constants.JT905_0x8103_0x004B;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// ISU的语音播报音量控制：0～9(0为静音，9为最高)
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// ISU的语音播报音量控制：0～9(0为静音，9为最高)
        /// 0x8103_0x004B
        /// 解析数据
        /// </summary>
        /// <param name="reader">JT905消息读取器</param>
        /// <param name="writer">消息写入</param>
        /// <param name="config">JT905接口配置</param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x004B JT905_0x8103_0x004B = new JT905_0x8103_0x004B();
            JT905_0x8103_0x004B.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x004B.ParamLength = reader.ReadByte();
            JT905_0x8103_0x004B.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ JT905_0x8103_0x004B.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x004B.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x004B.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x004B.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x004B.ParamValue.ReadNumber()}]参数值[ISU的语音播报音量控制：0～9(0为静音，9为最高)]", JT905_0x8103_0x004B.ParamValue);
        }
        /// <summary>
        /// ISU的语音播报音量控制：0～9(0为静音，9为最高)
        /// 0x8103_0x004B
        /// 消息反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x004B Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x004B JT905_0x8103_0x004B = new JT905_0x8103_0x004B();
            JT905_0x8103_0x004B.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x004B.ParamLength = reader.ReadByte();
            JT905_0x8103_0x004B.ParamValue = reader.ReadUInt32();
            return JT905_0x8103_0x004B;
        }
        /// <summary>
        /// ISU的语音播报音量控制：0～9(0为静音，9为最高)
        /// 0x8103_0x004B
        /// 消息序列化
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x004B value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}

                    
