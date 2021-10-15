using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.Internal;
using JT905.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace JT905.Protocol.Test.Simples
{
    public class Demo2
    {
        public JT905Serializer JT905Serializer;
        public Demo2()
        {
            IJT905Config JT905Config = new DefaultGlobalConfig();
            JT905Serializer = new JT905Serializer(JT905Config);
        }

        [Fact]
        public void Test1()
        {
            //1.转成byte数组
            byte[] bytes = "7E02000023103456789012007D02000000010000000200BA7F0E07E4F11C003C002110152110100104000000640202007D01347E".ToHexBytes();

            //2.将数组反序列化
            var jT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);

            //3.数据包头
            Assert.Equal(Enums.JT905MsgId.位置信息汇报.ToValue(), jT905Package.Header.MsgId);
            Assert.Equal(0x23, jT905Package.Header.DataLength);
            Assert.Equal(126, jT905Package.Header.MsgNum);
            Assert.Equal("103456789012", jT905Package.Header.ISU);

            //4.数据包体
            JT905_0x0200 jT905_0x0200 = (JT905_0x0200)jT905Package.Bodies;
            Assert.Equal((uint)1, jT905_0x0200.AlarmFlag);
            Assert.Equal(DateTime.Parse("2021-10-15 21:10:10"), jT905_0x0200.GPSTime);
            Assert.Equal(12222222, jT905_0x0200.Lat);
            Assert.Equal(132444444, jT905_0x0200.Lng);
            Assert.Equal(60, jT905_0x0200.Speed);
            Assert.Equal(0, jT905_0x0200.Direction);
            Assert.Equal((uint)2, jT905_0x0200.StatusFlag);
            //4.1.附加信息1
            Assert.Equal(100, ((JT905_0x0200_0x01)jT905_0x0200.BasicLocationAttachData[JT905Constants.JT905_0x0200_0x01]).Mileage);
            //4.2.附加信息2
            Assert.Equal(125, ((JT905_0x0200_0x02)jT905_0x0200.BasicLocationAttachData[JT905Constants.JT905_0x0200_0x02]).Oil);
        }
    }
}
