using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using JT905.Protocol.Interfaces;

namespace JT905.Protocol.Internal
{
    internal class DefaultMsgSNDistributedImpl : IJT905MsgSNDistributed
    {
        ConcurrentDictionary<string, int> counterDict;
        public DefaultMsgSNDistributedImpl()
        {
            counterDict = new ConcurrentDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        }
        public ushort Increment(string terminalPhoneNo)
        {
            return (ushort)counterDict.AddOrUpdate(terminalPhoneNo, 1, (id, count) => count + 1);
        }
    }
}
