using System;
using JT905.Protocol.Extensions;
using JT905.Protocol.MessageBody;
using JT905.Protocol.SerialPort;
using JT905.Protocol.SerialPort.Taximeter;
using Xunit;

namespace JT905.Protocol.Test.SerialPort;

public class Taximeter_0x00E8_Up_Test : BaseTest
{
    [Theory]
    [InlineData("55AA0046020000E8414133333333000000000000000000000000000000003432313038333139383830393036303131580023072619411941000001056700000000000000800000000C002255AA")]
    public void Test1(string hex)
    {
        var package = new SerialPortPackage
        {
            Device = 2,
            ManufacturerIdentify = 0,
            Body = new Taximeter_0x00E8_Up
            {
                AdditionalFee = 0,
                Amount = 8,
                BeginDateTime = DateTime.Parse("2023/7/26 19:41:00"),
                EmptyDrive = 56.7M,
                EndTime = TimeOnly.Parse("19:41:00"),
                IntegrationOfTransportationCard = Array.Empty<byte>(),
                License = "",
                LicensePlate = "AA3333",
                Mileage = 0.1M,
                QualificationCertificate = "42108319880906011X",
                Section = 12,
                TradeType = 0,
                WaitTime = TimeSpan.Parse("00:00:00")
            }
        };
        var data = serializer.Serialize(package);
        Assert.Equal(hex, data.ToHexString());
    }

    [Theory]
    [InlineData("55AA0046020000E8414133333333000000000000000000000000000000003432313038333139383830393036303131580023072619411941000001056700000000000000800000000C002255AA")]
    public void Test2(string hex)
    {
        var package = serializer.Deserialize(hex.ToHexBytes(), IBody.Types.Up);
        Assert.IsType<Taximeter_0x00E8_Up>(package.Body);
        var _0x00E8_Down = package.Body as Taximeter_0x00E8_Up;
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

    [Theory]
    [InlineData("414133333333000000000000000000000000000000003432313038333139383830393036303131580023072619411941000001056700000000000000800000000C00")]
    public void Test3(string hex)
    {
        var _0x00E8_Down = new Taximeter_0x00E8_Up
        {
            AdditionalFee = 0,
            Amount = 8,
            BeginDateTime = DateTime.Parse("2023/7/26 19:41:00"),
            EmptyDrive = 56.7M,
            EndTime = TimeOnly.Parse("19:41:00"),
            IntegrationOfTransportationCard = Array.Empty<byte>(),
            License = "",
            LicensePlate = "AA3333",
            Mileage = 0.1M,
            QualificationCertificate = "42108319880906011X",
            Section = 12,
            TradeType = 0,
            WaitTime = TimeSpan.Parse("00:00:00")
        };
        var data = serializer.Serialize(_0x00E8_Down);
        Assert.Equal(hex, data.ToHexString());
    }

    [Theory]
    [InlineData("414133333333000000000000000000000000000000003432313038333139383830393036303131580023072619411941000001056700000000000000800000000C00")]
    public void Test4(string hex)
    {
        var _0x00E8_Down = serializer.Deserialize<Taximeter_0x00E8_Up>(hex.ToHexBytes());
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

    [Theory]
    [InlineData("7E0B0500831019001985133E5E0000000000000100011E8A4E0450B1130000A92308012001180000000000000300011E8A7F0450B1240000A92308020859183603404036048EE100000100000000414133333333000000000000000000000000000000003432313038333139383830393036303131580023080120010859011098259700000000000259530000000F00707E")]
    public void Test5(string hex)
    {
        var package = jt905Serializer.Deserialize(hex.ToHexBytes());
        Assert.IsType<JT905_0x0B05>(package.Bodies);
        var _0x0b05 = package.Bodies as JT905_0x0B05;
        Assert.NotEmpty(_0x0b05.TaximeterData);
        var _0x00E8_Down = serializer.Deserialize<Taximeter_0x00E8_Up>(_0x0b05.TaximeterData);
    }
}
