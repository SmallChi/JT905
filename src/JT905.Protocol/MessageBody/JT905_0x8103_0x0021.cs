using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态，先判断登录状态，若登录再根据 ACC 状态
    /// </summary>
    public class JT905_0x8103_0x0021 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0021>, IJT905Analyze
    {
        /// <summary>
        /// 0x0021
        /// </summary>
        public override uint ParamId { get; set; } = 0x0021;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 位置汇报方案，
        /// 0：根据 ACC 状态； 
        /// 1：根据登录状态和 ACC 状态，先判断登录状态，若登录再根据 ACC 状态
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x0021 JT905_0x8103_0x0021 = new JT905_0x8103_0x0021();
            JT905_0x8103_0x0021.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0021.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0021.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ JT905_0x8103_0x0021.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0021.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0021.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0021.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0021.ParamValue.ReadNumber()}]参数值[位置汇报方案，0：根据 ACC 状态； 1：根据登录状态和 ACC 状态]", JT905_0x8103_0x0021.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0021 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0021 JT905_0x8103_0x0021 = new JT905_0x8103_0x0021();
            JT905_0x8103_0x0021.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0021.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0021.ParamValue = reader.ReadUInt32();
            return JT905_0x8103_0x0021;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0021 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
