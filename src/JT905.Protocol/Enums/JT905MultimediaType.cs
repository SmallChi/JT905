using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 多媒体类型
    /// </summary>
    public enum JT905MultimediaType : byte
    {
        /// <summary>
        /// 照片
        /// </summary>
        照片 = 0x00,
        /// <summary>
        /// 音频
        /// </summary>
        音频 = 0x01,
        /// <summary>
        /// 视频
        /// </summary>
        视频 = 0x02,

    }
}
