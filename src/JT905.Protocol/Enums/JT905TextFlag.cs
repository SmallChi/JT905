using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 文本信息标志位
    /// </summary>
    public enum  JT905TextFlag: byte
    {
        /// <summary>
        /// 紧急
        /// </summary>
        紧急 = 1,
        /// <summary>
        /// 显示装置显示
        /// </summary>
        显示装置显示 = 4,
        /// <summary>
        /// 语音合成播读
        /// </summary>
        语音合成播读 = 8,
        /// <summary>
        /// 广告屏显示
        /// </summary>
        广告屏显示 = 16,

    }
}
