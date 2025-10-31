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
        /// <summary>
        /// 消息ID
        /// </summary>
        public override ushort MsgId => (ushort)Enums.JT905MsgId.设备巡检应答;

        /// <summary>
        /// 消息描述
        /// </summary>
        public override string Description => "设备巡检应答";

        /// <summary>
        /// 
        /// </summary>

        public IList<TLV> TLVs { get; set; }


        /// <summary>
        /// 分析
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>

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
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>

        public JT905_0x0B11 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B11 value = new JT905_0x0B11();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.TLVs = new List<TLV>();
                //TODO:待解析JT905_0x0B11
                //while (reader.ReadCurrentRemainContentLength() > 0)
                //{

                //}
            }


            return value;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B11 value, IJT905Config config)
        {

            if (value.TLVs!=null&&value.TLVs.Count>0)
            {
                foreach (var item in value.TLVs)
                {
                    writer.WriteByte((byte)item.Tag);
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
    /// 设备巡检应答消息体
    /// </summary>
    public class TLV {
        /// <summary>
        /// 标签
        /// </summary>
        public Enums.JT905DeviceType Tag { get; set; }

        /// <summary>
        /// 长度
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

        /// <summary>
        /// 签到缓存数据条数
        /// </summary>
        public int SignedDataCount { get; set; }

        /// <summary>
        /// 签退缓存数据条数
        /// </summary>
        public int SignedOutDataCount { get; set; }

        /// <summary>
        /// 营运记录缓存条数
        /// </summary>
        public int NormalDataCount { get; set; }  

        /// <summary>
        /// 一卡通交易缓存条数
        /// </summary>
        public  CardTransactionDataCount { get; set; } 

    }
}


                    
