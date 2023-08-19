using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort
{
    /// <summary>
    /// 消息体
    /// </summary>
    public interface IBody
    {
        /// <summary>
        /// 消息id
        /// </summary>
        ushort MessageId { get; }
        /// <summary>
        /// 类型
        /// </summary>
        Types Type { get; }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        object Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config);

        /// <summary>
        /// 上下行类型
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// 上行
            /// <para>外接设备发往ISU</para>
            /// </summary>
            Up,
            /// <summary>
            /// 下行
            /// <para>ISU发往外接设备</para>
            /// </summary>
            Down
        }
    }

    /// <summary>
    /// 消息体接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBody<T> : IBody
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        new void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        new T Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config);
    }

    /// <summary>
    /// 消息体基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Body<T> : IBody<T> where T : class, IBody<T>, new()
    {
        /// <inheritdoc/>
        public abstract IBody.Types Type { get; }
        /// <inheritdoc/>
        public abstract ushort MessageId { get; }

        /// <inheritdoc/>
        public abstract T Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config);
        /// <inheritdoc/>
        public abstract void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config);

        object IBody.Deserialize(ref JT905SerialPortMessagePackReader reader, IJT905Config config)
        {
            return Deserialize(ref reader, config);
        }
    }
}