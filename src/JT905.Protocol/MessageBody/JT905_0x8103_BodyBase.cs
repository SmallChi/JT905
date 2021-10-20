namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 终端参数设置
    /// </summary>
    public abstract class JT905_0x8103_BodyBase
    {
        /// <summary>
        /// 参数 ID
        /// </summary>
        public abstract ushort ParamId { get; set; }

        /// <summary>
        /// 参数长度
        /// </summary>
        public abstract byte ParamLength { get; set; }
    }
}
