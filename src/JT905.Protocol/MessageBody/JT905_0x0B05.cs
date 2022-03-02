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
    /// 运营数据上传
    /// </summary>
    public class JT905_0x0B05 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0B05>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.运营数据上传;

        public override string Description => "运营数据上传";
        
        /// <summary>
        /// 空转重时车辆位置信息
        /// </summary>
        public JT905_0x0200 LdlingHeavyPosition { get; set; }
        /// <summary>
        /// 重转空时车辆位置信息
        /// </summary>
        public JT905_0x0200 HeavyLdlingPosition { get; set; }
        /// <summary>
        /// 营运ID
        /// </summary>
        public uint OperationId { get; set; }
        /// <summary>
        /// 评价ID
        /// </summary>
        public uint EvaluateId { get; set; }
        /// <summary>
        /// 评价选项
        /// </summary>
        public byte EvaluateOptions { get; set; }
        /// <summary>
        /// 评价选项扩展
        /// </summary>
        public ushort ExtEvaluateOptions { get; set; }
        /// <summary>
        /// 电召订单ID
        /// </summary>
        public uint CallingId { get; set; }
        /// <summary>
        /// 计价营运数据
        /// </summary>
        public byte[] TaximeterData { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0B05 value = new JT905_0x0B05();
            if (reader.ReadCurrentRemainContentLength() >= 25)
            {
                var tempData = reader.ReadVirtualArray(25);
                try
                {
                    JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                    writer.WriteStartObject("空转重时车辆位置信息");
                    config.GetAnalyze<JT905_0x0200>().Analyze(ref positionReader, writer, config);
                    writer.WriteEndObject();
                    reader.Skip(25);
                }
                catch
                {
                    //todo:异常就读完 标记错误就好le
                    //  PositionError = true;
                    reader.ReadContent(reader.ReadCurrentRemainContentLength());
                }

            }
            if (reader.ReadCurrentRemainContentLength() >= 25)
            {
                var tempData = reader.ReadVirtualArray(25);
                try
                {
                    JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                    writer.WriteStartObject("重转空时车辆位置信息");
                    config.GetAnalyze<JT905_0x0200>().Analyze(ref positionReader, writer, config);
                    writer.WriteEndObject();
                    reader.Skip(25);
                }
                catch
                {
                    //todo:异常就读完 标记错误就好le
                    //  PositionError = true;
                    reader.ReadContent(reader.ReadCurrentRemainContentLength());
                }

            }
            value.OperationId = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.OperationId.ReadNumber()}]营运ID", value.OperationId);
            value.EvaluateId = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.EvaluateId.ReadNumber()}]评价ID", value.EvaluateId);
            value.EvaluateOptions = reader.ReadByte();        
            writer.WriteNumber($"[{value.EvaluateOptions.ReadNumber()}]评价选项", value.EvaluateOptions);      
            value.ExtEvaluateOptions = reader.ReadUInt16();        
            writer.WriteNumber($"[{value.ExtEvaluateOptions.ReadNumber()}]评价选项扩展", value.ExtEvaluateOptions);
            value.CallingId = reader.ReadUInt32();        
            writer.WriteNumber($"[{value.CallingId.ReadNumber()}]电召订单ID", value.CallingId);
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.TaximeterData = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                writer.WriteString($"[{value.TaximeterData.ToHexString()}]计价营运数据", value.TaximeterData.ToHexString());
            }
            else
            {
                writer.WriteString("[]计价营运数据", "");
            }

        }

        public JT905_0x0B05 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0B05 value = new JT905_0x0B05();
            if (reader.ReadCurrentRemainContentLength() >= 25)
            {
                var tempData = reader.ReadVirtualArray(25);
                try
                {
                    JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                    value.LdlingHeavyPosition = config.GetMessagePackFormatter<JT905_0x0200>().Deserialize(ref positionReader, config);
                    reader.Skip(25);
                }
                catch
                {
                    //todo:异常就读完 标记错误就好le
                    //  PositionError = true;
                    reader.ReadContent(reader.ReadCurrentRemainContentLength());
                    return value;
                }
            }
            if (reader.ReadCurrentRemainContentLength() >= 25)
            {
                var tempData = reader.ReadVirtualArray(25);
                try
                {
                    JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                    value.HeavyLdlingPosition = config.GetMessagePackFormatter<JT905_0x0200>().Deserialize(ref positionReader, config);
                    reader.Skip(25);
                }
                catch
                {
                    //todo:异常就读完 标记错误就好le
                    //  PositionError = true;
                    reader.ReadContent(reader.ReadCurrentRemainContentLength());
                    return value;
                }
            }
            value.OperationId = reader.ReadUInt32();    
            value.EvaluateId = reader.ReadUInt32();    
            value.EvaluateOptions = reader.ReadByte(); 
            value.ExtEvaluateOptions = reader.ReadUInt16();  
            value.CallingId = reader.ReadUInt32();    
            if (reader.ReadCurrentRemainContentLength() > 0)
            {
                value.TaximeterData = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
            }
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B05 value, IJT905Config config)
        {
            config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer,value.LdlingHeavyPosition,config);
            config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer,value.HeavyLdlingPosition,config);
            writer.WriteUInt32(value.OperationId);
            writer.WriteUInt32(value.EvaluateId);
            writer.WriteByte(value.EvaluateOptions);
            writer.WriteUInt16(value.ExtEvaluateOptions);
            writer.WriteUInt32(value.CallingId);
            if(value.TaximeterData != null && value.TaximeterData.Length > 0)
            {
                writer.WriteArray(value.TaximeterData);
            }
            
        }
    }
}


                    
