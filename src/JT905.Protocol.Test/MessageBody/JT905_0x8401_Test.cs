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
    /// 设置电话本
    /// 系统测试
    /// </summary>
    public class JT905_0x8401_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x8401_Test()
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
                    MsgId = Enums.JT905MsgId.设置电话本.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8401() {
                    ContactCount = 0x00,
                    Contacts=new List<JT905ContactProperty>() {
                        new JT905ContactProperty(){ 
                            TelephoneBookContactType=Enums.JT905TelephoneBookContactType.呼入_呼出,
                            PhoneNumber="17363996772",
                            Contact="capfhz"
                        },
                        new JT905ContactProperty(){
                            TelephoneBookContactType=Enums.JT905TelephoneBookContactType.呼入,
                            PhoneNumber="13975955555",
                            Contact="测试0"
                        }
                    }

                }
            };
            var vs = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E840100281080000003160000020331373336333939363737320063617066687A0001313339373539353535353500B2E2CAD43000517E", vs);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E840100281080000003160000020331373336333939363737320063617066687A0001313339373539353535353500B2E2CAD43000517E".ToHexBytes();
            
            string v = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);
            
        }

        [Fact]
        public void Test3()
        {
            var hex = "7E840100151080000003160000010331373336333939363737320063617066687A00257E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.设置电话本.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.NotNull(jT905Package.Bodies);
        }





    }
}


                    
