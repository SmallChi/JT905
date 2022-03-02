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
    /// 订单任务下发
    /// </summary>
    public class JT905_0x8B00 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8B00>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.订单任务下发;

        public override string Description => "订单任务下发";
        
        /// <summary>
        /// 业务ID
        /// </summary>
        public uint BusinessId { get; set; }
        /// <summary>
        /// 业务类型 0：即时召车；1：预约召车；2：车辆指派
        /// </summary>
        public JT905BusinessType BusinessType { get; set; }
        /// <summary>
        /// 要车时间
        /// </summary>
        public DateTime TaxiTime { get; set; }
        /// <summary>
        /// 对乘客要车大概地点的描述
        /// </summary>
        public string BusinessDescription { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8B00 value = new JT905_0x8B00();
            value.BusinessId = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.BusinessId.ReadNumber()}]业务ID", value.BusinessId);
            value.BusinessType = (JT905BusinessType)reader.ReadByte();        
            writer.WriteString($"[{((byte)value.BusinessType).ReadNumber()}]业务类型", value.BusinessType.ToString());      
            value.TaxiTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.TaxiTime:yyMMddHHmmss}]要车时间", value.TaxiTime.ToString("yyyy-MM-dd HH:mm:ss"));        
            var BusinessDescription = reader.ReadVirtualArraryEndChar0();
            value.BusinessDescription = reader.ReadStringEndChar0();
            writer.WriteString($"[{BusinessDescription}]业务描述",value.BusinessDescription);        

        }

        public JT905_0x8B00 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8B00 value = new JT905_0x8B00();
            value.BusinessId = reader.ReadUInt32();    
            value.BusinessType = (JT905BusinessType)reader.ReadByte(); 
            value.TaxiTime = reader.ReadDateTime6();        
           
            value.BusinessDescription = reader.ReadStringEndChar0();
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8B00 value, IJT905Config config)
        {
            writer.WriteUInt32(value.BusinessId);
            writer.WriteByte((byte)value.BusinessType);
            writer.WriteDateTime6(value.TaxiTime);           
            writer.WriteStringEndChar0(value.BusinessDescription);
            
        }
    }
}


                    
