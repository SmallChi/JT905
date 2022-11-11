using System;

namespace JT905.Protocol.Extensions
{
    /// <summary>
    /// 
    /// ref:"www.codeproject.com/tips/447938/high-performance-csharp-byte-array-to-hex-string-t"
    /// </summary>
    public static partial class JT905BinaryExtensions
    {

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len">目标长度</param>
        /// <returns></returns>
        public static string FormatString(this string value,in int len)
        {
            string result = value ?? "";
            if (result.IndexOf('.')>0)
            {
                result = result.Replace(".", "");
            }
            else
            {
                result += "0";
            }
            int length=value.Length - len;
            if (length > 0)
            {
                result = result.Substring(length, len);
            }
            else
                result.PadLeft(len, '0');
            return result;
        }
        /// <summary>
        /// 字符串BCD8421编码
        /// </summary>
        /// <param name="value">待转换的字符串</param>
        /// <param name="len">BCD8421编码长度</param>
        /// <returns></returns>
        public static string ToBCDString(this string value, in int len)
        {
            return value.ToBCDByte(len).ToHexString();
        }
        /// <summary>
        /// 字符串BCD8421编码
        /// </summary>
        /// <param name="value">待转换的字符串</param>
        /// <param name="len">BCD8421编码长度</param>
        /// <returns></returns>

        public static byte[] ToBCDByte(this string value,in int len) {
            string bcdText = value ?? "";
            bcdText = bcdText.Replace(" ", "").Trim();
            int startIndex = 0;
            int noOfZero = len - bcdText.Length;
            if (noOfZero > 0)
            {
                //bcdText = bcdText.Insert(startIndex, new string('0', noOfZero));
                bcdText = bcdText.PadRight(len, '0');
            }
            int count = len / 2;
            byte[] aryTemp = new byte[count];
            var bcdSpan = bcdText.AsSpan();
            for (int i = 0; i < count; i++)
            {
                aryTemp[i] = Convert.ToByte(bcdSpan.Slice(startIndex, 2).ToString(), 16);
                startIndex += 2;
            }
            return aryTemp;
        }
        /// <summary>
        /// 整型数据转为BCD BYTE
        /// 为了兼容int类型，不使用byte做参数
        /// 支持0xFF一个字节的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte ToBCD(this int value) {
            byte result = 0;
            if (value<=0xFF)
            {
                int high = value / 10;
                int low = value % 10;
                result = (byte)(high << 4 | low);
            }
            return result;
        }
        /// <summary>
        /// 整型数据转为BCD
        /// </summary>
        /// <param name="value">待转换的字符串</param>
        /// <param name="count">BCD[n]</param>
        /// <returns></returns>
        public static byte[] ToBCDByte(this int value,int count) {
            byte[] vs = new byte[count];
            Span<byte> list = new Span<byte>(vs);
            int level = count - 1;
            var high = value / 100;
            var low = value % 100;
            if (high>0)
            {
                ToBCDByte(high, list, --count);
            }
            byte res = (byte)(((low / 10) << 4) + (low % 10));
            list[level] = res;
            return list.ToArray();
        }

        private static void ToBCDByte(int value, Span<byte> list, int byteCount)
        {
            int level = byteCount - 1;
            if (level < 0) return;
            var high = value / 100;
            var low = value % 100;
            if (high > 0)
            {
                ToBCDByte(high, list, --byteCount);
            }
            byte res = (byte)(((low / 10) << 4) + (low % 10));
            list[level] = res;
        }

        /// <summary>
        /// 16进制数组转16进制字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] source)
        {
            return Convert.ToHexString(source,0,source.Length);
        }

        /// <summary>
        /// 16进制字符串转16进制数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] ToHexBytes(this string hexString)
        {
            hexString = hexString.Replace(" ", "");

            return Convert.FromHexString(hexString);
        }

        /// <summary>
        /// 从内存块中读取16进制字符串
        /// </summary>
        /// <param name="read"></param>
        /// <param name="offset"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string ReadHexStringLittle(ReadOnlySpan<byte> read, ref int offset, int len)
        {
            //ReadOnlySpan<byte> source = read.Slice(offset, len);
            string hex=Convert.ToHexString(read.Slice(offset, len));
            offset += len;
            return hex;
        }

        /// <summary>
        /// 将16进制字符串写入对应数组中
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="data"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int WriteHexStringLittle(byte[] bytes, int offset, string data, int len)
        {
            if (data == null) data = "";
            data = data.Replace(" ", "");
            int startIndex = 0;
            if (data.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                startIndex = 2;
            }
            int length = len;
            if (length == -1)
            {
                length = (data.Length - startIndex) / 2;
            }
            int noOfZero = length * 2 + startIndex - data.Length;
            if (noOfZero > 0)
            {
                data = data.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            while (startIndex < data.Length && byteIndex < length)
            {
                bytes[offset + byteIndex] = Convert.ToByte(data.Substring(startIndex, 2), 16);
                startIndex += 2;
                byteIndex++;
            }
            return length;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ReadNumber(this byte value, string format = "X2")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ReadNumber(this int value, string format = "X8")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this uint value, string format = "X8")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this long value, string format = "X16")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this ulong value, string format = "X16")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this short value, string format = "X4")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public static string ReadNumber(this ushort value, string format = "X4")
        {
            return value.ToString(format);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this ushort value)
        {
            return System.Convert.ToString(value, 2).PadLeft(16, '0').AsSpan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this short value)
        {
            return System.Convert.ToString(value, 2).PadLeft(16, '0').AsSpan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this uint value)
        {
            return System.Convert.ToString(value, 2).PadLeft(32, '0').AsSpan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this int value)
        {
            return System.Convert.ToString(value, 2).PadLeft(32, '0').AsSpan();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReadOnlySpan<char> ReadBinary(this byte value)
        {
            return System.Convert.ToString(value, 2).PadLeft(8, '0').AsSpan();
        }
    }

}
