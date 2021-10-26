using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 位置汇报数据补传
    /// </summary>
    public class JT905_0x0203 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0203>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.位置汇报数据补传;

        public override string Description => "位置汇报数据补传";
        

        /// <summary>
        /// 位置信息汇报(0x0200) 消息体
        /// </summary>
        public List<JT905_0x0200> Positions { get; set; }

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0203 value = new JT905_0x0203();
            if (reader.ReadCurrentRemainContentLength()>=25)
            {
                while (reader.ReadCurrentRemainContentLength() >= 25)
                {
                    try
                    {
                        var tempData = reader.ReadVirtualArray(25);
                        JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                        writer.WriteStartObject("位置基本信息");
                        config.GetAnalyze<JT905_0x0200>().Analyze(ref positionReader, writer, config);
                        writer.WriteEndObject();
                        reader.Skip(25);
                    }
                    catch {
                        reader.ReadContent(reader.ReadCurrentRemainContentLength());
                    }
                    
                }
            }
        }

        public JT905_0x0203 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0203 value = new JT905_0x0203();
            
            if (reader.ReadCurrentRemainContentLength()>=25)
            {
                value.Positions = new List<JT905_0x0200>();
                while (reader.ReadCurrentRemainContentLength() >= 25)
                {
                    var tempData = reader.ReadVirtualArray(25);
                    try
                    {
                        JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                        value.Positions.Add(config.GetMessagePackFormatter<JT905_0x0200>().Deserialize(ref positionReader, config));
                        reader.Skip(25);
                    }
                    catch
                    {
                        reader.ReadContent(reader.ReadCurrentRemainContentLength());
                    }
                }
            }            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0203 value, IJT905Config config)
        {
            //config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer, value.Positions, config);
            if (value.Positions!=null&&value.Positions.Count>0)
            {
                foreach (var position in value.Positions)
                {
                    config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer, position, config);
                }
            }
        }
    }
}
