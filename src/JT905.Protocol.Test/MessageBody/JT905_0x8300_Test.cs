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
    public class JT905_0x8300_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x8300_Test()
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
                    MsgId = Enums.JT905MsgId.文本信息下发.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8300() {
                    TextFlag= (byte)Enums.JT905TextFlag.语音合成播读,
                    TextInfo="测试测试测试"
                }
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E8300000E108000000316000008B2E2CAD4B2E2CAD4B2E2CAD4004E7E", vs);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E8300000E108000000316000008B2E2CAD4B2E2CAD4B2E2CAD4004E7E".ToHexBytes();
            
            string v = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);
            
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
