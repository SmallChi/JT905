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
    /// 摄像头立即拍摄命令
    /// 系统测试
    /// </summary>
    public class JT905_0x8801_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x8801_Test()
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
                    MsgId = Enums.JT905MsgId.摄像头立即拍摄命令.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8801() {
                    ChannelId = 0x00,
                    ShootingCommand = 1,
                    VideoTime = 1,
                    SaveFlag = 0x00,
                    Resolution = 0x00,
                    VideoQuality = 1,
                    Lighting = 100,
                    Contrast = 100,
                    Saturability = 100,
                    Chroma = 0x00,

                }
            };
            var _0x8801Hex = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E8801000C1080000003160000000001000100000164646400657E", _0x8801Hex);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E8801000C1080000003160000000001000100000164646400657E".ToHexBytes();
            
            string _0x8801Json = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);
            
        }

        [Fact]
        public void Test3()
        {
            var hex = "7E8801000C1080000003160000000001000100000164646400657E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.摄像头立即拍摄命令.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.NotNull(jT905Package.Bodies);
        }





    }
}


                    
