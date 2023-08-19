using JT905.Protocol.Enums;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 计价器状态查询应答
    /// <para>计价器发往设备</para>
    /// </summary>
    public class Taximeter_0x0000_Up : Body<Taximeter_0x0000_Up>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Up;

        /// <inheritdoc/>
        public override ushort MessageId => 0x0000;

        /// <summary>
        /// 设备厂商编号
        /// </summary>
        public byte TerminalNumber { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public JT905DeviceType TerminalType { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public int TerminalSerial { get; set; }

        /// <summary>
        /// 硬件版本号
        /// </summary>
        public string TerminalVersion { get; set; }

        /// <summary>
        /// 软件主版本号
        /// </summary>
        public string SoftwareMainVersion { get; set; }

        /// <summary>
        /// 软件次版本号
        /// </summary>
        public string SoftwareSlaveVersion { get; set; }

        /// <summary>
        /// 设备状态
        /// <list type="table">
        /// <listheader>
        /// <term>值</term>
        /// <term>说明</term>
        /// </listheader>
        /// <item>
        /// <term>0x00</term>
        /// <term>设备正常</term>
        /// </item>
        /// <item>
        /// <term>0x01</term>
        /// <term>设备限制使用（次数限制）</term>
        /// </item>
        /// <item>
        /// <term>0x02</term>
        /// <term>设备限制使用（日期限制）</term>
        /// </item>
        /// <item>
        /// <term>0x04</term>
        /// <term>营运数据存储满</term>
        /// </item>
        /// <item>
        /// <term>0x08</term>
        /// <term>上下班签到签退信息满</term>
        /// </item>
        /// <item>
        /// <term>其他</term>
        /// <term>设备异常</term>
        /// </item>
        /// </list>
        /// </summary>
        public byte TerminalState { get; set; }

        /// <summary>
        /// 计价器工作状态
        /// <list type="table">
        /// <listheader>
        /// <term>值</term>
        /// <term>说明</term>
        /// </listheader>
        /// <item>
        /// <term>0x00</term>
        /// <term>签到已开机</term>
        /// </item>
        /// <item>
        /// <term>0x01</term>
        /// <term>签退未开机</term>
        /// </item>
        /// <item>
        /// <term>0x10</term>
        /// <term>签到，强制开机</term>
        /// </item>
        /// <item>
        /// <term>0x11</term>
        /// <term>签退，强制关机</term>
        /// </item>
        /// </list>
        /// </summary>
        public byte WorkerState { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// 经营许可证号
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// 驾驶员从业资格证号
        /// </summary>
        public string QualificationCertificate { get; set; }

        /// <summary>
        /// 营运次数
        /// </summary>
        public uint OperateCount { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x0000_Up Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            var result = new Taximeter_0x0000_Up();
            result.TerminalNumber = byte.Parse(reader.ReadBCD(2));
            result.TerminalType = (JT905DeviceType)byte.Parse(reader.ReadBCD(2));
            result.TerminalSerial = int.Parse(reader.ReadBCD(6));
            result.TerminalVersion = reader.ReadBCD(2);
            result.SoftwareMainVersion = reader.ReadBCD(2);
            result.SoftwareSlaveVersion = reader.ReadBCD(2);
            result.TerminalState = reader.ReadByte();
            result.WorkerState = reader.ReadByte();
            result.LicensePlate = reader.ReadString(6);
            result.License = reader.ReadString(16);
            result.QualificationCertificate = reader.ReadString(19);
            result.OperateCount = reader.ReadUInt32();
            return result;
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteBCD($"{TerminalNumber:X2}", 2);
            writer.WriteBCD($"{TerminalType:X2}", 2);
            writer.WriteBCD($"{TerminalSerial:X2}", 6);
            writer.WriteBCD(TerminalVersion, 2);
            writer.WriteBCD(SoftwareMainVersion, 2);
            writer.WriteBCD(SoftwareSlaveVersion, 2);
            writer.WriteByte(TerminalState);
            writer.WriteByte(WorkerState);
            writer.WriteASCII(LicensePlate, 6);
            writer.WriteASCII(License, 16);
            writer.WriteASCII(QualificationCertificate, 19);
            writer.WriteUInt32(OperateCount);
        }
    }
}