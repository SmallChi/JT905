using JT905.Protocol.Internal;
using JT905.Protocol.SerialPort;

namespace JT905.Protocol.Test.SerialPort;

public class BaseTest
{
    protected JT905Serializer jt905Serializer = new(new DefaultGlobalConfig());
    protected readonly SerialPortSerializer serializer = new();
}
