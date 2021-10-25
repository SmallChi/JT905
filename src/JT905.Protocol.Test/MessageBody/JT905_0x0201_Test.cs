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
    public class JT905_0x0201_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0201_Test()
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
                    MsgId = Enums.JT905MsgId.位置信息查询应答.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x0201 {
                    ReplyMsgNum = 0,
                    Position = new JT905_0x0200 {
                        Lat = 100000,
                        Lng=200000,
                        Direction=200,
                        Speed=175,
                         
                    }
                
                }
            };

            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E820100001080000003160000067E", vs);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E820100001080000003160000067E".ToHexBytes();
            
            string v = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);
            var _0x0201 = "7E 02 01 00 1B 10 80 00 00 03 16 00 C2 00 00 00 00 0E 00 80 00 01 81 00 F8 57 A4 03 F8 8E 4E 00 AC 6D 10 01 01 13 08 21 8A 7E ".ToHexBytes();
            string json = JT905Serializer.Analyze(_0x0201, options: JTJsonWriterOptions.Instance);
        }

        [Fact]
        public void Test3()
        {
            var hex = "7E820100001080000003160000067E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.位置信息查询.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.Null(jT905Package.Bodies);
        }





    }
}
