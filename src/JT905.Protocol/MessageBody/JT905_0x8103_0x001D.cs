using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 道路运输证 IC 卡认证备份服务器 IP 地址或域名，端口同主服务器
    /// </summary>
    public class JT905_0x8103_0x001D : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x001D>, IJT905Analyze
    {
        /// <summary>
        /// 0x001D
        /// </summary>
        public override uint ParamId { get; set; } = 0x001D;
        /// <summary>
        /// 数据长度
        /// </summary>
        public override byte ParamLength { get; set; }
        /// <summary>
        /// 道路运输证 IC 卡认证备份服务器 IP 地址或域名，端口同主服务器
        /// </summary>
        public string ParamValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x001D JT905_0x8103_0x001D = new JT905_0x8103_0x001D();
            JT905_0x8103_0x001D.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x001D.ParamLength = reader.ReadByte();
            var paramValue = reader.ReadVirtualArray(JT905_0x8103_0x001D.ParamLength);
            JT905_0x8103_0x001D.ParamValue = reader.ReadString(JT905_0x8103_0x001D.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x001D.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x001D.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x001D.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x001D.ParamLength);
            writer.WriteString($"[{paramValue.ToArray().ToHexString()}]参数值[道路运输证IC卡认证备份服务器IP]", JT905_0x8103_0x001D.ParamValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x001D Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x001D JT905_0x8103_0x001D = new JT905_0x8103_0x001D();
            JT905_0x8103_0x001D.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x001D.ParamLength = reader.ReadByte();
            JT905_0x8103_0x001D.ParamValue = reader.ReadString(JT905_0x8103_0x001D.ParamLength);
            return JT905_0x8103_0x001D;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x001D value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.Skip(1, out int skipPosition);
            writer.WriteString(value.ParamValue);
            int length = writer.GetCurrentPosition() - skipPosition - 1;
            writer.WriteByteReturn((byte)length, skipPosition);
        }
    }
}
