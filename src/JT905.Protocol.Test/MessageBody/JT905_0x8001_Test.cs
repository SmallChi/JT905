using JT905.Protocol.Extensions;
using JT905.Protocol.MessageBody;
using Xunit;

namespace JT905.Protocol.Test.MessageBody
{
    public class JT905_0x8001_Test
    {
        JT905Serializer JT905Serializer = new JT905Serializer();
        [Fact]
        public void Test1()
        {
            JT905Package JT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.中心通用应答.ToUInt16Value(),
                    ManualMsgNum = 10,
                    ISU = "12345678900",
                },
                Bodies = new JT905_0x8001
                {
                    AckMsgId = Enums.JT905MsgId.位置信息汇报.ToUInt16Value(),
                    PlatformResult = Enums.JT905PlatformResult.Success,
                    ReplyMsgNum = 100
                }
            };
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
            var hex = JT905Serializer.Serialize(JT905Package).ToHexString();
            Assert.Equal("7E80010005012345678900000A0064020000617E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 80 01 00 05 01 23 45 67 89 00 00 0A 00 64 02 00 00 61 7E".ToHexBytes();
            JT905Package JT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);
            Assert.Equal(Enums.JT905MsgId.中心通用应答.ToUInt16Value(), JT905Package.Header.MsgId);
            Assert.Equal(10, JT905Package.Header.MsgNum);
            Assert.Equal("12345678900", JT905Package.Header.ISU);

            JT905_0x8001 JT905Bodies = (JT905_0x8001)JT905Package.Bodies;
            Assert.Equal(Enums.JT905MsgId.位置信息汇报.ToUInt16Value(), JT905Bodies.AckMsgId);
            Assert.Equal(100, JT905Bodies.ReplyMsgNum);
            Assert.Equal(Enums.JT905PlatformResult.Success, JT905Bodies.PlatformResult);
        }

        [Fact]
        public void Test3()
        {
            var bytes = "7E 80 01 00 05 01 23 45 67 89 00 00 0A 00 64 02 00 00 61 7E".ToHexBytes();
            string json = JT905Serializer.Analyze<JT905Package>(bytes);
        }
    }
}
