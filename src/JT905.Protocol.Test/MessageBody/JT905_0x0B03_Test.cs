using JT905.Protocol.Extensions;
using JT905.Protocol.Internal;
using JT905.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JT905.Protocol.Test.MessageBody
{
    public class JT905_0x0B03_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0B03_Test()
        {
            IJT905Config JT905Config = new DefaultGlobalConfig();
            JT905Config.SkipCRCCode = true;
            JT905Serializer = new JT905Serializer(JT905Config);
        }

        [Fact]
        public void Test1()
        {

            byte[] vs = "7E0B030048103456789012007B000000010000000200BA7F0E07E4F11C003C002110152110103132333435360000000000000000000031323334353600000000000000000000000000453333394B4B202110172100177E".ToHexBytes();
            string v1 = JT905Serializer.Analyze(vs, options: JTJsonWriterOptions.Instance);
        }
        [Fact]
        public void Test2()
        {
            JT905Package jT905Package = new JT905Package();
            jT905Package.Header = new JT905Header
            {
                MsgId = Enums.JT905MsgId.上班签到信息.ToUInt16Value(),
                ManualMsgNum = 123,
                ISU = "103456789012"
            };
            JT905_0x0B03 bodies = new JT905_0x0B03();
            bodies.Position = new JT905_0x0200
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
            bodies.BusinessLicenseNumber = "123456";
            bodies.QualificationCode = "123456";
            bodies.PlateNo = "E339KK";
            bodies.Uptime = DateTime.Parse("2021-10-15 21:10:10");
            //bodies.ExtraAttributes= "00000A65".ToHexBytes();
            jT905Package.Bodies = bodies;
            byte[] vs = JT905Serializer.Serialize(jT905Package);
            string v = vs.ToHexString();
            Assert.Equal("7E0B030048103456789012007B000000010000000200BA7F0E07E4F11C003C002110152110103132333435360000000000000000000031323334353600000000000000000000000000453333394B4B202110152110057E", v);


        }
        [Fact]
        public void Test3()
        {
            byte[] bytes = "7E0B030048103456789012007B000000010000000200BA7F0E07E4F11C003C002110152110103132333435360000000000000000000031323334353600000000000000000000000000453333394B4B202110152110057E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(bytes);
            Assert.Equal(Enums.JT905MsgId.上班签到信息.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal((ushort)123, jT905Package.Header.MsgNum);
            Assert.Equal("103456789012", jT905Package.Header.ISU);
            JT905_0x0B03 jT905_0X0B03 = (JT905_0x0B03)jT905Package.Bodies;
            Assert.Equal((uint)1, jT905_0X0B03.Position.AlarmFlag);
            Assert.Equal(DateTime.Parse("2021-10-15 21:10:10"), jT905_0X0B03.Position.GPSTime);
            Assert.Equal(12222222, jT905_0X0B03.Position.Lat);
            Assert.Equal(132444444, jT905_0X0B03.Position.Lng);
            Assert.Equal((ushort)60, jT905_0X0B03.Position.Speed);
            Assert.Equal(0, jT905_0X0B03.Position.Direction);
            Assert.Equal((uint)2, jT905_0X0B03.Position.StatusFlag);
            Assert.Equal("123456", jT905_0X0B03.BusinessLicenseNumber);
            Assert.Equal("123456", jT905_0X0B03.QualificationCode);
            Assert.Equal("E339KK", jT905_0X0B03.PlateNo);
            Assert.Equal(DateTime.Parse("2021-10-15 21:10:00"), jT905_0X0B03.Uptime);
        }
    }
}
