using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;
using JT905.Protocol.Extensions;
using JT905.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace JT905.Protocol.Benchmark
{
    [Config(typeof(JT905SerializerConfig))]
    [MarkdownExporterAttribute.GitHub]
    [MemoryDiagnoser]
    [CategoriesColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    public class JT905SerializerContext
    {
        private byte[] bytes0x0200;
        private byte[] bytes0x0100;

        [Params(100, 10000, 100000)]
        public int N;

        private ushort MsgId0x0200;
        private ushort MsgId0x0100;
        JT905Serializer JT905Serializer;
        [GlobalSetup]
        public void Setup()
        {
            JT905Serializer = new JT905Serializer();
            bytes0x0200 = "7E0200005C11223344556622B8000000010000000200BA7F0E07E4F11C0028003C00001807151010100104000000640202003703020038040200011105010000000112060100000001011307000000020022012504000000172A0200F42B04000000F2300102310105167E".ToHexBytes();
            MsgId0x0200 = Enums.JT905MsgId.位置信息汇报.ToUInt16Value();
            MsgId0x0100 = Enums.JT905MsgId.终端注册.ToUInt16Value();
            bytes0x0100 = "7E 01 00 00 2D 00 01 23 45 67 89 00 0A 00 28 00 32 31 32 33 34 30 73 6D 61 6C 6C 63 68 69 31 32 33 30 30 30 30 30 30 30 30 30 43 48 49 31 32 33 30 01 D4 C1 41 31 32 33 34 35 BA 7E".ToHexBytes();
        }

        [Benchmark(Description = "0x0200_All_AttachId_Serialize"), BenchmarkCategory("0x0200Serializer")]
        public void TestJT905_0x0200_All_AttachId_Serialize()
        {
            for (int i = 0; i < N; i++)
            {
                JT905Package JT905Package = new JT905Package();
                JT905Package.Header = new JT905Header
                {
                    MsgId = MsgId0x0200,
                    MsgNum = 8888,
                    ISU = "112233445566",
                };
                JT905_0x0200 JT905UploadLocationRequest = new JT905_0x0200();
                JT905UploadLocationRequest.AlarmFlag = 1;
                JT905UploadLocationRequest.GPSTime = DateTime.Parse("2021-10-15 21:10:10");
                JT905UploadLocationRequest.Lat = 12222222;
                JT905UploadLocationRequest.Lng = 132444444;
                JT905UploadLocationRequest.Speed = 60;
                JT905UploadLocationRequest.Direction = 0;
                JT905UploadLocationRequest.StatusFlag = 2;
                JT905UploadLocationRequest.BasicLocationAttachData = new Dictionary<byte, JT905_0x0200_BodyBase>();
                //todo:
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x01, new JT905_0x0200_0x01
                //{
                //    Mileage = 100
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x02, new JT905_0x0200_0x02
                //{
                //    Oil = 55
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x03, new JT905_0x0200_0x03
                //{
                //     Speed=56
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x04, new JT905_0x0200_0x04
                //{
                //     EventId=1
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x11, new JT905_0x0200_0x11
                //{
                //     AreaId=1,
                //     JT905PositionType= Enums.JT905PositionType.圆形区域
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x12, new JT905_0x0200_0x12
                //{
                //    AreaId = 1,
                //    JT905PositionType = Enums.JT905PositionType.圆形区域,
                //    Direction= Enums.JT905DirectionType.出
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x13, new JT905_0x0200_0x13
                //{
                //     DrivenRoute= Enums.JT905DrivenRouteType.过长,
                //     DrivenRouteId=2,
                //     Time=34
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x25, new JT905_0x0200_0x25
                //{
                //     CarSignalStatus=23
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x2A, new JT905_0x0200_0x2A
                //{
                //    IOStatus=244
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x2B, new JT905_0x0200_0x2B
                //{
                //     Analog = 242
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x30, new JT905_0x0200_0x30
                //{
                //     WiFiSignalStrength=0x02
                //});
                //JT905UploadLocationRequest.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x31, new JT905_0x0200_0x31
                //{
                //     GNSSCount=0x05
                //});
                JT905Package.Bodies = JT905UploadLocationRequest;
                var result = JT905Serializer.Serialize(JT905Package);
            }
        }

        [Benchmark(Description = "0x0200_All_AttachId_Deserialize"), BenchmarkCategory("0x0200Serializer")]
        public void TestJT905_0x0200_Deserialize()
        {
            for (int i = 0; i < N; i++)
            {
                var result = JT905Serializer.Deserialize(bytes0x0200);
            }
        }

        [Benchmark(Description = "0x0100Serialize"), BenchmarkCategory("0x0100Serializer")]
        public void TestJT905_0x0100_Serialize()
        {
            for (int i = 0; i < N; i++)
            {
                JT905Package JT905Package = new JT905Package();
                JT905Package.Header = new JT905Header
                {
                    MsgId = MsgId0x0100,
                    MsgNum = (ushort)(i + 1),
                    ISU = "112233445566",
                };
                //JT905_0x0100 JT905_0X0100 = new JT905_0x0100();
                //JT905_0X0100.AreaID = 12345;
                //JT905_0X0100.CityOrCountyId = 23454;
                //JT905_0X0100.PlateColor = 0x02;
                //JT905_0X0100.PlateNo = "测A123456";
                //JT905_0X0100.TerminalId = "1234567";
                //JT905_0X0100.TerminalModel = "1234567890000";
                //JT905_0X0100.MakerId = "12345";
                //JT905Package.Bodies = JT905_0X0100;
                var result = JT905Serializer.Serialize(JT905Package);
            }
        }

        [Benchmark(Description = "0x0100Deserialize"), BenchmarkCategory("0x0100Serializer")]
        public void TestJT905_0x0100_Deserialize()
        {
            for (int i = 0; i < N; i++)
            {
                var result = JT905Serializer.Deserialize(bytes0x0100);
            }
        }
    }

    public class JT905SerializerConfig : ManualConfig
    {
        public JT905SerializerConfig()
        {
            AddJob(Job.Default.WithGcServer(false).WithToolchain(CsProjCoreToolchain.NetCoreApp10_0).WithPlatform(Platform.AnyCpu));
        }
    }
}
