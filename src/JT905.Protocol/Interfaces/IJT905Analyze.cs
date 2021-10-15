using JT905.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// JT905分析器
    /// </summary>
    public interface IJT905Analyze
    {
        /// <summary>
        /// 分析器
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config);
    }
}
