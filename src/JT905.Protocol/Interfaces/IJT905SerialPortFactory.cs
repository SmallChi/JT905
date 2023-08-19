using System.Collections.Generic;
using JT905.Protocol.SerialPort;

namespace JT905.Protocol.Interfaces
{
    public interface IJT905SerialPortFactory : IJT905ExternalRegister
    {
        /// <summary>
        /// 实例列表
        /// </summary>
        protected IDictionary<ushort, object> Map { get; }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        bool TryGetValue(ushort id, IBody.Types type, out IBody value);

        /// <summary>
        /// 设置实例列表
        /// </summary>>
        /// <returns></returns>
        IJT905SerialPortFactory SetMap<T>() where T : IBody<T>;
    }
}