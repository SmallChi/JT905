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
                        new CommandParameter_0x03{Value="01"}
                    }
                }
            };
            var hex = JT905Serializer.Serialize(JT905Package).ToHexString();
            Assert.Equal("7E810400A4012345678900000A000100020003000400050010001100120013001400150016001700180019001A001B001C001D0020002100220023002400250026002700280029002A002B002C002D002E002F00300040004100420043004400450046004700480049004A004B005000510052005300550056005700580059005A007000710072007300740080008100820090009100920093009400A000A100A200A300AF00B000B100B200B300B400B5507E", hex);
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E81040006012345678900000A000100020003007E".ToHexBytes();
            JT905Package JT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);
            Assert.Equal(Enums.JT905MsgId.查询ISU参数.ToUInt16Value(), JT905Package.Header.MsgId);
            Assert.Equal(10, JT905Package.Header.MsgNum);
            Assert.Equal("12345678900", JT905Package.Header.ISU);

            JT905_0x8104 bodies = (JT905_0x8104)JT905Package.Bodies;
            Assert.Equal(JT905Constants.JT905_0x8103_0x0001, bodies.ParamIds[0]);
            Assert.Equal(JT905Constants.JT905_0x8103_0x0002, bodies.ParamIds[1]);
            Assert.Equal(JT905Constants.JT905_0x8103_0x0003, bodies.ParamIds[2]);

        }

        [Fact]
        public void Test3()
        {
            var bytes = "7E810400A4012345678900000A000100020003000400050010001100120013001400150016001700180019001A001B001C001D0020002100220023002400250026002700280029002A002B002C002D002E002F00300040004100420043004400450046004700480049004A004B005000510052005300550056005700580059005A007000710072007300740080008100820090009100920093009400A000A100A200A300AF00B000B100B200B300B400B5507E".ToHexBytes();
            string json = JT905Serializer.Analyze<JT905Package>(bytes, options: JTJsonWriterOptions.Instance);
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
            string bcdTxt = "10";
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
