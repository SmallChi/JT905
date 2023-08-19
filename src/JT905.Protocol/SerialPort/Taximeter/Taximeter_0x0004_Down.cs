using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 运价参数查询
    /// </summary>
    public class Taximeter_0x0004_Down : Body<Taximeter_0x0004_Down>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Down;

        /// <inheritdoc/>
        public override ushort MessageId => 0x0004;

        /// <inheritdoc/>
        public override Taximeter_0x0004_Down Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return new();
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
        }
    }
}