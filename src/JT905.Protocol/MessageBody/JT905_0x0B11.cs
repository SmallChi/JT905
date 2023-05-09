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
    /// 设备巡检应答
    /// </summary>
    public class JT905_0x0B11 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0B11>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.设备巡检应答;

        public override string Description => "设备巡检应答";

        public IList<TLV> tLVs { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0B11 value = new JT905_0x0B11();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                //value.TypeID = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                //writer.WriteString($"[{value.TypeID.ToHexString()}]巡检设备类型代码", value.TypeID.ToHexString());
            }
            else
            {
                writer.WriteString("[]巡检设备类型代码", "");
            }

        }

        public JT905_0x0B11 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B11 value = new JT905_0x0B11();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.tLVs = new List<TLV>();
                //TODO:待解析JT905_0x0B11
                //while (reader.ReadCurrentRemainContentLength() > 0)
                //{

                //}
            }


            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B11 value, IJT905Config config)
        {

            if (value.tLVs!=null&&value.tLVs.Count>0)
            {
                foreach (var item in value.tLVs)
                {
                    writer.WriteByte((byte)item.DeviceType);
                    writer.Skip(2,out int postition);
                    writer.WriteBCD(item.SerialNumber,10);
                    writer.WriteBCD(item.HardwareVer,2);
                    writer.WriteBCD(item.SoftVer,4);
                    writer.WriteUInt32((uint)item.ISUStatus);
                    writer.WriteUInt32((uint)item.ISUAlarm);
                    int length = writer.GetCurrentPosition();
                }
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TLV {
        /// <summary>
        /// 
        /// </summary>
        public Enums.JT905DeviceType DeviceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Length { get; set; }
        
        /// <summary>
        /// 设备序列号
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 硬件版本号
        /// </summary>

        public string HardwareVer { get; set; }
        /// <summary>
        /// 软件版本号
        /// </summary>

        public string SoftVer { get; set; }
        /// <summary>
        /// ISU设备状态
        /// </summary>

        public Enums.JT905Status ISUStatus { get; set; }
        /// <summary>
        /// ISU报警标志
        /// </summary>

        public Enums.JT905Alarm ISUAlarm { get; set; }
    }
}


                    
