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
    public class JT905_0x0200_Test
    {
        public JT905Serializer JT905Serializer;
        public JT905_0x0200_Test()
        {
            IJT905Config JT905Config = new DefaultGlobalConfig();
            JT905Config.SkipCRCCode = true;
            JT905Serializer = new JT905Serializer(JT905Config);
        }

        [Fact]
        public void Test1()
        {
            // 7E
            // 02 00
            // 00 19
            // 10 01 11 11 11 11
            // 14 12
            // 00 00 18 00
            // 80 00 01 00
            // 00 F8 3B 9E      // 纬度
            // 03 F8 47 38      // 经度
            // 00 00
            // 00
            // 20 10 14 00 00 04
            // 52
            // 7E
            byte[] bytes = "7E020000271122334455660001000000010000000200BA7F0E07E4F11C003C002110152110100104000004B00202006F0302006F1C7E".ToHexBytes();
            var ttt = "7E 02 00 00 19 10 80 00 00 03 16 00 00 00 00 0E 00 80 00 01 81 00 F8 57 A4 03 F8 8E 4E 00 AC 6D 10 01 01 08 13 47 2D 7E ".ToHexBytes();
            var aaa = "7E 02 00 00 19 10 80 00 00 03 16 00 00 00 00 0E 00 80 00 01 81 00 F8 57 A4 03 F8 8E 4E 00 AC 6D 10 01 01 08 50 35 1C 7E".ToHexBytes();
            JT905Package jT905Package = JT905Serializer.Deserialize(bytes);
            JT905Package jT905Package1 = JT905Serializer.Deserialize(ttt);
            var json = JT905Serializer.Analyze(aaa, options: JTJsonWriterOptions.Instance);
        }

        [Fact]
        public void LngLatTest1()
        {
            var lat = 0x00F83B9E / 600000.0D;//27.11365
            var lng = 0x03F84738 / 600000.0D;//111.00468
        }
        [Fact]
        public void Test2()
        {
            JT905Package JT905Package = new JT905Package();
            JT905Package.Header = new JT905Header
            {
                MsgId = (ushort)Enums.JT905MsgId.位置信息汇报,
                MsgNum = 0,
                ISU = "108000000316",
            };
            JT905_0x8001 jT905_0X8001 = new JT905_0x8001()
            {
                ReplyMsgNum = 0,
                AckMsgId = (ushort)Enums.JT905MsgId.位置信息汇报,
                PlatformResult = 0
            };

            JT905_0x0200 jT905_0X0200 = new JT905_0x0200();
            jT905_0X0200.AlarmFlag = 1;
            jT905_0X0200.GPSTime = DateTime.Parse("2021-10-15 21:10:10");
            jT905_0X0200.Lat = 12222222;
            jT905_0X0200.Lng = 132444444;
            jT905_0X0200.Speed = 60;
            jT905_0X0200.Direction = 0;
            jT905_0X0200.StatusFlag = 2;
            jT905_0X0200.BasicLocationAttachData = new Dictionary<byte, JT905_0x0200_BodyBase>();
            jT905_0X0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x01, new JT905_0x0200_0x01 { Mileage = 1200 });
            jT905_0X0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x02, new JT905_0x0200_0x02 { Oil = 111 });
            jT905_0X0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x03, new JT905_0x0200_0x03 { Altitude = 111 });
            jT905_0X0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x11, new JT905_0x0200_0x11 { AreaId = 1, JT905PositionType = Enums.JT905PositionType.圆形区域 });
            jT905_0X0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x12, new JT905_0x0200_0x12 { AreaId = 1, JT905PositionType = Enums.JT905PositionType.圆形区域 });
            jT905_0X0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x13, new JT905_0x0200_0x13 { DrivenRouteId = 1, Time = 20, DrivenRoute = Enums.JT905DrivenRouteType.不足 });
            jT905_0X0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x14, new JT905_0x0200_0x14 { DrivenRouteId = 2, });
            JT905Package.Bodies = jT905_0X0200;
            string v = JT905Serializer.Serialize(JT905Package).ToHexString();
            jT905_0X8001.AckMsgId = (ushort)JT905.Protocol.Enums.JT905MsgId.ISU心跳;
            JT905Package.Bodies = jT905_0X8001;
            string _0x0002 = JT905Serializer.Serialize(JT905Package).ToHexString();

        }
        [Fact]

        public void Test4()
        {
            var botys = "7E020000471080000003160001000000010000000200BA7F0E07E4F11C003C002110152110100104000004B00202006F0302006F110501000000011206010000000100130700000001001400140400000002001E837E".ToHexBytes();
            string v = JT905Serializer.Analyze(botys, options: JTJsonWriterOptions.Instance);
        }
    }
}
