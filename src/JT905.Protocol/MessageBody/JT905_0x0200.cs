using JT905.Protocol.Enums;
using JT905.Protocol.Exceptions;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息汇报
    /// </summary>
    public class JT905_0x0200 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0200>, IJT905Analyze
    {
        /// <summary>
        /// 0x0200
        /// </summary>
        public override ushort MsgId { get; } = 0x0200;
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        public override string Description => "位置信息汇报";
        /// <summary>
        /// 报警标志 
        /// <see cref="JT905.Protocol.Enums.JT905Alarm"/>
        /// </summary>
        public uint AlarmFlag { get; set; }
        /// <summary>
        /// 状态位标志
        /// <see cref="Enums.JT905Status"/>
        /// </summary>
        public uint StatusFlag { get; set; }
        /// <summary>
        /// 纬度
        /// 除以600000.0D
        /// </summary>
        public int Lat { get; set; }
        /// <summary>
        /// 经度
        /// 除以600000.0D
        /// </summary>
        public int Lng { get; set; }
        /// <summary>
        /// 纬度
        /// 除以600000.0D
        /// </summary>
        public double ConvertLat { get { return Math.Round(Lat / 600000.0D,6); } }
        /// <summary>
        /// 经度
        /// 除以600000.0D
        /// </summary>
        public double ConvertLng { get { return Math.Round(Lng / 600000.0D, 6); } }
        /// <summary>
        /// 速度 1/10km/h
        /// </summary>
        public ushort Speed { get; set; }
        /// <summary>
        /// 方向 0-179，每刻度为两度正北为 0，顺时针
        /// </summary>
        public byte Direction { get; set; }
        /// <summary>
        /// YY-MM-DD-hh-mm-ss（GMT+8 时间，本标准中之后涉及的时间均采用此时区）
        /// </summary>
        public DateTime GPSTime { get; set; }
        /// <summary>
        /// 基础位置附加信息
        /// </summary>
        public Dictionary<byte, JT905_0x0200_BodyBase> BasicLocationAttachData { get; set; }
        /// <summary>
        /// 自定义位置附加信息
        /// 场景：
        /// 一个设备厂商对应多个设备类型，不同设备类型可能存在相同的自定义位置附加信息Id，导致自定义附加信息Id冲突，无法解析。
        /// 解决方式：
        /// 1.凡是解析自定义附加信息Id协议的，先进行分割存储，然后在根据外部的设备类型进行统一处理。
        /// 2.可以根据设备类型做个工厂，解耦对公共序列化器的依赖。
        /// 缺点：
        /// 依赖平台录入的设备类型
        /// </summary>
        public Dictionary<byte, JT905_0x0200_CustomBodyBase> CustomLocationAttachData { get; set; }
        /// <summary>
        /// 自定义位置附加信息2
        /// </summary>
        public Dictionary<ushort, JT905_0x0200_CustomBodyBase2> CustomLocationAttachData2 { get; set; }
        /// <summary>
        /// 自定义位置附加信息3
        /// </summary>
        public Dictionary<ushort, JT905_0x0200_CustomBodyBase3> CustomLocationAttachData3 { get; set; }
        /// <summary>
        /// 自定义位置附加信息4
        /// </summary>
        public Dictionary<byte, JT905_0x0200_CustomBodyBase4> CustomLocationAttachData4 { get; set; }
        /// <summary>
        /// 未知自定义附加数据【一切都是为了尽可能兼容】
        /// 形如:自定义_附加Id字节数_附加数据长度_附加Id
        /// 注意：这边不是最好的解决方式，最好的方式就是通过已知的自定义协议附加，根据提供的文档进行组织后在注册。
        /// 这边采用优先1-1的，然后是绝大多数设备厂家有2-1，少部分是2-2，最后是1_4。
        /// </summary>
        public Dictionary<ushort, byte[]> UnknownLocationAttachData { get; set; }
        /// <summary>
        /// 设备未知自定义附加数据（未注册）、数据解析异常
        /// </summary>
        public List<byte[]> ExceptionLocationAttachOriginalData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0200 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0200 value = new JT905_0x0200();
            value.AlarmFlag = reader.ReadUInt32();
            value.StatusFlag = reader.ReadUInt32();
            if (((value.StatusFlag >> 30) & 1) == 1)
            {   //南纬 268435456 0x10000000
                value.Lat = (int)reader.ReadUInt32();
            }
            else
            {
                value.Lat = reader.ReadInt32();
            }
            if (((value.StatusFlag >> 29) & 1) == 1)
            {   //西经 ‭134217728‬ 0x8000000
                value.Lng = (int)reader.ReadUInt32();
            }
            else
            {
                value.Lng = reader.ReadInt32();
            }
            value.Speed = reader.ReadUInt16();
            value.Direction = reader.ReadByte();
            value.GPSTime = reader.ReadDateTime6();
            // 位置附加信息
            value.BasicLocationAttachData = new Dictionary<byte, JT905_0x0200_BodyBase>();
            value.CustomLocationAttachData = new Dictionary<byte, JT905_0x0200_CustomBodyBase>();
            value.CustomLocationAttachData2 = new Dictionary<ushort, JT905_0x0200_CustomBodyBase2>();
            value.CustomLocationAttachData3 = new Dictionary<ushort, JT905_0x0200_CustomBodyBase3>();
            value.CustomLocationAttachData4 = new Dictionary<byte, JT905_0x0200_CustomBodyBase4>();
            value.ExceptionLocationAttachOriginalData = new List<byte[]>();
            value.UnknownLocationAttachData = new Dictionary<ushort, byte[]>();
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                try
                {
                    //正常自定义注册、正常数据解析，不支持国标乱序组包
                    //优先国标组包->自定义附加数据注册->未知/异常数据
                    //注意：最坏的是自定义的跟基础标准的附加信息Id冲突了,那么优先使用标准的进行解析
                    //基础标准附加Id、自定义标准附加Id、自定义标准附加Id 4
                    byte attachId = reader.ReadVirtualByte();
                    //自定义标准附加Id2、自定义标准附加Id3
                    ushort attachId2_3 = reader.ReadVirtualUInt16();
                    if (config.JT905_0X0200_Factory.Map.TryGetValue(attachId, out object attachInstance))
                    {
                        if (value.BasicLocationAttachData.ContainsKey(attachId))
                        {
                            //存在重复的就不解析，把数据统一放在异常定位数据里面
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            value.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            dynamic attachImpl = JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(attachInstance, ref reader, config);
                            value.BasicLocationAttachData.Add(attachImpl.AttachInfoId, attachImpl);
                        }
                    }
                    else if (config.JT905_0X0200_Custom_Factory.Map.TryGetValue(attachId, out object customAttachInstance))
                    {
                        if (value.CustomLocationAttachData.ContainsKey(attachId))
                        {
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            value.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            dynamic attachImpl = JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(customAttachInstance, ref reader, config);
                            value.CustomLocationAttachData.Add(attachImpl.AttachInfoId, attachImpl);
                        }
                    }
                    else if (config.JT905_0X0200_Custom_Factory.Map2.TryGetValue(attachId2_3, out object customAttachInstance2))
                    {
                        if (value.CustomLocationAttachData2.ContainsKey(attachId2_3))
                        {
                            reader.Skip(2);
                            byte attachLen = reader.ReadByte();
                            value.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 3, attachLen + 3).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            dynamic attachImpl = JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(customAttachInstance2, ref reader, config);
                            value.CustomLocationAttachData2.Add(attachImpl.AttachInfoId, attachImpl);
                        }
                    }
                    else if (config.JT905_0X0200_Custom_Factory.Map4.TryGetValue(attachId, out object customAttachInstance4))
                    {
                        if (value.CustomLocationAttachData4.ContainsKey(attachId))
                        {
                            reader.Skip(1);
                            int attachLen = reader.ReadInt32();
                            value.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 5, attachLen + 5).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            dynamic attachImpl = JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(customAttachInstance4, ref reader, config);
                            value.CustomLocationAttachData4.Add(attachImpl.AttachInfoId, attachImpl);
                        }
                    }
                    else if (config.JT905_0X0200_Custom_Factory.Map3.TryGetValue(attachId2_3, out object customAttachInstance3))
                    {
                        if (value.CustomLocationAttachData3.ContainsKey(attachId2_3))
                        {
                            reader.Skip(2);
                            ushort attachLen = reader.ReadUInt16();
                            value.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 4, attachLen + 4).ToArray());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            dynamic attachImpl = JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(customAttachInstance3, ref reader, config);
                            value.CustomLocationAttachData3.Add(attachImpl.AttachInfoId, attachImpl);
                        }
                    }
                    else
                    {
                        //未知的附加只通过标准的自定义附加信息来解析，其余的通过自己注册，自己实现的方式来解析
                        reader.Skip(1);
                        byte attachLen = reader.ReadByte();
                        int remainLength = reader.ReadCurrentRemainContentLength();
                        if (remainLength < attachLen)
                        {
                            value.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(remainLength).ToArray());
                        }
                        else
                        {
                            if (value.UnknownLocationAttachData.ContainsKey(attachId))
                            {
                                //存在重复的就不解析，把数据统一放在异常定位数据里面
                                value.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                            }
                            else
                            {
                                value.UnknownLocationAttachData.Add(attachId, reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray());
                            }
                            reader.Skip(attachLen);
                        }
                    }
                }
                catch
                {
                    try
                    {
                        var remainLength = reader.ReadCurrentRemainContentLength();
                        if (remainLength > 0)
                        {
                            value.ExceptionLocationAttachOriginalData.Add(reader.ReadArray(remainLength).ToArray());
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0200 value, IJT905Config config)
        {
            writer.WriteUInt32(value.AlarmFlag);
            writer.WriteUInt32(value.StatusFlag);
            //0x10000000 南纬 134217728
            //0x8000000  西经 ‭‬268435456
            //0x18000000 南纬-西经 134217728+268435456
            if (((value.StatusFlag >> 30) & 1) == 1)
            {
                uint lat = (uint)value.Lat;
                writer.WriteUInt32(lat);
            }
            else
            {
                if (value.Lat < 0)
                {
                    throw new JT905Exception(JT905ErrorCode.LatOrLngError, $"Lat {nameof(JT905_0x0200.StatusFlag)} ({value.StatusFlag}>>28) !=1");
                }
                writer.WriteInt32(value.Lat);
            }
            if (((value.StatusFlag >> 29) & 1) == 1)
            {
                uint lng = (uint)value.Lng;
                writer.WriteUInt32(lng);
            }
            else
            {
                if (value.Lng < 0)
                {
                    throw new JT905Exception(JT905ErrorCode.LatOrLngError, $"Lng {nameof(JT905_0x0200.StatusFlag)} ({value.StatusFlag}>>29) !=1");
                }
                writer.WriteInt32(value.Lng);
            }
            writer.WriteUInt16(value.Speed);
            writer.WriteByte(value.Direction);
            writer.WriteDateTime6(value.GPSTime);
            if (value.BasicLocationAttachData != null && value.BasicLocationAttachData.Count > 0)
            {
                foreach (var item in value.BasicLocationAttachData)
                {
                    try
                    {
                        JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(item.Value, ref writer, item.Value, config);
                    }
                    catch
                    {

                    }
                }
            }
            if (value.CustomLocationAttachData != null && value.CustomLocationAttachData.Count > 0)
            {
                foreach (var item in value.CustomLocationAttachData)
                {
                    JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(item.Value, ref writer, item.Value, config);
                }
            }
            if (value.CustomLocationAttachData2 != null && value.CustomLocationAttachData2.Count > 0)
            {
                foreach (var item in value.CustomLocationAttachData2)
                {
                    JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(item.Value, ref writer, item.Value, config);
                }
            }
            if (value.CustomLocationAttachData3 != null && value.CustomLocationAttachData3.Count > 0)
            {
                foreach (var item in value.CustomLocationAttachData3)
                {
                    JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(item.Value, ref writer, item.Value, config);
                }
            }
            if (value.CustomLocationAttachData4 != null && value.CustomLocationAttachData4.Count > 0)
            {
                foreach (var item in value.CustomLocationAttachData4)
                {
                    JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(item.Value, ref writer, item.Value, config);
                }
            }
            if (value.UnknownLocationAttachData != null && value.UnknownLocationAttachData.Count > 0)
            {
                foreach (var item in value.UnknownLocationAttachData)
                {
                    if (item.Value != null && item.Value.Length >= 2)
                    {
                        writer.WriteArray(item.Value);
                    }
                }
            }
            if (value.ExceptionLocationAttachOriginalData != null && value.ExceptionLocationAttachOriginalData.Count > 0)
            {
                foreach (var item in value.ExceptionLocationAttachOriginalData)
                {
                    if (item != null && item.Length >= 2)
                    {
                        writer.WriteArray(item);
                    }
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
            JT905_0x0200 value = new JT905_0x0200();
            value.AlarmFlag = reader.ReadUInt32();
            writer.WriteNumber($"[{value.AlarmFlag.ReadBinary().ToString()}]报警标志", value.AlarmFlag);
            value.StatusFlag = reader.ReadUInt32();
            var alarmFlagBits = Convert.ToString(value.AlarmFlag, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartObject("报警标志对象");
            writer.WriteString("[bit29~31]保留", alarmFlagBits.Slice(0, 3).ToString());
            writer.WriteString($"[bit28]计价器实时时钟超过规定的误差范围", $"{alarmFlagBits[3]}");
            writer.WriteString($"[bit27]录音设备故障", $"{alarmFlagBits[4]}");
            writer.WriteString($"[bit26]ISU存储异常", $"{alarmFlagBits[5]}");
            writer.WriteString($"[bit25]车辆非法位移", $"{alarmFlagBits[6]}");
            writer.WriteString($"[bit24]车辆非法点火", $"{alarmFlagBits[7]}");
            writer.WriteString($"[bit23]车速传感器故障", $"{alarmFlagBits[8]}");
            writer.WriteString($"[bit22]禁行路段行驶", $"{alarmFlagBits[9]}");
            writer.WriteString($"[bit21]路段行驶时间不足/过长", $"{alarmFlagBits[10]}");
            writer.WriteString($"[bit20]进出区域/路线", $"{alarmFlagBits[11]}");
            writer.WriteString($"[bit19]超时停车", $"{alarmFlagBits[12]}");
            writer.WriteString($"[bit18]当天累计驾驶超时", $"{alarmFlagBits[13]}");        
            writer.WriteString($"[bit17]连续驾驶超速", $"{alarmFlagBits[14]}");
            writer.WriteString($"[bit16]超速报警", $"{alarmFlagBits[15]}");
            writer.WriteString($"[bit15]LED顶灯故障", $"{alarmFlagBits[16]}");
            writer.WriteString($"[bit14]安全访问模块故障", $"{alarmFlagBits[17]}");
            writer.WriteString($"[bit13]液晶（LCD）显示屏故障", $"{alarmFlagBits[18]}");
            writer.WriteString($"[bit12]LED广告屏故障", $"{alarmFlagBits[19]}");
            writer.WriteString($"[bit11]服务评价器故障（前后排）", $"{alarmFlagBits[20]}");
            writer.WriteString($"[bit10]计价器故障", $"{alarmFlagBits[21]}");
            writer.WriteString($"[bit9]摄像头故障", $"{alarmFlagBits[22]}");
            writer.WriteString($"[bit8]语音合成（TTS）模块故障", $"{alarmFlagBits[23]}");
            writer.WriteString($"[bit7]液晶（LCD）显示ISU故障", $"{alarmFlagBits[24]}");
            writer.WriteString($"[bit6]ISU主电源掉电", $"{alarmFlagBits[25]}");
            writer.WriteString($"[bit5]ISU主电源欠压", $"{alarmFlagBits[26]}");
            writer.WriteString($"[bit4]卫星定位天线短路", $"{alarmFlagBits[27]}");
            writer.WriteString($"[bit3]卫星定位天线未接或被剪断", $"{alarmFlagBits[28]}");
            writer.WriteString($"[bit2]卫星定位模块发生故障", $"{alarmFlagBits[29]}");
            writer.WriteString($"[bit1]预警", $"{alarmFlagBits[30]}");
            writer.WriteString($"[bit0]紧急报警,触动报警开关后触发", $"{alarmFlagBits[31]}");
            writer.WriteEndObject();
            writer.WriteNumber($"[{value.StatusFlag.ReadBinary().ToString()}]状态位标志", value.StatusFlag);
            var StatusFlagBits = Convert.ToString(value.StatusFlag, 2).PadLeft(32, '0').AsSpan();
            writer.WriteStartObject("状态标志对象");
            writer.WriteString($"[bit15~bit31]保留", StatusFlagBits.Slice(0, 17).ToString());
            writer.WriteString($"[{StatusFlagBits[17]}]bit14", StatusFlagBits[17] == '0' ? "未到达限制营运次数/时间" : "已达到限制营运次数/时间");
            writer.WriteString($"[{StatusFlagBits[18]}]bit13", StatusFlagBits[18] == '0' ? "车辆未锁定" : "车辆锁定");
            writer.WriteString($"[{StatusFlagBits[19]}]bit12", StatusFlagBits[19] == '0' ? "车门解锁" : "车门加锁");
            writer.WriteString($"[{StatusFlagBits[20]}]bit11", StatusFlagBits[20] == '0' ? "车辆电路正常" : "车辆电路断开");
            writer.WriteString($"[{StatusFlagBits[21]}]bit10", StatusFlagBits[21] == '0' ? "车辆油路正常" : "车辆油路断开");
            writer.WriteString($"[{StatusFlagBits[22]}]bit9", StatusFlagBits[22] == '0' ? "空车" : "重车");
            writer.WriteString($"[{StatusFlagBits[23]}]bit8", StatusFlagBits[23] == '0' ? "ACC关" : "ACC开");
            writer.WriteString($"[bit7]保留", StatusFlagBits[24].ToString());
            writer.WriteString($"[{StatusFlagBits[25]}]bit6", StatusFlagBits[25] == '0' ? "默认" : "重转空");
            writer.WriteString($"[{StatusFlagBits[26]}]bit5", StatusFlagBits[26] == '0' ? "默认" : "空转重");
            writer.WriteString($"[{StatusFlagBits[27]}]bit4", StatusFlagBits[27] == '0' ? "未预约" : "预约(任务车)");
            writer.WriteString($"[{StatusFlagBits[28]}]bit3", StatusFlagBits[28] == '0' ? "营运状态" : "停运状态");
            writer.WriteString($"[{StatusFlagBits[29]}]bit2", StatusFlagBits[29] == '0' ? "东经" : "西经");
            writer.WriteString($"[{StatusFlagBits[30]}]bit1", StatusFlagBits[30] == '0' ? "北纬" : "南纬");
            writer.WriteString($"[{StatusFlagBits[31]}]bit0", StatusFlagBits[31] == '0' ? "已卫星定位" : "未卫星定位");
            writer.WriteEndObject();
            if (((value.StatusFlag >> 30) & 1) == 1)
            {   //南纬 268435456 0x10000000
                value.Lat = (int)reader.ReadUInt32();
                writer.WriteNumber($"[{value.Lat.ReadNumber()}]纬度", value.ConvertLat);
            }
            else
            {
                value.Lat = reader.ReadInt32();
                writer.WriteNumber($"[{value.Lat.ReadNumber()}]纬度", value.ConvertLat);
            }
            if (((value.StatusFlag >> 29) & 1) == 1)
            {   //西经 ‭134217728‬ 0x8000000
                value.Lng = (int)reader.ReadUInt32();
                writer.WriteNumber($"[{value.Lng.ReadNumber()}]经度", value.ConvertLng);
            }
            else
            {
                value.Lng = reader.ReadInt32();
                writer.WriteNumber($"[{value.Lng.ReadNumber()}]经度", value.ConvertLng);
            }

            value.Speed = reader.ReadUInt16();
            writer.WriteNumber($"[{value.Speed.ReadNumber()}]速度", value.Speed);
            value.Direction = reader.ReadByte();
            writer.WriteNumber($"[{value.Direction.ReadNumber()}]方向", value.Direction);
            value.GPSTime = reader.ReadDateTime6();
            writer.WriteString($"[{value.GPSTime:yyMMddHHmmss}]定位时间", value.GPSTime.ToString("yyyy-MM-dd HH:mm:ss"));
            // 位置附加信息
            value.BasicLocationAttachData = new Dictionary<byte, JT905_0x0200_BodyBase>();
            value.CustomLocationAttachData = new Dictionary<byte, JT905_0x0200_CustomBodyBase>();
            value.CustomLocationAttachData2 = new Dictionary<ushort, JT905_0x0200_CustomBodyBase2>();
            value.CustomLocationAttachData3 = new Dictionary<ushort, JT905_0x0200_CustomBodyBase3>();
            value.CustomLocationAttachData4 = new Dictionary<byte, JT905_0x0200_CustomBodyBase4>();
            value.ExceptionLocationAttachOriginalData = new List<byte[]>();
            writer.WriteStartArray("附加信息列表");
            while (reader.ReadCurrentRemainContentLength() > 0)
            {
                try
                {
                    //正常自定义注册、正常数据解析，不支持国标乱序组包
                    //优先国标组包->自定义附加数据注册->异常数据
                    //注意：最坏的是自定义的跟基础标准的附加信息Id冲突了,那么优先使用标准的进行解析
                    //基础标准附加Id、自定义标准附加Id、自定义标准附加Id 4
                    byte attachId = reader.ReadVirtualByte();
                    //自定义标准附加Id2、自定义标准附加Id3
                    ushort attachId2_3 = reader.ReadVirtualUInt16();
                    if (config.JT905_0X0200_Factory.Map.TryGetValue(attachId, out object attachInstance))
                    {
                        if (value.BasicLocationAttachData.ContainsKey(attachId))
                        {
                            //存在重复的就不解析，把数据统一放在异常定位数据里面
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            attachInstance.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.BasicLocationAttachData.Add(attachId, null);
                        }
                    }
                    else if (config.JT905_0X0200_Custom_Factory.Map.TryGetValue(attachId, out object customAttachInstance))
                    {
                        if (value.CustomLocationAttachData.ContainsKey(attachId))
                        {
                            reader.Skip(1);
                            byte attachLen = reader.ReadByte();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息_{attachId}", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            customAttachInstance.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.CustomLocationAttachData.Add(attachId, null);
                        }
                    }
                    else if (config.JT905_0X0200_Custom_Factory.Map4.TryGetValue(attachId, out object customAttachInstance4))
                    {
                        if (value.CustomLocationAttachData4.ContainsKey(attachId))
                        {
                            reader.Skip(1);
                            int attachLen = reader.ReadInt32();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息1_4_{attachId}", reader.ReadArray(reader.ReaderCount - 5, attachLen + 5).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            customAttachInstance4.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.CustomLocationAttachData4.Add(attachId, null);
                        }
                    }
                    else if (config.JT905_0X0200_Custom_Factory.Map2.TryGetValue(attachId2_3, out object customAttachInstance2))
                    {
                        if (value.CustomLocationAttachData2.ContainsKey(attachId2_3))
                        {
                            reader.Skip(2);
                            byte attachLen = reader.ReadByte();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息2_1", reader.ReadArray(reader.ReaderCount - 3, attachLen + 3).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            customAttachInstance2.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.CustomLocationAttachData2.Add(attachId2_3, null);
                        }
                    }
                    else if (config.JT905_0X0200_Custom_Factory.Map3.TryGetValue(attachId2_3, out object customAttachInstance3))
                    {
                        if (value.CustomLocationAttachData3.ContainsKey(attachId2_3))
                        {
                            reader.Skip(2);
                            ushort attachLen = reader.ReadUInt16();
                            writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                            writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                            writer.WriteString($"未知附加信息2_2_{attachId}", reader.ReadArray(reader.ReaderCount - 4, attachLen + 4).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        else
                        {
                            writer.WriteStartObject();
                            customAttachInstance3.Analyze(ref reader, writer, config);
                            writer.WriteEndObject();
                            value.CustomLocationAttachData3.Add(attachId2_3, null);
                        }
                    }
                    else
                    {
                        //未知的附加只通过标准的自定义附加信息来解析，其余的通过自己注册，自己实现的方式来解析
                        reader.Skip(1);
                        byte attachLen = reader.ReadByte();
                        int remainLength = reader.ReadCurrentRemainContentLength();
                        writer.WriteStartObject();
                        writer.WriteNumber($"[{attachId.ReadNumber()}]未知附加信息Id", attachId);
                        writer.WriteNumber($"[{attachLen.ReadNumber()}]未知附加信息长度", attachLen);
                        if ((attachLen + 2) > remainLength)
                        {
                            writer.WriteString($"未知附加信息", reader.ReadArray(remainLength).ToArray().ToHexString());
                        }
                        else
                        {
                            writer.WriteString($"未知附加信息", reader.ReadArray(reader.ReaderCount - 2, attachLen + 2).ToArray().ToHexString());
                            reader.Skip(attachLen);
                        }
                        writer.WriteEndObject();
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteStartObject();
                    writer.WriteString($"解析外部部未知附加信息报错", ex.StackTrace);
                    try
                    {
                        var remainLength = reader.ReadCurrentRemainContentLength();
                        if (remainLength > 0)
                        {
                            writer.WriteString($"未知附加信息", reader.ReadArray(remainLength).ToArray().ToHexString());
                        }
                        else
                        {
                            writer.WriteStartObject();
                            writer.WriteString($"未知附加信息", "无");
                            writer.WriteEndObject();
                        }
                    }
                    catch (Exception innerEx)
                    {
                        writer.WriteString($"解析内部未知附加信息报错", innerEx.StackTrace);
                    }
                    finally
                    {
                        writer.WriteEndObject();
                    }
                    break;
                }
            }
            writer.WriteEndArray();
        }
    }
}
