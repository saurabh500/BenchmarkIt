using BenchmarkDotNet.Attributes;
using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarks.InliningBenchmark
{
    public class BenchmarkInlining
    {

        [Benchmark]
        public void DoSomeThingInline1()
        {
            byte[] bytes = { 1, 2, 3, 4, 5, 6 };
            DoSomeThingInline2(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DoSomeThingInline2(byte[] bytes)
        {
            DoSomeThingInline3(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DoSomeThingInline3(byte[] bytes)
        {
            DoSomeThingInline4(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DoSomeThingInline4(byte[] bytes)
        {
            DoSomeThingInline5(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DoSomeThingInline5(byte[] bytes)
        {
            DoSomeThingInline6(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DoSomeThingInline6(byte[] bytes)
        {
            DoSomeThingInline7(bytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DoSomeThingInline7(byte[] bytes)
        {
            BinaryPrimitives.ReadInt16BigEndian(bytes);
        }

        [Benchmark(Baseline = true)]
        public void DoSomeThing1()
        {
            byte[] bytes = { 1, 2, 3, 4, 5, 6 };
            DoSomeThing2(bytes);
        }

        public void DoSomeThing2(byte[] bytes)
        {
            DoSomeThing3(bytes);
        }

        public void DoSomeThing3(byte[] bytes)
        {
            DoSomeThing4(bytes);
        }

        public void DoSomeThing4(byte[] bytes)
        {
            DoSomeThing5(bytes);
        }

        public void DoSomeThing5(byte[] bytes)
        {
            DoSomeThing6(bytes);
        }

        public void DoSomeThing6(byte[] bytes)
        {
            DoSomeThing7(bytes);
        }

        public void DoSomeThing7(byte[] bytes)
        {
            BinaryPrimitives.ReadInt16BigEndian(bytes);
        }

    }
}
