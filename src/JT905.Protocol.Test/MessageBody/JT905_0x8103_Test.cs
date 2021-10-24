using JT905.Protocol.Extensions;
using JT905.Protocol.MessageBody;
using Xunit;

namespace JT905.Protocol.Test.MessageBody
{
    public class JT905_0x8103_Test
    {
        JT905Serializer JT905Serializer = new JT905Serializer();
        [Fact]
        public void Test1()
        {
            JT905Package jT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.设置参数.ToUInt16Value(),
                    ManualMsgNum = 10,
                    ISU = "12345678900",
                },               

            };
            JT905_0x8103 jT905_0X8103 = new JT905_0x8103 { ParamList=new System.Collections.Generic.List<JT905_0x8103_BodyBase>()};
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x0001 { ParamValue=10 });
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x0002 { ParamValue = 15 });
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x0010 { ParamValue = "1111111" });
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x0011 { ParamValue = "2222322" });
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x004B { ParamValue = 9 });
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x0090 { ParamValue = "1221" });
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x0091 { ParamValue = "0000000000" });
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x0094 { ParamValue = "E339KK" });
            jT905_0X8103.ParamList.Add(new JT905_0x8103_0x00AF { ParamValue = 10 });
            jT905Package.Bodies = jT905_0X8103;
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
            Assert.Equal("7E81030048012345678900000A0001040000000A0002040000000F0010073131313131313100110732323232333232004B01090090043132323100910A30303030303030303030009406453333394B4B00AF02000A497E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E81030048012345678900000A0001040000000A0002040000000F0010073131313131313100110732323232333232004B01090090043132323100910A30303030303030303030009406453333394B4B00AF02000A497E".ToHexBytes();
            JT905Package JT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);
            Assert.Equal(Enums.JT905MsgId.设置参数.ToUInt16Value(), JT905Package.Header.MsgId);
            Assert.Equal(10, JT905Package.Header.MsgNum);
            Assert.Equal("12345678900", JT905Package.Header.ISU);

            JT905_0x8103 JT905Bodies = (JT905_0x8103)JT905Package.Bodies;
            
        }

        [Fact]
        public void Test3()
        {
            var b = "7E81030048012345678900000A0001040000000A0002040000000F0010073131313131313100110732323232333232004B01090090043132323100910A30303030303030303030009406453333394B4B00AF02000A497E".ToHexBytes();
            var bytes = "7E8103001C012345678900000A0300000001040000000A0000001007313131313131310000004B0109747E".ToHexBytes();
            string json = JT905Serializer.Analyze<JT905Package>(b,options:JTJsonWriterOptions.Instance);
        }
    }
}
