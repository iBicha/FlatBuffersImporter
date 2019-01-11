
# FlatBuffersImporter
Google's FlatBuffers (.fbs files) importer for Unity. 
## FlatBuffers
[FlatBuffers](http://google.github.io/flatbuffers/) is an efficient cross platform serialization library for C++, C#, C, Go, Java, JavaScript, Lobster, Lua, TypeScript, PHP, Python, and Rust. It was originally created at Google for game development and other performance-critical applications.

While the serialisation with FlatBuffers is more explicit and requires more boilerplate, it can be beneficial for higher performance.

Check out the [tutorial](http://google.github.io/flatbuffers/flatbuffers_guide_tutorial.html) to know more about how to work with FlatBuffers.

Current bundled version of the compiler and the .Net scripts is `1.10.0`

## FlatBuffersImporter
This repository simplifies the task of generating C# code from `.fbs` files, through a [`ScriptedImporter`](https://docs.unity3d.com/ScriptReference/Experimental.AssetImporters.ScriptedImporter.html), by triggering the "flat compiler" with the `-n` flag to generate C# files.

### Usage
Add a `.fbs` file to your project (along with `FlatBuffers`) and the importer should automatically generate C# files for you.
 
The importer will generate a `MySchema.cs` script from your `MySchema.fbs` schema files in the same folder where they are located by default. 

You can change the folder for the generated script by selecting the `.fbs` file, and changing the `Generated Source Path` variable in the inspector. You can choose to reimport the asset, or use the `Regenerate code` button in the inspector to trigger C# script generation.

Check out the example in the `FlatBuffersExample` containing a `MonsterSchema.fbs` file and its corresponding `MonsterSchema.cs` generated script, along side an example scene of how to use it.

### Scripting Define Symbols

You can add the following symbols to Unity to enable certain features within FlatBuffers' `ByteBuffer` :

-  `UNSAFE_BYTEBUFFER `
   - This will use **unsafe code** to manipulate the underlying byte array. This can yield a reasonable performance increase.
-  `BYTEBUFFER_NO_BOUNDS_CHECK`
   - This will disable the bounds check asserts to the byte array. This can yield a small performance gain in normal code.
-  `ENABLE_SPAN_T`
   - This will enable reading and writing blocks of memory with a `Span<T>` instead if just `T[]`. You can also enable writing directly to shared memory or other types of memory by providing a custom implementation of `ByteBufferAllocator`.
   -  `ENABLE_SPAN_T` also requires `UNSAFE_BYTEBUFFER` to be defined
-  `NATIVE_ARRAY_ALLOCATOR`
   - This will enable the `NativeArrayAllocator` which is a `ByteBufferAllocator` implementation that uses Unity's [NativeArray<byte>](https://docs.unity3d.com/ScriptReference/Unity.Collections.NativeArray_1.html). When `NATIVE_ARRAY_ALLOCATOR` is not defined, `NativeArrayAllocator` falls back to the default `ByteArrayAllocator`.
   -  `NATIVE_ARRAY_ALLOCATOR` also requires `UNSAFE_BYTEBUFFER` to be defined.

### Contribution
Issues/PRs are welcome.

### License
- The `FlatBuffers` Importer is under MIT.
- The .Net implementation of `FlatBuffers` and the actual compiler are under Apache 2.0
