using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// ISU控制
    /// </summary>
    public class JT905_0x8105 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8105>, IJT905Analyze
    {
        public const int CommandParameterCount = 6;
        public override ushort MsgId => Enums.JT905MsgId.ISU控制.ToUInt16Value();

        public override string Description => "ISU控制";
        /// <summary>
        /// 命令字
        /// </summary>

        public byte CommandWord { get; set; }

        public IList<ICommandParameter> CommandParameters { get; set; }

        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            throw new NotImplementedException();
        }

        public JT905_0x8105 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8105 value, IJT905Config config)
        {
            writer.WriteByte(value.CommandWord);
            if (value.CommandWord==1)
            {
                if (value.CommandParameters!=null&&value.CommandParameters.Count>0)
                {
                    for (int i = 0; i < CommandParameterCount; i++)
                    {
                        var cmd = value.CommandParameters.FirstOrDefault(f => f.Order == i);
                        if (cmd!=null)
                        {
                            byte[] vs = cmd.ToBytes();
                            writer.WriteArray(vs);
                        }
                    }
                }
            }
        }
    }
    #region 命令参数
    /// <summary>
    /// 命令参数接口
    /// </summary>
    public interface ICommandParameter:ICommandParameterConvert
    {
        /// <summary>
        /// 排序
        /// </summary>
        int Order { get; }
        /// <summary>
        /// 命令名称
        /// </summary>
        string CommandName { get; }

       
    }
    public interface ICommandParameterConvert
    {
        /// <summary>
        /// 转为byte数组
        /// </summary>
        /// <returns></returns>
        byte[] ToBytes();
        /// <summary>
        /// 将byte数组转为命令值
        /// </summary>
        /// <param name="bytes"></param>
        void ToValue(byte[] bytes);

    }
    public interface ICommandParameterValue<TValue>
    {
        /// <summary>
        /// 对应参数值
        /// </summary>
        TValue Value { get; set; }
    }

    /// <summary>
    /// 设备类型
    /// </summary>
    public class CommandParameter_0x01 : ICommandParameter, ICommandParameterValue<byte?>
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Order => 0;

        public string CommandName => "设备类型";

        /// <summary>
        /// 0x00	ISU
        /// 0x01	通信模块
        /// 0x02	计价器
        /// 0x03	出租汽车安全模块
        /// 0x04	LED 显示屏
        /// 0x05	智能顶灯
        /// 0x06	服务评价器(后排)
        /// 0x07	摄像装置
        /// 0x08	卫星定位设备
        /// 0x09	液晶(LCD) 多媒体屏
        /// 0x10	ISU 人机交互设备
        /// 0x11	服务评价器(前排)
        /// 其他 RFU
        /// </summary>

        public byte? Value { get; set; } = 0x00;

        public byte[] ToBytes()
        {
            if (!Value.HasValue) return default;
            return new byte[1] { Value.Value };
        }

        public void ToValue(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                Value = bytes[0];
            }
        }
    }
    public class CommandParameter_0x02 : ICommandParameter, ICommandParameterValue<byte?>
    {
        public int Order => 1;

        public string CommandName => "厂商标识";

        public byte? Value { get; set; }

        public byte[] ToBytes()
        {
            if (!Value.HasValue) return default;
            return new byte[1] { Value.Value };
        }

        public void ToValue(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                Value = bytes[0];
            }
        }
    }
    /// <summary>
    /// 硬件版本号
    /// </summary>
    public class CommandParameter_0x03 : ICommandParameter, ICommandParameterValue<string>
    {
        public int Order => 2;

        public string CommandName => "厂商标识";

        public string Value { get; set; }

        public byte[] ToBytes()
        {
            return Value.ToBCDByte(1);
        }

        public void ToValue(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                Value = bytes.ToHexString();
            }
        }
    }
    /// <summary>
    /// 软件版本号
    /// </summary>
    public class CommandParameter_0x04 : ICommandParameter, ICommandParameterValue<string>
    {
        public int Order => 3;

        public string CommandName => "软件版本号";

        public string Value { get; set; }

        public byte[] ToBytes()
        {
            return Value.ToBCDByte(2);
        }

        public void ToValue(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                Value = bytes.ToHexString();
            }
        }
    }
    /// <summary>
    /// APN
    /// </summary>
    public class CommandParameter_0x05 : ICommandParameter, ICommandParameterValue<string>
    {
        public int Order => 3;

        public string CommandName => "APN";

        public string Value { get; set; }

        public byte[] ToBytes()
        {
            return JT905Constants.Encoding.GetBytes(Value);
        }

        public void ToValue(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                Value = JT905Constants.Encoding.GetString(bytes); ;
            }
        }
    }

    /// <summary>
    /// 拨号用户名
    /// </summary>
    public class CommandParameter_0x06 : ICommandParameter, ICommandParameterValue<string>
    {
        public int Order => 3;

        public string CommandName => "拨号用户名";

        public string Value { get; set; }

        public byte[] ToBytes()
        {
            return JT905Constants.Encoding.GetBytes(Value);
        }

        public void ToValue(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                Value = JT905Constants.Encoding.GetString(bytes);
            }
        }
    }
    /// <summary>
    /// 拨号密码
    /// </summary>
    public class CommandParameter_0x07 : ICommandParameter, ICommandParameterValue<string>
    {
        public int Order => 3;

        public string CommandName => "拨号密码";

        public string Value { get; set; }

        public byte[] ToBytes()
        {
            return JT905Constants.Encoding.GetBytes(Value);
        }

        public void ToValue(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                Value = JT905Constants.Encoding.GetString(bytes);
            }
        }
    }
    /// <summary>
    /// 升级服务器地址
    /// </summary>
    public class CommandParameter_0x08 : ICommandParameter, ICommandParameterValue<string>
    {
        public int Order => 3;

        public string CommandName => "升级服务器地址";

        public string Value { get; set; }

        public byte[] ToBytes()
        {
            return JT905Constants.Encoding.GetBytes(Value);
        }

        public void ToValue(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                Value = JT905Constants.Encoding.GetString(bytes);
            }
        }
    }
    /// <summary>
    /// 升级服务器端口
    /// </summary>
    public class CommandParameter_0x09 : ICommandParameter, ICommandParameterValue<ushort?>
    {
        
        public int Order => 3;

        public string CommandName => "升级服务器端口";

        public ushort? Value { get; set; }

        public byte[] ToBytes()
        {
            JT905MessagePackWriter msgpackWriter = new JT905MessagePackWriter();
            msgpackWriter.WriteUInt16((ushort)Value);

            return msgpackWriter.FlushAndGetRealArray();
        }

        public void ToValue(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
    #endregion


}
