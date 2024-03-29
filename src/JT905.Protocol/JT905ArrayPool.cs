﻿using System.Buffers;

namespace JT905.Protocol
{
    /// <summary>
    /// 内存池
    /// </summary>
    internal static class JT905ArrayPool
    {
        private readonly static ArrayPool<byte> ArrayPool;

        static JT905ArrayPool()
        {
            ArrayPool = ArrayPool<byte>.Create();
        }
        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="minimumLength"></param>
        /// <returns></returns>
        public static byte[] Rent(int minimumLength)
        {
            return ArrayPool.Rent(minimumLength);
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="array"></param>
        /// <param name="clearArray"></param>
        public static void Return(byte[] array, bool clearArray = false)
        {
            ArrayPool.Return(array, clearArray);
        }
    }
}
