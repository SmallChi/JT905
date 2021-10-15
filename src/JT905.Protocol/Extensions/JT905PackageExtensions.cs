using JT905.Protocol.Enums;

namespace JT905.Protocol.Extensions
{
    /// <summary>
    /// JT905创建包扩展
    /// </summary>
    public static partial class JT905PackageExtensions
    {
        /// <summary>
        /// 根据消息Id创建包
        /// </summary>
        /// <typeparam name="TJT905Bodies"></typeparam>
        /// <param name="msgId"></param>
        /// <param name="isu"></param>
        /// <param name="bodies"></param>
        /// <returns></returns>
        public static JT905Package Create<TJT905Bodies>(this JT905MsgId msgId, string isu, TJT905Bodies bodies)
            where TJT905Bodies : JT905Bodies
        {
            JT905Package JT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = (ushort)msgId,
                    ISU = isu,
                },
                Bodies = bodies
            };
            return JT905Package;
        }
        /// <summary>
        /// 根据消息Id创建包
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="isu"></param>
        /// <returns></returns>
        public static JT905Package Create(this JT905MsgId msgId, string isu)
        {
            JT905Package JT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = (ushort)msgId,
                    ISU = isu,
                }
            };
            return JT905Package;
        }
        /// <summary>
        /// 根据自定义消息Id创建包
        /// </summary>
        /// <typeparam name="TJT905Bodies"></typeparam>
        /// <param name="msgId"></param>
        /// <param name="isu"></param>
        /// <param name="bodies"></param>
        /// <returns></returns>
        public static JT905Package CreateCustomMsgId<TJT905Bodies>(this ushort msgId, string isu, TJT905Bodies bodies)
            where TJT905Bodies : JT905Bodies
        {
            JT905Package JT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = msgId,
                    ISU = isu
                },
                Bodies = bodies
            };
            return JT905Package;
        }
        /// <summary>
        /// 根据自定义消息Id创建包
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="isu"></param>
        /// <returns></returns>
        public static JT905Package CreateCustomMsgId(this ushort msgId, string isu)
        {
            JT905Package JT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = msgId,
                    ISU = isu
                }
            };
            return JT905Package;
        }
    }
}
