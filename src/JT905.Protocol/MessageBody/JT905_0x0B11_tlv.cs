using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 
    /// </summary>
    public class JT905_0x0B11_tlv : IJT905MessagePackFormatter<JT905_0x0B11_tlv>, IJT905Analyze
    {
        public JT905_0x0B11_tlv Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x0B11_tlv value, IJT905Config config)
        {
            throw new NotImplementedException();
        }

        void IJT905Analyze.Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            throw new NotImplementedException();
        }
    }
}
