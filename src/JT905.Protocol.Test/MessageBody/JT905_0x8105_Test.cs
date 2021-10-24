using JT905.Protocol.Extensions;
using JT905.Protocol.MessageBody;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using Xunit;

namespace JT905.Protocol.Test.MessageBody
{
    public class JT905_0x8105_Test
    {
        JT905Serializer JT905Serializer = new JT905Serializer();
        [Fact]
        public void Test1()
        {
            
            JT905MessagePackWriter writer = new JT905MessagePackWriter();
            JT905Package JT905Package = new JT905Package
            {
                Header = new JT905Header
                {
                    MsgId = Enums.JT905MsgId.ISU控制.ToUInt16Value(),
                    ManualMsgNum = 10,
                    ISU = "12345678900",
                },
                Bodies = new JT905_0x8105
                {
                    CommandWord=1,
                    CommandParameters=new List<ICommandParameter>
                    {
                        new CommandParameter_0x01(),
                        new CommandParameter_0x02{Value=10},
                        new CommandParameter_0x03{Value="01"},
                        new CommandParameter_0x04{Value="1201"},
                        new CommandParameter_0x05{Value="capfhz"},
                        new CommandParameter_0x06{Value="capfhz@qq.com"},
                        new CommandParameter_0x07{Value="1234567890@ABV"},
                        new CommandParameter_0x08{Value="www.capfhz.com"},
                        new CommandParameter_0x09{Value=5600}
                    }
                }
            };
            var hex = JT905Serializer.Serialize(JT905Package).ToHexString();

            JT905_0x8105 jT905_0X8105 = new JT905_0x8105 { CommandWord = 2 };
            JT905Package.Bodies = jT905_0X8105;
            string v = JT905Serializer.Serialize(JT905Package).ToHexString();
            Assert.Equal("7E8105003B012345678900000A01000A01120163617066687A0063617066687A4071712E636F6D003132333435363738393040414256007777772E63617066687A2E636F6D0015E0DB7E", hex);
            Assert.Equal("7E81050001012345678900000A02047E", v);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E8105003B012345678900000A01000A01120163617066687A0063617066687A4071712E636F6D003132333435363738393040414256007777772E63617066687A2E636F6D0015E0DB7E".ToHexBytes();
            JT905Package JT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);
            Assert.Equal(Enums.JT905MsgId.ISU控制.ToUInt16Value(), JT905Package.Header.MsgId);
            
            Assert.Equal(10, JT905Package.Header.MsgNum);
            Assert.Equal("12345678900", JT905Package.Header.ISU);

            
        }

        [Fact]
        public void Test3()
        {
            var bytes = "7E8105003B012345678900000A01000A01120163617066687A0063617066687A4071712E636F6D003132333435363738393040414256007777772E63617066687A2E636F6D0015E0DB7E".ToHexBytes();
            byte[] vs = "7E81050001012345678900000A02047E".ToHexBytes();
            string json = JT905Serializer.Analyze<JT905Package>(bytes, options: JTJsonWriterOptions.Instance);
            string v = JT905Serializer.Analyze<JT905Package>(vs, options: JTJsonWriterOptions.Instance);
        }

        [Fact]
        public void Test4()
        {
            string bcdTxt = "10";
            string v = bcdTxt.Insert(0, new string('0', 2));
            //string v1 = bcdTxt.PadLeft(4,'\0'); 

        }
        [Fact]
        public void Test5()
        {
            string APN = "capfhz";
            byte[] vs = JT905Constants.Encoding.GetBytes(APN+'\0');
            string v3 = vs.ToHexString();


            string bcdTxt = "10";
            byte[] vs1 = bcdTxt.ToBCDByte(2);
            int startIndex = 0;
            //string v = bcdTxt.Insert(0, new string('\0', 2));
            string v1 = bcdTxt.PadLeft(4, '0');
            int byteIndex = 0;
            int count = 4 / 2;
            byte[] aryTemp = new byte[count];
            var bcdSpan = v1.AsSpan();
            for (int i = 0; i < count; i++)
            {
                aryTemp[i] = Convert.ToByte(bcdSpan.Slice(startIndex, 2).ToString(),16);
                startIndex += 2;
            }
            string v2 = aryTemp.ToHexString();

            string aa = "01";
            int year = 123456700;
            string v = year.ToBCDByte(5).ToHexString();

        }
    }
}
