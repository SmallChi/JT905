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
    public class JT905_0x8202_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x8202_Test()
        {
            IJT905Config JT905Config = new DefaultGlobalConfig();
            JT905Config.SkipCRCCode = true;
            JT905Serializer = new JT905Serializer(JT905Config);
        }

        [Fact]
        public void Test1()
        {
            JT905.Protocol.JT905Package package = new JT905Package {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.位置跟踪控制.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8202() {
                    ControlType = Enums.JT905ControlType.按时间间隔_持续时间,
                    Interval = 2,
                    DurationOrDistance = 20
                }
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E8202000710800000031600000100050000000A0C7E", vs);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E8202000710800000031600000100050000000A0C7E".ToHexBytes();
            
            string v = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);

            var _0x0202Hex = "7E 02 02 00 19 10 80 00 00 03 16 00 21 10 00 0C 00 80 00 01 01 00 F8 5D 59 03 F8 8D C1 00 41 A8 21 10 26 11 38 06 BB 7E".ToHexBytes();
            string v1 = JT905Serializer.Analyze(_0x0202Hex, options: JTJsonWriterOptions.Instance);
            var _0x0203Hex = "7E020300FA108000000316001200000E008000010100F8588703F8902D00308C21082418583300000E008000010100F8588703F8902D00308C21082418590400000E008000010100F8588703F8902D00308C21082418593400000E008000010100F8588703F8902D00308C21082419000500000E008000010100F8588703F8902D00308C21082419003500000E008000010100F8588703F8902D00308C21082419010600000E008000010100F8588703F8902D00308C21082419013600000E008000010100F8588703F8902D00308C21082419020700000E008000010100F8588703F8902D00308C21082419023700000E008000010100F8588703F8902D00308C2108241903080D7E".ToHexBytes();
            string _0x0203Json = JT905Serializer.Analyze(_0x0203Hex, options: JTJsonWriterOptions.Instance);
            JT905Package jT905_0x0203 = JT905Serializer.Deserialize(_0x0203Hex);
        }

        [Fact]
        public void Test3()
        {
            var hex = "7E8202000710800000031600000000050000000A0D7E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.位置信息查询.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.Null(jT905Package.Bodies);
        }





    }
}
