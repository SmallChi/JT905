using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    public enum JT905ControlType:byte
    {
        /// <summary>
        /// 按时间间隔_持续时间
        /// </summary>
        按时间间隔_持续时间 = 0x00,
        /// <summary>
        /// 按距离间隔_持续距离
        /// </summary>
        按距离间隔_持续距离 = 0x11,
        /// <summary>
        /// 按时间间隔_持续距离
        /// </summary>
        按时间间隔_持续距离 = 0x01,
        /// <summary>
        /// 按距离间隔_持续时间
        /// </summary>
        按距离间隔_持续时间 = 0x10,
        /// <summary>
        /// 停止当前跟踪
        /// </summary>
        停止当前跟踪 = 0xFF,

    }
}
