using System.Text.Json;
using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 道路运输证 IC 卡认证主服务器 TCP 端口
    /// </summary>
    public class JT905_0x8103_0x001B : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x001B>, IJT905Analyze
    {
        /// <summary>
        /// 0x001B
        /// </summary>
        public override uint ParamId { get; set; } = 0x001B;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        ///道路运输证 IC 卡认证主服务器 TCP 端口
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
            JT905_0x8103_0x001B JT905_0x8103_0x001B = new JT905_0x8103_0x001B();
            JT905_0x8103_0x001B.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x001B.ParamLength = reader.ReadByte();
            JT905_0x8103_0x001B.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ JT905_0x8103_0x001B.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x001B.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x001B.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x001B.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x001B.ParamValue.ReadNumber()}]参数值[道路运输证IC卡认证主服务器TCP端口]", JT905_0x8103_0x001B.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x001B Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x001B JT905_0x8103_0x001B = new JT905_0x8103_0x001B();
            JT905_0x8103_0x001B.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x001B.ParamLength = reader.ReadByte();
            JT905_0x8103_0x001B.ParamValue = reader.ReadUInt32();
            return JT905_0x8103_0x001B;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x001B value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}
