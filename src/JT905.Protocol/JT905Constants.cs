using System;
using System.Collections.Generic;
using System.Text;

namespace JT905.Protocol
{
    /// <summary>
    /// JT905常量
    /// </summary>
    public static class JT905Constants
    {
        static JT905Constants()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding = Encoding.GetEncoding("GBK");
        }
        /// <summary>
        /// 日期限制于2000年
        /// </summary>
        public const int DateLimitYear = 2000;
        /// <summary>
        /// 
        /// </summary>
        public static readonly DateTime UTCBaseTime = new DateTime(1970, 1, 1);
        /// <summary>
        /// 
        /// </summary>
        public static Encoding Encoding { get; }
        #region JT905_0x0200
        /// <summary>
        /// 里程，UINT32，0. 1km，对应车上里程表读数
        /// JT905_0x0200_0x01
        /// </summary>
        public const byte JT905_0x0200_0x01 = 0x01;
        /// <summary>
        /// 油量，UINT16，0. 1L，对应车上油量表读数
        /// JT905_0x0200_0x02
        /// </summary>
        public const byte JT905_0x0200_0x02 = 0x02;
        /// <summary>
        /// 海拔高度，INT16，单位为米(m)
        /// JT905_0x0200_0x03
        /// </summary>
        public const byte JT905_0x0200_0x03 = 0x03;
       
        /// <summary>
        /// 超速报警附加信息
        /// JT905_0x0200_0x11
        /// </summary>
        public const byte JT905_0x0200_0x11 = 0x11;
        /// <summary>
        /// 进出区域/路线报警附加信息
        /// JT905_0x0200_0x12
        /// </summary>
        public const byte JT905_0x0200_0x12 = 0x12;
        /// <summary>
        /// 路段行驶时间不足 /   过长报警附加信息
        /// JT905_0x0200_0x13
        /// </summary>
        public const byte JT905_0x0200_0x13 = 0x13;
        /// <summary>
        /// 禁行路段行驶报警附加信息
        /// JT905_0x0200_0x14
        /// </summary>
        public const byte JT905_0x0200_0x25 = 0x14;

        #endregion

        #region JT905_0x8103
        /// <summary>
        /// ISU心跳发送间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0001 = 0x0001;
        /// <summary>
        /// TCP消息应答超时时间，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0002 = 0x0002;
        /// <summary>
        /// TCP消息重传次数
        /// </summary>
        public const ushort JT905_0x8103_0x0003 = 0x0003;
        /// <summary>
        /// SMS消息应答超时时间，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0004 = 0x0004;
        /// <summary>
        /// SMS消息重传次数
        /// </summary>
        public const ushort JT905_0x8103_0x0005 = 0x0005;
        /// <summary>
        /// 主服务器APN，无线通信拨号访问点
        /// </summary>
        public const ushort JT905_0x8103_0x0010 = 0x0010;
        /// <summary>
        /// 主服务器无线通信拨号用户名
        /// </summary>
        public const ushort JT905_0x8103_0x0011 = 0x0011;
        /// <summary>
        /// 主服务器无线通信拨号密码
        /// </summary>
        public const ushort JT905_0x8103_0x0012 = 0x0012;
        /// <summary>
        /// 主服务器地址，IP或域名
        /// </summary>
        public const ushort JT905_0x8103_0x0013 = 0x0013;
        /// <summary>
        /// 备份服务器APN，无线通信拨号访问点
        /// </summary>
        public const ushort JT905_0x8103_0x0014 = 0x0014;
        /// <summary>
        /// 备份服务器无线通信拨号用户名
        /// </summary>
        public const ushort JT905_0x8103_0x0015 = 0x0015;
        /// <summary>
        /// 备份服务器无线通信拨号密码
        /// </summary>
        public const ushort JT905_0x8103_0x0016 = 0x0016;
        /// <summary>
        /// 备份服务器地址，IP或域名
        /// </summary>
        public const ushort JT905_0x8103_0x0017 = 0x0017;
        /// <summary>
        /// 主服务器TCP端口
        /// </summary>
        public const ushort JT905_0x8103_0x0018 = 0x0018;
        /// <summary>
        /// 备份服务器TCP端口
        /// </summary>
        public const ushort JT905_0x8103_0x0019 = 0x0019;
        /// <summary>
        /// 一卡通或支付平台主服务器地址，IP或域名
        /// </summary>
        public const ushort JT905_0x8103_0x001A = 0x001A;
        /// <summary>
        /// 一卡通或支付平台主服务器TCP端口
        /// </summary>
        public const ushort JT905_0x8103_0x001B = 0x001B;
        /// <summary>
        /// 一卡通或支付平台备份服务器地址，IP或域名
        /// </summary>
        public const ushort JT905_0x8103_0x001C = 0x001C;
        /// <summary>
        /// 一卡通或支付平台备份服务器TCP端口
        /// </summary>
        public const ushort JT905_0x8103_0x001D = 0x001D;
        /// <summary>
        /// 位置汇报策略，0：定时汇报；1：定距汇报；2：定时+定距汇报
        /// </summary>
        public const ushort JT905_0x8103_0x0020 = 0x0020;
        /// <summary>
        /// 位置汇报方案，0：根据ACC状态；1：根据空重车状态；2：根据登录状态+ACC状态，先判断登录状态，若登录再根据ACC状态；3：根据登录状态+空重车状态，先判断登录状态，若登录再根据空重车状态
        /// </summary>
        public const ushort JT905_0x8103_0x0021 = 0x0021;
        /// <summary>
        /// 未登录汇报时间间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0022 = 0x0022;
        /// <summary>
        /// ACCOFF汇报时间间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0023 = 0x0023;
        /// <summary>
        /// ACCON汇报时间间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0024 = 0x0024;
        /// <summary>
        /// 空车汇报时间间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0025 = 0x0025;
        /// <summary>
        /// 重车汇报时间间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0026 = 0x0026;
        /// <summary>
        /// 休眠时汇报时间间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0027 = 0x0027;
        /// <summary>
        /// 紧急报警时汇报时间间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0028 = 0x0028;
        /// <summary>
        /// 未登录汇报距离间隔，单位为米(m)
        /// </summary>
        public const ushort JT905_0x8103_0x0029 = 0x0029;
        /// <summary>
        /// ACCOFF汇报距离间隔，单位为米(m)
        /// </summary>
        public const ushort JT905_0x8103_0x002A = 0x002A;
        /// <summary>
        /// ACCON汇报距离间隔，单位为米(m)
        /// </summary>
        public const ushort JT905_0x8103_0x002B = 0x002B;
        /// <summary>
        /// 空车汇报距离间隔，单位为米(m)
        /// </summary>
        public const ushort JT905_0x8103_0x002C = 0x002C;
        /// <summary>
        /// 重车汇报距离间隔，单位为米(m)
        /// </summary>
        public const ushort JT905_0x8103_0x002D = 0x002D;
        /// <summary>
        /// 休眠时汇报距离间隔，单位为米(m)
        /// </summary>
        public const ushort JT905_0x8103_0x002E = 0x002E;
        /// <summary>
        /// 紧急报警时汇报距离间隔，单位为米(m)
        /// </summary>
        public const ushort JT905_0x8103_0x002F = 0x002F;
        /// <summary>
        /// 拐点补传角度，﹤180°
        /// </summary>
        public const ushort JT905_0x8103_0x0030 = 0x0030;
        /// <summary>
        /// 监控指挥中心电话号码
        /// </summary>
        public const ushort JT905_0x8103_0x0040 = 0x0040;
        /// <summary>
        /// 复位电话号码
        /// </summary>
        public const ushort JT905_0x8103_0x0041 = 0x0041;
        /// <summary>
        /// 恢复出厂设置电话号码
        /// </summary>
        public const ushort JT905_0x8103_0x0042 = 0x0042;
        /// <summary>
        /// 监控指挥中心SMS电话号码
        /// </summary>
        public const ushort JT905_0x8103_0x0043 = 0x0043;
        /// <summary>
        /// 接收ISUSMS文本报警号码
        /// </summary>
        public const ushort JT905_0x8103_0x0044 = 0x0044;
        /// <summary>
        /// ISU电话接听策略，0：自动接听；1：ACCON时自动接听，OFF时手动接听
        /// </summary>
        public const ushort JT905_0x8103_0x0045 = 0x0045;
        /// <summary>
        /// 每次最长通话时间，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0046 = 0x0046;
        /// <summary>
        /// 当月最长通话时间，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0047 = 0x0047;
        /// <summary>
        /// 电话短号长度
        /// </summary>
        public const ushort JT905_0x8103_0x0048 = 0x0048;
        /// <summary>
        /// 监听电话号码
        /// </summary>
        public const ushort JT905_0x8103_0x0049 = 0x0049;
        /// <summary>
        /// ISU设备维护密码
        /// </summary>
        public const ushort JT905_0x8103_0x004A = 0x004A;
        /// <summary>
        /// ISU的语音播报音量控制：0～9(0为静音，9为最高)
        /// </summary>
        public const ushort JT905_0x8103_0x004B = 0x004B;
        /// <summary>
        /// 报警屏蔽字，与位置信息汇报消息中的报警标志相对应，相应位为1，则相应报警被屏蔽
        /// </summary>
        public const ushort JT905_0x8103_0x0050 = 0x0050;
        /// <summary>
        /// 报警发送文本SMS开关，与位置信息汇报消息中的报警标志相对应，相应位为1，则相应报警时发送文本SMS
        /// </summary>
        public const ushort JT905_0x8103_0x0051 = 0x0051;
        /// <summary>
        /// 报警拍摄开关，与位置信息汇报消息中的报警标志相对应，相应位为1，则相应报警时摄像头拍摄
        /// </summary>
        public const ushort JT905_0x8103_0x0052 = 0x0052;
        /// <summary>
        /// 报警拍摄存储标志，与位置信息汇报消息中的报警标志相对应，相应位为1，则对相应报警时拍的照片进行存储，否则实时上传
        /// </summary>
        public const ushort JT905_0x8103_0x0053 = 0x0053;
        /// <summary>
        /// 最高速度，单位为千米每小时(km/h)
        /// </summary>
        public const ushort JT905_0x8103_0x0055 = 0x0055;
        /// <summary>
        /// 超速持续时间，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0056 = 0x0056;
        /// <summary>
        /// 连续驾驶时间门限，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0057 = 0x0057;
        /// <summary>
        /// 最小休息时间，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0058 = 0x0058;
        /// <summary>
        /// 最长停车时间，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x0059 = 0x0059;
        /// <summary>
        /// 当天累计驾驶时间门限，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x005A = 0x005A;
        /// <summary>
        /// 图像/视频质量，1～10，1最好
        /// </summary>
        public const ushort JT905_0x8103_0x0070 = 0x0070;
        /// <summary>
        /// 亮度，0～255
        /// </summary>
        public const ushort JT905_0x8103_0x0071 = 0x0071;
        /// <summary>
        /// 对比度，0～127
        /// </summary>
        public const ushort JT905_0x8103_0x0072 = 0x0072;
        /// <summary>
        /// 饱和度，0～127
        /// </summary>
        public const ushort JT905_0x8103_0x0073 = 0x0073;
        /// <summary>
        /// 色度，0～255
        /// </summary>
        public const ushort JT905_0x8103_0x0074 = 0x0074;
        /// <summary>
        /// 车辆里程表读数，0.1km
        /// </summary>
        public const ushort JT905_0x8103_0x0080 = 0x0080;
        /// <summary>
        /// 车辆所在的省域ID，1～255
        /// </summary>
        public const ushort JT905_0x8103_0x0081 = 0x0081;
        /// <summary>
        /// 车辆所在的市域ID，1～255
        /// </summary>
        public const ushort JT905_0x8103_0x0082 = 0x0082;
        /// <summary>
        /// 计价器营运次数限制，0～9999；0表示不做限制
        /// </summary>
        public const ushort JT905_0x8103_0x0090 = 0x0090;
        /// <summary>
        /// 计价器营运时间限制，YYYYMMDDhh，0000000000表示不做限制
        /// </summary>
        public const ushort JT905_0x8103_0x0091 = 0x0091;
        /// <summary>
        /// 出租车企业营运许可证号
        /// </summary>
        public const ushort JT905_0x8103_0x0092 = 0x0092;
        /// <summary>
        /// 出租车企业简称
        /// </summary>
        public const ushort JT905_0x8103_0x0093 = 0x0093;
        /// <summary>
        /// 出租车车牌号码(不包括汉字)
        /// </summary>
        public const ushort JT905_0x8103_0x0094 = 0x0094;
        /// <summary>
        /// ISU录音模式(0x01：全程录音；0x02：翻牌录音)
        /// </summary>
        public const ushort JT905_0x8103_0x00A0 = 0x00A0;
        /// <summary>
        /// ISU录音文件最大时长，1～255，单位为分钟(min)
        /// </summary>
        public const ushort JT905_0x8103_0x00A1 = 0x00A1;
        /// <summary>
        /// 液晶(LCD)心跳时间间隔，1～60，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x00A2 = 0x00A2;
        /// <summary>
        /// LED心跳时间间隔，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x00A3 = 0x00A3;
        /// <summary>
        /// ACCOFF后进入休眠模式的时间，单位为秒(s)
        /// </summary>
        public const ushort JT905_0x8103_0x00AF = 0x00AF;
        /// <summary>
        /// 视频服务器协议模式，0x00：TCP；0x01：UDP
        /// </summary>
        public const ushort JT905_0x8103_0x00B0 = 0x00B0;
        /// <summary>
        /// 视频服务器APN，无线通信拨号访问点
        /// </summary>
        public const ushort JT905_0x8103_0x00B1 = 0x00B1;
        /// <summary>
        /// 视频服务器无线通信拨号用户名
        /// </summary>
        public const ushort JT905_0x8103_0x00B2 = 0x00B2;
        /// <summary>
        /// 视频服务器无线通信拨号密码
        /// </summary>
        public const ushort JT905_0x8103_0x00B3 = 0x00B3;
        /// <summary>
        /// 视频服务器地址，IP或域名
        /// </summary>
        public const ushort JT905_0x8103_0x00B4 = 0x00B4;
        /// <summary>
        /// 视频服务器端口
        /// </summary>
        public const ushort JT905_0x8103_0x00B5 = 0x00B5;

        #endregion
    }
}
