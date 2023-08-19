using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.Internal;
using JT905.Protocol.MessagePack;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol
{
    /// <summary>
    /// JT905序列化器
    /// </summary>
    public  class JT905Serializer
    {
        private readonly static JT905Package jT905Package = new JT905Package();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="JT905Config"></param>
        public JT905Serializer(IJT905Config JT905Config)
        {
            this.JT905Config = JT905Config;
        }
        /// <summary>
        /// 
        /// </summary>
        public JT905Serializer():this(new DefaultGlobalConfig())
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string SerializerId => JT905Config.ConfigId;

        public readonly IJT905Config JT905Config;

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] Serialize(JT905Package package, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackWriter JT905MessagePackWriter = new JT905MessagePackWriter(buffer, version);
                jT905Package.Serialize(ref JT905MessagePackWriter, package, JT905Config);
                return JT905MessagePackWriter.FlushAndGetEncodingArray();
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> SerializeReadOnlySpan(JT905Package package, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackWriter JT905MessagePackWriter = new JT905MessagePackWriter(buffer, version);
                jT905Package.Serialize(ref JT905MessagePackWriter, package, JT905Config);
                return JT905MessagePackWriter.FlushAndGetEncodingReadOnlySpan();
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public JT905Package Deserialize(ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                JT905MessagePackReader.Decode(buffer);
                return jT905Package.Deserialize(ref JT905MessagePackReader, JT905Config);
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte [] Serialize<T>(T obj, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                var formatter = JT905Config.GetMessagePackFormatter<T>();
                JT905MessagePackWriter JT905MessagePackWriter = new JT905MessagePackWriter(buffer, version);
                formatter.Serialize(ref JT905MessagePackWriter, obj, JT905Config);
                return JT905MessagePackWriter.FlushAndGetEncodingArray();
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> SerializeReadOnlySpan<T>(T obj, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                var formatter = JT905Config.GetMessagePackFormatter<T>();
                JT905MessagePackWriter JT905MessagePackWriter = new JT905MessagePackWriter(buffer, version);
                formatter.Serialize(ref JT905MessagePackWriter, obj, JT905Config);
                return JT905MessagePackWriter.FlushAndGetEncodingReadOnlySpan();
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public T Deserialize<T>(ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                if(CheckPackageType(typeof(T)))
                    JT905MessagePackReader.Decode(buffer);
                var formatter = JT905Config.GetMessagePackFormatter<T>();
                return formatter.Deserialize(ref JT905MessagePackReader, JT905Config);
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool CheckPackageType(Type type)
        {
            return type == typeof(JT905Package) || type == typeof(JT905HeaderPackage);
        }

        /// <summary>
        /// 用于负载或者分布式的时候，在网关只需要解到头部。
        /// 根据头部的消息Id进行分发处理，可以防止小部分性能损耗。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public JT905HeaderPackage HeaderDeserialize(ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                JT905MessagePackReader.Decode(buffer);
                return new JT905HeaderPackage(ref JT905MessagePackReader,JT905Config);
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <param name="version"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public dynamic Deserialize(ReadOnlySpan<byte> bytes, Type type, JT905Version version = JT905Version.JTT2014, int minBufferSize = 4096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                var formatter = JT905Config.GetMessagePackFormatterByType(type);
                JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                if (CheckPackageType(type))
                    JT905MessagePackReader.Decode(buffer);
                return JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(formatter,ref JT905MessagePackReader, JT905Config);
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public string Analyze(ReadOnlySpan<byte> bytes,  JT905Version version = JT905Version.JTT2014, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                JT905MessagePackReader.Decode(buffer);
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                jT905Package.Analyze(ref JT905MessagePackReader, utf8JsonWriter, JT905Config);
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value;
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }       
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public string Analyze<T>(ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                if (CheckPackageType(typeof(T)))
                    JT905MessagePackReader.Decode(buffer);
                var analyze = JT905Config.GetAnalyze<T>();
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteStartObject();
                analyze.Analyze(ref JT905MessagePackReader, utf8JsonWriter, JT905Config);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value;
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 用于分包组合
        /// </summary>
        /// <param name="msgid">对应消息id</param>
        /// <param name="bytes">组合的数据体</param>
        /// <param name="version">对应版本号</param>
        /// <param name="options">序列化选项</param>
        /// <param name="minBufferSize">默认65535</param>
        /// <returns></returns>
        public string Analyze(ushort msgid,ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, JsonWriterOptions options = default, int minBufferSize = 65535)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                if(JT905Config.MsgIdFactory.TryGetValue(msgid,out object msgHandle))
                {
                    if (JT905Config.FormatterFactory.FormatterDict.TryGetValue(msgHandle.GetType().GUID, out object instance))
                    {
                        using MemoryStream memoryStream = new MemoryStream();
                        using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                        JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                        utf8JsonWriter.WriteStartObject();
                        instance.Analyze(ref JT905MessagePackReader, utf8JsonWriter, JT905Config);
                        utf8JsonWriter.WriteEndObject();
                        utf8JsonWriter.Flush();
                        string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                        return value;
                    }
                    return $"未找到对应的0x{msgid:X2}消息数据体类型";
                }
                return $"未找到对应的0x{msgid:X2}消息数据体类型";
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 用于分包组合
        /// </summary>
        /// <param name="msgid">对应消息id</param>
        /// <param name="bytes">组合的数据体</param>
        /// <param name="version">对应版本号</param>
        /// <param name="options">序列化选项</param>
        /// <param name="minBufferSize">默认65535</param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer(ushort msgid, ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, JsonWriterOptions options = default, int minBufferSize = 65535)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                if (JT905Config.MsgIdFactory.TryGetValue(msgid, out object msgHandle))
                {
                    if (JT905Config.FormatterFactory.FormatterDict.TryGetValue(msgHandle.GetType().GUID, out object instance))
                    {
                        using MemoryStream memoryStream = new MemoryStream();
                        using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                        JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                        utf8JsonWriter.WriteStartObject();
                        instance.Analyze(ref JT905MessagePackReader, utf8JsonWriter, JT905Config);
                        utf8JsonWriter.WriteEndObject();
                        utf8JsonWriter.Flush();
                        return memoryStream.ToArray();
                    }
                    return Encoding.UTF8.GetBytes($"未找到对应的0x{msgid:X2}消息数据体类型");
                }
                return Encoding.UTF8.GetBytes($"未找到对应的0x{msgid:X2}消息数据体类型");
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer(ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                JT905MessagePackReader.Decode(buffer);
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                jT905Package.Analyze(ref JT905MessagePackReader, utf8JsonWriter, JT905Config);
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="version"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer<T>(ReadOnlySpan<byte> bytes, JT905Version version = JT905Version.JTT2014, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = JT905ArrayPool.Rent(minBufferSize);
            try
            {
                JT905MessagePackReader JT905MessagePackReader = new JT905MessagePackReader(bytes, version);
                if (CheckPackageType(typeof(T)))
                    JT905MessagePackReader.Decode(buffer);
                var analyze = JT905Config.GetAnalyze<T>();
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteStartObject();
                analyze.Analyze(ref JT905MessagePackReader, utf8JsonWriter, JT905Config);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
            finally
            {
                JT905ArrayPool.Return(buffer);
            }
        }
    }
}
