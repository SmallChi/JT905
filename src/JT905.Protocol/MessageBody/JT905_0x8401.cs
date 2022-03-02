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
    /// 设置电话本
    /// </summary>
    public class JT905_0x8401 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8401>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.设置电话本;

        public override string Description => "设置电话本";

        /// <summary>
        /// 联系人总数
        /// </summary>
        public byte ContactCount { get; set; }
        /// <summary>
        /// 联系人项
        /// </summary>

        public IList<JT905ContactProperty> Contacts { get; set; }


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8401 value = new JT905_0x8401();
            value.ContactCount = reader.ReadByte();
            writer.WriteNumber($"[{value.ContactCount.ReadNumber()}]联系人总数", value.ContactCount);
            //writer.WriteString($"联系人项", reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray().ToHexString());
            writer.WriteStartArray("联系人项");
            if (value.ContactCount > 0)
            {
                while (reader.ReadCurrentRemainContentLength() > 0)
                {
                    writer.WriteStartObject();
                    var TelephoneBookContactType = reader.ReadByte();
                    writer.WriteString($"[{TelephoneBookContactType.ReadNumber()}]标志",((JT905TelephoneBookContactType)TelephoneBookContactType).ToString());
                    var phoneNumber = reader.ReadVirtualArraryEndChar0();
                    var PhoneNumber = reader.ReadStringEndChar0();
                    writer.WriteString($"[{phoneNumber}]电话号码", PhoneNumber);
                    var contact = reader.ReadVirtualArraryEndChar0();
                    var Contact = reader.ReadStringEndChar0();
                    writer.WriteString($"[{contact}]联系人", Contact);
                    writer.WriteEndObject();
                }               
            }
            writer.WriteEndArray();
        }

        public JT905_0x8401 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8401 value = new JT905_0x8401();
            value.ContactCount = reader.ReadByte();
            try
            {
                if (value.ContactCount > 0)
                {
                    value.Contacts = new List<JT905ContactProperty>();
                    while (reader.ReadCurrentRemainContentLength() > 0)
                    {

                        value.Contacts.Add(new JT905ContactProperty
                        {
                            TelephoneBookContactType = (JT905TelephoneBookContactType)reader.ReadByte(),
                            PhoneNumber = reader.ReadStringEndChar0(),
                            Contact = reader.ReadStringEndChar0(),
                        });
                    }
                }

            }
            catch { }

            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8401 value, IJT905Config config)
        {
            if (value.Contacts != null && value.Contacts.Count > 0)
            {
                writer.WriteByte((byte)value.Contacts.Count);
                foreach (var item in value.Contacts)
                {
                    writer.WriteByte((byte)(item.TelephoneBookContactType));
                    if (item.PhoneNumber.Length > 20)
                    {
                        int length = item.PhoneNumber.Length - 20;
                        writer.WriteStringEndChar0(item.PhoneNumber.Substring(length));
                    }
                    else
                        writer.WriteStringEndChar0(item.PhoneNumber);
                    if (item.Contact.Length > 10)
                    {
                        int length = item.Contact.Length - 20;
                        writer.WriteStringEndChar0(item.Contact.Substring(length));
                    }
                    else
                        writer.WriteStringEndChar0(item.Contact);
                }
            }


        }
    }

    /// <summary>
    /// 电话本联系人项数据
    /// </summary>
    public class JT905ContactProperty
    {
        /// <summary>
        /// 标志 1：呼入；2：呼出；3：呼入/呼出
        /// </summary>
        public JT905TelephoneBookContactType TelephoneBookContactType { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 联系人 经 GBK 编码
        /// </summary>
        public string Contact { get; set; }
    }
}



