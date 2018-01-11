// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System.Collections.Sequences
{
    public class IBufferList
    {
        public Memory<byte> Memory { get; protected set; }

        public IBufferList Next { get; protected set; }

        public long VirtualIndex { get; protected set; }
    }
}
