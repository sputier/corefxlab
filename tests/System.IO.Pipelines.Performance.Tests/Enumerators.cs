using System.IO.Pipelines.Tests;
using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
namespace System.IO.Pipelines.Performance.Tests
{
    [Config(typeof(CoreConfig))]
    public class Reading
    {
        private const int InnerLoopCount = 512;
        public ReadableBuffer _readableBuffer;

        [Setup]
        public void Setup()
        {
            _readableBuffer = BufferUtilities.CreateBuffer(2048 * 2048);
        }
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        public virtual int ReadableBufferReader()
        {
            var reader = new ReadableBufferReader(_readableBuffer); ;
            int ch;
            do
            {
                ch = reader.Take();
            } while (ch != -1);
            return ch;
        }

        [Benchmark(OperationsPerInvoke = InnerLoopCount)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public byte MemoryIteratorWithIndexer()
        {
            byte ch = 0;
            foreach (var memory in _readableBuffer)
            {
                var span = memory.Span;
                for (int i = 0; i < span.Length; i++)
                {
                    ch = span[i];
                }
            }
            return ch;
        }

        [Benchmark(OperationsPerInvoke = InnerLoopCount)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public unsafe byte MemoryIteratorWithPointer()
        {
            byte ch = 0;
            foreach (var memory in _readableBuffer)
            {
                var span = memory.Span;
                var length = span.Length;

                fixed (byte* data = &span.DangerousGetPinnableReference())
                {
                    for (int i = 0; i < length; i++)
                    {
                        ch = data[i];
                    }
                }
            }
            return ch;
        }
    }

    [Config(typeof(CoreConfig))]
    public class Enumerators
    {
        private const int InnerLoopCount = 512;

        private ReadableBuffer _readableBuffer;

        [Setup]
        public void Setup()
        {
            _readableBuffer = BufferUtilities.CreateBuffer(Enumerable.Range(100, 200).ToArray());
        }

        [Benchmark(OperationsPerInvoke = InnerLoopCount)]
        public void MemoryEnumerator()
        {
            for (int i = 0; i < InnerLoopCount; i++)
            {
                var enumerator = new MemoryEnumerator(_readableBuffer.Start, _readableBuffer.End);
                while (enumerator.MoveNext())
                {
                    var memory = enumerator.Current;
                }
            }
        }

        [Benchmark(OperationsPerInvoke = InnerLoopCount)]
        public void SegmentEnumerator()
        {
            for (int i = 0; i < InnerLoopCount; i++)
            {
                var enumerator = new SegmentEnumerator(_readableBuffer.Start, _readableBuffer.End);
                while (enumerator.MoveNext())
                {
                    var segmentPart = enumerator.Current;
                }
            }
        }
    }
}
