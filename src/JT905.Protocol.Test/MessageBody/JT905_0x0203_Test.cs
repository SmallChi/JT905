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
    public class JT905_0x0203_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0203_Test()
        {
            IJT905Config JT905Config = new DefaultGlobalConfig();
            JT905Config.SkipCRCCode = true;
            JT905Serializer = new JT905Serializer(JT905Config);
        }

        [Fact]
        public void Test1()
        {
            JT905.Protocol.JT905Package package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.位置汇报数据补传.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x0203()
                {
                    Positions = new List<JT905_0x0200>()
                    {
                        new JT905_0x0200{ 
                            AlarmFlag=(uint)(Enums.JT905Alarm.预警|Enums.JT905Alarm.车速传感器故障),
                            StatusFlag=(uint)Enums.JT905Status.ACC开,
                            Lng=1214411,
                            Lat=1415245,
                            Speed=120,
                            Direction=110,
                            GPSTime=DateTime.Parse("2021-09-12 14:13:01")
                        }
                    }
                }
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E02030019108000000316000000800002000001000015984D001287CB00786E210912141301AA7E", vs);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2()
        {

            var _0x0203Hex = "7E020300FA108000000316001200000E008000010100F8588703F8902D00308C21082418583300000E008000010100F8588703F8902D00308C21082418590400000E008000010100F8588703F8902D00308C21082418593400000E008000010100F8588703F8902D00308C21082419000500000E008000010100F8588703F8902D00308C21082419003500000E008000010100F8588703F8902D00308C21082419010600000E008000010100F8588703F8902D00308C21082419013600000E008000010100F8588703F8902D00308C21082419020700000E008000010100F8588703F8902D00308C21082419023700000E008000010100F8588703F8902D00308C2108241903080D7E".ToHexBytes();
            string _0x0203Json = JT905Serializer.Analyze(_0x0203Hex, options: JTJsonWriterOptions.Instance);
            JT905Package jT905_0x0203 = JT905Serializer.Deserialize(_0x0203Hex);
        }

    }
}
