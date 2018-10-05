using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

#if NATIVE_ARRAY_ALLOCATOR && !UNSAFE_BYTEBUFFER
#error NATIVE_ARRAY_ALLOCATOR requires UNSAFE_BYTEBUFFER to also be defined
#endif

namespace FlatBuffers
{
#if NATIVE_ARRAY_ALLOCATOR
    public class NativeArrayAllocator : ByteBufferAllocator
    {
        private NativeArray<byte> _byteArray;

        public NativeArrayAllocator(byte[] buffer)
        {
            //TODO (PERF): this makes a copy of `buffer`. Should we pin and use pointer?
            _byteArray = new NativeArray<byte>(buffer, Allocator.Persistent);
            SetPointerAndLength();
        }

        public override void GrowFront(int newSize)
        {
            if ((Length & 0xC0000000) != 0)
                throw new Exception(
                    "ByteBuffer: cannot grow buffer beyond 2 gigabytes.");

            if (newSize < Length)
                throw new Exception("ByteBuffer: cannot truncate buffer.");
    
            //TODO (PERF): Use NativeList<T> to grow same buffer?
            var newBuffer = new NativeArray<byte>(newSize, Allocator.Persistent);
            _byteArray.CopyTo(newBuffer);
            _byteArray.Dispose();
            _byteArray = newBuffer;
            SetPointerAndLength();
        }

        public override void Dispose()
        {
            _byteArray.Dispose();
        }

#if !ENABLE_SPAN_T
        public override byte[] ByteArray
        {
            get { return _byteArray.ToArray(); }
        }
#endif

        private unsafe void SetPointerAndLength()
        {
            Buffer = (byte*) _byteArray.GetUnsafePtr();
            Length = _byteArray.Length;
        }
    }
#else
    //Fallback to default allocator
    public class NativeArrayAllocator : ByteArrayAllocator
    {
        public NativeArrayAllocator(byte[] buffer) : base(buffer)
        {
        }
    }
#endif
}