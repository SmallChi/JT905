using JT905.Protocol.Enums;
using System;

namespace JT905.Protocol.Exceptions
{
    /// <summary>
    /// JT905异常处理类
    /// </summary>
    [Serializable]
    public class JT905Exception : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        public JT905Exception(JT905ErrorCode errorCode) : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        public JT905Exception(JT905ErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="ex"></param>
        public JT905Exception(JT905ErrorCode errorCode, Exception ex) : base(ex.Message, ex)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public JT905Exception(JT905ErrorCode errorCode, string message, Exception ex) : base(message, ex)
        {
            ErrorCode = errorCode;
        }
        /// <summary>
        /// JT905统一错误码
        /// </summary>
        public JT905ErrorCode ErrorCode { get; }
    }
}
