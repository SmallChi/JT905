using JT905.Protocol.Interfaces;
using JT905.Protocol.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
[assembly: InternalsVisibleTo("JT905.Protocol.Benchmark")]
[assembly: InternalsVisibleTo("JT905.Protocol.Test")]
namespace JT905.Protocol
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class JT905DependencyInjectionExtensions
    {
        /// <summary>
        /// 注册905配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="JT905Config"></param>
        /// <returns></returns>
        public static IJT905Builder AddJT905Configure(this IServiceCollection services, IJT905Config JT905Config)
        {
            services.AddSingleton(JT905Config.GetType(), JT905Config);
            return new DefaultBuilder(services, JT905Config);
        }
        /// <summary>
        /// 注册905配置
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="JT905Config"></param>
        /// <returns></returns>
        public static IJT905Builder AddJT905Configure(this IJT905Builder builder, IJT905Config JT905Config)
        {
            builder.Services.AddSingleton(JT905Config.GetType(), JT905Config);
            return builder;
        }
        /// <summary>
        /// 注册905配置
        /// </summary>
        /// <typeparam name="TJT905Config"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IJT905Builder AddJT905Configure<TJT905Config>(this IServiceCollection services)where TJT905Config : IJT905Config,new()
        {
            var config = new TJT905Config();
            services.AddSingleton(typeof(TJT905Config), config);
            return new DefaultBuilder(services, config);
        }
        /// <summary>
        /// 注册905配置
        /// </summary>
        /// <typeparam name="TJT905Config"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IJT905Builder AddJT905Configure<TJT905Config>(this IJT905Builder builder) where TJT905Config : IJT905Config, new()
        {
            var config = new TJT905Config();
            builder.Services.AddSingleton(typeof(TJT905Config), config);
            return builder;
        }
        /// <summary>
        /// 注册905配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IJT905Builder AddJT905Configure(this IServiceCollection services)
        {
            DefaultGlobalConfig config = new DefaultGlobalConfig();
            services.AddSingleton<IJT905Config>(config);
            return new DefaultBuilder(services, config);
        }
    }
}
