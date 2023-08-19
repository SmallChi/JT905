using System;
using System.Globalization;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 运价参数查询应答
    /// </summary>
    public class Taximeter_0x0004_Up : Body<Taximeter_0x0004_Up>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Up;

        /// <inheritdoc/>
        public override ushort MessageId => 0x0004;

        /// <summary>
        /// 启用时间
        /// </summary>
        public DateTime EnableDateTime { get; set; }

        /// <summary>
        /// 白天往返单价
        /// </summary>
        public double DayRoundtripPrice { get; set; }

        /// <summary>
        /// 夜间往返单价
        /// </summary>
        public double NightRoundtripPrice { get; set; }

        /// <summary>
        /// 白天单程单价
        /// </summary>
        public double DayOneWayPrice { get; set; }

        /// <summary>
        /// 夜间单程单价
        /// </summary>
        public double NightOneWayPrice { get; set; }

        /// <summary>
        /// 白天二次空贴单价
        /// </summary>
        public double DaySecondEmptyTagPrice { get; set; }

        /// <summary>
        /// 夜间二次空贴单价
        /// </summary>
        public double NightSecondEmptyTagPrice { get; set; }

        /// <summary>
        /// 白天起步单价
        /// </summary>
        public double DayInitialPrice { get; set; }

        /// <summary>
        /// 夜间起步单价
        /// </summary>
        public double NightInitialPrice { get; set; }

        /// <summary>
        /// 续程里程数
        /// </summary>
        public double ContinuationMileage { get; set; }

        /// <summary>
        /// 起程公里
        /// </summary>
        public double InitialKilometers { get; set; }

        /// <summary>
        /// 二次空贴公里
        /// </summary>
        public double SecondEmptyTagKilometers { get; set; }

        /// <summary>
        /// 白天等候时间单价
        /// </summary>
        public double DaylightWaitingPrice { get; set; }

        /// <summary>
        /// 夜间等候时间单价
        /// </summary>
        public double NightWaitingPrice { get; set; }

        /// <summary>
        /// 免费等候时间
        /// </summary>
        public TimeOnly FreeWaitingTime { get; set; }

        /// <summary>
        /// 加价时间
        /// </summary>
        public TimeOnly SurchargeTime { get; set; }

        /// <summary>
        /// 夜间开始时间
        /// </summary>
        public TimeOnly NighttimeStartTime { get; set; }

        /// <summary>
        /// 夜间结束时间
        /// </summary>
        public TimeOnly NighttimeEndTime { get; set; }

        /// <summary>
        /// 系统预留
        /// </summary>
        public byte[] RUF { get; set; }

        /// <summary>
        /// 厂商自定义扩展
        /// </summary>
        public byte[] Customize { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x0004_Up Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return new()
            {
                EnableDateTime = DateTime.ParseExact(reader.ReadHex(5), "YYYYMMddHH", null, DateTimeStyles.None),
                DayRoundtripPrice = (int.TryParse(reader.ReadHex(2), out var dayRoundtripPrice) ? dayRoundtripPrice : 0) / 100,
                NightRoundtripPrice = (int.TryParse(reader.ReadHex(2), out var nightRoundtripPrice) ? nightRoundtripPrice : 0) / 100,
                DayOneWayPrice = (int.TryParse(reader.ReadHex(2), out var dayOneWayPrice) ? dayOneWayPrice : 0) / 100,
                NightOneWayPrice = (int.TryParse(reader.ReadHex(2), out var nightOneWayPrice) ? nightOneWayPrice : 0) / 100,
                DaySecondEmptyTagPrice = (int.TryParse(reader.ReadHex(2), out var daySecondEmptyTagPrice) ? daySecondEmptyTagPrice : 0) / 100,
                NightSecondEmptyTagPrice = (int.TryParse(reader.ReadHex(2), out var nightSecondEmptyTagPrice) ? nightSecondEmptyTagPrice : 0) / 100,
                DayInitialPrice = (int.TryParse(reader.ReadHex(2), out var dayInitialPrice) ? dayInitialPrice : 0) / 100,
                NightInitialPrice = (int.TryParse(reader.ReadHex(2), out var nightInitialPrice) ? nightInitialPrice : 0) / 100,
                ContinuationMileage = (int.TryParse(reader.ReadHex(2), out var continuationMileage) ? continuationMileage : 0) / 100,
                InitialKilometers = (int.TryParse(reader.ReadHex(2), out var initialKilometers) ? initialKilometers : 0) / 100,
                SecondEmptyTagKilometers = (int.TryParse(reader.ReadHex(2), out var secondEmptyTagKilometers) ? secondEmptyTagKilometers : 0) / 100,
                DaylightWaitingPrice = (int.TryParse(reader.ReadHex(2), out var daylightWaitingPrice) ? daylightWaitingPrice : 0) / 100,
                NightWaitingPrice = (int.TryParse(reader.ReadHex(2), out var nightWaitingPrice) ? nightWaitingPrice : 0) / 100,
                FreeWaitingTime = TimeOnly.ParseExact(reader.ReadHex(2), "mmss"),
                SurchargeTime = TimeOnly.ParseExact(reader.ReadHex(2), "mmss"),
                NighttimeStartTime = TimeOnly.ParseExact(reader.ReadHex(2), "HHmm"),
                NighttimeEndTime = TimeOnly.ParseExact(reader.ReadHex(2), "HHmm"),
                RUF = reader.ReadArray(22).ToArray(),
                Customize = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray()
            };
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteHex(EnableDateTime.ToString("YYYYMMddHH"));
            writer.WriteHex($"{DayRoundtripPrice * 100:0000}");
            writer.WriteHex($"{NightRoundtripPrice * 100:0000}");
            writer.WriteHex($"{DayOneWayPrice * 100:0000}");
            writer.WriteHex($"{NightOneWayPrice * 100:0000}");
            writer.WriteHex($"{DaySecondEmptyTagPrice * 100:0000}");
            writer.WriteHex($"{NightSecondEmptyTagPrice * 100:0000}");
            writer.WriteHex($"{DayInitialPrice * 100:0000}");
            writer.WriteHex($"{NightInitialPrice * 100:0000}");
            writer.WriteHex($"{ContinuationMileage * 100:0000}");
            writer.WriteHex($"{InitialKilometers * 100:0000}");
            writer.WriteHex($"{SecondEmptyTagKilometers * 100:0000}");
            writer.WriteHex($"{DaylightWaitingPrice * 100:0000}");
            writer.WriteHex($"{NightWaitingPrice * 100:0000}");
            writer.WriteHex($"{FreeWaitingTime:mmss}");
            writer.WriteHex($"{SurchargeTime:mmss}");
            writer.WriteHex($"{NighttimeStartTime:HHmm}");
            writer.WriteHex($"{NighttimeEndTime:HHmm}");
            writer.WriteArray(RUF);
            writer.WriteArray(Customize);
        }
    }
}