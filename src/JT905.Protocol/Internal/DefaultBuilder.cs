using JT905.Protocol.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Internal
{
    /// <summary>
    /// 默认JT905构造器
    /// </summary>
    class DefaultBuilder : IJT905Builder
    {
        /// <summary>
        /// DI服务
        /// </summary>
        public IServiceCollection Services { get; }
        /// <summary>
        /// JT905配置
        /// </summary>
        public IJT905Config Config { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public DefaultBuilder(IServiceCollection services, IJT905Config config)
        {
            Services = services;
            Config = config;
        }
    }
}
