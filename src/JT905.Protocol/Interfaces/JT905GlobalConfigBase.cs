using System;
using System.Reflection;
using System.Text;
using JT905.Protocol.Enums;
using JT905.Protocol.Internal;
using JT905.Protocol.MessageBody;
using JT905.Protocol.SerialPort;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 全局配置基类
    /// </summary>
    public abstract class JT905GlobalConfigBase : IJT905Config
    {
        /// <summary>
        /// 
        /// </summary>
        protected JT905GlobalConfigBase()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MsgSNDistributed = new DefaultMsgSNDistributedImpl();
            SkipCRCCode = false;
            MsgIdFactory = new JT905MsgIdFactory();
            Encoding = Encoding.GetEncoding("GBK");
            FormatterFactory = new JT905FormatterFactory();
            JT905_0X0200_Custom_Factory = new JT905_0x0200_Custom_Factory();
            JT905_0X0200_Factory = new JT905_0x0200_Factory();
            JT905_0X8103_Custom_Factory = new JT905_0x8103_Custom_Factory();
            JT905_0X8103_Factory = new JT905_0x8103_Factory();
            JT905TaximeterFactory = new JT905SerialPortFactory();
            ISULength = 12;
            Trim = true;
        }
        /// <summary>
        /// 配置Id
        /// </summary>
        public abstract string ConfigId { get; protected set; }
        /// <summary>
        /// 分布式消息自增流水号
        /// </summary>
        public virtual IJT905MsgSNDistributed MsgSNDistributed { get; set; }
        /// <summary>
        /// 905消息Id工厂
        /// </summary>
        public virtual IJT905MsgIdFactory MsgIdFactory { get; set; }
        /// <summary>
        /// GBK编码
        /// </summary>
        public virtual Encoding Encoding { get; set; }
        /// <summary>
        /// 跳过校验码验证
        /// 默认false
        /// </summary>
        public virtual bool SkipCRCCode { get; set; }
        /// <summary>
        /// 序列化器工厂
        /// </summary>
        public virtual IJT905FormatterFactory FormatterFactory { get; set; }
        /// <summary>
        /// 0x0200自定义附加信息工厂
        /// </summary>
        public virtual IJT905_0x0200_Custom_Factory JT905_0X0200_Custom_Factory { get; set; }
        /// <summary>
        /// 0x0200附加信息工厂
        /// </summary>
        public virtual IJT905_0x0200_Factory JT905_0X0200_Factory { get; set; }
        /// <summary>
        /// 0x8103自定义终端参数设置自定义消息工厂
        /// </summary>
        public virtual IJT905_0x8103_Custom_Factory JT905_0X8103_Custom_Factory { get; set; }
        /// <summary>
        /// 0x8103终端参数设置消息工厂
        /// </summary>
        public virtual IJT905_0x8103_Factory JT905_0X8103_Factory { get; set; }
        /// <summary>
        /// 串口计价器消息工厂
        /// </summary>
        public virtual IJT905SerialPortFactory JT905TaximeterFactory { get; set; }
        /// <summary>
        /// 终端SIM卡长度
        /// </summary>
        public virtual int ISULength { get; set; }
        /// <summary>
        /// 是否去掉头尾空格
        /// </summary>
        public virtual bool Trim { get; set; }

        /// <summary>
        /// 外部扩展程序集注册
        /// </summary>
        /// <param name="externalAssemblies"></param>
        /// <returns></returns>
        public virtual IJT905Config Register(params Assembly[] externalAssemblies)
        {
            if (externalAssemblies != null)
            {
                foreach (var easb in externalAssemblies)
                {
                    MsgIdFactory.Register(easb);
                    FormatterFactory.Register(easb);
                    JT905_0X0200_Factory.Register(easb);
                    JT905_0X0200_Custom_Factory.Register(easb);
                    JT905_0X8103_Factory.Register(easb);
                    JT905_0X8103_Custom_Factory.Register(easb);
                    JT905TaximeterFactory.Register(easb);
                }
            }
            return this;
        }
        /// <summary>
        /// 替换原有消息
        /// </summary>
        /// <typeparam name="TSourceJT905Bodies"></typeparam>
        /// <typeparam name="TTargetJT905Bodies"></typeparam>
        public void ReplaceMsgId<TSourceJT905Bodies, TTargetJT905Bodies>()
            where TSourceJT905Bodies : JT905Bodies
            where TTargetJT905Bodies : JT905Bodies, new()
        {
            TTargetJT905Bodies bodies = new TTargetJT905Bodies();
            MsgIdFactory.Map[bodies.MsgId] = bodies;
            FormatterFactory.FormatterDict.Remove(typeof(TSourceJT905Bodies).GUID);
            FormatterFactory.FormatterDict.Add(typeof(TTargetJT905Bodies).GUID, bodies);
        }
    }
}
