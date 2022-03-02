using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 存储图像检索应答
    /// </summary>
    public class JT905_0x0802 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0802>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.存储图像检索应答;

        public override string Description => "存储图像检索应答";

        /// <summary>
        /// 应答流水号
        /// </summary>
        public byte MsgNum { get; set; }
        /// <summary>
        /// 检索总项数据包大小
        /// </summary>
        public uint RetrievesTotal { get; set; }
        /// <summary>
        /// 当前检索项在总项 数据中的偏移量
        /// </summary>
        public uint CurrentRetrievesOffset { get; set; }
        /// <summary>
        /// 检索项
        /// </summary>
        public uint[] RetrievalItem { get; set; }




        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0802 value = new JT905_0x0802();
            value.MsgNum = reader.ReadByte();
            writer.WriteNumber($"[{value.MsgNum.ReadNumber()}]应答流水号", value.MsgNum);
            value.RetrievesTotal = reader.ReadUInt32();
            writer.WriteNumber($"[{value.RetrievesTotal.ReadNumber()}]检索总项数据包大小 总项数x4", value.RetrievesTotal);
            value.CurrentRetrievesOffset = reader.ReadUInt32();
            writer.WriteNumber($"[{value.CurrentRetrievesOffset.ReadNumber()}]当前检索项在总项 数据中的偏移量", value.CurrentRetrievesOffset);
            //value.RetrievalItem = reader.ReadUInt32();
            string v = reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray().ToHexString();
            writer.WriteString($"检索项", v);
            uint count = value.RetrievesTotal / 4;
            writer.WriteStartObject("图像检索项数据");
            if (count>0)
            {
                
                for (int i = 0; i < count; i++)
                {
                    uint photoID = reader.ReadUInt32();
                    writer.WriteNumber($"[{photoID.ReadNumber()}]照片ID号",photoID);
                }               
            }
            writer.WriteEndObject();

        }

        public JT905_0x0802 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0802 value = new JT905_0x0802();
            value.MsgNum = reader.ReadByte();
            value.RetrievesTotal = reader.ReadUInt32();
            value.CurrentRetrievesOffset = reader.ReadUInt32();
            uint length = value.RetrievesTotal / 4;
            value.RetrievalItem = new uint[length];
            for (int i = 0; i < length; i++)
            {
                if (reader.ReadCurrentRemainContentLength() > 0)
                    value.RetrievalItem[i] = reader.ReadUInt32();
            }
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0802 value, IJT905Config config)
        {
            writer.WriteByte(value.MsgNum);
            writer.WriteUInt32((uint)(value.RetrievalItem.Length * 4));
            writer.WriteUInt32(value.CurrentRetrievesOffset);
            if (value.RetrievalItem.Length > 0)
            {
                foreach (var photoID in value.RetrievalItem)
                {
                    writer.WriteUInt32(photoID);
                }
            }


        }
    }
}



