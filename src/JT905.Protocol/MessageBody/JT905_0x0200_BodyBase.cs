using System;
using System.Collections.Generic;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 位置附加信息
    /// </summary>
    public abstract class JT905_0x0200_BodyBase
    {
        /// <summary>
        /// 附加信息Id
        /// </summary>
        public abstract byte AttachInfoId { get; set; }

        /// <summary>
        /// 附加信息长度
        /// </summary>
        public abstract byte AttachInfoLength { get; set; }
    }
}
