using JT905.Protocol.MessagePack;

namespace JT905.Protocol.SerialPort
{
    /// <summary>
    /// 串口数据包
    /// </summary>
    public class SerialPortPackage
    {
        /// <summary>
        /// 开始标记
        /// </summary>
        public ushort BeginMark { get; private set; } = 0x55AA;

        /// <summary>
        /// 长度
        /// </summary>
        public ushort Length { get; private set; }

        /// <summary>
        /// 设备类型
        /// <list type="table">
        /// <listheader>
        /// <term>设备类型代码</term>
        /// <term>设备名称</term>
        /// </listheader>
        /// <item>
        /// <term>0x00</term>
        /// <term>ISU</term>
        /// </item>
        /// <item>
        /// <term>0x01</term>
        /// <term>通信模块</term>
        /// </item>
        /// <item>
        /// <term>0x02</term>
        /// <term>计价器</term>
        /// </item>
        /// <item>
        /// <term>0x03</term>
        /// <term>出租汽车安全模块</term>
        /// </item>
        /// <item>
        /// <term>0x04</term>
        /// <term>LED 显示屏</term>
        /// </item>
        /// <item>
        /// <term>0x05</term>
        /// <term>智能顶灯</term>
        /// </item>
        /// <item>
        /// <term>0x06</term>
        /// <term>服务评价器(后排)</term>
        /// </item>
        /// <item>
        /// <term>0x07</term>
        /// <term>摄像装置</term>
        /// </item>
        /// <item>
        /// <term>0x08</term>
        /// <term>卫星定位设备</term>
        /// </item>
        /// <item>
        /// <term>0x09</term>
        /// <term>液品(LCD)多媒体屏</term>
        /// </item>
        /// <item>
        /// <term>0x10</term>
        /// <term>ISU 人机交互设备</term>
        /// </item>
        /// <item>
        /// <term>0x11</term>
        /// <term>服务评价器(前排)</term>
        /// </item>
        /// <item>
        /// <term>其他</term>
        /// <term>rfu</term>
        /// </item>
        /// </list>
        /// </summary>
        public byte Device { get; set; }

        /// <summary>
        /// 是否为自定义标识
        /// </summary>
        public byte ManufacturerIdentify { get; set; }

        /// <summary>
        /// 命令字
        /// <para>命令字第一字节用以区分是标准指令与厂家自定义指令,0x00 表示标准指令,其他为自定义指令;</para>
        /// </summary>
        public ushort MessageId { get; private set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public IBody Body { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        public byte CheckCode { get; private set; }

        /// <summary>
        /// 结束标记
        /// </summary>
        public ushort EndMark { get; private set; } = 0x55aa;

        /// <inheritdoc/>
        public SerialPortPackage Deserialize(ref JT905SerialPortMessagePackReader reader, IBody.Types type, IJT905Config config)
        {
            BeginMark = reader.ReadUInt16();
            Length = reader.ReadUInt16();
            Device = reader.ReadByte();
            ManufacturerIdentify = reader.ReadByte();
            MessageId = reader.ReadUInt16();
            if (config.JT905TaximeterFactory.TryGetValue(MessageId, type, out var instance))
            {
                Body = instance.Deserialize(ref reader, config) as IBody;
            }
            CheckCode = reader.ReadByte();
            EndMark = reader.ReadUInt16();
            return this;
        }

        /// <inheritdoc/>
        public void Serialize(ref JT905SerialPortMessagePackWriter writer, IJT905Config config)
        {
            writer.WriteUInt16(BeginMark);
            writer.Skip(2, out var position);
            writer.WriteByte(Device);
            writer.WriteByte(ManufacturerIdentify);
            writer.WriteUInt16(MessageId > 0 ? MessageId : Body.MessageId);
            Body.Serialize(ref writer, config);
            writer.WriteUInt16Return((ushort)(writer.GetCurrentPosition() - position - 2), position);
            writer.WriteXor(2);
            writer.WriteUInt16(EndMark);
        }
    }
}