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
    /// 下班签退信息上传
    /// </summary>
    public class JT905_0x0B04 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0B04>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.下班签退信息上传;

        public override string Description => "下班签退信息上传";

        /// <summary>
        /// 位置基本信息
        /// </summary>
        public JT905_0x0200 Position { get; set; }
        /// <summary>
        /// 企业经营许可证号
        /// </summary>
        public string BusinessLicenseNumber { get; set; }
        /// <summary>
        /// 驾驶员从业资格证号
        /// </summary>
        public string QualificationCode { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 计价器 K 值
        /// </summary>
        public string TaximeterKValue { get; set; }
        /// <summary>
        /// 当班开机时间
        /// </summary>
        public DateTime OnDutyPowerOnTime { get; set; }
        /// <summary>
        /// 当班关机时间
        /// </summary>
        public DateTime OnDutyPowerOffTime { get; set; }
        /// <summary>
        /// 当班里程
        /// </summary>
        public string OnDutyMileage { get; set; }
        /// <summary>
        /// 当班运营里程
        /// </summary>
        public string OnDutyOperationMileage { get; set; }
        /// <summary>
        /// 车次
        /// </summary>
        public string TrainNumber { get; set; }
        /// <summary>
        /// 计时时间
        /// </summary>
        public string TimingTime { get; set; }
        /// <summary>
        /// 总计金额
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// 卡收金额
        /// </summary>
        public string CardAmount { get; set; }
        /// <summary>
        /// 卡次
        /// </summary>
        public string CardCount { get; set; }
        /// <summary>
        /// 班间里程
        /// </summary>
        public string OnDutyMileageBetween { get; set; }
        /// <summary>
        /// 总计里程
        /// </summary>
        public string TotalMileage { get; set; }
        /// <summary>
        /// 总运营里程
        /// </summary>
        public string TotalOperationMileage { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public string UnitPrice { get; set; }
        /// <summary>
        /// 总运营次数
        /// </summary>
        public uint TotalOperations { get; set; }
        /// <summary>
        /// 签退方式
        /// </summary>
        public byte SignType { get; set; }
        /// <summary>
        /// 扩展属性
        /// </summary>
        public byte[] ExtraAttributes { get; set; }




        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0B04 value = new JT905_0x0B04();
            if (reader.ReadCurrentRemainContentLength() >= 25)
            {
                var tempData = reader.ReadVirtualArray(25);
                try
                {
                    JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                    writer.WriteStartObject("位置基本信息");
                    config.GetAnalyze<JT905_0x0200>().Analyze(ref positionReader, writer, config);
                    writer.WriteEndObject();
                    reader.Skip(25);
                }
                catch
                {
                    //todo:异常就读完 标记错误就好le
                    //  PositionError = true;
                    reader.ReadContent(reader.ReadCurrentRemainContentLength());
                }

            }
            var BusinessLicenseNumber = reader.ReadVirtualArray(16).ToArray().ToHexString();
            value.BusinessLicenseNumber = reader.ReadString(16);
            writer.WriteString($"[{BusinessLicenseNumber}]企业经营许可证号", value.BusinessLicenseNumber);
            var QualificationCode = reader.ReadVirtualArray(19).ToArray().ToHexString();
            value.QualificationCode = reader.ReadString(19);
            writer.WriteString($"[{QualificationCode}]驾驶员从业资格证号", value.QualificationCode);
            var PlateNo = reader.ReadVirtualArray(6).ToArray().ToHexString();
            value.PlateNo = reader.ReadString(6);
            writer.WriteString($"[{PlateNo}]车牌号", value.PlateNo);
            var TaximeterKValue = reader.ReadVirtualArray(2).ToArray().ToHexString();
            value.TaximeterKValue = reader.ReadBCD(4);
            writer.WriteString($"[{TaximeterKValue}]计价器 K 值", value.TaximeterKValue);
            value.OnDutyPowerOnTime = reader.ReadDateTime6Ext();
            writer.WriteString($"[{value.OnDutyPowerOnTime:yyyyMMddHHmm}]当班开机时间", value.OnDutyPowerOnTime.ToString("yyyy-MM-dd HH:mm:ss"));
            value.OnDutyPowerOffTime = reader.ReadDateTime6Ext();
            writer.WriteString($"[{value.OnDutyPowerOffTime:yyyyMMddHHmm}]当班关机时间", value.OnDutyPowerOffTime.ToString("yyyy-MM-dd HH:mm:ss"));
            var OnDutyMileage = reader.ReadVirtualArray(3).ToArray().ToHexString();
            value.OnDutyMileage = reader.ReadBCD(6,'.');
            writer.WriteString($"[{OnDutyMileage}]当班里程", value.OnDutyMileage);
            var OnDutyOperationMileage = reader.ReadVirtualArray(3).ToArray().ToHexString();
            value.OnDutyOperationMileage = reader.ReadBCD(6,'.');
            writer.WriteString($"[{OnDutyOperationMileage}]当班运营里程", value.OnDutyOperationMileage);
            var TrainNumber = reader.ReadVirtualArray(2).ToArray().ToHexString();
            value.TrainNumber = reader.ReadBCD(4);
            writer.WriteString($"[{TrainNumber}]车次", value.TrainNumber);
            var TimingTime = reader.ReadVirtualArray(3).ToArray().ToHexString();
            value.TimingTime = reader.ReadBCD(6,false);
            writer.WriteString($"[{TimingTime}]计时时间", value.TimingTime);
            var TotalAmount = reader.ReadVirtualArray(3).ToArray().ToHexString();
            value.TotalAmount = reader.ReadBCD(6,'.');
            writer.WriteString($"[{TotalAmount }]总计金额", value.TotalAmount);
            var CardAmount = reader.ReadVirtualArray(3).ToArray().ToHexString();
            value.CardAmount = reader.ReadBCD(6, '.'); 
            writer.WriteString($"[{CardAmount}]卡收金额", value.CardAmount);
            var CardCount = reader.ReadVirtualArray(2).ToArray().ToHexString();
            value.CardCount = reader.ReadBCD(4,false);
            writer.WriteString($"[{CardCount}]卡次", value.CardCount);
            var OnDutyMileageBetween = reader.ReadVirtualArray(2).ToArray().ToHexString();
            value.OnDutyMileageBetween = reader.ReadBCD(4);
            writer.WriteString($"[{OnDutyMileageBetween}]班间里程", value.OnDutyMileageBetween);
            var TotalMileage = reader.ReadVirtualArray(4).ToArray().ToHexString();
            value.TotalMileage = reader.ReadBCD(8,'.');
            writer.WriteString($"[{TotalMileage}]总计里程", value.TotalMileage);
            var TotalOperationMileage = reader.ReadVirtualArray(4).ToArray().ToHexString();
            value.TotalOperationMileage = reader.ReadBCD(8, '.');
            writer.WriteString($"[{TotalOperationMileage}]总运营里程", value.TotalOperationMileage);
            var UnitPrice = reader.ReadVirtualArray(2).ToArray().ToHexString();
            value.UnitPrice = reader.ReadBCD(4,2,'.');
            writer.WriteString($"[{UnitPrice}]单价", value.UnitPrice);
            value.TotalOperations = reader.ReadUInt32();
            writer.WriteNumber($"[{value.TotalOperations.ReadNumber()}]总运营次数", value.TotalOperations);
            value.SignType = reader.ReadByte();
            writer.WriteNumber($"[{value.SignType.ReadNumber()}]签退方式", value.SignType);
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.ExtraAttributes = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                writer.WriteString($"[{value.ExtraAttributes.ToHexString()}]扩展属性", value.ExtraAttributes.ToHexString());
            }
            else
            {
                writer.WriteString("[]扩展属性", "");
            }

        }

        public JT905_0x0B04 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B04 value = new JT905_0x0B04();
            if (reader.ReadCurrentRemainContentLength() >= 25)
            {
                var tempData = reader.ReadVirtualArray(25);
                try
                {
                    JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                    value.Position = config.GetMessagePackFormatter<JT905_0x0200>().Deserialize(ref positionReader, config);
                    reader.Skip(25);
                }
                catch
                {
                    //todo:异常就读完 标记错误就好le
                    //  PositionError = true;
                    reader.ReadContent(reader.ReadCurrentRemainContentLength());
                    return value;
                }
            }

            value.BusinessLicenseNumber = reader.ReadASCII(16);

            value.QualificationCode = reader.ReadASCII(19);

            value.PlateNo = reader.ReadASCII(6);

            value.TaximeterKValue = reader.ReadBCD(4);
            value.OnDutyPowerOnTime = reader.ReadDateTime6Ext();
            value.OnDutyPowerOffTime = reader.ReadDateTime6Ext();

            value.OnDutyMileage = reader.ReadBCD(6,'.');

            value.OnDutyOperationMileage = reader.ReadBCD(6, '.');

            value.TrainNumber = reader.ReadBCD(4);

            value.TimingTime = reader.ReadBCD(6);

            value.TotalAmount = reader.ReadBCD(6,'.');

            value.CardAmount = reader.ReadBCD(6,'.');

            value.CardCount = reader.ReadBCD(4);

            value.OnDutyMileageBetween = reader.ReadBCD(4,'.');

            value.TotalMileage = reader.ReadBCD(8,'.');

            value.TotalOperationMileage = reader.ReadBCD(8,'.');

            value.UnitPrice = reader.ReadBCD(4,2,'.');
            value.TotalOperations = reader.ReadUInt32();
            value.SignType = reader.ReadByte();
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.ExtraAttributes = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            }


            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B04 value, IJT905Config config)
        {
            config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer, value.Position, config);

            writer.WriteASCII(value.BusinessLicenseNumber.PadRight(16, '\0'));
            writer.WriteASCII(value.QualificationCode.PadRight(19, '\0'));
            if (value.PlateNo.Length > 6)
            {
                int l = value.PlateNo.Length - 6;
                writer.WriteASCII(value.PlateNo.Substring(l));
            }
            else if (value.PlateNo.Length < 6)
            {
                writer.WriteASCII(value.PlateNo.PadRight(6, '\0'));
            }
            else
            {
                writer.WriteASCII(value.PlateNo);
            }
            writer.WriteBCD(value.TaximeterKValue, 4);
            writer.WriteDateTime6YYYYMMddHHmm(value.OnDutyPowerOnTime);
            writer.WriteDateTime6YYYYMMddHHmm(value.OnDutyPowerOffTime);
            if (value.OnDutyMileage.IndexOf(".") > 0)
            {
                value.OnDutyMileage = value.OnDutyMileage.Replace(".", "");
            }
            else
            {
                value.OnDutyMileage += "0";
            }
            if (value.OnDutyMileage.Length > 6)
            {
                int length = value.PlateNo.Length - 6;
                writer.WriteBCD(value.OnDutyMileage, 6);
            }
            else if (value.OnDutyMileage.Length < 6)
            {
                writer.WriteBCD(value.OnDutyMileage, 6);
            }
            else
                writer.WriteBCD(value.OnDutyMileage, 6);

            writer.WriteBCD(value.OnDutyOperationMileage.FormatString(6), 6);
            writer.WriteBCD(value.TrainNumber, 4);
            writer.WriteBCD(value.TimingTime.Replace(":",""), 6);
            writer.WriteBCD(value.TotalAmount.FormatString(6), 6);
            writer.WriteBCD(value.CardAmount.FormatString(6), 6);
           
            writer.WriteBCD(value.CardCount, 4);            
            writer.WriteBCD(value.OnDutyMileageBetween.FormatString(4), 4);
            writer.WriteBCD(value.TotalMileage.FormatString(8), 8);
            writer.WriteBCD(value.TotalOperationMileage.FormatString(8), 8);
            if (!string.IsNullOrEmpty(value.UnitPrice))
            {
                if (value.UnitPrice.IndexOf(".")>0)
                {
                    value.UnitPrice = value.UnitPrice.Replace(".","");
                }
                else
                {
                    value.UnitPrice += "00";
                }
                if (value.UnitPrice.Length>4)
                {
                    value.UnitPrice = value.UnitPrice.PadLeft(4,'0');
                }
            }
            else
            {
                value.UnitPrice = "0000";
            }
            writer.WriteBCD(value.UnitPrice.Replace(".",""), 4);
            writer.WriteUInt32(value.TotalOperations);
            writer.WriteByte(value.SignType);
            if (value.ExtraAttributes != null && value.ExtraAttributes.Length > 0)
            {
                writer.WriteArray(value.ExtraAttributes);
            }

        }
    }
}



