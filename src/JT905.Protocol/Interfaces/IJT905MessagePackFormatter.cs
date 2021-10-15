using JT905.Protocol.MessagePack;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 序列化器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IJT905MessagePackFormatter<T> : IJT905Formatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        void Serialize(ref JT905MessagePackWriter writer, T value, IJT905Config config);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        T Deserialize(ref JT905MessagePackReader reader, IJT905Config config);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IJT905Formatter
    {

    }
}
