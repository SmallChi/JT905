using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{

    /// <summary>
    /// 设备巡检内容
    /// </summary>
    public abstract class JT905_0x0B11_TLV
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public abstract byte DeviceType { get; set; }

        /// <summary>
        /// 设备巡检结果的长度
        /// </summary>
        public abstract byte Length { get; set; }

        /// <summary>
        /// 设备序列号
        /// </summary>
        public abstract string SerialNumber { get; set; }
        /// <summary>
        /// 硬件版本号
        /// </summary>

        public abstract string HardwareVer { get; set; }
        /// <summary>
        /// 软件版本号
        /// </summary>

        public abstract string SoftVer { get; set; }
    }
}



