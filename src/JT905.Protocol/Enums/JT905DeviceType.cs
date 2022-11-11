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
        /// <summary>
        /// ISU
        /// </summary>
        ISU = 0x00,
        /// <summary>
        /// 通信模块
        /// </summary>
        通信模块 = 0x01,
        /// <summary>
        /// 通信模块
        /// </summary>
        计价器 = 0x02,
        /// <summary>
        /// 计价器
        /// </summary>
        出租汽车安全模块 = 0x03,
        /// <summary>
        /// LED显示屏
        /// </summary>
        LED显示屏 = 0x04,
        /// <summary>
        /// 
        /// </summary>
        智能顶灯 = 0x05,
        /// <summary>
        /// 智能顶灯
        /// </summary>
        服务评价器_后排 = 0x06,
        /// <summary>
        /// 摄像装置
        /// </summary>
        摄像装置 = 0x07,
        /// <summary>
        /// 卫星定位设备
        /// </summary>
        卫星定位设备 = 0x08,
        /// <summary>
        /// 液晶_LCD多媒体屏
        /// </summary>
        液晶_LCD多媒体屏 = 0x09,
        /// <summary>
        /// ISU人机交互设备
        /// </summary>
        ISU人机交互设备 = 0x10,
        /// <summary>
        /// 服务评价器_前排
        /// </summary>
        服务评价器_前排 = 0x11,
        /// <summary>
        /// RFU 保留供将来使用
        /// </summary>
        RFU = 0xFF,

    }
}
