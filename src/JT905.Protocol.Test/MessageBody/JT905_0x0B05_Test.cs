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
    /// 运营数据上传
    /// 系统测试
    /// </summary>
    public class JT905_0x0B05_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0B05_Test()
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
                    MsgId = Enums.JT905MsgId.运营数据上传.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x0B05() {
                    LdlingHeavyPosition = new JT905_0x0200(),
                    HeavyLdlingPosition = new JT905_0x0200(),
                    OperationId = 1,
                    EvaluateId = 1,
                    EvaluateOptions = 0x00,
                    ExtEvaluateOptions = 1,
                    CallingId = 1,
                    TaximeterData = new byte[] { 1,3,4,1,6},

                }
            };
            var _0x0B05Hex = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E0B050046108000000316000000000000000000000000000000000000000000010101000000000000000000000000000000000000000000000101010000000000000100000001000001000000010103040106CC7E", _0x0B05Hex);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E0B050046108000000316000000000000000000000000000000000000000000010101000000000000000000000000000000000000000000000101010000000000000100000001000001000000010103040106CC7E".ToHexBytes();
            
            string _0x0B05Json = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);
            
        }

        [Fact]
        public void Test3()
        {
            var hex = "7E0B050046108000000316000000000000000000000000000000000000000000010101000000000000000000000000000000000000000000000101010000000000000100000001000001000000010103040106CC7E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.运营数据上传.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.NotNull(jT905Package.Bodies);
        }





    }
}


                    
