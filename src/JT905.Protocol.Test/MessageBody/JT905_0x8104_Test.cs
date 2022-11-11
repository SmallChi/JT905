using JT905.Protocol.Extensions;
using JT905.Protocol.MessageBody;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT905.Protocol.Test.MessageBody
{
    public class JT905_0x8104_Test
    {
        JT905Serializer JT905Serializer = new JT905Serializer();
        [Fact]
        public void Test1()
        {
            JT905MessagePackWriter writer = new JT905MessagePackWriter();
            JT905Package JT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.查询ISU参数.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8104
                {
                    ParamIds = new List<ushort>() {
                        ////ISU心跳发送间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0001,
                        ////TCP消息应答超时时间，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0002,
                        ////TCP消息重传次数
                        //JT905Constants.JT905_0x8103_0x0003,
                        ////SMS消息应答超时时间，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0004,
                        ////SMS消息重传次数
                        //JT905Constants.JT905_0x8103_0x0005,
                        ////主服务器APN，无线通信拨号访问点
                        //JT905Constants.JT905_0x8103_0x0010,
                        //主服务器无线通信拨号用户名
                        JT905Constants.JT905_0x8103_0x0011,
                        //主服务器无线通信拨号密码
                        JT905Constants.JT905_0x8103_0x0012,
                        //主服务器地址，IP或域名
                        JT905Constants.JT905_0x8103_0x0013,
                        //备份服务器APN，无线通信拨号访问点
                        JT905Constants.JT905_0x8103_0x0014,
                        //备份服务器无线通信拨号用户名
                        JT905Constants.JT905_0x8103_0x0015,
                        //备份服务器无线通信拨号密码
                        JT905Constants.JT905_0x8103_0x0016,
                        //备份服务器地址，IP或域名
                        JT905Constants.JT905_0x8103_0x0017,
                        //主服务器TCP端口
                        JT905Constants.JT905_0x8103_0x0018,
                        ////备份服务器TCP端口
                        //JT905Constants.JT905_0x8103_0x0019,
                        ////一卡通或支付平台主服务器地址，IP或域名
                        //JT905Constants.JT905_0x8103_0x001A,
                        ////一卡通或支付平台主服务器TCP端口
                        //JT905Constants.JT905_0x8103_0x001B,
                        ////一卡通或支付平台备份服务器地址，IP或域名
                        //JT905Constants.JT905_0x8103_0x001C,
                        ////一卡通或支付平台备份服务器TCP端口
                        //JT905Constants.JT905_0x8103_0x001D,
                        ////位置汇报策略，0：定时汇报；1：定距汇报；2：定时+定距汇报
                        //JT905Constants.JT905_0x8103_0x0020,
                        ////位置汇报方案，0：根据ACC状态；1：根据空重车状态；2：根据登录状态+ACC状态，先判断登录状态，若登录再根据ACC状态；3：根据登录状态+空重车状态，先判断登录状态，若登录再根据空重车状态
                        //JT905Constants.JT905_0x8103_0x0021,
                        ////未登录汇报时间间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0022,
                        ////ACCOFF汇报时间间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0023,
                        ////ACCON汇报时间间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0024,
                        ////空车汇报时间间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0025,
                        ////重车汇报时间间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0026,
                        ////休眠时汇报时间间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0027,
                        ////紧急报警时汇报时间间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0028,
                        ////未登录汇报距离间隔，单位为米(m)
                        //JT905Constants.JT905_0x8103_0x0029,
                        ////ACCOFF汇报距离间隔，单位为米(m)
                        //JT905Constants.JT905_0x8103_0x002A,
                        ////ACCON汇报距离间隔，单位为米(m)
                        //JT905Constants.JT905_0x8103_0x002B,
                        ////空车汇报距离间隔，单位为米(m)
                        //JT905Constants.JT905_0x8103_0x002C,
                        ////重车汇报距离间隔，单位为米(m)
                        //JT905Constants.JT905_0x8103_0x002D,
                        ////休眠时汇报距离间隔，单位为米(m)
                        //JT905Constants.JT905_0x8103_0x002E,
                        ////紧急报警时汇报距离间隔，单位为米(m)
                        //JT905Constants.JT905_0x8103_0x002F,
                        ////拐点补传角度，﹤180°
                        //JT905Constants.JT905_0x8103_0x0030,
                        ////监控指挥中心电话号码
                        //JT905Constants.JT905_0x8103_0x0040,
                        ////复位电话号码
                        //JT905Constants.JT905_0x8103_0x0041,
                        ////恢复出厂设置电话号码
                        //JT905Constants.JT905_0x8103_0x0042,
                        ////监控指挥中心SMS电话号码
                        //JT905Constants.JT905_0x8103_0x0043,
                        ////接收ISUSMS文本报警号码
                        //JT905Constants.JT905_0x8103_0x0044,
                        ////ISU电话接听策略，0：自动接听；1：ACCON时自动接听，OFF时手动接听
                        //JT905Constants.JT905_0x8103_0x0045,
                        ////每次最长通话时间，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0046,
                        ////当月最长通话时间，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0047,
                        ////电话短号长度
                        //JT905Constants.JT905_0x8103_0x0048,
                        ////监听电话号码
                        //JT905Constants.JT905_0x8103_0x0049,
                        ////ISU设备维护密码
                        //JT905Constants.JT905_0x8103_0x004A,
                        ////ISU的语音播报音量控制：0～9(0为静音，9为最高)
                        //JT905Constants.JT905_0x8103_0x004B,
                        ////报警屏蔽字，与位置信息汇报消息中的报警标志相对应，相应位为1，则相应报警被屏蔽
                        //JT905Constants.JT905_0x8103_0x0050,
                        ////报警发送文本SMS开关，与位置信息汇报消息中的报警标志相对应，相应位为1，则相应报警时发送文本SMS
                        //JT905Constants.JT905_0x8103_0x0051,
                        ////报警拍摄开关，与位置信息汇报消息中的报警标志相对应，相应位为1，则相应报警时摄像头拍摄
                        //JT905Constants.JT905_0x8103_0x0052,
                        ////报警拍摄存储标志，与位置信息汇报消息中的报警标志相对应，相应位为1，则对相应报警时拍的照片进行存储，否则实时上传
                        //JT905Constants.JT905_0x8103_0x0053,
                        ////最高速度，单位为千米每小时(km/h)
                        //JT905Constants.JT905_0x8103_0x0055,
                        ////超速持续时间，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0056,
                        ////连续驾驶时间门限，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0057,
                        ////最小休息时间，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0058,
                        ////最长停车时间，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x0059,
                        ////当天累计驾驶时间门限，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x005A,
                        ////图像/视频质量，1～10，1最好
                        //JT905Constants.JT905_0x8103_0x0070,
                        ////亮度，0～255
                        //JT905Constants.JT905_0x8103_0x0071,
                        ////对比度，0～127
                        //JT905Constants.JT905_0x8103_0x0072,
                        ////饱和度，0～127
                        //JT905Constants.JT905_0x8103_0x0073,
                        ////色度，0～255
                        //JT905Constants.JT905_0x8103_0x0074,
                        ////车辆里程表读数，0.1km
                        //JT905Constants.JT905_0x8103_0x0080,
                        ////车辆所在的省域ID，1～255
                        //JT905Constants.JT905_0x8103_0x0081,
                        ////车辆所在的市域ID，1～255
                        //JT905Constants.JT905_0x8103_0x0082,
                        ////计价器营运次数限制，0～9999；0表示不做限制
                        //JT905Constants.JT905_0x8103_0x0090,
                        ////计价器营运时间限制，YYYYMMDDhh，0000000000表示不做限制
                        //JT905Constants.JT905_0x8103_0x0091,
                        ////出租车企业营运许可证号
                        //JT905Constants.JT905_0x8103_0x0092,
                        ////出租车企业简称
                        //JT905Constants.JT905_0x8103_0x0093,
                        ////出租车车牌号码(不包括汉字)
                        //JT905Constants.JT905_0x8103_0x0094,
                        ////ISU录音模式(0x01：全程录音；0x02：翻牌录音)
                        //JT905Constants.JT905_0x8103_0x00A0,
                        ////ISU录音文件最大时长，1～255，单位为分钟(min)
                        //JT905Constants.JT905_0x8103_0x00A1,
                        ////液晶(LCD)心跳时间间隔，1～60，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x00A2,
                        ////LED心跳时间间隔，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x00A3,
                        ////ACCOFF后进入休眠模式的时间，单位为秒(s)
                        //JT905Constants.JT905_0x8103_0x00AF,
                        ////视频服务器协议模式，0x00：TCP；0x01：UDP
                        //JT905Constants.JT905_0x8103_0x00B0,
                        ////视频服务器APN，无线通信拨号访问点
                        //JT905Constants.JT905_0x8103_0x00B1,
                        ////视频服务器无线通信拨号用户名
                        //JT905Constants.JT905_0x8103_0x00B2,
                        ////视频服务器无线通信拨号密码
                        //JT905Constants.JT905_0x8103_0x00B3,
                        ////视频服务器地址，IP或域名
                        //JT905Constants.JT905_0x8103_0x00B4,
                        ////视频服务器端口
                        //JT905Constants.JT905_0x8103_0x00B5
                    }
                }
            };
            var hex = JT905Serializer.Serialize(JT905Package).ToHexString();
            Assert.Equal("7E81040010108000000316000000110012001300140015001600170018187E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E81040006012345678900000A000100020003007E".ToHexBytes();
            JT905Package JT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);
            Assert.Equal(Enums.JT905MsgId.查询ISU参数.ToUInt16Value(), JT905Package.Header.MsgId);
            Assert.Equal(10, JT905Package.Header.MsgNum);
            Assert.Equal("12345678900", JT905Package.Header.ISU);

            JT905_0x8104 bodies = (JT905_0x8104)JT905Package.Bodies;
            Assert.Equal(JT905Constants.JT905_0x8103_0x0001, bodies.ParamIds[0]);
            Assert.Equal(JT905Constants.JT905_0x8103_0x0002, bodies.ParamIds[1]);
            Assert.Equal(JT905Constants.JT905_0x8103_0x0003, bodies.ParamIds[2]);

        }

        [Fact]
        public void Test3()
        {
            var bytes = "7e8104000210010739502700000010cf7e".ToHexBytes();
            string json = JT905Serializer.Analyze<JT905Package>(bytes, options: JTJsonWriterOptions.Instance);
        }

        [Fact]
        public void Test4()
        {
            string bcdTxt = "10";
            string v = bcdTxt.Insert(0, new string('0', 2));
            //string v1 = bcdTxt.PadLeft(4,'\0'); 

        }
        [Fact]
        public void Test5()
        {
            string bcdTxt = "10";
            int startIndex = 0;
            //string v = bcdTxt.Insert(0, new string('\0', 2));
            string v1 = bcdTxt.PadLeft(4, '0');
            int byteIndex = 0;
            int count = 4 / 2;
            byte[] aryTemp = new byte[count];
            var bcdSpan = v1.AsSpan();
            for (int i = 0; i < count; i++)
            {
                aryTemp[i] = Convert.ToByte(bcdSpan.Slice(startIndex, 2).ToString(),16);
                startIndex += 2;
            }
            string v2 = aryTemp.ToHexString();

            string aa = "01";
            int year = 123456700;
            string v = year.ToBCDByte(5).ToHexString();

        }
    }
}
