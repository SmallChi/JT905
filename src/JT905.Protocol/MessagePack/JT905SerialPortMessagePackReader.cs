using System;
using System.Buffers.Binary;
using System.Linq;
using System.Text;
using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;

namespace JT905.Protocol.MessagePack
{
    /// <summary>
    /// JT905消息读取器
    /// </summary>
    public ref struct JT905SerialPortMessagePackReader
    {
        /// <summary>
        /// 读取buffer
        /// </summary>
        public ReadOnlySpan<byte> Reader { get; }
        /// <summary>
        /// 原数据
        /// </summary>
        public ReadOnlySpan<byte> SrcBuffer { get; }
        /// <summary>
        /// 读取到的数量
        /// </summary>
        public int ReaderCount { get; private set; }
        /// <summary>
        /// JT905版本号
        /// </summary>
        public JT905Version Version { get; set; }
        private readonly bool hasStartMark;
        private readonly bool hasEndMark;
        /// <summary>
        /// 解码（转义还原）,计算校验和
        /// </summary>
        /// <param name="srcBuffer"></param>
        /// <param name="version">默认JT905Version.JTT2014</param>
        public JT905SerialPortMessagePackReader(ReadOnlySpan<byte> srcBuffer, JT905Version version = JT905Version.JTT2014)
        {
            if (srcBuffer.Length > 5)
            {
                hasStartMark = srcBuffer[0] == 0x55 && srcBuffer[1] == 0xaa;
                hasEndMark = srcBuffer[^2] == 0x55 && srcBuffer[^1] == 0xaa;
            }
            SrcBuffer = srcBuffer;
            ReaderCount = 0;
            Version = version;
            Reader = srcBuffer;
        }

        /// <summary>
        /// 读取有符号位的两字节数值类型
        /// </summary>
        /// <returns></returns>
        public short ReadInt16()
        {
            return BinaryPrimitives.ReadInt16BigEndian(GetReadOnlySpan(2));
        }
        /// <summary>
        /// 读取无符号位的两字节数值类型
        /// </summary>
        /// <returns></returns>
        public ushort ReadUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetReadOnlySpan(2));
        }


        /// <summary>
        /// 读取无符号位的四字节数值类型
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt32()
        {
            return BinaryPrimitives.ReadUInt32BigEndian(GetReadOnlySpan(4));
        }
        /// <summary>
        /// 读取有符号位的四字节数值类型
        /// </summary>
        /// <returns></returns>
        public int ReadInt32()
        {
            return BinaryPrimitives.ReadInt32BigEndian(GetReadOnlySpan(4));
        }
        /// <summary>
        /// 读取无符号位的八字节数值类型
        /// </summary>
        /// <returns></returns>
        public ulong ReadUInt64()
        {
            return BinaryPrimitives.ReadUInt64BigEndian(GetReadOnlySpan(8));
        }
        /// <summary>
        /// 读取有符号位的八字节数值类型
        /// </summary>
        /// <returns></returns>
        public long ReadInt64()
        {
            return BinaryPrimitives.ReadInt64BigEndian(GetReadOnlySpan(8));
        }
        /// <summary>
        /// 读取一个字节
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return GetReadOnlySpan(1)[0];
        }
        /// <summary>
        /// 读取一个字符
        /// </summary>
        /// <returns></returns>
        public char ReadChar()
        {
            return (char)GetReadOnlySpan(1)[0];
        }



        /// <summary>
        /// 虚拟读取一个字节，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public byte ReadVirtualByte()
        {
            return GetVirtualReadOnlySpan(1)[0];
        }
        /// <summary>
        /// 虚拟读取一个数组，不计入内存偏移量
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> ReadVirtualArray(int count)
        {
            return GetVirtualReadOnlySpan(count);
        }
        /// <summary>
        /// 虚拟读取无符号位的两字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public ushort ReadVirtualUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetVirtualReadOnlySpan(2));
        }
        /// <summary>
        /// 虚拟读取有符号位的两字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public short ReadVirtualInt16()
        {
            return BinaryPrimitives.ReadInt16BigEndian(GetVirtualReadOnlySpan(2));
        }
        /// <summary>
        /// 虚拟读取无符号位的四字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public uint ReadVirtualUInt32()
        {
            return BinaryPrimitives.ReadUInt32BigEndian(GetVirtualReadOnlySpan(4));
        }
        /// <summary>
        /// 虚拟读取有符号位的四字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public int ReadVirtualInt32()
        {
            return BinaryPrimitives.ReadInt32BigEndian(GetVirtualReadOnlySpan(4));
        }
        /// <summary>
        /// 虚拟读取无符号位的八字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public ulong ReadVirtualUInt64()
        {
            return BinaryPrimitives.ReadUInt64BigEndian(GetVirtualReadOnlySpan(8));
        }
        /// <summary>
        /// 虚拟读取有符号位的八字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public long ReadVirtualInt64()
        {
            return BinaryPrimitives.ReadInt64BigEndian(GetVirtualReadOnlySpan(8));
        }
        /// <summary>
        /// 读取数字编码 
        /// 大端模式、高位在前
        /// </summary>
        /// <param name="len"></param>
        public string ReadBigNumber(int len)
        {
            ulong result = 0;
            var readOnlySpan = GetReadOnlySpan(len);
            for (int i = 0; i < len; i++)
            {
                ulong currentData = (ulong)readOnlySpan[i] << (8 * (len - i - 1));
                result += currentData;
            }
            return result.ToString();
        }
        /// <summary>
        /// 读取固定大小的内存块
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> ReadArray(int len)
        {
            return GetReadOnlySpan(len).Slice(0, len);
        }
        /// <summary>
        /// 读取固定大小的内存块
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> ReadArray(int start, int end)
        {
            return Reader.Slice(start, end);
        }
        /// <summary>
        /// 读取GBK字符串编码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public string ReadString(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string value = JT905Constants.Encoding.GetString(readOnlySpan.Slice(0, len).ToArray());
            return value.Trim('\0');
        }
        /// <summary>
        /// 读取ASCII编码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public string ReadASCII(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string value = Encoding.ASCII.GetString(readOnlySpan.Slice(0, len).ToArray());
            return value;
        }
        /// <summary>
        /// 读取剩余数据体内容为字符串模式
        /// </summary>
        /// <returns></returns>
        public string ReadRemainStringContent()
        {
            return ReadString(ReadCurrentRemainContentLength());
        }
        /// <summary>
        /// 读取16进制编码
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public string ReadHex(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string hex = HexUtil.DoHexDump(readOnlySpan, 0, len);
            return hex;
        }
        /// <summary>
        /// 读取六字节日期,yyMMddHHmmss
        /// </summary>
        /// <param name="format">>D2： 10  X2：16</param>
        public DateTime ReadDateTime6(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                int year = Convert.ToInt32(readOnlySpan[0].ToString(format)) + JT905Constants.DateLimitYear;
                int month = Convert.ToInt32(readOnlySpan[1].ToString(format));
                int day = Convert.ToInt32(readOnlySpan[2].ToString(format));
                int hour = Convert.ToInt32(readOnlySpan[3].ToString(format));
                int minute = Convert.ToInt32(readOnlySpan[4].ToString(format));
                int second = Convert.ToInt32(readOnlySpan[5].ToString(format));
                d = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                d = JT905Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取六字节日期,yyyyMMddHHmmss
        /// </summary>
        /// <param name="format">>D2： 10  X2：16</param>
        public DateTime ReadDateTime7(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(7);
                int year = Convert.ToInt32(readOnlySpan[0].ToString(format) + readOnlySpan[1].ToString(format));
                int month = Convert.ToInt32(readOnlySpan[2].ToString(format));
                int day = Convert.ToInt32(readOnlySpan[3].ToString(format));
                int hour = Convert.ToInt32(readOnlySpan[4].ToString(format));
                int minute = Convert.ToInt32(readOnlySpan[5].ToString(format));
                int second = Convert.ToInt32(readOnlySpan[6].ToString(format));
                d = new DateTime(year, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                d = JT905Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取六字节日期,yyyyMMddHHmm
        /// </summary>
        /// <param name="format">>D2： 10  X2：16</param>
        public DateTime ReadDateTime6Ext(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                //int year = BinaryPrimitives.ReadInt16BigEndian(readOnlySpan.Slice(0,2));
                int year = Convert.ToInt32(readOnlySpan[0].ToString(format) + readOnlySpan[1].ToString(format));
                int month = Convert.ToInt32(readOnlySpan[2].ToString(format));
                int day = Convert.ToInt32(readOnlySpan[3].ToString(format));
                int hour = Convert.ToInt32(readOnlySpan[4].ToString(format));
                int minute = Convert.ToInt32(readOnlySpan[5].ToString(format));
                d = new DateTime(year, month, day, hour, minute, 0);
            }
            catch (Exception)
            {
                d = JT905Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的六字节日期,yyMMddHHmmss
        /// </summary>
        /// <param name="format">>D2： 10  X2：16</param>
        public DateTime? ReadDateTimeNull6(string format = "X2")
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                int year = Convert.ToInt32(readOnlySpan[0].ToString(format));
                int month = Convert.ToInt32(readOnlySpan[1].ToString(format));
                int day = Convert.ToInt32(readOnlySpan[2].ToString(format));
                int hour = Convert.ToInt32(readOnlySpan[3].ToString(format));
                int minute = Convert.ToInt32(readOnlySpan[4].ToString(format));
                int second = Convert.ToInt32(readOnlySpan[5].ToString(format));
                if (year == 0 && month == 0 && day == 0 && hour == 0 && minute == 0 && second == 0) return null;
                d = new DateTime(year + JT905Constants.DateLimitYear, month, day, hour, minute, second);
            }
            catch (Exception)
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取五字节日期,HH-mm-ss-msms|HH-mm-ss-fff
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime ReadDateTime5(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(5);
                StringBuilder sb = new StringBuilder(4);
                sb.Append(readOnlySpan[3].ToString(format));
                sb.Append(readOnlySpan[4].ToString(format));
                d = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                Convert.ToInt32(readOnlySpan[0].ToString(format)),
                Convert.ToInt32(readOnlySpan[1].ToString(format)),
                Convert.ToInt32(readOnlySpan[2].ToString(format)),
                Convert.ToInt32(sb.ToString().TrimStart()));
            }
            catch
            {
                d = JT905Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的五字节日期,HH-mm-ss-msms|HH-mm-ss-fff
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime? ReadDateTimeNull5(string format = "X2")
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(5);
                StringBuilder sb = new StringBuilder(4);
                sb.Append(readOnlySpan[3].ToString("X2"));
                sb.Append(readOnlySpan[4].ToString("X2"));
                int hour = Convert.ToInt32(readOnlySpan[0].ToString(format));
                int minute = Convert.ToInt32(readOnlySpan[1].ToString(format));
                int second = Convert.ToInt32(readOnlySpan[2].ToString(format));
                int millisecond = Convert.ToInt32(sb.ToString().TrimStart());
                if (hour == 0 && minute == 0 && second == 0 && millisecond == 0) return null;
                d = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                hour,
                minute,
                second,
                millisecond);
            }
            catch
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取四字节日期，YYYYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime ReadDateTime4(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(4);
                StringBuilder sb = new StringBuilder(4);
                sb.Append(readOnlySpan[0].ToString(format));
                sb.Append(readOnlySpan[1].ToString(format));
                d = new DateTime(
                Convert.ToInt32(sb.ToString()),
                Convert.ToInt32(readOnlySpan[2].ToString(format)),
                Convert.ToInt32(readOnlySpan[3].ToString(format)));
            }
            catch (Exception)
            {
                d = JT905Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的四字节日期，YYYYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime? ReadDateTimeNull4(string format = "X2")
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(4);
                StringBuilder sb = new StringBuilder(4);
                sb.Append(readOnlySpan[0].ToString(format));
                sb.Append(readOnlySpan[1].ToString(format));
                int year = Convert.ToInt32(sb.ToString());
                int month = Convert.ToInt32(readOnlySpan[2].ToString(format));
                int day = Convert.ToInt32(readOnlySpan[3].ToString(format));
                if (year == 0 && month == 0 && day == 0) return null;
                d = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                return null;
            }
            return d;
        }
        /// <summary>
        /// 读取三字节日期，YYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime ReadDateTime3(string format = "X2")
        {
            DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(3);
                d = new DateTime(
                Convert.ToInt32(readOnlySpan[0].ToString(format)) + JT905Constants.DateLimitYear,
                Convert.ToInt32(readOnlySpan[1].ToString(format)),
                Convert.ToInt32(readOnlySpan[2].ToString(format)));
            }
            catch (Exception)
            {
                d = JT905Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取可空类型的三字节日期，YYMMDD
        /// </summary>
        /// <param name="format">D2： 10  X2：16</param>
        public DateTime? ReadDateTimeNull3(string format = "X2")
        {
            DateTime? d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(3);
                int year = Convert.ToInt32(readOnlySpan[0].ToString(format));
                int month = Convert.ToInt32(readOnlySpan[1].ToString(format));
                int day = Convert.ToInt32(readOnlySpan[2].ToString(format));
                if (year == 0 && month == 0 && day == 0) return null;
                d = new DateTime(
                 year + JT905Constants.DateLimitYear, month, day
                );
            }
            catch (Exception)
            {
                d = JT905Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取UTC时间类型
        /// </summary>
        /// <returns></returns>
        public DateTime ReadUTCDateTime()
        {
            DateTime d;
            try
            {
                ulong result = 0;
                var readOnlySpan = GetReadOnlySpan(8);
                for (int i = 0; i < 8; i++)
                {
                    ulong currentData = (ulong)readOnlySpan[i] << (8 * (8 - i - 1));
                    result += currentData;
                }
                d = JT905Constants.UTCBaseTime.AddSeconds(result).AddHours(8);
            }
            catch (Exception)
            {
                d = JT905Constants.UTCBaseTime;
            }
            return d;
        }
        /// <summary>
        /// 读取BCD编码
        /// </summary>
        /// <param name="len"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public string ReadBCD(int len, bool trim = true)
        {
            int count = len / 2;
            var readOnlySpan = GetReadOnlySpan(count);
            StringBuilder bcdSb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                bcdSb.Append(readOnlySpan[i].ToString("X2"));
            }
            if (trim)
            {
                return bcdSb.ToString().TrimStart('0');
            }
            else
            {
                return bcdSb.ToString();
            }
        }

        /// <summary>
        /// 读取BCD编码，返回以输入分隔符格式化字符串
        /// 电召服务费：XXX-X
        /// 里程：XXXXX.X
        /// </summary>
        /// <param name="len">长度</param>
        /// <param name="separator">分隔符</param>
        /// <returns>XXXX-X</returns>
        public string ReadBCD(int len, char separator)
        {
            int count = len / 2;
            var readOnlySpan = GetReadOnlySpan(count);
            StringBuilder bcdSb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                if (i < count - 1)
                {
                    bcdSb.Append(readOnlySpan[i].ToString("X2"));
                }
                else
                {
                    ReadOnlySpan<char> readOnlySpan1 = readOnlySpan[i].ToString("X2").AsSpan();
                    string tempStr = $"{readOnlySpan1[0]}{separator}{readOnlySpan1[1]}";
                    bcdSb.Append(tempStr);
                }
            }

            return bcdSb.ToString();
        }
        /// <summary>
        /// 读取BCD编码
        /// </summary>
        /// <param name="len">长度</param>
        /// <param name="v2">分隔符位置</param>
        /// <param name="v3">分隔符</param>
        /// <returns></returns>
        public string ReadBCD(int len, int v2, char v3)
        {
            string tempStr = ReadBCD(len, false);
            int v = tempStr.Length - v2;
            var prefix = tempStr.Substring(0, v);
            var postfix = tempStr.Substring(v, v2);
            return prefix + v3 + postfix;
        }
        /// <summary>
        /// 读取数量大小的内存块
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private ReadOnlySpan<byte> GetReadOnlySpan(int count)
        {
            ReaderCount += count;
            return Reader.Slice(ReaderCount - count);
        }
        /// <summary>
        /// 虚拟读取数量大小的内存块，不计入内存偏移量
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> GetVirtualReadOnlySpan(int count)
        {
            return Reader.Slice(ReaderCount, count);
        }
        /// <summary>
        /// 读取数据体内存块
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> ReadContent(int count = 0)
        {
            if (hasEndMark)
            {
                //内容长度=总长度-读取的长度-2（校验码1位+终止符1位）
                int totalContent = Reader.Length - ReaderCount - 3;
                //实际读取内容长度
                int realContent = totalContent - count;
                int tempReaderCount = ReaderCount;
                ReaderCount += realContent;
                return Reader.Slice(tempReaderCount, realContent);
            }
            else
            {
                return Reader.Slice(ReaderCount);
            }
        }
        /// <summary>
        /// 读取一整串字符串到\0结束
        /// </summary>
        /// <returns></returns>
        public string ReadStringEndChar0()
        {
            var remainSpans = Reader.Slice(ReaderCount, ReadCurrentRemainContentLength());
            int length = remainSpans.IndexOf((byte)'\0') + 1;
            string value = JT905Constants.Encoding.GetString(ReadArray(length).ToArray());
            return value.Trim('\0');
        }
        /// <summary>
        /// 虚拟读取一整串字符串到\0结束，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public string ReadVirtualStringEndChar0()
        {
            var remainSpans = Reader.Slice(ReaderCount);
            string value = JT905Constants.Encoding.GetString(GetVirtualReadOnlySpan(remainSpans.IndexOf((byte)'\0') + 1).ToArray());
            return value.Trim('\0');
        }
        /// <summary>
        /// 虚拟读取一整串字符串到\0结束，不计入内存偏移量
        /// </summary>
        /// <returns></returns>
        public string ReadVirtualArraryEndChar0()
        {
            var remainSpans = Reader.Slice(ReaderCount);
            return GetVirtualReadOnlySpan(remainSpans.IndexOf((byte)'\0') + 1).ToArray().ToHexString();
        }
        /// <summary>
        /// 读取剩余数据体内容长度
        /// </summary>
        /// <returns></returns>
        public int ReadCurrentRemainContentLength()
        {
            if (hasEndMark)
            {
                //内容长度=总长度-读取的长度-2（校验码1位+终止符1位）
                return Reader.Length - ReaderCount - 3;
            }
            else
            {
                return Reader.Length - ReaderCount;
            }
        }
        /// <summary>
        /// 跳过多少字节
        /// </summary>
        /// <param name="count"></param>
        public void Skip(int count = 1)
        {
            ReaderCount += count;
        }
        /// <summary>
        /// 读取JT19056校验码
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        public (byte CalculateXorCheckCode, byte RealXorCheckCode) ReadCarDVRCheckCode(int currentPosition)
        {
            var reader = Reader.Slice(currentPosition, ReaderCount - currentPosition);
            byte calculateXorCheckCode = 0;
            foreach (var item in reader)
            {
                calculateXorCheckCode = (byte)(calculateXorCheckCode ^ item);
            }
            var realXorCheckCode = Reader.Slice(ReaderCount)[0];
            return (calculateXorCheckCode, realXorCheckCode);
        }
    }
}
