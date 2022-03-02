using JT905.Protocol.Enums;
using JT905.Protocol.Extensions;
using JT905.Protocol.Interfaces;
using JT905.Protocol.MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JT905.Protocol.MessageBody
{
    /// <summary>
    /// 车辆控制
    /// </summary>
    public class JT905_0x8500 : JT905Bodies, IJT905MessagePackFormatter<JT905_0x8500>, IJT905Analyze
    {
        public override ushort MsgId => (ushort)Enums.JT905MsgId.车辆控制;

        public override string Description => "车辆控制";
        
        /// <summary>
        /// 控制项
        /// 0:油路
        /// 1:电路
        /// 2:车门
        /// 3:车辆
        /// </summary>
        public JT905ControlItem ControlItem { get; set; }
        /// <summary>
        /// 控制命令
        /// 油路 :0:恢复车辆油路,1:断开车辆油路
        /// 电路 :0:恢复车辆电路,1:断开车辆电路
        /// 车门 :0:车门解锁,1:车门加锁
        /// 车辆 :0:车辆解除锁定,1:车辆锁定
        /// </summary>
        public byte ControlCommand { get; set; }

        


        public void Analyze(ref JT905MessagePackReader reader, Utf8JsonWriter writer, IJT905Config config)
        {
            JT905_0x8500 value = new JT905_0x8500();
            value.ControlItem = (JT905ControlItem)reader.ReadByte();        
            writer.WriteString($"[{((byte)value.ControlItem).ReadNumber()}]控制项", value.ControlItem.ToString());      
            value.ControlCommand = reader.ReadByte();
            switch (value.ControlItem)
            {
                case JT905ControlItem.油路:
                    writer.WriteString($"[{value.ControlCommand.ReadNumber()}]控制命令", value.ControlCommand==0? "恢复车辆油路" : "断开车辆油路");
                    break;
                case JT905ControlItem.电路:
                    writer.WriteString($"[{value.ControlCommand.ReadNumber()}]控制命令", value.ControlCommand == 0 ? "恢复车辆电路" : "断开车辆电路");
                    break;
                case JT905ControlItem.车门:
                    writer.WriteString($"[{value.ControlCommand.ReadNumber()}]控制命令", value.ControlCommand == 0 ? "车门解锁" : "车门加锁");
                    break;
                case JT905ControlItem.车辆:
                    writer.WriteString($"[{value.ControlCommand.ReadNumber()}]控制命令", value.ControlCommand == 0 ? "车辆解除锁定" : "车辆锁定");
                    break;
                default:
                    break;
            }
             

        }

        public JT905_0x8500 Deserialize(ref JT905MessagePackReader reader, IJT905Config config)
        {
            JT905_0x8500 value = new JT905_0x8500();
            value.ControlItem = (JT905ControlItem)reader.ReadByte(); 
            value.ControlCommand = reader.ReadByte(); 
            
            
            return value;
        }

        public void Serialize(ref JT905MessagePackWriter writer, JT905_0x8500 value, IJT905Config config)
        {
            writer.WriteByte((byte)value.ControlItem);
            writer.WriteByte(value.ControlCommand);
            
        }
    }
}


                    
