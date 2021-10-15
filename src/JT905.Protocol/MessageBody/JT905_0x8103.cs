
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
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905_0x8103 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            //todo:Deserialize
            return default;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8103 value, IJT905Config config)
        {
            //todo:Serialize
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        /// <param name="config"></param>
        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            //todo:Analyze
        }
    }
}
