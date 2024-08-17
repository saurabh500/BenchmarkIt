# Does inlining impact perf?

# Problem statement

If I have a method which does a small amount of work, then should I mark it to be aggressively inlined?

# The benchmark 

Calls a series of methods, with `[MethodImpl(MethodImplOptions.AggressiveInlining)]` set of them.
Benchmarks another series of methods which call into each other with no-inlining.

# Benchmark result

Nothing conclusive. Both of these basically yield the same result. 

Perhaps the experiment needs be more well-defined.

Sometimes inlined methods were 0.01 times worse and in some cases they were 0.01 times better.
