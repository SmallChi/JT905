using JT905.Protocol.Extensions;
using JT905.Protocol.Internal;
using JT905.Protocol.MessageBody;
using System.Collections.Generic;
using Xunit;

namespace JT905.Protocol.Test.MessageBody
{
    public class JT905_0x8302_0x0302_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x8302_0x0302_Test()
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
                    MsgId = Enums.JT905MsgId.提问下发.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8302()
                {
                   Flag= (byte)Enums.JT905TextFlag.语音合成播读,
                   IssueId=1,
                   Issue="请你是谁？",
                   Answers=new List<JT905_0x8302.Answer>
                   {
                       new JT905_0x8302.Answer{ Id=1,Content="张三" },
                       new JT905_0x8302.Answer{ Id=2,Content="李四" },
                       new JT905_0x8302.Answer{ Id=3,Content="王五" },
                       new JT905_0x8302.Answer{ Id=4,Content="老六" }

                   }
                  
                }
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E8302002810800000031600000800000001C7EBC4E3CAC7CBADA3BF0001D5C5C8FD0002C0EECBC40003CDF5CEE50004C0CFC1F9007D017E", vs);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2()
        {
            var hex = "7E8302002810800000031600000800000001C7EBC4E3CAC7CBADA3BF0001D5C5C8FD0002C0EECBC40003CDF5CEE50004C0CFC1F9007D017E".ToHexBytes();

            string v = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test3()
        {
            var hex = "7E8302002810800000031600000800000001C7EBC4E3CAC7CBADA3BF0001D5C5C8FD0002C0EECBC40003CDF5CEE50004C0CFC1F9007D017E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.提问下发.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);            
        }

        [Fact]
        public void Test_0x0302()
        {
            JT905.Protocol.JT905Package package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.提问应答.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x0302()
                {
                    IssueId = 0x01,
                    AnswerId=3,
                }
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E0302000510800000031600000000000103837E", vs);

            var _0x3001Hex = "7E0302000510800000031600000000000103837E".ToHexBytes();

            string _0x0301Json = JT905Serializer.Analyze(_0x3001Hex,options:JTJsonWriterOptions.Instance);

            JT905Package jT905Package = JT905Serializer.Deserialize(_0x3001Hex);


        }




    }
}
