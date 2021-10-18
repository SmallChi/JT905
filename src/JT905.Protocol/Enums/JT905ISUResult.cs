using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// ISU应答结果
    /// </summary>
    public enum JT905ISUResult :byte
    {
        /// <summary>
        /// 成功/确认
        /// </summary>
        Success = 0x00,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 0x01,
        /// <summary>
        /// 消息有误
        /// </summary>
        MessageError = 0x02,
    }
}
