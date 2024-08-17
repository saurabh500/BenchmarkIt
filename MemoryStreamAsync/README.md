# How expensive is the async statemachine?

# Problem statement

Should I write code to make multiple async calls to an object with buffered data, or should I read a big chunk of data expected, and work synchronously on top of the data?

# The benchmark 


# Benchmark result
``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22631
AMD EPYC 7763, 1 CPU, 32 logical and 16 physical cores
.NET SDK=8.0.304
  [Host]     : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT
  DefaultJob : .NET 8.0.8 (8.0.824.36612), X64 RyuJIT


```
|        Method | UseLargeChunks | depth |       Mean |    Error |  StdDev |  Gen 0 | Allocated |
|-------------- |--------------- |------ |-----------:|---------:|--------:|-------:|----------:|
| **ReadWithDepth** |          **False** |     **0** | **1,282.2 ns** |  **4.30 ns** | **3.81 ns** | **0.0057** |      **96 B** |
| **ReadWithDepth** |          **False** |     **3** | **1,353.2 ns** |  **8.83 ns** | **8.26 ns** | **0.0057** |      **96 B** |
| **ReadWithDepth** |          **False** |     **5** | **1,375.8 ns** |  **4.23 ns** | **3.75 ns** | **0.0057** |      **96 B** |
| **ReadWithDepth** |          **False** |     **9** | **1,431.6 ns** |  **6.73 ns** | **6.30 ns** | **0.0057** |      **96 B** |
| **ReadWithDepth** |          **False** |    **15** | **1,539.5 ns** | **10.05 ns** | **9.40 ns** | **0.0057** |      **96 B** |
| **ReadWithDepth** |           **True** |     **0** |   **171.7 ns** |  **1.79 ns** | **1.68 ns** | **0.0401** |     **672 B** |
| **ReadWithDepth** |           **True** |     **3** |   **215.2 ns** |  **1.70 ns** | **1.59 ns** | **0.0401** |     **672 B** |
| **ReadWithDepth** |           **True** |     **5** |   **238.4 ns** |  **1.71 ns** | **1.60 ns** | **0.0401** |     **672 B** |
| **ReadWithDepth** |           **True** |     **9** |   **301.8 ns** |  **2.54 ns** | **2.37 ns** | **0.0401** |     **672 B** |
| **ReadWithDepth** |           **True** |    **15** |   **433.8 ns** |  **1.81 ns** | **1.70 ns** | **0.0401** |     **672 B** |


# How to read this report

UseLargeChunks-> This is a variation where 512 bytes of data were fetch in one async call, into memory (from memory) and then this data was repeated used by BinaryPrimitives to convert to an int32 value.

depth -> This was used to simulate various number of layers of async call, to get to the reading from stream. 

As the depth of the async stack increases, there is increased runtime.

Also reading smaller chunks of data from memory using async APIs, had a larger overhead compared to reading a large chunk in one go.

