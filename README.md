# FlatBuffersImporter
Google's FlatBuffers (.fbs files) importer for Unity.

## FlatBuffers 
[FlatBuffers](http://google.github.io/flatbuffers/)  is an efficient cross platform serialization library for C++, C#, C, Go, Java, JavaScript, Lobster, Lua, TypeScript, PHP, Python, and Rust. It was originally created at Google for game development and other performance-critical applications.

Check out the [tutorial](http://google.github.io/flatbuffers/flatbuffers_guide_tutorial.html)  to know more about how to work with FlatBuffers.

## FlatBuffersImporter
This repository simplifies the task of generating C# code from `.fbs` files, through a [`ScriptedImporter`](https://docs.unity3d.com/ScriptReference/Experimental.AssetImporters.ScriptedImporter.html)

While the serialisation with FlatBuffers is more explicit and requires more boilerplate it can be needed for higher performance.

## Usage
Add a `.fbs` file to your project (along with `FlatBuffers ` and the importer should automatically generate C# files for you.
Check out the example in the `FlatBuffersExample` containing a `MonsterSchema.fbs` file and its corresponding `MonsterSchema.cs` generated script, along side an example scene of how to use it.

### Contribution
Issues/PRs are welcome.
