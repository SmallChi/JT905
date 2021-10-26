namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 事件项
    /// </summary>
    public class JT905EventProperty
    {
        /// <summary>
        /// 事件ID
        /// 若 ISU 已有同 ID 的事件，则被覆盖
        /// </summary>
        public byte EventId { get; set; }
        /// <summary>
        /// 事件内容
        /// 最长为 20byte
        /// </summary>
        public string EventContent { get;  set; }
    }
}