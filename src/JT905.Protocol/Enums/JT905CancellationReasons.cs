using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 取消原因
    /// 0:事故
    /// 1:路堵
    /// 2:其他
    /// </summary>
    public enum JT905CancellationReasons : byte
    {
        事故 = 0,
        路堵 = 1,
        其他 = 2,
    }
}
