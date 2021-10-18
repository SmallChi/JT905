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
    /// 上班签到信息
    /// </summary>
    public class JT905_0x0B03 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0B03>, IJT905Analyze
    {
        /// <summary>
        /// 0x0B03
        /// </summary>
        public override ushort MsgId { get; } = (ushort)Enums.JT905MsgId.上班签到信息.ToUInt16Value();
        /// <summary>
        /// 上班签到信息
        /// </summary>

        public override string Description => "上班签到信息";
        /// <summary>
        /// 位置基本信息
        /// 详见 0x0200 交易
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
        /// 开机时间
        /// </summary>

        public DateTime Uptime { get; set; }
        /// <summary>
        /// 扩展属性
        /// 可根据实际管理需要进行扩展，当有扩展需求时，则该项有内容，否则该项无内容
        /// </summary>

        public byte[] ExtraAttributes { get; set; }

        /// <summary>
        /// 解析上班签到信息
        /// 0x0B03
        /// </summary>
        /// <param name="reader">JT905消息读取器</param>
        /// <param name="writer"> UTF-8 编码 JSON 在不使用缓存的情况下按顺序写入文本</param>
        /// <param name="config">JT905接口配置</param>

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0B03 value = new JT905_0x0B03();
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
                    //reader.ReadContent(reader.ReadCurrentRemainContentLength());
                }

            }
            //todo:
            var BusinessLicenseNumber = reader.ReadVirtualArray(16).ToArray().ToHexString();
            value.BusinessLicenseNumber = reader.ReadString(16);
            writer.WriteString($"[{BusinessLicenseNumber}]企业经营许可证号",value.BusinessLicenseNumber);
            string QualificationCode = reader.ReadVirtualArray(19).ToArray().ToHexString();
            value.QualificationCode = reader.ReadString(19);
            writer.WriteString($"[{QualificationCode}]驾驶员从业资格证号", value.QualificationCode);
            var PlateNo=reader.ReadVirtualArray(6).ToArray().ToHexString();
            value.PlateNo = reader.ReadString(6);
            writer.WriteString($"[{PlateNo}]车牌号", value.PlateNo);
            value.Uptime = reader.ReadDateTime6Ext();
            writer.WriteString($"[{value.Uptime:yyyyMMddHHmm}]开机时间", value.Uptime.ToString("yyyy-MM-dd HH:mm:ss"));            
            //1
            //通过剩余的长度就知道有没有扩展数据
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
        /// <summary>
        /// 反序列化111
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0B03 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B03 value = new JT905_0x0B03();
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
            //todo:
            value.BusinessLicenseNumber = reader.ReadString(16);
            value.QualificationCode = reader.ReadString(19);
            value.PlateNo = reader.ReadString(6);
            value.Uptime = reader.ReadDateTime6Ext();
            //1
            //通过剩余的长度就知道有没有扩展数据
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.ExtraAttributes = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            }   
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B03 value, IJT905Config config)
        {
            config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer,value.Position,config);
            writer.WriteString(value.BusinessLicenseNumber.PadRight(16,'\0'));
            writer.WriteString(value.QualificationCode.PadRight(19, '\0'));
            if (value.PlateNo.Length > 6)
            {
                int l = value.PlateNo.Length - 6;
                writer.WriteString(value.PlateNo.Substring(l));
            }
            else if(value.PlateNo.Length < 6)
            {
                writer.WriteString(value.PlateNo.PadRight(6, '\0'));
            }
            else
            {
                writer.WriteString(value.PlateNo);
            }

            writer.WriteDateTime6YYYYMMddHHmm(value.Uptime);
            if(value.ExtraAttributes != null && value.ExtraAttributes.Length > 0)
            {
                writer.WriteArray(value.ExtraAttributes);
            }
        }
    }
}
