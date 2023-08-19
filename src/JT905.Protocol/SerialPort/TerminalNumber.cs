using JT905.Protocol.Enums;

namespace JT905.Protocol.SerialPort
{
    /// <summary>
    /// 设备编码
    /// </summary>
    public class TerminalNumber
    {
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public JT905DeviceType Type { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public string Serial { get; set; }
    }
}