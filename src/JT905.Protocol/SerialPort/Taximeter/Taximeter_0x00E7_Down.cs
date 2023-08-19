using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 单次营运开始通知应答
    /// </summary>
    public class Taximeter_0x00E7_Down : Body<Taximeter_0x00E7_Down>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Down;

        /// <inheritdoc/>
        public override ushort MessageId => 0x00E7;

        /// <summary>
        /// 操作结果
        /// </summary>
        public ResultType Result { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x00E7_Down Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return new()
            {
                Result = (ResultType)reader.ReadByte()
            };
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteByte((byte)Result);
        }

        /// <summary>
        /// 结果类型
        /// </summary>
        public enum ResultType
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