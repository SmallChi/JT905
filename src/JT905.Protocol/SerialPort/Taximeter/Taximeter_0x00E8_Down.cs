using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 单次营运结束后营运数据发送应答
    /// </summary>
    public class Taximeter_0x00E8_Down : Body<Taximeter_0x00E8_Down>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Down;

        /// <inheritdoc/>
        public override ushort MessageId => 0x00E8;

        /// <summary>
        /// 操作结果
        /// </summary>
        public Results Result { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x00E8_Down Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return new()
            {
                Result = (Results)reader.ReadByte()
            };
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteByte((byte)Result);
        }

        /// <summary>
        /// 操作结果
        /// </summary>
        public enum Results
        {
            /// <summary>
            /// 执行正确
            /// </summary>
            Success = 0x90,
            /// <summary>
            /// 执行错误
            /// </summary>
            Fail = 0xFF
        }
    }
}