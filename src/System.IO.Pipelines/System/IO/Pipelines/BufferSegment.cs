// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Buffers;
using System.Collections.Sequences;
using System.Diagnostics;
using System.Text;

namespace System.IO.Pipelines
{
    internal class BufferSegment: IBufferList
    {
        /// <summary>
        /// The Start represents the offset into Array where the range of "active" bytes begins. At the point when the block is leased
        /// the Start is guaranteed to be equal to 0. The value of Start may be assigned anywhere between 0 and
        /// Buffer.Length, and must be equal to or less than End.
        /// </summary>
        public int Start { get; private set; }

        /// <summary>
        /// The End represents the offset into Array where the range of "active" bytes ends. At the point when the block is leased
        /// the End is guaranteed to be equal to Start. The value of Start may be assigned anywhere between 0 and
        /// Buffer.Length, and must be equal to or less than End.
        /// </summary>
        public int End
        {
            get => _end;
            set
            {
                Debug.Assert(Start - value <= AvailableMemory.Length);

                _end = value;
                Memory = AvailableMemory.Slice(Start, _end - Start);
            }
        }

        /// <summary>
        /// The buffer being tracked if segment owns the memory
        /// </summary>
        private OwnedMemory<byte> _ownedMemory;

        private int _end;

        private BufferSegment _nextSegment;

        public void SetMemory(OwnedMemory<byte> buffer)
        {
            SetMemory(buffer, 0, 0);
        }

        public void SetMemory(OwnedMemory<byte> ownedMemory, int start, int end, bool readOnly = false)
        {
            _ownedMemory = ownedMemory;
            _ownedMemory.Retain();

            AvailableMemory = _ownedMemory.Memory;

            ReadOnly = readOnly;
            VirtualIndex = 0;
            Start = start;
            End = end;
            Next = null;
        }

        public void ResetMemory()
        {
            _ownedMemory.Release();
            _ownedMemory = null;
            AvailableMemory = default;
        }

        public Memory<byte> AvailableMemory { get; private set; }

        public int Length => End - Start;

        public BufferSegment NextSegment => (BufferSegment) Next;

        /// <summary>
        /// If true, data should not be written into the backing block after the End offset. Data between start and end should never be modified
        /// since this would break cloning.
        /// </summary>
        public bool ReadOnly { get; private set; }

        /// <summary>
        /// The amount of writable bytes in this segment. It is the amount of bytes between Length and End
        /// </summary>
        public int WritableBytes => AvailableMemory.Length - End;

        /// <summary>
        /// ToString overridden for debugger convenience. This displays the "active" byte information in this block as ASCII characters.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Memory.IsEmpty)
            {
                return "<EMPTY>";
            }

            var builder = new StringBuilder();
            SpanLiteralExtensions.AppendAsLiteral(Memory.Span, builder);
            return builder.ToString();
        }

        public void SetNext(BufferSegment segment)
        {
            Debug.Assert(segment != null);
            Debug.Assert(Next == null);
            Debug.Assert(segment.Next == null);
            Next = segment;
            segment.VirtualIndex = VirtualIndex + Length;
        }
    }
}
