using JT905.Protocol.Extensions;
using JT905.Protocol.MessageBody;
using Xunit;

namespace JT905.Protocol.Test.MessageBody
{
    public class JT905_0x0105_Test
    {
        JT905Serializer JT905Serializer = new JT905Serializer();
        [Fact]
        public void Test1()
        {
            JT905Package jT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.ISU升级结果报告消息.ToUInt16Value(),
                    ManualMsgNum = 10,
                    ISU = "12345678900",
                },

            };
            JT905_0x0105 _JT905_0X0105 = new JT905_0x0105 ();
            _JT905_0X0105.DeviceType=0;
            _JT905_0X0105.VendorID=15;
            _JT905_0X0105.HardwareVer = "10";
            _JT905_0X0105.SoftVer = "1201";
            _JT905_0X0105.UpStatus = 10;
            jT905Package.Bodies = _JT905_0X0105;
            //"7E 
            //80 01 
            //00 05 
            //01 23 45 67 89 00 
            //00 0A 
            //00 64
            //02 00 
            //00 
            //61 
            //7E"
            var hex = JT905Serializer.Serialize(jT905Package).ToHexString();
            Assert.Equal("7E01050006012345678900000A000F1012010A877E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E01050006012345678900000A000F1012010A877E".ToHexBytes();
            JT905Package JT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);
            Assert.Equal(Enums.JT905MsgId.ISU升级结果报告消息.ToUInt16Value(), JT905Package.Header.MsgId);
            Assert.Equal(10, JT905Package.Header.MsgNum);
            Assert.Equal("12345678900", JT905Package.Header.ISU);

        }

        [Fact]
        public void Test3()
        {
            var b = "7E01050006012345678900000A000F1012010A877E".ToHexBytes();
            string json = JT905Serializer.Analyze<JT905Package>(b, options: JTJsonWriterOptions.Instance);
        }
    }
}
