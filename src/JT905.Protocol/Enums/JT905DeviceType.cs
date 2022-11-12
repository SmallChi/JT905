using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum JT905DeviceType:byte
    {
        ISU = 0x00,
        通信模块 = 0x01,
        计价器 = 0x02,
        出租汽车安全模块 = 0x03,
        LED显示屏=0x04,
        智能顶灯 = 0x05,
        服务评价器_后排=0x06,
        摄像装置 = 0x07,
        卫星定位设备 = 0x08,
        液晶_LCD多媒体屏 = 0x09,
        ISU人机交互设备 = 0x10,
        服务评价器_前排=0x11,
        RFU = 0xFF,

    }
}
