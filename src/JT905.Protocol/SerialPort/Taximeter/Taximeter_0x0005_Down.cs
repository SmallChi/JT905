using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort.Taximeter
{
    /// <summary>
    /// 运价参数设置
    /// </summary>
    public class Taximeter_0x0005_Down : Body<Taximeter_0x0005_Down>
    {
        /// <inheritdoc/>
        public override IBody.Types Type => IBody.Types.Down;

        /// <inheritdoc/>
        public override ushort MessageId => 0x0005;

        /// <summary>
        /// 参数设置元数据
        /// <para>参数设置应至少包括参数查询中返回的参数，各厂商自定义参数区内容及编码以及参数的启用时间、参数版本等</para>
        /// </summary>
        public byte[] Metadata { get; set; }

        /// <inheritdoc/>
        public override Taximeter_0x0005_Down Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return new()
            {
                Metadata = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray()
            };
        }

        /// <inheritdoc/>
        public override void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteArray(Metadata);
        }
    }
}