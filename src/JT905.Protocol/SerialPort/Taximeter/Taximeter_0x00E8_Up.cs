using System;
using System.Globalization;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 单次营运结束后营运数据
    /// </summary>
    public class Taximeter_0x00E8_Up : Body<Taximeter_0x00E8_Up>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Up;

        /// <inheritdoc/>
        public override ushort MessageId => 0x00E8;

        /// <summary>
        /// 车牌号
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// 营运许可证号
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// 驾驶员从业资格证号
        /// </summary>
        public string QualificationCertificate { get; set; }

        /// <summary>
        /// 上车时间
        /// </summary>
        public DateTime BeginDateTime { get; set; }

        /// <summary>
        /// 下车时间
        /// </summary>
        public TimeOnly EndTime { get; set; }

        /// <summary>
        /// 计程
        /// </summary>
        public decimal Mileage { get; set; }

        /// <summary>
        /// 空驶
        /// </summary>
        public decimal EmptyDrive { get; set; }

        /// <summary>
        /// 附加费
        /// </summary>
        public decimal AdditionalFee { get; set; }

        /// <summary>
        /// 等待计时
        /// </summary>
        public TimeSpan WaitTime { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 当前车次
        /// </summary>
        public uint Section { get; set; }

        /// <summary>
        /// 交易类型
        /// <list type="table">
        /// <listheader>
        /// <term>设备类型代码</term>
        /// <term>设备名称</term>
        /// </listheader>
        /// <item>
        /// <term>0x00</term>
        /// <term>现金交易</term>
        /// </item>
        /// <item>
        /// <term>0x01</term>
        /// <term>M1卡交易</term>
        /// </item>
        /// <item>
        /// <term>0x03</term>
        /// <term>CPU卡交易</term>
        /// </item>
        /// <item>
        /// <term>0x09</term>
        /// <term>其他交易</term>
        /// </item>
        /// </list>
        /// </summary>
        public byte TradeType { get; set; }

        /// <summary>
        /// 一卡通数据
        /// </summary>
        public byte[] IntegrationOfTransportationCard { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x00E8_Up Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            var result = new Taximeter_0x00E8_Up();
            result.LicensePlate = reader.ReadString(6);
            result.License = reader.ReadString(16);
            result.QualificationCertificate = reader.ReadString(19);
            DateTime.TryParseExact(reader.ReadHex(5), "yyMMddHHmm", null, DateTimeStyles.None, out var dateTime);
            result.BeginDateTime = dateTime;
            result.EndTime = TimeOnly.Parse($"{int.Parse(reader.ReadHex(1)):D2}:{int.Parse(reader.ReadHex(1))}:00");
            result.Mileage = int.TryParse(reader.ReadBCD(6), out var mileage) ? mileage / 10m : 0;
            result.EmptyDrive = int.TryParse(reader.ReadBCD(4), out var emptyDrive) ? emptyDrive / 10m : 0;
            result.AdditionalFee = int.TryParse(reader.ReadBCD(6), out var additionalFee) ? additionalFee / 10m : 0;
            result.WaitTime = TimeSpan.Parse($"{reader.ReadBCD(2).PadLeft(2, '0')}:{reader.ReadBCD(2).PadLeft(2, '0')}:00");
            result.Amount = int.TryParse(reader.ReadBCD(6), out var amount) ? amount / 10m : 0;
            result.Section = reader.ReadUInt32();
            result.TradeType = reader.ReadByte();
            result.IntegrationOfTransportationCard = reader.ReadContent().ToArray();
            return result;
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteASCII(LicensePlate, 6);
            writer.WriteASCII(License, 16);
            writer.WriteASCII(QualificationCertificate, 19);
            writer.WriteHex(BeginDateTime.ToString("yyMMddHHmm"));
            writer.WriteHex(EndTime.ToString("HHmm"));
            writer.WriteBCD((Mileage * 10).ToString("000000"), 6);
            writer.WriteBCD((EmptyDrive * 10).ToString("0000"), 4);
            writer.WriteBCD((AdditionalFee * 10).ToString("000000"), 6);
            writer.WriteBCD($"{WaitTime.Hours:D2}{WaitTime.Minutes:D2}", 4);
            writer.WriteBCD((Amount * 10).ToString("000000"), 6);
            writer.WriteUInt32(Section);
            writer.WriteByte(TradeType);
            writer.WriteArray(IntegrationOfTransportationCard ?? Array.Empty<byte>());
        }
    }
}