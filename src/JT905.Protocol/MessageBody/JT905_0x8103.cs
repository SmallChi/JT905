
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 设置终端参数
    /// </summary>
    public class JT905_0x8103 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8103>, IJT905Analyze
    {
        /// <summary>
        /// 0x8103
        /// </summary>
        public override ushort MsgId { get; } = 0x8103;
        /// <summary>
        /// 设置终端参数
        /// </summary>
        public override string Description => "设置终端参数";
        /// <summary>
        /// 参数总数
        /// </summary>
        internal byte ParamCount
        {
            get
            {
                if (CustomParamList != null)
                {
                    return (byte)(ParamList.Count + CustomParamList.Count);
                }
                return (byte)(ParamList.Count);
            }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        public List<JT905_0x8103_BodyBase> ParamList { get; set; }
        /// <summary>
        /// 自定义参数列表
        /// </summary>
        public List<JT905_0x8103_CustomBodyBase> CustomParamList { get; set; }
        /// <summary>
        /// 0x8103反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            //todo:Deserialize
            JT905_0x8103 value = new JT905_0x8103()
            {
                ParamList = new List<JT905_0x8103_BodyBase>(),
                CustomParamList = new List<JT905_0x8103_CustomBodyBase>()
            };
            //1.参数总数
            var paramCount = reader.ReadByte();
            //2.遍历所有的参数
            try
            {
                for (int i = 0; i < paramCount; i++)
                {
                    var paramId = reader.ReadVirtualUInt16();//参数ID         
                    if (config.JT905_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                    {
                        dynamic attachImpl = JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(instance, ref reader, config);
                        value.ParamList.Add(attachImpl);
                    }
                    else if (config.JT905_0X8103_Custom_Factory.Map.TryGetValue(paramId, out object customInstance))
                    {
                        dynamic attachImpl = JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(customInstance, ref reader, config);
                        value.CustomParamList.Add(attachImpl);
                    }
                }
            }
            catch { }
            return value;
        }
        /// <summary>
        /// 0x8103序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103 value, IJT905Config config)
        {
            //todo:Serialize
            writer.WriteByte(value.ParamCount);
            try
            {
                foreach (var item in value.ParamList)
                {
                    JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(item, ref writer, item, config);
                }
                if (value.CustomParamList != null)
                {
                    foreach (var item in value.CustomParamList)
                    {
                        JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(item, ref writer, item, config);
                    }
                }
            }
            catch { }

        }
        /// <summary>
        /// 0x8103解析成JSON
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            //todo:Analyze
            var paramCount = reader.ReadByte();//参数总数
            writer.WriteNumber($"[{paramCount.ReadNumber()}]参数总数", paramCount);
            try
            {
                writer.WriteStartArray("参数项");
                for (int i = 0; i < paramCount; i++)
                {
                    var paramId = reader.ReadVirtualUInt16();//参数ID 
                    if (config.JT905_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                    {
                        writer.WriteStartObject();
                        instance.Analyze(ref reader, writer, config);
                        writer.WriteEndObject();
                    }
                    else if (config.JT905_0X8103_Factory.Map.TryGetValue(paramId, out object customInstance))
                    {
                        writer.WriteStartObject();
                        customInstance.Analyze(ref reader, writer, config);
                        writer.WriteEndObject();
                    }
                }
               
            }
            catch (Exception ex)
            {

                writer.WriteString($"异常信息", ex.StackTrace);
            }
        }
    }
}
