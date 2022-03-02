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
    /// 电话回拨
    /// </summary>
    public class JT905_0x8400 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8400>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.电话回拨;

        public override string Description => "电话回拨";
        
        /// <summary>
        /// 标志 0：普通通话；1：监听
        /// </summary>
        public byte Flag { get; set; }
        /// <summary>
        /// 电话号码 最长为 20byte
        /// </summary>
        public string PhoneNumber { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8400 value = new JT905_0x8400();
            value.Flag = reader.ReadByte();        
            writer.WriteNumber($"[{value.Flag.ReadNumber()}]标志 0：普通通话；1：监听", value.Flag);      
            var PhoneNumber = reader.ReadVirtualArraryEndChar0();
            value.PhoneNumber = reader.ReadStringEndChar0();
            writer.WriteString($"[{PhoneNumber}]电话号码 最长为 20byte",value.PhoneNumber);        

        }

        public JT905_0x8400 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8400 value = new JT905_0x8400();
            value.Flag = reader.ReadByte();            
            value.PhoneNumber = reader.ReadStringEndChar0();
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8400 value, IJT905Config config)
        {
            writer.WriteByte(value.Flag);
            if (value.PhoneNumber.Length>20)
            {
                int length = value.PhoneNumber.Length - 20;
                writer.WriteStringEndChar0(value.PhoneNumber.Substring(length));
            }
            else
            {
                writer.WriteStringEndChar0(value.PhoneNumber);
            }
            
            
        }
    }
}


                    
