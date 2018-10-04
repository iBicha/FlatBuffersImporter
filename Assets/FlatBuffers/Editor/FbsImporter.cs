using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace FlatBuffers.Editor
{
    [ScriptedImporter(1, "fbs")]
    public class FbsImporter : ScriptedImporter
    {
        [Tooltip("A folder to save generated scripts to")]
        public DefaultAsset generatedSourcePath;

#if UNITY_EDITOR_WIN
        private const string k_FlatCompiler = "flatc.exe";
#else
        private const string k_FlatCompiler = "flatc";
#endif
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var schemaFile = Path.GetFullPath(ctx.assetPath);

            if (generatedSourcePath == null)
            {
                generatedSourcePath = AssetDatabase.LoadAssetAtPath<DefaultAsset>(Path.GetDirectoryName(ctx.assetPath));
                ctx.AddObjectToAsset("generatedSourcePath", generatedSourcePath);
                ctx.SetMainObject(generatedSourcePath);
            }

            var sourceFolder = Path.GetFullPath(AssetDatabase.GetAssetPath(generatedSourcePath));

            var flatcPath = Path.GetFullPath(Path.Combine("Assets/FlatBuffers/Editor/Compiler", k_FlatCompiler));
            var procArgs = "-n \"" + schemaFile + "\" --gen-onefile";

            var process = new Process();
            process.StartInfo = new ProcessStartInfo(flatcPath, procArgs);
            process.StartInfo.WorkingDirectory = sourceFolder;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();

            var stderr = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                ctx.LogImportError(stderr);
            }

//            var generatedScriptPath = ctx.assetPath.Replace(".fbs", ".cs");
//            AssetDatabase.ImportAsset(generatedScriptPath, ImportAssetOptions.ForceSynchronousImport);
//            var generatedScript = AssetDatabase.LoadAssetAtPath<MonoScript>(generatedScriptPath);
        }
    }
}