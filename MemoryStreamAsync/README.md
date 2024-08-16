# How expensive is the async statemachine?

# Problem statement

Should I write code to make multiple async calls to an object with buffered data, or should I read a big chunk of data expected, and work synchronously on top of the data?

# The benchmark 

## Setup


1. The `ReadInChunksAsync` function reads 512 bytes of data in memory and repeatedly converts that to int32 till it has read through the 512 bytes.

2. The `ReadInt32ValuesAsync` function reads from the buffered stream on demand. To create an int32, it first reads 32 bytes from the stream and then converts it to an int32. This simulates multiple async calls to buffered data, to create an int32


# Benchmark result
``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22631
12th Gen Intel Core i7-1265U, 1 CPU, 12 logical and 10 physical cores
.NET SDK=8.0.304
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT
  DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT


```
|               Method |       Mean |    Error |    StdDev |     Median |  Gen 0 | Allocated |
|--------------------- |-----------:|---------:|----------:|-----------:|-------:|----------:|
|    ReadInChunksAsync |   176.8 ns |  6.44 ns |  18.59 ns |   171.3 ns | 0.1070 |     672 B |
| ReadInt32ValuesAsync | 1,420.6 ns | 71.40 ns | 209.39 ns | 1,387.6 ns | 0.0153 |      96 B |
