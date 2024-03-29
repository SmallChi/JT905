﻿using JT905.Protocol.Extensions;
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
    /// 中心巡检设备
    /// 系统测试
    /// </summary>
    public class JT905_0x8B11_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x8B11_Test()
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
            JT905.Protocol.JT905Package package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.中心巡检设备.ToUInt16Value(),
                    ManualMsgNum = 0,
                    ISU = "108000000316",
                },
                Bodies = new JT905_0x8B11()
                {
                    TypeID = new List<Enums.JT905DeviceType> {
                        Enums.JT905DeviceType.ISU
                    },

                }
            };
            var _0x8B11Hex = JT905Serializer.Serialize(package).ToHexString();
            Assert.Equal("7E8B1100011080000003160000001E7E", _0x8B11Hex);
            //string v = JT905Serializer.Analyze(vs,options:JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test2()
        {
            var hex = "7E8B11000010800000031600001F7E".ToHexBytes();

            string _0x8B11Json = JT905Serializer.Analyze(hex, options: JTJsonWriterOptions.Instance);

        }

        [Fact]
        public void Test3()
        {
            var hex = "7E8B11000010800000031600001F7E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(hex);
            Assert.Equal(Enums.JT905MsgId.中心巡检设备.ToUInt16Value(), jT905Package.Header.MsgId);
            Assert.Equal(0, jT905Package.Header.MsgNum);
            Assert.Equal("108000000316", jT905Package.Header.ISU);
            //Assert.NotNull(jT905Package.Bodies);
        }





    }
}



