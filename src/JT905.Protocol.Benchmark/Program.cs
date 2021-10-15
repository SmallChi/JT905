using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using System;

namespace JT905.Protocol.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //安装NuGet包，BenchmarkDotNet
            //在需要做性能测试的方法前加上属性[Benchmark]。 
            Summary summary = BenchmarkRunner.Run<JT905SerializerContext>();
        }
    }
}
