using JT905.Protocol.Enums;
using JT905.Protocol.Exceptions;
using JT905.Protocol.Interfaces;
using System;
using System.Collections.Concurrent;

namespace JT905.Protocol
{
    /// <summary>
    /// JT905配置扩展
    /// </summary>
    public static class JT905ConfigExtensions
    {
        private readonly static ConcurrentDictionary<string, JT905Serializer> JT905SerializerDict = new ConcurrentDictionary<string, JT905Serializer>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 通过类型获取对应的消息序列化器
        /// </summary>
        /// <param name="JT905Config"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetMessagePackFormatterByType(this IJT905Config JT905Config,Type type)
        {
            if (!JT905Config.FormatterFactory.FormatterDict.TryGetValue(type.GUID, out var formatter))
            {
                throw new JT905Exception(JT905ErrorCode.NotGlobalRegisterFormatterAssembly, type.FullName);
            }
            return formatter;
        }
        /// <summary>
        /// 通过类型获取对应的消息分析器
        /// </summary>
        /// <param name="JT905Config"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetAnalyzeByType(this IJT905Config JT905Config, Type type)
        {
            if (!JT905Config.FormatterFactory.FormatterDict.TryGetValue(type.GUID, out var analyze))
            {
                throw new JT905Exception(JT905ErrorCode.NotGlobalRegisterFormatterAssembly, type.FullName);
            }
            return analyze;
        }
        /// <summary>
        /// 获取对应的消息序列化器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JT905Config"></param>
        /// <returns></returns>
        public static IJT905MessagePackFormatter<T> GetMessagePackFormatter<T>(this IJT905Config JT905Config)
        {
            return (IJT905MessagePackFormatter<T>)GetMessagePackFormatterByType(JT905Config,typeof(T));
        }
        /// <summary>
        /// 获取对应的消息分析器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="JT905Config"></param>
        /// <returns></returns>
        public static IJT905Analyze GetAnalyze<T>(this IJT905Config JT905Config)
        {
            return (IJT905Analyze)GetAnalyzeByType(JT905Config, typeof(T));
        }
    }
}
