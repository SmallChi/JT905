﻿using System;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// 报警标志
    /// </summary>
    [Flags]
    public enum JT905Alarm : uint
    {

        ///<summary>
        ///紧急报警，触动报警开关后触发
        ///标志维持至报警条件解除
        ///</summary>
        紧急报警_触动报警开关后触发 = 1,
        ///<summary>
        ///预警
        ///标志维持至预警条件解除或预警转化成为报警事件
        ///</summary>
        预警 = 2,
        ///<summary>
        ///卫星定位模块发生故障
        ///标志维持至报警条件解除
        ///</summary>
        卫星定位模块发生故障 = 4,
        ///<summary>
        ///卫星定位天线未接或被剪断
        ///标志维持至报警条件解除
        ///</summary>
        卫星定位天线未接或被剪断 = 8,
        ///<summary>
        ///卫星定位天线短路
        ///标志维持至报警条件解除
        ///</summary>
        卫星定位天线短路 = 16,
        ///<summary>
        ///ISU主电源欠压
        ///标志维持至报警条件解除
        ///</summary>
        ISU主电源欠压 = 32,
        ///<summary>
        ///ISU主电源掉电
        ///标志维持至报警条件解除
        ///</summary>
        ISU主电源掉电 = 64,
        ///<summary>
        ///液晶LCD显示ISU故障
        ///标志维持至报警条件解除
        ///</summary>
        液晶LCD显示ISU故障 = 128,
        ///<summary>
        ///语音合成TTS模块故障
        ///标志维持至报警条件解除
        ///</summary>
        语音合成TTS模块故障 = 256,
        ///<summary>
        ///摄像头故障
        ///标志维持至报警条件解除
        ///</summary>
        摄像头故障 = 512,
        ///<summary>
        ///计价器故障
        ///标志维持至报警条件解除
        ///</summary>
        计价器故障 = 1024,
        ///<summary>
        ///服务评价器故障前后排
        ///标志维持至报警条件解除
        ///</summary>
        服务评价器故障 = 2048,
        ///<summary>
        ///LED广告屏故障
        ///标志维持至报警条件解除
        ///</summary>
        LED广告屏故障 = 4096,
        ///<summary>
        ///液晶LCD显示屏故障
        ///标志维持至报警条件解除
        ///</summary>
        液晶LCD显示屏故障 = 8192,
        ///<summary>
        ///LED顶灯故障
        ///标志维持至报警条件解除
        ///</summary>
        LED顶灯故障 = 32768,
        ///<summary>
        ///超速报警
        ///标志维持至报警条件解除
        ///</summary>
        超速报警 = 65536,
        ///<summary>
        ///连续驾驶超时
        ///标志维持至报警条件解除
        ///</summary>
        连续驾驶超时 = 131072,
        ///<summary>
        ///当天累计驾驶超时
        ///标志维持至报警条件解除
        ///</summary>
        当天累计驾驶超时 = 262144,
        ///<summary>
        ///超时停车
        ///标志维持至报警条件解除
        ///</summary>
        超时停车 = 524288,
        ///<summary>
        ///进出区域/路线
        ///收到应答后清0
        ///</summary>
        进出区域_路线 = 1048576,
        ///<summary>
        ///路段行驶时间不足/过长
        ///收到应答后清0
        ///</summary>
        路段行驶时间不足_过长 = 2097152,
        ///<summary>
        ///禁行路段行驶
        ///收到应答后清0
        ///</summary>
        禁行路段行驶 = 4194304,
        ///<summary>
        ///车速传感器故障
        ///标志维持至报警条件解除
        ///</summary>
        车速传感器故障 = 8388608,
        ///<summary>
        ///车辆非法点火
        ///收到应答后清0
        ///</summary>
        车辆非法点火 = 16777216,
        ///<summary>
        ///车辆非法位移
        ///收到应答后清0
        ///</summary>
        车辆非法位移 = 33554432,
        ///<summary>
        ///ISU存储异常
        ///标志维持至报警条件解除
        ///</summary>
        ISU存储异常 = 67108864,
        ///<summary>
        ///录音设备故障
        ///标志维持至报警条件解除
        ///</summary>
        录音设备故障 = 134217728,
        ///<summary>
        ///计价器实时时钟超过规定的误差范围
        ///标志维持至报警条件解除
        ///</summary>
        计价器实时时钟超过规定的误差范围 = 268435456,

    }
}