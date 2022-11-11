using JT905.Protocol.Interfaces;
using JT905.Protocol.Extensions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using JT905.Protocol.MessageBody;
using JT905.Protocol.Internal;

namespace JT905.Protocol.Test.Simples
{
    public class Demo1
    {
        public JT905Serializer JT905Serializer;
        public Demo1()
        {
            IJT905Config JT905Config = new DefaultGlobalConfig();
            JT905Serializer = new JT905Serializer(JT905Config);
        }

        [Fact]
        public void Test1()
        {
            JT905Package JT905Package = new JT905Package();
            JT905Package.Header = new JT905Header
            {
                MsgId = Enums.JT905MsgId.位置信息汇报.ToUInt16Value(),
                ManualMsgNum = 126,
                ISU = "103456789012"
            };

            JT905_0x0200 JT905_0x0200 = new JT905_0x0200
            {
                AlarmFlag = 1,
                GPSTime = DateTime.Parse("2021-10-15 21:10:10"),
                Lat = 12222222,
                Lng = 132444444,
                Speed = 60,
                Direction = 0,
                StatusFlag = 2,
                BasicLocationAttachData = new Dictionary<byte, JT905_0x0200_BodyBase>()
            };

            JT905_0x0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x01, new JT905_0x0200_0x01
            {
                Mileage = 100
            });

            JT905_0x0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x02, new JT905_0x0200_0x02
            {
                Oil = 125
            });

            JT905Package.Bodies = JT905_0x0200;

            byte[] data = JT905Serializer.Serialize(JT905Package);

            var hex = data.ToHexString();

            Assert.Equal("7E02000023103456789012007D02000000010000000200BA7F0E07E4F11C003C002110152110100104000000640202007D01347E", hex);
        }
    }
}
