using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 拍照原因
    /// 0:进入重车拍照
    /// 1:服务评价拍照
    /// 2:报警拍照
    /// 3:中心主动拍照
    /// </summary>
    public enum JT905PhotoReason : byte
    {
        /// <summary>
        /// 进入重车拍照
        /// </summary>
        进入重车拍照 = 0,
        /// <summary>
        /// 服务评价拍照
        /// </summary>
        服务评价拍照 = 1,
        /// <summary>
        /// 报警拍照
        /// </summary>
        报警拍照 = 2,
        /// <summary>
        /// 中心主动拍照
        /// </summary>
        中心主动拍照 = 3,
    }
}
