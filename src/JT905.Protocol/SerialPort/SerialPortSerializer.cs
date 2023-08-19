using System;
using JT905.Protocol.Enums;
using JT905.Protocol.Internal;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort
{
    public class SerialPortSerializer
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="JT905Config"></param>
        public SerialPortSerializer(IJT905Config JT905Config)
        {
            this.JT905Config = JT905Config;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public SerialPortSerializer() : this(new DefaultGlobalConfig()) { }

        /// <summary>
        /// 配置标识
        /// </summary>
        public string SerializerId => JT905Config.ConfigId;

        private readonly IJT905Config JT905Config;

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] Serialize(SerialPortPackage package, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                var writer = new JT905SerialPortMessagePackWriter(buffer, version);
                package.Serialize(ref writer, JT905Config);
                return writer.FlushAndGetEncodingArray();
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public SerialPortPackage Deserialize(ReadOnlySpan<byte> bytes, IBody.Types type, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                var JT905MessagePackReader = new JT905SerialPortMessagePackReader(bytes, version);
                return new SerialPortPackage().Deserialize(ref JT905MessagePackReader, type, JT905Config);
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] Serialize<T>(T package, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096) where T : Body<T>, new()
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                var writer = new JT905SerialPortMessagePackWriter(buffer, version);
                package.Serialize(ref writer, JT905Config);
                return writer.FlushAndGetEncodingArray();
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public T Deserialize<T>(ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096) where T : Body<T>, new()
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                var JT905MessagePackReader = new JT905SerialPortMessagePackReader(bytes, version);
                return new T().Deserialize(ref JT905MessagePackReader, JT905Config);
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
    }
}