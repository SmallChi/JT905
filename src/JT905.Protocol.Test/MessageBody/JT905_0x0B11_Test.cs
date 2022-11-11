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
    /// <summary>
    /// 设备巡检应答
    /// 系统测试
    /// </summary>
    public class JT905_0x0B11_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0B11_Test()
        {
            IJT905Config JT905Config = new DefaultGlobalConfig();
            JT905Config.SkipCRCCode = true;
            JT905Serializer = new JT905Serializer(JT905Config);
        }
        /// <summary>
        /// 测试组包
        /// </summary>
        [Fact]
        public void Test1_Serialize()
        {
            JT905.Protocol.JT905Package package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.设备巡检应答.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },               

            };
            JT905_0x0B11 jT905_0X0B11 = new JT905_0x0B11()
            {
                tLVs = new Dictionary<byte, JT905_0x0B11_TLV>()

            };
            jT905_0X0B11.tLVs.Add((byte)Enums.JT905DeviceType.计价器, new JT905_0x0B11_0x00 { SerialNumber="1301", HardwareVer="12",SoftVer="1234"});
            var _0x0B11Hex = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E0B1100131080000003160000000000000000000012100100000000000000008F7E", _0x0B11Hex);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2()
        {
            var hex = "7e0b1100e6100107395027204100140107395027183503c000000800004c000000000202370000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000003430000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004200000000000000000000000000000000000000000000000000000000000000000052000000000000000000000000000000000000000000000000000000000000000001004170120000706000000000000667e".ToHexBytes();

            string _0x0B11Json = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test3()
        {
            var hex = "7E0B1100131080000003160000000000000000000012100100000000000000008F7E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.设备巡检应答.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.NotNull(jT905Package.Bodies);
        }





    }
}



