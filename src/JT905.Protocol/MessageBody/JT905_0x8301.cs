using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 事件设置
    /// 0x8301
    /// </summary>
    public class JT905_0x8301 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8301>, IJT905Analyze
    {
        /// <summary>
        /// 0x8301
        /// </summary>
        public override ushort MsgId { get; } = (ushort)Enums.JT905MsgId.事件设置;
        /// <summary>
        /// 事件设置
        /// </summary>
        public override string Description => "事件设置";

        /// <summary>
        /// 事件项个数
        /// 0：删除 ISU 现有所有事件
        /// </summary>
        public byte EventCount { get; set; }
        /// <summary>
        /// 事件项列表
        /// 长度不大于 499byte， 否则分多条消息下发；
        /// 事件项组成数据格式见表 30
        /// </summary>
        public List<JT905EventProperty> EventItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8301 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8301 JT905_0X8301 = new JT905_0x8301();
            JT905_0X8301.EventCount = reader.ReadByte();
            JT905_0X8301.EventItems = new List<JT905EventProperty>();
            for (var i = 0; i < JT905_0X8301.EventCount; i++)
            {
                JT905EventProperty JT905EventProperty = new JT905EventProperty();
                JT905EventProperty.EventId = reader.ReadByte();
                JT905EventProperty.EventContent = reader.ReadStringEndChar0();
                JT905_0X8301.EventItems.Add(JT905EventProperty);
            }
            return JT905_0X8301;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8301 value, IJT905Config config)
        {
            
            if (value.EventItems != null && value.EventItems.Count > 0)
            {
                writer.WriteByte((byte)value.EventItems.Count);
                foreach (var item in value.EventItems)
                {
                    writer.WriteByte(item.EventId);
                    // 先计算内容长度（汉字为两个字节）
                    writer.WriteStringEndChar0(item.EventContent);
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
            JT905_0x8301 value = new JT905_0x8301();
            value.EventCount = reader.ReadByte();
            writer.WriteNumber($"[{value.EventCount.ReadNumber()}]设置总数", value.EventCount);
            writer.WriteStartArray("事件项列表");
            for (var i = 0; i < value.EventCount; i++)
            {
                writer.WriteStartObject();
                JT905EventProperty JT905EventProperty = new JT905EventProperty();
                JT905EventProperty.EventId = reader.ReadByte();
                writer.WriteNumber($"[{JT905EventProperty.EventId.ReadNumber()}]事件ID ", JT905EventProperty.EventId);
                var eventContenBuffer = reader.ReadVirtualArraryEndChar0();
                JT905EventProperty.EventContent = reader.ReadStringEndChar0();
                writer.WriteString($"[{eventContenBuffer}]事件内容", JT905EventProperty.EventContent);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

    }
}
