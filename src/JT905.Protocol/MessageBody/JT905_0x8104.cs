using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 查询ISU参数
    /// </summary>
    public class JT905_0x8104 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8104>, IJT905Analyze
    {
        public override ushort MsgId => Enums.JT905MsgId.查询ISU参数.ToUInt16Value();

        public override string Description => "查询ISU参数";

        /// <summary>
        /// 参数 ID
        /// 一次可以查询多个参数
        /// </summary>

        public IList<ushort> ParamIds { get; set; }
        /// <summary>
        /// 0x8104解析
        /// 查询ISU参数
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            try
            {
                if (reader.ReadCurrentRemainContentLength()>0)
                {
                    writer.WriteStartArray("参数项");
                    while (reader.ReadCurrentRemainContentLength() > 0)
                    {
                        ushort paramId = reader.ReadUInt16();
                        //config IJT905Config要了解这里面的功能
                        //首先，昨晚做的
                        //每个消息需要描述 IJT905Description 实现这个接口
                        //对应instance实力就可以转换
                        if (config.JT905_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                        {
                            //将实力转换并判断
                            if (instance is IJT905Description description)
                            {
                                writer.WriteStartObject();
                                writer.WriteString($"[{paramId.ReadNumber()}]参数ID", description.Description);
                                writer.WriteEndObject();
                            }

                        }
                        else if (config.JT905_0X8103_Custom_Factory.Map.TryGetValue(paramId, out object customInstance))
                        {
                            if (customInstance is IJT905Description description)
                            {
                                writer.WriteEndObject();
                                writer.WriteString($"[{paramId.ReadNumber()}]参数ID", description.Description);
                                writer.WriteEndObject();
                            }
                        }
                    }
                    writer.WriteEndArray();
                }
           
                
            }
            catch (Exception ex)
            {

                writer.WriteString($"异常信息", ex.StackTrace);
            }
        }
        /// <summary>
        /// 0x8104反序列化
        /// 查询ISU参数
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8104 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8104 value = new JT905_0x8104();
            try
            {
                List<ushort> paramIds = new List<ushort>();
                while (reader.ReadCurrentRemainContentLength() > 0)
                {
                    paramIds.Add(reader.ReadUInt16());
                }
                value.ParamIds = paramIds;
            }
            catch { }
            return value;
        }
        /// <summary>
        /// 0x8104序列化
        /// 查询ISU参数
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8104 value, IJT905Config config)
        {
            if (value.ParamIds != null && value.ParamIds.Count > 0)
            {
                foreach (var item in value.ParamIds)
                {
                    writer.WriteUInt16(item);
                }
            }
        }
    }
}
