using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;

namespace JT905.Protocol
{
    /// <summary>
    /// 头部
    /// </summary>
    public class JT905Header : IJT905MessagePackFormatter<JT905Header>
    {
        /// <summary>
        /// 消息ID 
        /// <see cref="JT905.Protocol.Enums.JT905MsgId"/>
        /// </summary>
        public ushort MsgId { get; set; }
        /// <summary>
        /// 消息体长度
        /// </summary>
        public int DataLength { get; set; } = 0;
        /// <summary>
        /// ISU标识
        /// </summary>
        public string ISU { get;  set; }
        /// <summary>
        /// 消息流水号 0
        /// 发送计数器
        /// 占用两个字节，为发送信息的序列号，用于接收方检测是否有信息的丢失，上级平台和下级平台接自己发送数据包的个数计数，互不影响。
        /// 程序开始运行时等于零，发送第一帧数据时开始计数，到最大数后自动归零
        /// </summary>
        public ushort MsgNum { get;  set; }
        /// <summary>
        /// 手动消息流水号（only test）
        /// 发送计数器
        /// 占用两个字节，为发送信息的序列号，用于接收方检测是否有信息的丢失，上级平台和下级平台接自己发送数据包的个数计数，互不影响。
        /// 程序开始运行时等于零，发送第一帧数据时开始计数，到最大数后自动归零
        /// </summary>
        public ushort? ManualMsgNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public JT905Header Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905Header JT905Header = new JT905Header();
            // 1.消息ID
            JT905Header.MsgId = reader.ReadUInt16();
            // 2.消息体长度
            JT905Header.DataLength = reader.ReadUInt16();
            // 3.ISU标识
            JT905Header.ISU = reader.ReadBCD(config.ISULength, config.Trim);
            // 4.消息流水号
            JT905Header.MsgNum = reader.ReadUInt16();
            return JT905Header;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="config"></param>
        public void Serialize(ref JT905MessagePackWriter writer, JT905Header value, IJT905Config config)
        {
            // 1.消息ID
            writer.WriteUInt16(value.MsgId);
            // 2.消息体长度
            writer.WriteUInt16((ushort)value.DataLength);
            // 3.终端手机号
            writer.WriteBCD(value.ISU, config.ISULength);        
            // 4.消息流水号
            writer.WriteUInt16(value.MsgNum);
        }
    }
}
