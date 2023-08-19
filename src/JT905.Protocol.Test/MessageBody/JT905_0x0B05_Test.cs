using System;
using JT905.Protocol.Extensions;
using JT905.Protocol.Internal;
using JT905.Protocol.MessageBody;
using JT905.Protocol.SerialPort;
using JT905.Protocol.SerialPort.Taximeter;
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
            JT905Package package = new()
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.运营数据上传.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x0B05()
                {
                    LdlingHeavyPosition = new JT905_0x0200(),
                    HeavyLdlingPosition = new JT905_0x0200(),
                    OperationId = 1,
                    EvaluateId = 1,
                    EvaluateOptions = 0x00,
                    ExtEvaluateOptions = 1,
                    CallingId = 1,
                    TaximeterData = new byte[] { 1, 3, 4, 1, 6 },
                }
            };
            var _0x0B05Hex = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E0B050046108000000316000000000000000000000000000000000000000000010101000000000000000000000000000000000000000000000101010000000000000100000001000001000000010103040106CC7E", _0x0B05Hex);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2()
        {
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

        [Theory]
        [InlineData("7E0B05008310190019851300D90000000000000100011E8A720450B1360000B22307261941340000000000000300011E8A720450B1360000B223072619414335F53A400000000000000100000000414133333333000000000000000000000000000000003432313038333139383830393036303131580023072619411941000001056700000000000000800000000C00927E")]
        public void Test4(string hex)
        {
            var jT905Package = JT905Serializer.Deserialize<JT905Package>(hex.ToHexBytes());
            var serialPortSerializer = new SerialPortSerializer();
            var _0x00E8_Down = serialPortSerializer.Deserialize<Taximeter_0x00E8_Up>((jT905Package.Bodies as JT905_0x0B05).TaximeterData);
            Assert.NotNull(_0x00E8_Down);
            Assert.Equal(0, _0x00E8_Down.AdditionalFee);
            Assert.Equal(8, _0x00E8_Down.Amount);
            Assert.Equal(DateTime.Parse("2023/7/26 19:41:00"), _0x00E8_Down.BeginDateTime);
            Assert.Equal(56.7M, _0x00E8_Down.EmptyDrive);
            Assert.Equal(TimeOnly.Parse("19:41:00"), _0x00E8_Down.EndTime);
            Assert.Empty(_0x00E8_Down.IntegrationOfTransportationCard);
            Assert.Equal("", _0x00E8_Down.License);
            Assert.Equal("AA3333", _0x00E8_Down.LicensePlate);
            Assert.Equal(0.1M, _0x00E8_Down.Mileage);
            Assert.Equal("42108319880906011X", _0x00E8_Down.QualificationCertificate);
            Assert.Equal(12M, _0x00E8_Down.Section);
            Assert.Equal(0, _0x00E8_Down.TradeType);
            Assert.Equal(TimeSpan.Parse("00:00:00"), _0x00E8_Down.WaitTime);
        }
    }
}