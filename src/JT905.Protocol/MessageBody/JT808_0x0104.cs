using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System.Collections.Generic;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 查询ISU参数应答
    /// </summary>
    public class JT905_0x0104 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0104>, IJT905Analyze
    {
        /// <summary>
        /// 0x0104
        /// </summary>
        public override ushort MsgId { get; } = Enums.JT905MsgId.查询ISU参数应答.ToUInt16Value();
        /// <summary>
        /// 查询终端参数应答
        /// </summary>
        public override string Description => "查询ISU参数应答";
        /// <summary>
        /// 应答流水号
        /// 查询指定终端参数的流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        
        /// <summary>
        /// 参数列表
        /// </summary>
        public IList<JT905_0x8103_BodyBase> ParamList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x0104 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0104 JT905_0x0104 = new JT905_0x0104();
            JT905_0x0104.MsgNum = reader.ReadUInt16();            
            while(reader.ReadCurrentRemainContentLength()>0)
            {
                var paramId = reader.ReadVirtualUInt16();//参数ID         
                if (config.JT905_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                {
                    if (JT905_0x0104.ParamList != null)
                    {
                        JT905_0x0104.ParamList.Add(JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(instance, ref reader, config));
                    }
                    else
                    {
                        JT905_0x0104.ParamList = new List<JT905_0x8103_BodyBase> { JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(instance, ref reader, config) };
                    }
                }
                else {
                    //对于未能解析的自定义项，过滤其长度，以保证后续解析正常
                    reader.Skip(4);//跳过参数id长度
                    var len = reader.ReadByte();//获取协议长度
                    reader.Skip(len);//跳过协议内容
                }
            }
            return JT905_0x0104;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0104 value, IJT905Config config)
        {
            writer.WriteUInt16(value.MsgNum);            
            foreach (var item in value.ParamList)
            {
                object obj = config.GetMessagePackFormatterByType(item.GetType());
                JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(obj, ref writer, item, config);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0104 JT905_0x0104 = new JT905_0x0104();
            JT905_0x0104.MsgNum = reader.ReadUInt16();            
            writer.WriteNumber($"[{JT905_0x0104.MsgNum.ReadNumber()}]应答流水号", JT905_0x0104.MsgNum);            
            writer.WriteStartArray($"参数列表");
            if (reader.ReadCurrentRemainContentLength()>0)
            {
                while(reader.ReadCurrentRemainContentLength() > 0)
                {
                    writer.WriteStartObject();
                    var paramId = reader.ReadVirtualUInt16();//参数ID         
                    if (config.JT905_0X8103_Factory.Map.TryGetValue(paramId, out object instance))
                    {
                        if (instance is IJT905Analyze analyze)
                        {
                            analyze.Analyze(ref reader, writer, config);
                        }
                    }
                    writer.WriteEndObject();
                }
            }
            
            writer.WriteEndArray();
        }
    }
}
