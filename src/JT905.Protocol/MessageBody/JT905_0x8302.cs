using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 提问下发
    /// 0x8302
    /// 2019版本已作删除
    /// </summary>
    public class JT905_0x8302 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8302>, IJT905Analyze
    {
        /// <summary>
        /// 0x8302
        /// </summary>
        public override ushort MsgId { get; } = 0x8302;
        /// <summary>
        /// 提问下发
        /// </summary>
        public override string Description => "提问下发";
        /// <summary>
        /// 标志
        /// 提问下发标志位定义
        /// </summary>
        public byte Flag { get; set; }

        /// <summary>
        /// 问题ID
        /// 提问下发标志位定义见表 33
        /// </summary>
        public uint IssueId { get; set; }

        /// <summary>
        /// 问题
        /// 最长 100byte
        /// 问题文本，经 GBK 编码，长度为 N
        /// </summary>
        public string Issue { get; set; }
        /// <summary>
        /// 候选答案列表 
        /// 需保证消息体长度不大于 500byte，候选答案 组成见表 34
        /// </summary>
        public List<Answer> Answers { get; set; }
        /// <summary>
        /// 候选答案信息
        /// </summary>
        public class Answer
        {
            /// <summary>
            /// 答案 ID
            /// </summary>
            public byte Id { get; set; }
            
            /// <summary>
            /// 答案内容 
            /// 答案内容，经 GBK 编码
            /// </summary>
            public string Content { get; set; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8302 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8302 JT905_0X8302 = new JT905_0x8302();
            JT905_0X8302.Flag = reader.ReadByte();
            JT905_0X8302.IssueId = reader.ReadUInt32();
            JT905_0X8302.Issue = reader.ReadStringEndChar0();
            JT905_0X8302.Answers = new List<JT905_0x8302.Answer>();
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                try
                {
                    JT905_0x8302.Answer answer = new JT905_0x8302.Answer();
                    answer.Id = reader.ReadByte();
                    answer.Content = reader.ReadStringEndChar0();
                    JT905_0X8302.Answers.Add(answer);
                }
                catch
                {
                    break;
                }
            }
            return JT905_0X8302;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8302 value, IJT905Config config)
        {
            writer.WriteByte(value.Flag);
            writer.WriteUInt32(value.IssueId);
            // 先计算内容长度（汉字为两个字节）
            //writer.Skip(1, out int issuePosition);
            writer.WriteStringEndChar0(value.Issue);
            if (value.Answers != null && value.Answers.Count > 0)
            {
                foreach (var item in value.Answers)
                {
                    writer.WriteByte(item.Id);
                    writer.WriteStringEndChar0(item.Content);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8302 value = new JT905_0x8302();
            value.Flag = reader.ReadByte();
            writer.WriteString($"[{value.Flag.ReadNumber()}]标志", ((Enums.JT905TextFlag)value.Flag).ToString());
            value.IssueId = reader.ReadUInt32();
            writer.WriteNumber($"[{value.IssueId.ReadNumber()}]问题ID", value.IssueId);
            var issueBuffer= reader.ReadVirtualArraryEndChar0();
            value.Issue = reader.ReadStringEndChar0();
            writer.WriteString($"[{issueBuffer}]问题文本", value.Issue);
            writer.WriteStartArray("候选答案列表");
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                writer.WriteStartObject();
                JT905_0x8302.Answer answer = new JT905_0x8302.Answer();
                answer.Id = reader.ReadByte();
                writer.WriteNumber($"[{answer.Id.ReadNumber()}]答案ID", answer.Id);                
                var answerBuffer = reader.ReadVirtualArraryEndChar0();
                answer.Content = reader.ReadStringEndChar0();
                writer.WriteString($"[{answerBuffer}]答案内容", answer.Content);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }
}
