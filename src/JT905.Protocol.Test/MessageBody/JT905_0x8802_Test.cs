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
    /// 存储图像检索
    /// 系统测试
    /// </summary>
    public class JT905_0x8802_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x8802_Test()
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
            JT905.Protocol.JT905Package package = new JT905Package {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.存储图像检索.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8802() {
                    ChannelId = 0x00,
                    PhotoReason = Enums.JT905PhotoReason.进入重车拍照,
                    StartTime = DateTime.Parse("2021-10-15 21:10:10"),
                    EndTime = DateTime.Parse("2021-10-15 21:10:10"),

                }
            };
            var _0x8802Hex = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E8802000E10800000031600000000211015211010211015211010017E", _0x8802Hex);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E8802000E10800000031600000000211015211010211015211010017E".ToHexBytes();
            
            string _0x8802Json = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);
            
        }

        [Fact]
        public void Test3()
        {
            var hex = "7E8802000E10800000031600000000211015211010211015211010017E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.存储图像检索.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.NotNull(jT905Package.Bodies);
        }





    }
}


                    
