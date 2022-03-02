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
    /// 车辆控制应答
    /// </summary>
    public class JT905_0x0500 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x0500>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.车辆控制应答;

        public override string Description => "车辆控制应答";

        /// <summary>
        /// 应答流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        public JT905_0x0200 Position { get; set; }




        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x0500 value = new JT905_0x0500();
            value.MsgNum = reader.ReadUInt16();
            writer.WriteNumber($"[{value.MsgNum.ReadNumber()}]应答流水号", value.MsgNum);
            if (reader.ReadCurrentRemainContentLength() >= 25)
            {
                var tempData = reader.ReadVirtualArray(25);
                try
                {
                    JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                    writer.WriteStartObject("位置信息汇报");
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

        }

        public JT905_0x0500 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x0500 value = new JT905_0x0500();
            value.MsgNum = reader.ReadUInt16();
            if (reader.ReadCurrentRemainContentLength() >= 25)
            {
                var tempData = reader.ReadVirtualArray(25);
                try
                {
                    JT905MessagePackReader positionReader = new JT905MessagePackReader(tempData, reader.Version);
                    value.Position = config.GetMessagePackFormatter<JT905_0x0200>().Deserialize(ref positionReader, config);
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


            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0500 value, IJT905Config config)
        {
            writer.WriteUInt16(value.MsgNum);
            config.GetMessagePackFormatter<JT905_0x0200>().Serialize(ref writer, value.Position, config);

        }
    }
}



