# FlatBuffersImporter
Google's FlatBuffers (.fbs files) importer for Unity.

## FlatBuffers 
[FlatBuffers](http://google.github.io/flatbuffers/)  is an efficient cross platform serialization library for C++, C#, C, Go, Java, JavaScript, Lobster, Lua, TypeScript, PHP, Python, and Rust. It was originally created at Google for game development and other performance-critical applications.

While the serialisation with FlatBuffers is more explicit and requires more boilerplate, it can be beneficial for higher performance.

Check out the [tutorial](http://google.github.io/flatbuffers/flatbuffers_guide_tutorial.html)  to know more about how to work with FlatBuffers.

Current bundled version of the compiler and the .Net scripts is `1.10.0`

## FlatBuffersImporter
This repository simplifies the task of generating C# code from `.fbs` files, through a [`ScriptedImporter`](https://docs.unity3d.com/ScriptReference/Experimental.AssetImporters.ScriptedImporter.html)

The importer will generate a `MySchema.cs` script from your `MySchema.fbs` schema files in the same folder where they are located by default.
You can change the folder for the generated script by selecting the `.fbs` file, and changing the `Generated Source Path` variable in the inspector. You can choose to reimport the asset, or use the `Regenerate code` button in the inspector to trigger C# script generation.

## Usage
Add a `.fbs` file to your project (along with `FlatBuffers ` and the importer should automatically generate C# files for you.
Check out the example in the `FlatBuffersExample` containing a `MonsterSchema.fbs` file and its corresponding `MonsterSchema.cs` generated script, along side an example scene of how to use it.

### Contribution
Issues/PRs are welcome.
