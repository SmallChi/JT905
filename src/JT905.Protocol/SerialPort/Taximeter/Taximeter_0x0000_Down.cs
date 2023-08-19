using System;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 计价器状态查询
    /// <para>设备发往计价器</para>
    /// </summary>
    public class Taximeter_0x0000_Down : Body<Taximeter_0x0000_Down>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Down;

        /// <inheritdoc/>
        public override ushort MessageId => 0x0000;

        /// <summary>
        /// ISU当前时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x0000_Down Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return new()
            {
                DateTime = reader.ReadDateTime7()
            };
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteDateTime7(DateTime);
        }
    }
}