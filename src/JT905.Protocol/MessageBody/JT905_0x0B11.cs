
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
    /// 设备巡检应答
    /// </summary>
    public class JT905_0x0B11 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0B11>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.设备巡检应答;

        public override string Description => "设备巡检应答";
        /// <summary>
        /// 应答数据
        /// T:设备类型
        /// L:设备巡检结果的长度
        /// V:设备巡检内容
        /// </summary>

        public Dictionary<byte,JT905_0x0B11_TLV> tLVs { get; set; }

        /// <summary>
        /// 设备未知自定义附加数据（未注册）、数据解析异常
        /// 设备未知巡检内容（未注册）、数据解析异常
        /// </summary>
        public List<byte[]> ExceptionDeviceInspectionContents { get; set; }




        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0B11 value = new JT905_0x0B11();
            try
            {
                writer.WriteStartArray("应答数据");
                value.tLVs = new Dictionary<byte, JT905_0x0B11_TLV>();
                while (reader.ReadCurrentRemainContentLength() > 0)
                {
                    
                    byte devType = reader.ReadVirtualByte();                    
                    if (config.JT905_0x0B11_Factory.Map.TryGetValue(devType,out object deviceTypeInstance))
                    {

                        if (value.tLVs.ContainsKey(devType))
                        {
                            //存在重复的就不解析，把数据统一放在异常定位数据里面
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            writer.WriteNumber($"[{devType.ReadNumber()}]未知附加信息Id", devType);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            deviceTypeInstance.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                        }

                    }
                    else
                    {
                        //未知的附加只通过标准的自定义附加信息来解析，其余的通过自己注册，自己实现的方式来解析
                        reader.Skip(1);
                        byte attachLen = reader.ReadByte();
                        int remainLength = reader.ReadCurrentRemainContentLength();
                        writer.WriteStartObject();
                        writer.WriteString($"[{devType.ReadNumber()}]设备类型", ((JT905DeviceType)devType).ToString());
                        writer.WriteNumber($"[{attachLen.ReadNumber()}]设备巡检结果的长度", attachLen);
                        if ((attachLen + 2) > remainLength)
                        {
                            writer.WriteString($"未知信息1", reader.ReadArray(remainLength).ToArray().ToHexString());
                        }
                        else
                        {

                            writer.WriteString($"未知信息2", reader.ReadArray(reader.ReaderCount, attachLen).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        writer.WriteEndObject();

                    }

                }
                writer.WriteEndArray();
            }
            catch (Exception ex)
            {

                writer.WriteString($"异常信息", ex.StackTrace);
            }
            
           
        }

        public JT905_0x0B11 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B11 value = new JT905_0x0B11();
            try
            {

                value.tLVs = new Dictionary<byte, JT905_0x0B11_TLV>();
                value.ExceptionDeviceInspectionContents = new List<byte[]>();
                while (reader.ReadCurrentRemainContentLength() > 0)
                {
                    byte attachId = reader.ReadVirtualByte();
                    if (config.JT905_0x0B11_Factory.Map.TryGetValue(attachId,out object attachInstance))
                    {

                    }
                    // var tlv = new JT905_0x0B11_TLV();
                    // value.tLVs.Add(attachId,tlv);
                }
                           

            }
            catch { }
           

            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B11 value, IJT905Config config)
        {

            if (value.tLVs != null && value.tLVs.Count > 0)
            {
                foreach (var item in value.tLVs)
                {
                    try
                    {
                        JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(item.Value,ref writer,item.Value,config);
                    }
                    catch{}
                }
            }
        }
    }
    ///// <summary>
    ///// 应答数据
    ///// T:设备类型
    ///// L:设备巡检结果的长度
    ///// V:设备巡检内容
    ///// </summary>
    //public class TLV : DeviceContents
    //{
    //    /// <summary>
    //    /// 设备类型
    //    /// </summary>
    //    public Enums.JT905DeviceType DeviceType { get; set; }

    //    /// <summary>
    //    /// 设备巡检结果的长度
    //    /// </summary>
    //    public byte Length { get; set; }
    //}
    ///// <summary>
    ///// 设备巡检内容
    ///// </summary>
    //public class DeviceContents
    //{
    //    /// <summary>
    //    /// 设备序列号
    //    /// </summary>
    //    public string SerialNumber { get; set; }
    //    /// <summary>
    //    /// 硬件版本号
    //    /// </summary>

    //    public string HardwareVer { get; set; }
    //    /// <summary>
    //    /// 软件版本号
    //    /// </summary>

    //    public string SoftVer { get; set; }
    //    /// <summary>
    //    /// ISU设备状态
    //    /// </summary>

    //    public Enums.JT905Status ISUStatus { get; set; }
    //    /// <summary>
    //    /// ISU报警标志
    //    /// </summary>

    //    public Enums.JT905Alarm ISUAlarmFlag { get; set; }
    //    /// <summary>
    //    /// 签到缓存数据条数
    //    /// 未成功上传的签到记录数
    //    /// </summary>
    //    public byte CheckInCache { get; set; }
    //    /// <summary>
    //    /// 签退缓存数据条数
    //    /// 未成功上传的签退记录数
    //    /// </summary>
    //    public byte CheckOutCache { get; set; }

    //    /// <summary>
    //    /// 营运记录数据条数
    //    /// 未成功上传的营运记录数
    //    /// </summary>
    //    public byte OperationRecordCache { get; set; }
    //    /// <summary>
    //    /// 一卡通交易缓存条数
    //    /// 未上传成功的一卡通交易记录数
    //    /// </summary>

    //    public byte ChinaT_unionDealCache { get; set; }
    //}
}



