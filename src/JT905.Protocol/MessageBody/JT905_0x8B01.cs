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
    /// 下发抢答结果信息
    /// </summary>
    public class JT905_0x8B01 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8B01>, IJT905Analyze
    {
        private decimal _serviceCharge;

        public override ushort MsgId => (ushort)Enums.JT905MsgId.下发抢答结果信息;

        public override string Description => "下发抢答结果信息";
        
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
        public DateTime CallingTime { get; set; }
        /// <summary>
        /// 乘客位置经度
        /// </summary>
        public uint StartLongitude { get; set; }
        /// <summary>
        /// 乘客位置纬度
        /// </summary>
        public uint StartLatitude { get; set; }
        /// <summary>
        /// 目的地位置经度
        /// </summary>
        public uint DestinationLongitude { get; set; }
        /// <summary>
        /// 目的地位置经度
        /// </summary>
        public uint DestinationLatitude { get; set; }
        /// <summary>
        /// 电召服务费
        /// </summary>
        public string CarServiceCharge { get; set; }
        /// <summary>
        /// 乘客电话号码
        /// </summary>
        public string PassengerPhone { get; set; }
        /// <summary>
        /// 对乘客要车大概地点的描述
        /// </summary>
        public string BusinessDescription { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8B01 value = new JT905_0x8B01();
            value.BusinessId = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.BusinessId.ReadNumber()}]业务ID", value.BusinessId);
            value.BusinessType = (JT905BusinessType)reader.ReadByte();        
            writer.WriteString($"[{((byte)value.BusinessType).ReadNumber()}]业务类型 0：即时召车；1：预约召车；2：车辆指派", value.BusinessType.ToString());      
            value.CallingTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.CallingTime:yyMMddHHmmss}]要车时间", value.CallingTime.ToString("yyyy-MM-dd HH:mm:ss"));        
            value.StartLongitude = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.StartLongitude.ReadNumber()}]乘客位置经度", value.StartLongitude);
            value.StartLatitude = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.StartLatitude.ReadNumber()}]乘客位置纬度", value.StartLatitude);
            value.DestinationLongitude = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.DestinationLongitude.ReadNumber()}]目的地位置经度", value.DestinationLongitude);
            value.DestinationLatitude = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.DestinationLatitude.ReadNumber()}]目的地位置经度", value.DestinationLatitude);
            var CarServiceCharge = reader.ReadVirtualArray(2).ToArray().ToHexString();
            value.CarServiceCharge = reader.ReadBCD(4, '-'); ;
            writer.WriteString($"[{CarServiceCharge}]电召服务费",value.CarServiceCharge);        
            var PassengerPhone = reader.ReadVirtualArraryEndChar0();
            value.PassengerPhone = reader.ReadStringEndChar0();
            writer.WriteString($"[{PassengerPhone}]乘客电话号码",value.PassengerPhone);        
            var BusinessDescription = reader.ReadVirtualArraryEndChar0();
            value.BusinessDescription = reader.ReadStringEndChar0();
            writer.WriteString($"[{BusinessDescription}]对乘客要车大概地点的描述",value.BusinessDescription);        

        }

        public JT905_0x8B01 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8B01 value = new JT905_0x8B01();
            value.BusinessId = reader.ReadUInt32();    
            value.BusinessType = (JT905BusinessType)reader.ReadByte(); 
            value.CallingTime = reader.ReadDateTime6();        
            value.StartLongitude = reader.ReadUInt32();    
            value.StartLatitude = reader.ReadUInt32();    
            value.DestinationLongitude = reader.ReadUInt32();    
            value.DestinationLatitude = reader.ReadUInt32();    
           
            value.CarServiceCharge = reader.ReadBCD(4,'-');
           
            value.PassengerPhone = reader.ReadStringEndChar0();
           
            value.BusinessDescription = reader.ReadStringEndChar0();
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8B01 value, IJT905Config config)
        {
            writer.WriteUInt32(value.BusinessId);
            writer.WriteByte((byte)value.BusinessType);
            writer.WriteDateTime6(value.CallingTime);
            writer.WriteUInt32(value.StartLongitude);
            writer.WriteUInt32(value.StartLatitude);
            writer.WriteUInt32(value.DestinationLongitude);
            writer.WriteUInt32(value.DestinationLatitude);
            if (value.CarServiceCharge.IndexOf('-')>0||value.CarServiceCharge.IndexOf('.')>0)
            {
                string v = value.CarServiceCharge.Replace("-","").Replace(".","");
                value.CarServiceCharge = v;
                //decimal.TryParse(v,out _serviceCharge);
            }
            else
            {
                value.CarServiceCharge += "0";
            }
            writer.WriteBCD(value.CarServiceCharge,4);
           
            writer.WriteStringEndChar0(value.PassengerPhone);
           
            writer.WriteStringEndChar0(value.BusinessDescription);
            
        }
    }
}


                    
