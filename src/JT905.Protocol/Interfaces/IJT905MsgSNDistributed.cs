namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// JT905分布式自增流水号
    /// </summary>
    public interface IJT905MsgSNDistributed
    {
        /// <summary>
        /// 根据终端ISU号自增
        /// </summary>
        /// <param name="isu"></param>
        /// <returns></returns>
        ushort Increment(string isu);
    }
}
