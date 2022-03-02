using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 业务类型 0：即时召车；1：预约召车；2：车辆指派
    /// </summary>
    public enum JT905BusinessType : byte
    {
        /// <summary>
        /// 即时召车
        /// </summary>
        即时召车 = 0,
        /// <summary>
        /// 预约召车
        /// </summary>
        预约召车 = 1,
        /// <summary>
        /// 车辆指派
        /// </summary>
        车辆指派 = 2,
    }
}
