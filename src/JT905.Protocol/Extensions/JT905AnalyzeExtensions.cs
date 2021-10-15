using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.Extensions
{
    /// <summary>
    /// JT905分析器扩展
    /// </summary>
    public static class JT905AnalyzeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public static void Analyze(this object instance, ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            if(instance is IJT905Analyze analyze)
            {
                analyze.Analyze(ref reader, writer, config);
            }
            else
            {
                throw new NotImplementedException($"Not Implemented {instance.GetType().FullName} {nameof(IJT905Analyze)}");
            }
        }
    }
}
