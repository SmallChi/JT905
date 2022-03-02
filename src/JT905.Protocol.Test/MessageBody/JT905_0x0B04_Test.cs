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
    /// 下班签退信息上传
    /// 系统测试
    /// </summary>
    public class JT905_0x0B04_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0B04_Test()
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
                    MsgId = Enums.JT905MsgId.下班签退信息上传.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x0B04() {
                    Position = new JT905_0x0200(),
                    BusinessLicenseNumber = "430524000012",
                    QualificationCode = "4305240000145",
                    PlateNo = "E34999",
                    TaximeterKValue = "1111",
                    OnDutyPowerOnTime = DateTime.Parse("2021-11-01 10:46:00"),
                    OnDutyPowerOffTime = DateTime.Parse("2021-11-01 10:46:00"),
                    OnDutyMileage = "30.1",
                    OnDutyOperationMileage = "20",
                    TrainNumber = "1111",
                    TimingTime = "10:46:00",
                    TotalAmount  = "230.5",
                    CardAmount = "0",
                    CardCount = "0",
                    OnDutyMileageBetween = "120",
                    TotalMileage = "625201.5",
                    TotalOperationMileage = "320500.5",
                    UnitPrice = "2",
                    TotalOperations = 1,
                    SignType = 0x00,
                    ExtraAttributes = new byte[] { 1},

                }
            };
            var _0x0B04Hex = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E0B0400751080000003160000000000000000000000000000000000000000000101010000003433303532343030303031320000000034333035323430303030313435000000000000453334393939111120211101104620211101104600030100020011111046000023050000000000120006252015032050050200000000010001B67E", _0x0B04Hex);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2() {
            var hex = "7E0B0400751080000003160000000000000000000000000000000000000000000101010000003433303532343030303031320000000034333035323430303030313435000000000000453334393939111120211101104620211101104600030100020011111046000023050000000000120006252015032050050200000000010001B67E".ToHexBytes();
            
            string _0x0B04Json = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);
            
        }

        [Fact]
        public void Test3()
        {
            var hex = "7E0B0400751080000003160000000000000000000000000000000000000000000101010000003433303532343030303031320000000034333035323430303030313435000000000000453334393939111120211101104620211101104600030100020011111046000023050000000000120006252015032050050200000000010001B67E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.下班签退信息上传.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            Assert.NotNull(jT905Package.Bodies);
        }





    }
}


                    
