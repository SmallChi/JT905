using System.Text.Json;

using JT905.Protocol.Extensions;

using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// GNSS 模块详细定位数据上传方式
    /// 0x00，本地存储，不上传（默认值）；
    /// 0x01，按时间间隔上传；
    /// 0x02，按距离间隔上传；
    /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
    /// 0x0C，按累计距离上传，达到距离后自动停止上传；
    /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
    /// </summary>
    public class JT905_0x8103_0x0094 : JT905_0x8103_BodyBase, IJT905MessagePackFormatter<JT905_0x8103_0x0094>, IJT905Analyze
    {
        /// <summary>
        /// 0x0094
        /// </summary>
        public override uint ParamId { get; set; } = 0x0094;
        /// <summary>
        /// 数据长度
        /// 1 byte
        /// </summary>
        public override byte ParamLength { get; set; } = 1;
        /// <summary>
        /// GNSS 模块详细定位数据上传方式
        /// 0x00，本地存储，不上传（默认值）；
        /// 0x01，按时间间隔上传；
        /// 0x02，按距离间隔上传；
        /// 0x0B，按累计时间上传，达到传输时间后自动停止上传；
        /// 0x0C，按累计距离上传，达到距离后自动停止上传；
        /// 0x0D，按累计条数上传，达到上传条数后自动停止上传。
        /// </summary>
        public byte ParamValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8103_0x0094 JT905_0x8103_0x0094 = new JT905_0x8103_0x0094();
            JT905_0x8103_0x0094.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0094.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0094.ParamValue = reader.ReadByte();
            writer.WriteNumber($"[{ JT905_0x8103_0x0094.ParamId.ReadNumber()}]参数ID", JT905_0x8103_0x0094.ParamId);
            writer.WriteNumber($"[{JT905_0x8103_0x0094.ParamLength.ReadNumber()}]参数长度", JT905_0x8103_0x0094.ParamLength);
            writer.WriteString($"[{ JT905_0x8103_0x0094.ParamValue.ReadNumber()}]参数值[GNSS模块详细定位数据上传方式]", GetUploadType( JT905_0x8103_0x0094.ParamValue));
            string GetUploadType(byte num) {
                switch (num)
                {
                    case 0x00:
                        return "本地存储，不上传（默认值）";
                    case 0x01:
                        return "按时间间隔上传";
                    case 0x02:
                        return "按距离间隔上传";
                    case 0x0B:
                        return "按累计时间上传，达到传输时间后自动停止上传";
                    case 0x0C:
                        return "按累计距离上传，达到距离后自动停止上传";
                    case 0x0D:
                        return "按累计条数上传，达到上传条数后自动停止上传";
                    default:
                        return "未识别";
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103_0x0094 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8103_0x0094 JT905_0x8103_0x0094 = new JT905_0x8103_0x0094();
            JT905_0x8103_0x0094.ParamId = reader.ReadUInt32();
            JT905_0x8103_0x0094.ParamLength = reader.ReadByte();
            JT905_0x8103_0x0094.ParamValue = reader.ReadByte();
            return JT905_0x8103_0x0094;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103_0x0094 value, IJT905Config config)
        {
            writer.WriteUInt32(value.ParamId);
            writer.WriteByte(value.ParamLength);
            writer.WriteByte(value.ParamValue);
        }
    }
}
