using JT905.Protocol.Enums;
using JT905.Protocol.Exceptions;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Text.Json;

namespace JT905.Protocol
{
    /// <summary>
    /// JT905数据包
    /// </summary>
    public class JT905Package : IJT905MessagePackFormatter<JT905Package>, IJT905Analyze
    {
        /// <summary>
        /// 起始符
        /// </summary>
        public const byte BeginFlag = 0x7e;
        /// <summary>
        /// 终止符
        /// </summary>
        public const byte EndFlag = 0x7e;
        /// <summary>
        /// 起始符
        /// </summary>
        public byte Begin { get; set; } = BeginFlag;
        /// <summary>
        /// 头数据
        /// </summary>
        public JT905Header Header { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public JT905Bodies Bodies { get; set; }
        /// <summary>
        /// 未知数据体
        /// </summary>
        public byte[] UnknownBodies { get; set; }
        /// <summary>
        /// 校验码
        /// 从消息头开始，同后一字节异或，直到校验码前一个字节，占用一个字节。
        /// </summary>
        public byte CheckCode { get; set; }
        /// <summary>
        /// 终止符
        /// </summary>
        public byte End { get; set; } = EndFlag;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905Package Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            // 1. 验证校验和
            if (!config.SkipCRCCode)
            {
                if (!reader.CheckXorCodeVali)
                {
                    throw new JT905Exception(JT905ErrorCode.CheckCodeNotEqual, $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
                }
            }
            JT905Package value = new JT905Package();
            // ---------------开始解包--------------
            // 2.读取起始位置        
            value.Begin = reader.ReadStart();
            // 3.读取头部信息
            value.Header = new JT905Header();
            //  3.1.读取消息Id
            value.Header.MsgId = reader.ReadUInt16();
            //  3.2.读取消息体属性长度
            value.Header.DataLength = reader.ReadUInt16();
            //  3.3.读取ISU标识
            value.Header.ISU = reader.ReadBCD(config.ISULength, config.Trim);
            //  3.4.读取消息流水号
            value.Header.MsgNum = reader.ReadUInt16();
            // 4.处理数据体
            //  4.1.判断有无数据体
            if (value.Header.DataLength > 0)
            {
               
                if (config.MsgIdFactory.TryGetValue(value.Header.MsgId, out object instance))
                {
                    try
                    {
                        //4.2.处理消息体
                        value.Bodies = JT905MessagePackFormatterResolverExtensions.JT905DynamicDeserialize(
                                instance,
                                ref reader, config);
                    }
                    catch (Exception ex)
                    {
                        throw new JT905Exception(JT905ErrorCode.BodiesParseError, ex);
                    }
                }
                else
                {
                    //用剩余包的长度，不用数据体上来的
                    value.UnknownBodies = reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray();
                }
            }
            // 5.读取校验码
            value.CheckCode = reader.ReadByte();
            // 6.读取终止位置
            value.End = reader.ReadEnd();
            // ---------------解包完成--------------
            return value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905Package value, IJT905Config config)
        {
            // ---------------开始组包--------------
            // 1.起始符
            writer.WriteStart();
            // 2.写入头部 //部分有带数据体的长度，那么先跳过写入头部部分
            //  2.1.消息ID
            writer.WriteUInt16(value.Header.MsgId);
            //  2.2.消息体长度最后在反写
            writer.Skip(2, out int msgBodiesLengthPosition);
            //  2.3 ISU标识
            writer.WriteBCD(value.Header.ISU, config.ISULength);
            //  2.4.消息流水号
            if (value.Header.ManualMsgNum.HasValue)
            {
                writer.WriteUInt16(value.Header.ManualMsgNum.Value);
            }
            else
            {
                value.Header.MsgNum = config.MsgSNDistributed.Increment(value.Header.ISU);
                writer.WriteUInt16(value.Header.MsgNum);
            }
            // 记录数据写入时候的偏移量
            int headerLength = writer.GetCurrentPosition();
            // 3.处理数据体部分
            if (value.Bodies != null)
            {
                if (!value.Bodies.SkipSerialization)
                {
                    JT905MessagePackFormatterResolverExtensions.JT905DynamicSerialize(value.Bodies,
                        ref writer, value.Bodies,
                        config);
                }
            }
            else if(value.UnknownBodies!=null && value.UnknownBodies.Length > 0)
            {
                writer.WriteArray(value.UnknownBodies);
            }
            //  3.1.处理数据体长度
            //      2.2.回写消息体属性
            value.Header.DataLength = (writer.GetCurrentPosition() - headerLength);
            writer.WriteUInt16Return((ushort)value.Header.DataLength, msgBodiesLengthPosition);
            // 4.校验码
            writer.WriteXor();
            // 5.终止符
            writer.WriteEnd();
            // 6.编码
            writer.WriteEncode();
            // ---------------组包结束--------------
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            // ---------------开始解析对象--------------
            writer.WriteStartObject();
            // 1. 验证校验和
            if (!reader.CheckXorCodeVali)
            {
                writer.WriteString("检验和错误", $"{reader.RealCheckXorCode}!={reader.CalculateCheckXorCode}");
            }
            // 2.读取起始位置
            byte start = reader.ReadStart();
            writer.WriteNumber($"[{start.ReadNumber()}]开始", start);
            var msgid = reader.ReadUInt16();
            // 3.读取头部信息
            //  3.1.读取消息Id
            writer.WriteNumber($"[{msgid.ReadNumber()}]消息Id", msgid);
            //  3.2.读取消息体属性长度
            ushort messageBodyLength = reader.ReadUInt16();    
            writer.WriteNumber($"[{messageBodyLength.ReadNumber()}]消息体长度", messageBodyLength);
            //  3.3.读取ISU标识
            var ISU = reader.ReadBCD(config.ISULength, false);
            writer.WriteString($"[{ISU.PadLeft(config.ISULength, '0')}]ISU标识", ISU);
            //  3.4.读取消息流水号
            var msgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{msgNum.ReadNumber()}]消息流水号", msgNum);
            // 4.处理数据体
            //  4.1.判断有无数据体
            if (messageBodyLength > 0)
            {
                //数据体属性对象 开始
                writer.WriteStartObject("数据体对象");
                string description = "数据体";
                if (config.MsgIdFactory.TryGetValue(msgid, out object instance))
                {
                    if (instance is IJT905Description JT905Description)
                    {
                        //4.2.处理消息体
                        description = JT905Description.Description;
                    }
                    try
                    {
                        //数据体长度正常
                        writer.WriteString($"{description}", reader.ReadVirtualArray(reader.ReadCurrentRemainContentLength()).ToArray().ToHexString());
                        if (instance is IJT905Analyze analyze)
                        {
                            //4.2.处理消息体
                            analyze.Analyze(ref reader, writer, config);
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        writer.WriteString($"数据体解析异常,无可用数据体进行解析", ex.StackTrace);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        writer.WriteString($"数据体解析异常,无可用数据体进行解析", ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        writer.WriteString($"数据体异常", ex.StackTrace);
                    }
                }
                else
                {
                    writer.WriteString($"[未知]数据体", reader.ReadArray(reader.ReadCurrentRemainContentLength()).ToArray().ToHexString());
                }         
                //数据体属性对象 结束
                writer.WriteEndObject();
            }
            else
            {
                if (config.MsgIdFactory.TryGetValue(msgid, out object instance))
                {
                    //数据体属性对象 开始
                    writer.WriteStartObject("数据体对象");
                    string description = "[Null]数据体";
                    if (instance is IJT905Description JT905Description)
                    {
                        //4.2.处理消息体
                        description = JT905Description.Description;
                    }
                    writer.WriteNull(description);
                    //数据体属性对象 结束
                    writer.WriteEndObject();
                }
                else
                {
                    writer.WriteNull($"[Null]数据体");
                }
            }
            try
            {
                // 5.读取校验码
                reader.ReadByte();
                writer.WriteNumber($"[{reader.RealCheckXorCode.ReadNumber()}]校验码", reader.RealCheckXorCode);
                // 6.读取终止位置
                byte end = reader.ReadEnd();
                writer.WriteNumber($"[{end.ReadNumber()}]结束", end);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                writer.WriteString($"数据解析异常,无可用数据进行解析", ex.StackTrace);
            }
            catch (Exception ex)
            {
                writer.WriteString($"数据解析异常", ex.StackTrace);
            }
            finally
            {
                writer.WriteEndObject();
            }
        }
    }
}
