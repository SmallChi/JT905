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
    public class JT905_0x0001_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0001_Test()
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
                    MsgId = Enums.JT905MsgId.ISU通用应答.ToUInt16Value(),
                    ManualMsgNum = 1203,
                    ISU = "012345678900",
                },
                Bodies = new JT905_0x0001
                {
                    ReplyMsgId = Enums.JT905MsgId.ISU心跳.ToUInt16Value(),
                    ReplyMsgNum = 1000,
                    ISUResult = Enums.JT905ISUResult.Success
                }
            };
            byte[] vs = JT905Serializer.Serialize(package);
            string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            JT905Package package = new JT905Package {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.ISU心跳.ToUInt16Value(),
                    ManualMsgNum = 1203,
                    ISU = "012345678900",
                },
            };

            byte[] vs = JT905Serializer.Serialize(package);
            string v = JT905Serializer.Analyze(vs, options: JTJsonWriterOptions.Instance);
        }





    }
}
