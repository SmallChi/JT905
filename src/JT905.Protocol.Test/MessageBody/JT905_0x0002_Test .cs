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
    public class JT905_0x0002_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0002_Test()
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
                    MsgId = Enums.JT905MsgId.ISU心跳.ToUInt16Value(),
                    ManualMsgNum = 1203,
                    ISU = "012345678900",
                },
                Bodies = new JT905_0x0002()
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E0002000001234567890004B33C7E",vs);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E0002000001234567890004B33C7E".ToHexBytes();
            
            string v = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);
        }

        [Fact]
        public void Test3()
        {
            var hex = "7E0002000001234567890004B33C7E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.ISU心跳.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(1203, jT905Package.Header.MsgNum);
            Assert.Equal("12345678900", jT905Package.Header.ISU);
            Assert.Null(jT905Package.Bodies);
        }





    }
}
