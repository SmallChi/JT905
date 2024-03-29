﻿using JT905.Protocol.Extensions;
using JT905.Protocol.MessageBody;
using Xunit;

namespace JT905.Protocol.Test.MessageBody
{
    public class JT905_0x0104_Test
    {
        JT905Serializer JT905Serializer = new JT905Serializer();
        [Fact]
        public void Test1()
        {
            JT905Package jT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.查询ISU参数应答.ToUInt16Value(),
                    ManualMsgNum = 10,
                    ISU = "12345678900",
                },               

            };
            JT905_0x0104 jT905_0X0104 = new JT905_0x0104 { ParamList=new System.Collections.Generic.List<JT905_0x8103_BodyBase>()};
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x0001 { ParamValue=10 });
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x0002 { ParamValue = 15 });
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x0010 { ParamValue = "1111111" });
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x0011 { ParamValue = "2222322" });
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x004B { ParamValue = 9 });
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x0090 { ParamValue = "1221" });
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x0091 { ParamValue = "0000000000" });
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x0094 { ParamValue = "E339KK" });
            jT905_0X0104.ParamList.Add(new JT905_0x8103_0x00AF { ParamValue = 10 });
            jT905Package.Bodies = jT905_0X0104;
            //"7E 
            //80 01 
            //00 05 
            //01 23 45 67 89 00 
            //00 0A 
            //00 64
            //02 00 
            //00 
            //61 
            //7E"
            var hex = JT905Serializer.Serialize(jT905Package).ToHexString();
            Assert.Equal("7E0104004A012345678900000A00000001040000000A0002040000000F0010073131313131313100110732323232333232004B01090090043132323100910A30303030303030303030009406453333394B4B00AF02000ACC7E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E0104004A012345678900000A00000001040000000A0002040000000F0010073131313131313100110732323232333232004B01090090043132323100910A30303030303030303030009406453333394B4B00AF02000ACC7E".ToHexBytes();
            JT905Package JT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);
            Assert.Equal(Enums.JT905MsgId.查询ISU参数应答.ToUInt16Value(), JT905Package.Header.MsgId);
            Assert.Equal(10, JT905Package.Header.MsgNum);
            Assert.Equal("12345678900", JT905Package.Header.ISU);

            JT905_0x0104 JT905Bodies = (JT905_0x0104)JT905Package.Bodies;
            
        }

        [Fact]
        public void Test3()
        {
            var b = "7E 01 04 00 3B 10 80 00 00 03 16 00 A0 00 00 00 11 00 00 12 00 00 13 0C 34 32 2E 34 39 2E 31 35 2E 32 31 33 00 14 05 43 4D 49 4F 54 00 15 00 00 16 00 00 17 0C 34 32 2E 34 39 2E 31 35 2E 32 31 33 00 18 04 00 00 18 3B 6D 7E ".ToHexBytes();
            var bytes = "7E8103001C012345678900000A0300000001040000000A0000001007313131313131310000004B0109747E".ToHexBytes();
            string json = JT905Serializer.Analyze<JT905Package>(b,options:JTJsonWriterOptions.Instance);
        }
    }
}
