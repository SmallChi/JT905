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
    public class JT905_0x8301_0x0301_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x8301_0x0301_Test()
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
                    MsgId = Enums.JT905MsgId.事件设置.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8301()
                {
                    EventItems = new List<JT905EventProperty>()
                    {
                        new JT905EventProperty(){
                            EventId=0x01,
                            EventContent="测试测试测试测试测试测试"
                        },
                        new JT905EventProperty(){
                            EventId=0x02,
                            EventContent="我是一个中国人"
                        },
                    }
                }
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E8301002B10800000031600000201B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD40002CED2CAC7D2BBB8F6D6D0B9FAC8CB005D7E", vs);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2()
        {
            var hex = "7E8301002B10800000031600000201B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD40002CED2CAC7D2BBB8F6D6D0B9FAC8CB005D7E".ToHexBytes();

            string v = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test3()
        {
            var hex = "7E8301002B10800000031600000201B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD4B2E2CAD40002CED2CAC7D2BBB8F6D6D0B9FAC8CB005D7E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.事件设置.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.NotNull(jT905Package.Bodies);
        }

        [Fact]
        public void Test_0x0301()
        {
            JT905.Protocol.JT905Package package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.事件报告.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x0301()
                {
                    EventId = 0x01
                }
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E03010001108000000316000001877E", vs);

            var _0x3001Hex = "7E03010001108000000316000001877E".ToHexBytes();

            string _0x0301Json = JT905Serializer.Analyze(_0x3001Hex,options:JTJsonWriterOptions.Instance);

            JT905Package jT905Package = JT905Serializer.Deserialize(_0x3001Hex);


        }




    }
}
