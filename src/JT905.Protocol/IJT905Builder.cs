using JT905.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol
{
    /// <summary>
    /// JT905构造器
    /// </summary>
    public interface IJT905Builder
    {
        /// <summary>
        /// JT905配置
        /// </summary>
        IJT905Config Config { get; }
        /// <summary>
        /// 服务注册
        /// </summary>
        IServiceCollection Services { get; }
    }
}
