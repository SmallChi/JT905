using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// ISU通用应答
    /// </summary>
    public class JT905_0x0001 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0001>, IJT905Analyze
    {
        public override ushort MsgId { get; } = JT905MsgId.ISU通用应答.ToUInt16Value();

        public override string Description => "ISU通用应答";
        /// <summary>
        /// 应答流水号
        /// 对应的平台消息的流水号
        /// </summary>
        public ushort ReplyMsgNum { get; set; }
        /// <summary>
        /// 应答 ID
        /// 对应的平台消息的 ID
        /// <see cref="JT905.Protocol.Enums.JT905MsgId"/>
        /// </summary>
        public ushort ReplyMsgId { get; set; }

        /// <summary>
        /// 结果
        /// 0：成功/确认；1：失败；2：消息有误；
        /// </summary>
        public JT905ISUResult ISUResult { get; set; }
        /// <summary>
        /// 0x0100解析
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            var replyMsgNum = reader.ReadUInt16();
            var replyMsgId = reader.ReadUInt16();
            var iSUResult = reader.ReadByte();
            writer.WriteNumber($"[{replyMsgNum.ReadNumber()}]应答流水号", replyMsgNum);
            writer.WriteNumber($"[{replyMsgId.ReadNumber()}]应答消息Id", replyMsgId);
            writer.WriteString($"[{iSUResult.ReadNumber()}]结果", ((Enums.JT905ISUResult)iSUResult).ToString());
        }
        /// <summary>
        /// 0x0100反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0001 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0001 jT905_0X0001 = new JT905_0x0001();
            jT905_0X0001.ReplyMsgNum = reader.ReadUInt16();
            jT905_0X0001.ReplyMsgId = reader.ReadUInt16();
            jT905_0X0001.ISUResult = (JT905ISUResult)reader.ReadByte();
            return jT905_0X0001;
        }
        /// <summary>
        /// 0x0100序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0001 value, IJT905Config config)
        {
            writer.WriteUInt16(value.ReplyMsgNum);
            writer.WriteUInt16(value.ReplyMsgId);
            writer.WriteByte((byte)value.ISUResult);
        }
    }
}
