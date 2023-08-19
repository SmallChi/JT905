using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JT905.Protocol.Enums;
using JT905.Protocol.Interfaces;
using JT905.Protocol.SerialPort;

namespace JT905.Protocol
{
    /// <summary>
    /// JT905接口配置
    /// </summary>
    public interface IJT905Config
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        string ConfigId { get; }
        /// <summary>
        /// 消息流水号
        /// </summary>
        IJT905MsgSNDistributed MsgSNDistributed { get; set; }
        /// <summary>
        /// 消息工厂
        /// </summary>
        IJT905MsgIdFactory MsgIdFactory { get; set; }
        /// <summary>
        /// 序列化器工厂
        /// </summary>
        IJT905FormatterFactory FormatterFactory { get; set; }
        /// <summary>
        /// 自定义附加信息工厂
        /// </summary>
        IJT905_0x0200_Custom_Factory JT905_0X0200_Custom_Factory { get; set; }
        /// <summary>
        /// 附加信息工厂
        /// </summary>
        IJT905_0x0200_Factory JT905_0X0200_Factory { get; set; }
        /// <summary>
        ///自定义设置终端参数工厂
        /// </summary>
        IJT905_0x8103_Custom_Factory JT905_0X8103_Custom_Factory { get; set; }
        /// <summary>
        ///设置终端参数工厂
        /// </summary>
        IJT905_0x8103_Factory JT905_0X8103_Factory { get; set; }
        /// <summary>
        /// 计价器命令工厂
        /// </summary>
        IJT905SerialPortFactory JT905TaximeterFactory { get; set; }
        /// <summary>
        /// 统一编码
        /// </summary>
        Encoding Encoding { get; set; }
        /// <summary>
        /// 跳过校验码
        /// 测试的时候需要手动修改值，避免验证
        /// 默认：false
        /// </summary>
        bool SkipCRCCode { get; set; }
        /// <summary>
        /// ReadBCD是否需要去0操作
        /// 默认是去0
        /// 注意:有时候对协议来说是有意义的0
        /// </summary>
        bool Trim { get; set; }
        /// <summary>
        /// 设备终端号(默认12位)
        /// </summary>
        int ISULength { get; set; }
        /// <summary>
        /// 全局注册外部程序集
        /// </summary>
        /// <param name="externalAssemblies"></param>
        /// <returns></returns>
        IJT905Config Register(params Assembly[] externalAssemblies);
        /// <summary>
        /// 替换原有消息
        /// </summary>
        void ReplaceMsgId<TSourceJT905Bodies, TTargetJT905Bodies>()
            where TSourceJT905Bodies : JT905Bodies
            where TTargetJT905Bodies : JT905Bodies, new();

    }
}
