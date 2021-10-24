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
    /// ISU升级结果报告消息
    /// </summary>
    public class JT905_0x0105 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0105>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.ISU升级结果报告消息;

        public override string Description => "ISU升级结果报告消息";
        /// <summary>
        /// 设备类型
        /// </summary>

        public byte DeviceType { get; set; } = 0x00;
        /// <summary>
        /// 厂商标识
        /// </summary>
        public byte VendorID { get; set; }
        /// <summary>
        /// 硬件版本号
        /// </summary>
        public string HardwareVer { get; set; }
        /// <summary>
        /// 软件版本号
        /// </summary>
        public string SoftVer { get; set; }
        /// <summary>
        /// 升级结果
        /// </summary>
        public byte UpStatus { get; set; } = 0x00;

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            try
            {
                var DeviceType = reader.ReadByte();
                string devName = "";
                switch (DeviceType)
                {
                    case 0x00: devName = "ISU"; break;
                    case 0x01: devName = "通信模块"; break;
                    case 0x02: devName = "计价器"; break;
                    case 0x03: devName = "出租汽车安全模块"; break;
                    case 0x04: devName = "LED显示屏"; break;
                    case 0x05: devName = "智能顶灯"; break;
                    case 0x06: devName = "服务评价器(后排)"; break;
                    case 0x07: devName = "摄像装置"; break;
                    case 0x08: devName = "卫星定位设备"; break;
                    case 0x09: devName = "液晶(LCD)多媒体屏"; break;
                    case 0x10: devName = "ISU人机交互设备"; break;
                    case 0x11: devName = "服务评价器(前排)"; break;
                    default:
                        devName = "RFU";
                        break;
                }
                writer.WriteString($"[{DeviceType.ReadNumber()}]设备类型", devName);
                var VendorID = reader.ReadByte();
                writer.WriteNumber($"[{VendorID.ReadNumber()}]厂商标识", VendorID);
                string HardwareVerHex = reader.ReadVirtualArray(1).ToArray().ToHexString();
                var HardwareVer = reader.ReadBCD(2);
                writer.WriteString($"[{HardwareVer}]硬件版本", HardwareVer);
                string SoftVerrHex = reader.ReadVirtualArray(2).ToArray().ToHexString();
                var SoftVer = reader.ReadBCD(4);
                writer.WriteString($"[{SoftVer}]软件版本", SoftVer);
                var UpStatus = reader.ReadByte();
                writer.WriteNumber($"[{UpStatus}]升级结果：", UpStatus);
            }
            catch (Exception ex)
            {

                writer.WriteString("解析失败", ex.Message.ToString());
            }
        }

        public JT905_0x0105 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0105 jT905_0X0105 = new JT905_0x0105();
            try
            {
                jT905_0X0105.DeviceType = reader.ReadByte();
                jT905_0X0105.VendorID = reader.ReadByte();
                jT905_0X0105.HardwareVer = reader.ReadBCD(2);
                jT905_0X0105.SoftVer = reader.ReadBCD(4);
                jT905_0X0105.UpStatus = reader.ReadByte();
            }
            catch { }
            return jT905_0X0105;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0105 value, IJT905Config config)
        {
            writer.WriteByte(value.DeviceType);
            writer.WriteByte(value.VendorID);
            writer.WriteBCD(value.HardwareVer, 2);
            writer.WriteBCD(value.SoftVer, 4);
            writer.WriteByte(value.UpStatus);
        }
    }
}
