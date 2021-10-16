using JT905.Protocol.Extensions;
using JT905.Protocol.Internal;
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
            byte[] bytes = "7E0200001910010111111114A610004C00C000000000F8345603F89E38000000201014000002E97E".ToHexBytes();
            var json = JT905Serializer.Analyze(bytes);
        }
    }
}
