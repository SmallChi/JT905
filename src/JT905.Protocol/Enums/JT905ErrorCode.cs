namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 异常错误码
    /// </summary>
    public enum JT905ErrorCode
    {
        /// <summary>
        /// JT905校验和不相等
        /// </summary>
        CheckCodeNotEqual = 1001,
        /// <summary>
        /// 消息头解析错误
        /// </summary>
        HeaderParseError = 1002,
        /// <summary>
        /// 消息体解析错误
        /// </summary>
        BodiesParseError = 1003,
        /// <summary>
        /// 验证长度
        /// </summary>
        VailLength = 1004,
        /// <summary>
        /// 没有实现对应的类型
        /// </summary>
        NotImplType = 1005,
        /// <summary>
        /// 长度不够
        /// </summary>
        NotEnoughLength = 1006,
        /// <summary>
        /// 没有全局注册格式化器
        /// IJT905MessagePackFormatter
        /// </summary>
        NotGlobalRegisterFormatterAssembly = 1007,        
        /// <summary>
        /// 经纬度错误
        /// </summary>
        LatOrLngError = 1008
    }
}
