using System;
using JT905.Protocol.Extensions;
using JT905.Protocol.SerialPort;
using JT905.Protocol.SerialPort.Taximeter;
using Xunit;

namespace JT905.Protocol.Test.SerialPort;

public class Taximeter_0x0000_Down_Test : BaseTest
{
    [Theory]
    [InlineData("55AA000B02000000201111111111112955AA")]
    public void Test1(string hex)
    {
        var package = new SerialPortPackage
        {
            Device = 2,
            ManufacturerIdentify = 0,
            Body = new Taximeter_0x0000_Down
            {
                DateTime = DateTime.Parse("2011-11-11 11:11:11")
            }
        };
        var data = serializer.Serialize(package);
        Assert.Equal(hex, data.ToHexString());
    }

    [Theory]
    [InlineData("55AA000B02000000201111111111112955AA")]
    public void Test2(string hex)
    {
        var package = serializer.Deserialize(hex.ToHexBytes(), IBody.Types.Down);
        Assert.NotNull(package);
        Assert.Equal(11, package.Length);
        Assert.Equal(2, package.Device);
        Assert.Equal(0, package.ManufacturerIdentify);
        Assert.Equal(0, package.MessageId);
        var body = package.Body;
        Assert.NotNull(body);
        Assert.IsType<Taximeter_0x0000_Down>(body);
        var _0X0000_Down = body as Taximeter_0x0000_Down;
        Assert.Equal(DateTime.Parse("2011-11-11 11:11:11"), _0X0000_Down.DateTime);
        Assert.Equal(0x29, package.CheckCode);
    }
}
