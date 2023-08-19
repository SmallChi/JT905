using System;
using System.Globalization;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 单次营运开始通知指令
    /// </summary>
    public class Taximeter_0x00E7_Up : Body<Taximeter_0x00E7_Up>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Up;

        /// <inheritdoc/>
        public override ushort MessageId => 0x00E7;

        /// <summary>
        /// 进入重车时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x00E7_Up Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return new()
            {
                DateTime = DateTime.ParseExact(reader.ReadHex(7), "YYYYMMddHHmmss", null, DateTimeStyles.None)
            };
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteHex(DateTime.ToString("YYYYMMddHHmmss"));
        }
    }
}