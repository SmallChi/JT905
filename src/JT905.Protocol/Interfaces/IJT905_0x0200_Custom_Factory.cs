using JT905.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol.Interfaces
{
    /// <summary>
    /// 自定义0x0200附加信息工厂
    /// </summary>
    public interface IJT905_0x0200_Custom_Factory: IJT905ExternalRegister
    {
        /// <summary>
        /// map JT905_0x0200_CustomBodyBase
        /// </summary>
        IDictionary<byte, object> Map { get; }
        /// <summary>
        /// map JT905_0x0200_CustomBodyBase2
        /// </summary>
        IDictionary<ushort, object> Map2 { get; }
        /// <summary>
        /// map JT905_0x0200_CustomBodyBase3
        /// </summary>
        IDictionary<ushort, object> Map3 { get; }
        /// <summary>
        /// map JT905_0x0200_CustomBodyBase4
        /// </summary>
        IDictionary<byte, object> Map4 { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT905_0x0200_CustomBody"></typeparam>
        /// <returns></returns>
        IJT905_0x0200_Custom_Factory SetMap<TJT905_0x0200_CustomBody>() where TJT905_0x0200_CustomBody : JT905_0x0200_CustomBodyBase;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT905_0x0200_CustomBody2"></typeparam>
        /// <returns></returns>
        IJT905_0x0200_Custom_Factory SetMap2<TJT905_0x0200_CustomBody2>() where TJT905_0x0200_CustomBody2 : JT905_0x0200_CustomBodyBase2;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT905_0x0200_CustomBody3"></typeparam>
        /// <returns></returns>
        IJT905_0x0200_Custom_Factory SetMap3<TJT905_0x0200_CustomBody3>() where TJT905_0x0200_CustomBody3 : JT905_0x0200_CustomBodyBase3;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TJT905_0x0200_CustomBody4"></typeparam>
        /// <returns></returns>
        IJT905_0x0200_Custom_Factory SetMap4<TJT905_0x0200_CustomBody4>() where TJT905_0x0200_CustomBody4 : JT905_0x0200_CustomBodyBase4;
    }
}
