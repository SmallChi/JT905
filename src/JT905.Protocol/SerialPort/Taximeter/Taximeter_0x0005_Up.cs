using System;
using System.Globalization;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 运价参数设置应答
    /// </summary>
    public class Taximeter_0x0005_Up : Body<Taximeter_0x0005_Up>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Up;

        /// <inheritdoc/>
        public override ushort MessageId => 0x0005;

        /// <summary>
        /// 操作结果
        /// </summary>
        public ResultType Result { get; set; }

        /// <summary>
        /// 参数数据包中的参数启用时间，YYYYMMDDhh
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x0005_Up Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return new()
            {
                Result = (ResultType)reader.ReadByte(),
                DateTime = DateTime.ParseExact(reader.ReadHex(5), "YYYYMMddHH", null, DateTimeStyles.None)
            };
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteByte((byte)Result);
            writer.WriteHex(DateTime.ToString("YYYYMMddHH"));
        }
        /// <summary>
        /// 结果类型
        /// </summary>
        public enum ResultType
        {
            /// <summary>
            /// 参数下载成功
            /// </summary>
            Success = 0x00,
            /// <summary>
            /// 参数包校验失败，设置失败
            /// </summary>
            FailWithValidationOrConfigFailure = 0x01,
            /// <summary>
            /// 同一版本，无需设置
            /// </summary>
            VersionMatch = 0x02,
            /// <summary>
            /// 设备不支持
            /// </summary>
            deviceNotSupported = 0xff
        }
    }
}