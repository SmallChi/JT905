using System.Text.Json;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 一卡通或支付平台备份服务器地址，IP或域名
    /// 0x8103_=0x001C
    /// </summary>
    public class JT905_0x8103_0x001C : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x001C>, IJT905Analyze
    {
        /// <summary>
        /// 0x001C
        /// </summary>
        public override uint ParamId { get; set; } = JT905Constants.JT905_0x8103_0x001C;
        /// <summary>
        /// 数据长度
        /// 4 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 4;
        /// <summary>
        /// 一卡通或支付平台备份服务器地址，IP或域名
        /// </summary>
        public uint ParamValue { get; set; }
        /// <summary>
        /// 一卡通或支付平台备份服务器地址，IP或域名
        /// 0x8103_0x001C
        /// 解析数据
        /// </summary>
        /// <param name="reader">JT905消息读取器</param>
        /// <param name="writer">消息写入</param>
        /// <param name="config">JT905接口配置</param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x001C JT905_0x8103_0x001C = new JT905_0x8103_0x001C();
            JT905_0x8103_0x001C.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x001C.ParamLength = reader.ReadByte();
            JT905_0x8103_0x001C.ParamValue = reader.ReadUInt32();
            writer.WriteNumber($"[{ JT905_0x8103_0x001C.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x001C.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x001C.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x001C.ParamLength);
            writer.WriteNumber($"[{ JT905_0x8103_0x001C.ParamValue.ReadNumber()}]参数值[一卡通或支付平台备份服务器地址，IP或域名]", JT905_0x8103_0x001C.ParamValue);
        }
        /// <summary>
        /// 一卡通或支付平台备份服务器地址，IP或域名
        /// 0x8103_0x001C
        /// 消息反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x001C Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x001C JT905_0x8103_0x001C = new JT905_0x8103_0x001C();
            JT905_0x8103_0x001C.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x001C.ParamLength = reader.ReadByte();
            JT905_0x8103_0x001C.ParamValue = reader.ReadUInt32();
            return JT905_0x8103_0x001C;
        }
        /// <summary>
        /// 一卡通或支付平台备份服务器地址，IP或域名
        /// 0x8103_0x001C
        /// 消息序列化
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x001C value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteUInt32(value.ParamValue);
        }
    }
}

                    
