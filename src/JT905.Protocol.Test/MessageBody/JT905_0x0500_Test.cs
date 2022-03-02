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
    /// 车辆控制应答
    /// 系统测试
    /// </summary>
    public class JT905_0x0500_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0500_Test()
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
                    MsgId = Enums.JT905MsgId.车辆控制应答.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x0500()
                {
                    MsgNum = 1,
                    Position = new JT905_0x0200
                    {
                        AlarmFlag = 0,
                        StatusFlag = (uint)(Enums.JT905Status.车辆油路断开 | Enums.JT905Status.车辆电路断开),
                        Lat = 12201222,
                        Lng = 24445445,
                        Speed = 120,
                        Direction = 120,
                        GPSTime = DateTime.Parse("2021-10-15 21:10:10")

                    },

                }
            };
            var _0x0500Hex = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E0500001B108000000316000000010000000000000C0000BA2D0601750205007878211015211010717E", _0x0500Hex);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2()
        {
            var hex = "7E0500001B108000000316000000010000000000000C0000BA2D0601750205007878211015211010717E".ToHexBytes();

            string _0x0500Json = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test3()
        {
            var hex = "7E0500001B108000000316000000010000000000000C0000BA2D0601750205007878211015211010717E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.车辆控制应答.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.NotNull(jT905Package.Bodies);
        }





    }
}



