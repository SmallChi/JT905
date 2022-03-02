using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 车辆控制项
    /// 0x8500
    /// </summary>
    public enum JT905ControlItem : byte
    {
        油路 = 0,
        电路 = 1,
        车门 = 2,
        车辆 = 3,
    }
}
