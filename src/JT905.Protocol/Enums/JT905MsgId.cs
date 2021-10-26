﻿namespace JT905.Protocol.Enums
{
    /// <summary>
    /// JT905消息
    /// </summary>
    public enum JT905MsgId : ushort
    {
        /// <summary>
        /// ISU通用应答
        /// </summary>
        ISU通用应答 = 0x0001,

        /// <summary>
        /// 平台通用应答
        /// 0x8001
        /// </summary>
        中心通用应答 = 0x8001,
        /// <summary>
        /// 终端心跳
        /// 0x0002
        /// </summary>
        ISU心跳 = 0x0002,
        /// <summary>
        /// 补传分包请求
        /// 0x8003
        /// </summary>
        补传分包请求 = 0x8003,
        /// <summary>
        /// 终端注册
        /// 0x0100
        /// </summary>
        终端注册 = 0x0100,
        /// <summary>
        /// 终端注册应答
        /// 0x8100
        /// </summary>
        终端注册应答 = 0x8100,
        /// <summary>
        /// 终端注销
        /// 0x0003
        /// </summary>
        终端注销 = 0x0003,
        /// <summary>
        /// 终端鉴权
        /// 0x0102
        /// </summary>
        终端鉴权 = 0x0102,
        /// <summary>
        /// 设置终端参数
        /// 0x8103
        /// </summary>
        设置参数 = 0x8103,
        /// <summary>
        /// 查询终端参数
        /// 0x8104
        /// </summary>
        查询ISU参数 = 0x8104,
        /// <summary>
        /// 查询终端参数应答
        /// 0x0104
        /// </summary>
        查询ISU参数应答 = 0x0104,
        /// <summary>
        /// 终端控制
        /// 0x8105
        /// </summary>
        ISU控制 = 0x8105,

        /// <summary>
        /// ISU升级结果报告消息
        /// </summary>

        ISU升级结果报告消息 = 0x0105,
        /// <summary>
        /// 查询指定终端参数
        /// 0x8106
        /// </summary>
        查询指定终端参数 = 0x8106,
        /// <summary>
        /// 查询终端属性
        /// 0x8107
        /// </summary>
        查询终端属性 = 0x8107,
        /// <summary>
        /// 查询终端属性应答
        /// 0x0107
        /// </summary>
        查询终端属性应答 = 0x0107,
        /// <summary>
        /// 下发终端升级包
        /// 0x8108
        /// </summary>
        下发终端升级包 = 0x8108,
        /// <summary>
        /// 终端升级结果通知
        /// 0x0108
        /// </summary>
        终端升级结果通知 = 0x0108,
        /// <summary>
        /// 位置信息汇报
        /// 0x0200
        /// </summary>
        位置信息汇报 = 0x0200,
        /// <summary>
        /// 位置信息查询
        /// 0x8201
        /// </summary>
        位置信息查询 = 0x8201,
        /// <summary>
        /// 位置信息查询应答
        /// 0x0201
        /// </summary>
        位置信息查询应答 = 0x0201,
        /// <summary>
        /// 位置跟踪控制
        /// 0x8202
        /// </summary>
        位置跟踪控制 = 0x8202,
        /// <summary>
        /// 位置信息查询应答
        /// 0x0202
        /// </summary>
        位置跟踪信息汇报 = 0x0202,
        /// <summary>
        /// 位置信息查询应答
        /// 0x0203
        /// </summary>
        位置汇报数据补传 = 0x0203,

        /// <summary>
        /// 人工确认报警消息
        /// 0x8203
        /// </summary>
        人工确认报警消息 = 0x8203,
        /// <summary>
        /// 文本信息下发
        /// 0x8300
        /// </summary>
        文本信息下发 = 0x8300,
        /// <summary>
        /// 事件设置
        /// 0x8301
        /// </summary>
        事件设置 = 0x8301,
        /// <summary>
        /// 事件报告
        /// 0x0301
        /// </summary>
        事件报告 = 0x0301,
        /// <summary>
        /// 提问下发
        /// 0x8302
        /// </summary>
        提问下发 = 0x8302,
        /// <summary>
        /// 提问应答
        /// 0x0302
        /// </summary>
        提问应答 = 0x0302,
        /// <summary>
        /// 信息点播菜单设置
        /// 0x8303
        /// </summary>
        信息点播菜单设置 = 0x8303,
        /// <summary>
        /// 信息点播/取消
        /// 0x0303
        /// </summary>
        信息点播_取消 = 0x0303,
        /// <summary>
        /// 信息服务
        /// 0x8304
        /// </summary>
        信息服务 = 0x8304,
        /// <summary>
        /// 电话回拨
        /// 0x8400
        /// </summary>
        电话回拨 = 0x8400,
        /// <summary>
        /// 设置电话本
        /// 0x8401
        /// </summary>
        设置电话本 = 0x8401,
        /// <summary>
        /// 车辆控制
        /// 0x8500
        /// </summary>
        车辆控制 = 0x8500,
        /// <summary>
        /// 车辆控制应答
        /// 0x0500
        /// </summary>
        车辆控制应答 = 0x0500,
        /// <summary>
        /// 设置圆形区域
        /// 0x8600
        /// </summary>
        设置圆形区域 = 0x8600,
        /// <summary>
        /// 删除圆形区域
        /// 0x8601
        /// </summary>
        删除圆形区域 = 0x8601,
        /// <summary>
        /// 设置矩形区域
        /// 0x8602
        /// </summary>
        设置矩形区域 = 0x8602,
        /// <summary>
        /// 删除矩形区域
        /// 0x8603
        /// </summary>
        删除矩形区域 = 0x8603,
        /// <summary>
        /// 设置多边形区域
        /// 0x8604
        /// </summary>
        设置多边形区域 = 0x8604,
        /// <summary>
        /// 删除多边形区域
        /// 0x8605
        /// </summary>
        删除多边形区域 = 0x8605,
        /// <summary>
        /// 设置路线
        /// 0x8606
        /// </summary>
        设置路线 = 0x8606,
        /// <summary>
        /// 删除路线
        /// 0x8607
        /// </summary>
        删除路线 = 0x8607,
        /// <summary>
        /// 行驶记录仪数据采集命令
        /// 0x8700
        /// </summary>
        行驶记录仪数据采集命令 = 0x8700,
        /// <summary>
        /// 行驶记录仪数据上传
        /// 0x0700
        /// </summary>
        行驶记录仪数据上传 = 0x0700,
        /// <summary>
        /// 行驶记录仪参数下传命令
        /// 0x8701
        /// </summary>
        行驶记录仪参数下传命令 = 0x8701,
        /// <summary>
        /// 电子运单上报
        /// 0x0701
        /// </summary>
        电子运单上报 = 0x0701,
        /// <summary>
        /// 驾驶员身份信息采集上报
        /// 0x0702
        /// </summary>
        驾驶员身份信息采集上报 = 0x0702,
        /// <summary>
        /// 上报驾驶员身份信息请求
        /// 0x8702
        /// </summary>
        上报驾驶员身份信息请求 = 0x8702,
        /// <summary>
        /// 定位数据批量上传
        /// 0x0704
        /// </summary>
        定位数据批量上传 = 0x0704,
        /// <summary>
        /// CAN总线数据上传
        /// 0x0705
        /// </summary>
        CAN总线数据上传 = 0x0705,
        /// <summary>
        /// 多媒体事件信息上传
        /// 0x0800
        /// </summary>
        多媒体事件信息上传 = 0x0800,
        /// <summary>
        /// 多媒体数据上传
        /// 0x0801
        /// </summary>
        多媒体数据上传 = 0x0801,
        /// <summary>
        /// 多媒体数据上传应答
        /// 0x8800
        /// </summary>
        多媒体数据上传应答 = 0x8800,
        /// <summary>
        /// 摄像头立即拍摄命令
        /// 0x8801
        /// </summary>
        摄像头立即拍摄命令 = 0x8801,
        /// <summary>
        /// 摄像头立即拍摄命令应答
        /// 0x0805
        /// </summary>
        摄像头立即拍摄命令应答 = 0x0805,
        /// <summary>
        /// 存储多媒体数据检索
        /// 0x8802
        /// </summary>
        存储多媒体数据检索 = 0x8802,
        /// <summary>
        /// 存储多媒体数据上传
        /// 0x8803
        /// </summary>
        存储多媒体数据上传 = 0x8803,
        /// <summary>
        /// 录音开始命令
        /// 0x8804
        /// </summary>
        录音开始命令 = 0x8804,
        /// <summary>
        /// 单条存储多媒体数据检索上传命令
        /// 0x8805
        /// </summary>
        单条存储多媒体数据检索上传命令 = 0x8805,
        /// <summary>
        /// 数据下行透传
        /// 0x8900
        /// </summary>
        数据下行透传 = 0x8900,
        /// <summary>
        /// 数据上行透传
        /// 0x0900
        /// </summary>
        数据上行透传 = 0x0900,
        /// <summary>
        /// 数据压缩上报
        /// 0x0901
        /// </summary>
        数据压缩上报 = 0x0901,
        /// <summary>
        ///  平台RSA公钥 
        ///  0x8A00
        /// </summary>
        平台RSA公钥 = 0x8A00,
        /// <summary>
        ///  终端RSA公钥 
        ///  0x0A00
        /// </summary>
        终端RSA公钥 = 0x0A00,
        /// <summary>
        ///  查询服务器时间请求 
        ///  0x0004
        /// </summary>
        查询服务器时间请求 = 0x0004,
        /// <summary>
        ///  查询服务器时间应答 
        ///  0x8004
        /// </summary>
        查询服务器时间应答 = 0x8004,
        /// <summary>
        ///  终端补传分包请求 
        ///  0x0005
        /// </summary>
        终端补传分包请求 = 0x0005,
        /// <summary>
        ///  链路检测 
        ///  0x8204
        /// </summary>
        链路检测 = 0x8204,
        /// <summary>
        ///  查询区域或线路数据 
        ///  0x8608
        /// </summary>
        查询区域或线路数据 = 0x8608,
        /// <summary>
        ///  查询区域或线路数据应答 
        ///  0x0608
        /// </summary>
        查询区域或线路数据应答 = 0x0608,
        /// <summary>
        ///  存储多媒体数据检索应答 
        ///  0x0802
        /// </summary>
        存储多媒体数据检索应答 = 0x0802,
        /// <summary>
        /// 上班签到信息
        /// 0x0B03
        /// </summary>
        上班签到信息上传 = 0x0B03,
        
    }
}
