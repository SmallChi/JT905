using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Enums
{
   public enum JT905TelephoneBookContactType:byte
    {
        /// <summary>
        /// 呼入
        /// </summary>
        呼入 = 1,
        /// <summary>
        /// 呼出
        /// </summary>
        呼出 = 2,
        /// <summary>
        /// 呼入_呼出
        /// </summary>
        呼入_呼出 = 3
    }
}
