using System;
using System.Globalization;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 计价器营运数据补传
    /// </summary>
    public class Taximeter_0x00F2_Up : Body<Taximeter_0x00F2_Up>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Up;

        /// <inheritdoc/>
        public override ushort MessageId => 0x00F2;

        /// <summary>
        /// 营运数据
        /// </summary>
        public Taximeter_0x00E8_Up Data { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x00F2_Up Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            var item = new Taximeter_0x00E8_Up();
            item.LicensePlate = reader.ReadString(6);
            item.License = reader.ReadString(16);
            item.QualificationCertificate = reader.ReadString(19);
            DateTime.TryParseExact(reader.ReadHex(5), "yyMMddHHmm", null, DateTimeStyles.None, out var dateTime);
            item.BeginDateTime = dateTime;
            item.EndTime = TimeOnly.Parse($"{int.Parse(reader.ReadHex(1)):D2}:{int.Parse(reader.ReadHex(1))}:00");
            item.Mileage = int.TryParse(reader.ReadBCD(6), out var mileage) ? mileage / 10m : 0;
            item.EmptyDrive = int.TryParse(reader.ReadBCD(4), out var emptyDrive) ? emptyDrive / 10m : 0;
            item.AdditionalFee = int.TryParse(reader.ReadBCD(6), out var additionalFee) ? additionalFee / 10m : 0;
            item.WaitTime = TimeSpan.Parse($"{reader.ReadBCD(2).PadLeft(2, '0')}:{reader.ReadBCD(2).PadLeft(2, '0')}:00");
            item.Amount = int.TryParse(reader.ReadBCD(6), out var amount) ? amount / 10m : 0;
            item.Section = reader.ReadUInt32();
            item.TradeType = reader.ReadByte();
            item.IntegrationOfTransportationCard = reader.ReadContent().ToArray();
            return new() { Data = item };
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteASCII(Data.LicensePlate, 6);
            writer.WriteASCII(Data.License, 16);
            writer.WriteASCII(Data.QualificationCertificate, 19);
            writer.WriteHex(Data.BeginDateTime.ToString("yyMMddHHmm"));
            writer.WriteHex(Data.EndTime.ToString("HHmm"));
            writer.WriteBCD((Data.Mileage * 10).ToString("000000"), 6);
            writer.WriteBCD((Data.EmptyDrive * 10).ToString("0000"), 4);
            writer.WriteBCD((Data.AdditionalFee * 10).ToString("000000"), 6);
            writer.WriteBCD($"{Data.WaitTime.Hours:D2}{Data.WaitTime.Minutes:D2}", 4);
            writer.WriteBCD((Data.Amount * 10).ToString("000000"), 6);
            writer.WriteUInt32(Data.Section);
            writer.WriteByte(Data.TradeType);
            writer.WriteArray(Data.IntegrationOfTransportationCard ?? Array.Empty<byte>());
        }
    }
}