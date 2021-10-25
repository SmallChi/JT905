using JT905.Protocol;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 位置信息查询
    /// </summary>
    public class JT905_0x8201: JT905Bodies
    {
        /// <summary>
        /// 0x8201
        /// </summary>
        public override ushort MsgId { get; } = (ushort)Enums.JT905MsgId.位置信息查询;
        /// <summary>
        /// 跳过数据体序列化
        /// </summary>
        public override bool SkipSerialization { get; set; } = true;
        /// <summary>
        /// 位置信息查询
        /// </summary>
        public override string Description => "位置信息查询";
    }
}
