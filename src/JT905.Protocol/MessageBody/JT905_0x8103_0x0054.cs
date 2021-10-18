using System.Text.Json;
using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 关键标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警为关键报警
    /// </summary>
    public class JT905_0x8103_0x0054 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0054>, IJT905Analyze
    {
        /// <summary>
        /// 0x0054
        /// </summary>
        public override uint ParamId { get; set; } = 0x0054;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 关键标志，与位置信息汇报消息中的报警标志相对应，相应位为 1 则对相应报警为关键报警
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
            JT905_0x8103_0x0054 JT905_0x8103_0x0054 = new JT905_0x8103_0x0054();
            JT905_0x8103_0x0054.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0054.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0054.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ JT905_0x8103_0x0054.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0054.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0054.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0054.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x0054.ParamValue.ReadNumber()}]参数值[关键标志]", JT905_0x8103_0x0054.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0054 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0054 JT905_0x8103_0x0054 = new JT905_0x8103_0x0054();
            JT905_0x8103_0x0054.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0054.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0054.ParamValue = reader.ReadUInt32();
            return JT905_0x8103_0x0054;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0054 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
