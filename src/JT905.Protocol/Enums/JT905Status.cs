using System;

namespace JT905.Protocol.Enums
{
    /// <summary>
    /// JT808车辆状态位
    /// </summary>
    [Flags]
    public enum JT905Status : uint
    {
        ///<summary>
        ///未卫星定位
        ///</summary>
        未卫星定位 = 1,
        ///<summary>
        ///南纬
        ///</summary>
        南纬 = 2,
        ///<summary>
        ///西经
        ///</summary>
        西经 = 4,
        ///<summary>
        ///停运状态
        ///</summary>
        停运状态 = 8,
        ///<summary>
        ///预约(任务车)
        ///</summary>
        预约_任务车=16,
        ///<summary>
        ///空转重
        ///</summary>
        空转重 = 32,
        ///<summary>
        ///重转空
        ///</summary>
        重转空 = 64,
        ///<summary>
        ///预留
        ///</summary>
        预留 = 128,
        ///<summary>
        ///ACC开
        ///</summary>
        ACC开 = 256,
        ///<summary>
        ///重车
        ///</summary>
        重车 = 512,
        ///<summary>
        ///车辆油路断开
        ///</summary>
        车辆油路断开 = 1024,
        ///<summary>
        ///车辆电路断开
        ///</summary>
        车辆电路断开 = 2048,
        ///<summary>
        ///车门加锁
        ///</summary>
        车门加锁 = 4096,
        ///<summary>
        ///车辆锁定
        ///</summary>
        车辆锁定 = 8192,
        ///<summary>
        ///已达到限制营运次数_时间
        ///</summary>
        已达到限制营运次数_时间 = 16384,

    }
}
