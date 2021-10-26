# JT905协议

[![MIT Licence](https://img.shields.io/github/license/mashape/apistatus.svg)](https://github.com/SmallChi/JT905/blob/main/LICENSE)![.NET Core](https://github.com/SmallChi/JT905/workflows/.NET%20Core/badge.svg?branch=main)

## 前提条件

1. 掌握进制转换：二进制转十六进制；
2. 掌握BCD编码、Hex编码；
3. 掌握各种位移、异或；
4. 掌握常用反射；
5. 掌握JObject的用法；
6. 掌握快速ctrl+c、ctrl+v；
7. 掌握Span\<T\>的基本用法
8. 掌握以上装逼技能，就可以开始搬砖了。

## JT905数据结构解析

### 数据包[JT905Package]

| 头标识 | 数据头       | 数据体      | 校验码    | 尾标识 |
| :----: | :---------: |  :---------: | :----------: | :----: |
| Begin  | JT905Header |  JT905Bodies |CheckCode | End    |
| 7E     | -           | -           | -         | 7E     |

### 数据头[JT905Header]

| 消息ID | 消息体长度 | ISU标识 | 消息流水号 |
| :---: | :---: | :---: |:---: |
| MsgId  | DataLength | ISU | MsgNum |

#### 消息体属性[JT905Bodies]

> 根据对应消息ID：MsgId

***注意：数据内容(除去头和尾标识)进行转义判断***

转义规则如下:

1. 若数据内容中有出现字符 0x7e 的，需替换为字符 0x7d 紧跟字符 0x02;
2. 若数据内容中有出现字符 0x7d 的，需替换为字符 0x7d 紧跟字符 0x01;

反转义的原因：确认JT905协议的TCP消息边界。

### 举个栗子1

#### 1.组包：

> MsgId 0x0200:位置信息汇报

``` csharp

JT905Package JT905Package = new JT905Package();

JT905Package.Header = new JT905Header
{
    MsgId = Enums.JT905MsgId.位置信息汇报.ToUInt16Value(),
    ManualMsgNum = 126,
    ISU = "103456789012"
};

JT905_0x0200 JT905_0x0200 = new JT905_0x0200
{
    AlarmFlag = 1,
    GPSTime = DateTime.Parse("2021-10-15 21:10:10"),
    Lat = 12222222,
    Lng = 132444444,
    Speed = 60,
    Direction = 0,
    StatusFlag = 2,
    BasicLocationAttachData = new Dictionary<byte, JT905_0x0200_BodyBase>()
};

JT905_0x0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x01, new JT905_0x0200_0x01
{
    Mileage = 100
});

JT905_0x0200.BasicLocationAttachData.Add(JT905Constants.JT905_0x0200_0x02, new JT905_0x0200_0x02
{
    Oil = 125
});

JT905Package.Bodies = JT905_0x0200;

byte[] data = JT905Serializer.Serialize(JT905Package);

var hex = data.ToHexString();

// 输出结果Hex：
// 7E02000023103456789012007D02000000010000000200BA7F0E07E4F11C003C002110152110100104000000640202007D01347E
```

#### 2.手动解包：

``` csharp
1.原包：
7E 02 00 00 23 10 34 56 78 90 12 00 (7D 02) 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 3C 00 21 10 15 21 10 10 01 04 00 00 00 64 02 02 00 (7D 01) 34 7E

2.进行反转义
7D 02 ->7E
7D 01 ->7D
反转义后
7E 02 00 00 23 10 34 56 78 90 12 00 7E 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 3C 00 21 10 15 21 10 10 01 04 00 00 00 64 02 02 00 7D 34 7E

3.拆解
7E                  --头标识
02 00               --数据头->消息ID
00 23               --数据头->消息体属性
10 34 56 78 90 12   --数据头->终端手机号
00 7E               --数据头->消息流水号
00 00 00 01         --消息体->报警标志
00 00 00 02         --消息体->状态位标志
00 BA 7F 0E         --消息体->纬度
07 E4 F1 1C         --消息体->经度
00 3C               --消息体->速度
00                  --消息体->方向
21 10 15 21 10 10   --消息体->GPS时间
01                  --消息体->附加信息->里程
04                  --消息体->附加信息->长度
00 00 00 64         --消息体->附加信息->数据
02                  --消息体->附加信息->油量
02                  --消息体->附加信息->长度
00 7D               --消息体->附加信息->数据
34                  --检验码
7E                  --尾标识
```

#### 3.程序解包：

``` csharp
//1.转成byte数组
byte[] bytes = "7E02000023103456789012007D02000000010000000200BA7F0E07E4F11C003C002110152110100104000000640202007D01347E".ToHexBytes();

//2.将数组反序列化
var jT905Package = JT905Serializer.Deserialize<JT905Package>(bytes);

//3.数据包头
Assert.Equal(Enums.JT905MsgId.位置信息汇报.ToValue(), jT905Package.Header.MsgId);
Assert.Equal(0x23, jT905Package.Header.DataLength);
Assert.Equal(126, jT905Package.Header.MsgNum);
Assert.Equal("103456789012", jT905Package.Header.ISU);

//4.数据包体
JT905_0x0200 jT905_0x0200 = (JT905_0x0200)jT905Package.Bodies;
Assert.Equal((uint)1, jT905_0x0200.AlarmFlag);
Assert.Equal(DateTime.Parse("2021-10-15 21:10:10"), jT905_0x0200.GPSTime);
Assert.Equal(12222222, jT905_0x0200.Lat);
Assert.Equal(132444444, jT905_0x0200.Lng);
Assert.Equal(60, jT905_0x0200.Speed);
Assert.Equal(0, jT905_0x0200.Direction);
Assert.Equal((uint)2, jT905_0x0200.StatusFlag);
//4.1.附加信息1
Assert.Equal(100, ((JT905_0x0200_0x01)jT905_0x0200.BasicLocationAttachData[JT905Constants.JT905_0x0200_0x01]).Mileage);
//4.2.附加信息2
Assert.Equal(125, ((JT905_0x0200_0x02)jT905_0x0200.BasicLocationAttachData[JT905Constants.JT905_0x0200_0x02]).Oil);
```

## NuGet安装

| Package Name| Version| Preview  Version |Downloads|Remark|
| --- | --- | --- | ---| --- |
| Install-Package JT905 | ![JT905](https://img.shields.io/nuget/v/JT905.svg) | ![JT905](https://img.shields.io/nuget/vpre/JT905.svg)|![JT905](https://img.shields.io/nuget/dt/JT905.svg) |JT905|

## 使用BenchmarkDotNet性能测试报告（只是玩玩，不能当真）

> todo:


## JT905终端通讯协议消息对照表

| 序号  | 消息ID        | 完成情况 | 测试情况 | 消息体名称                     |
| :---: | :---: | :---: | :---: | :--- |
| 1   | 0x0001        | √        | √        | ISU通用应答                   |
| 2   | 0x8001        | √        | √        | 中心通用应答                   |
| 3   | 0x0002        | √        | √        | ISU心跳                   |
| 4   | 0x8103        | √        | √        | 设置参数                   |
| 5   | 0x8104        | √        | √        | 查询ISU参数                   |
| 6    | 0x0104        | √        | √        | 查询ISU参数应答                   |
| 7   | 0x8105        | √        | √        | ISU控制                   |
| 8   | 0x0105        | √        | √        | ISU升级结果报告消息                   |
| 9   | 0x0200        | √        | √        | 位置信息汇报                   |
| 10   | 0x8201        | √        | √        | 位置信息查询                   |
| 11  | 0x0201        | √        | √        | 位置信息查询应答                   |
| 12   | 0x8202        | √        | √        | 位置跟踪控制                   |
| 13   | 0x0202        | √        | √        | 位置跟踪信息汇报                   |
| 14   | 0x0203        | √        | √        | 位置汇汇报数据补传                   |
| 15   | 0x8300        | √        | √        | 文本信息下发                   |
| 16   | 0x8301        | √        | √        | 事件设置                   |
| 17   | 0x0301        | √        | √        | 事件报告                   |
| 18   | 0x8302        | √        | √        | 提问下发                   |
| 19   | 0x0302        | √        | √        | 提问应答                   |
| 20   | 0x0B03        | √        | √        |上班签到信息上传                   |
> todo:
