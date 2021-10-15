using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 外部注册
    /// </summary>
    public interface IJT905ExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalAssembly"></param>
        void Register(Assembly externalAssembly);
    }
}
