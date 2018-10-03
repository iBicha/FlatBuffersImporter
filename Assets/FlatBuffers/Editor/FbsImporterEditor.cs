using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

namespace FlatBuffers.Editor
{
	[CustomEditor(typeof(FbsImporter))]

	public class FbsImporterEditor : ScriptedImporterEditor {

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			//Just a little shortcut to regenerate code
			if (GUILayout.Button("Regenerate code"))
			{
				ApplyAndImport();
			}
		}
	}
}
