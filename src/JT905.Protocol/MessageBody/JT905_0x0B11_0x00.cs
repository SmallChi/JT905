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
    public class JT905_0x0B11_0x00 : JT905_0x0B11_TLV, IJT905MessagePackFormatter<JT905_0x0B11_0x00>, IJT905Analyze
    {
        public override byte DeviceType { get; set; } = (byte)Enums.JT905DeviceType.ISU;
        public override byte Length { get; set; }
        public override string SerialNumber { get; set; }
        public override string HardwareVer { get; set; }
        public override string SoftVer { get; set; }
        /// <summary>
        /// ISU设备状态
        /// </summary>

        public Enums.JT905Status ISUStatus { get; set; }
        /// <summary>
        /// ISU报警标志
        /// </summary>

        public Enums.JT905Alarm ISUAlarmFlag { get; set; }
        /// <summary>
        /// 签到缓存数据条数
        /// 未成功上传的签到记录数
        /// </summary>
        public byte CheckInCache { get; set; }
        /// <summary>
        /// 签退缓存数据条数
        /// 未成功上传的签退记录数
        /// </summary>
        public byte CheckOutCache { get; set; }

        /// <summary>
        /// 营运记录数据条数
        /// 未成功上传的营运记录数
        /// </summary>
        public byte OperationRecordCache { get; set; }
        /// <summary>
        /// 一卡通交易缓存条数
        /// 未上传成功的一卡通交易记录数
        /// </summary>

        public byte ChinaT_unionDealCache { get; set; }


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0B11_0x00 value = new JT905_0x0B11_0x00();
            value.DeviceType=reader.ReadByte();
            writer.WriteNumber($"[{value.DeviceType.ReadNumber()}]设备类型", value.DeviceType);
            value.Length = reader.ReadByte();
            writer.WriteNumber($"[{value.Length.ReadNumber()}]设备巡检结果的长度", value.Length);
            value.SerialNumber = reader.ReadBCD(10,false);
            writer.WriteString($"[{value.SerialNumber}]设备序列号",value.SerialNumber);
            value.HardwareVer = reader.ReadBCD(2,false);
            writer.WriteString($"[{value.HardwareVer}]硬件版本号", value.HardwareVer);
            value.SoftVer = reader.ReadBCD(4, false);
            writer.WriteString($"[{value.SoftVer}]软件版本号", value.SoftVer);
            value.ISUStatus = (Enums.JT905Status)reader.ReadInt32();
            writer.WriteNumber($"[{value.ISUStatus}]ISU设备状态", (uint)value.ISUStatus);
            value.ISUAlarmFlag = (Enums.JT905Alarm)reader.ReadInt32();
            uint iSUAlarmFlag = ((uint)value.ISUAlarmFlag);
            writer.WriteNumber($"[{iSUAlarmFlag.ReadNumber()}]ISU报警标志", (uint)value.ISUAlarmFlag);
            value.CheckInCache=reader.ReadByte();
            writer.WriteNumber($"[{value.CheckInCache.ReadNumber()}]签到缓存数据条数", (uint)value.CheckInCache);
            value.CheckOutCache = reader.ReadByte();
            writer.WriteNumber($"[{value.CheckOutCache.ReadNumber()}]签退缓存数据条数", (uint)value.CheckOutCache);
            value.OperationRecordCache = reader.ReadByte();
            writer.WriteNumber($"[{value.OperationRecordCache.ReadNumber()}]营运记录数据条数", (uint)value.OperationRecordCache);
            value.ChinaT_unionDealCache = reader.ReadByte();
            writer.WriteNumber($"[{value.ChinaT_unionDealCache.ReadNumber()}]一卡通交易缓存条数", (uint)value.ChinaT_unionDealCache);
        }

        public JT905_0x0B11_0x00 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B11_0x00 value = new JT905_0x0B11_0x00();
            value.DeviceType = reader.ReadByte();
            value.Length = reader.ReadByte();
            value.SerialNumber = reader.ReadBCD(10, false);
            value.HardwareVer = reader.ReadBCD(2, false);
            value.SoftVer = reader.ReadBCD(4, false);
            value.ISUStatus = (Enums.JT905Status)reader.ReadInt32();
            value.ISUAlarmFlag = (Enums.JT905Alarm)reader.ReadInt32();
            value.CheckInCache = reader.ReadByte();
            value.CheckOutCache = reader.ReadByte();
            value.OperationRecordCache = reader.ReadByte();
            value.ChinaT_unionDealCache = reader.ReadByte();
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B11_0x00 value, IJT905Config config)
        {
            writer.WriteByte(value.DeviceType);
            writer.WriteByte(value.Length);
            writer.WriteBCD(value.SerialNumber,10);
            writer.WriteBCD(value.HardwareVer, 2);
            writer.WriteBCD(value.SoftVer, 4);
            writer.WriteUInt32((uint)value.ISUStatus);
            writer.WriteUInt32((uint)value.ISUAlarmFlag);
            writer.WriteByte(value.CheckInCache);
            writer.WriteByte(value.CheckOutCache);
            writer.WriteByte(value.OperationRecordCache);
            writer.WriteByte(value.ChinaT_unionDealCache);
        }
    }
}



