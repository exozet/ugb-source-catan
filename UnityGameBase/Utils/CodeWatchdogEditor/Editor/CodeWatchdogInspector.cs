﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using CodeWatchdog;
using System.IO;

// http://unitypatterns.com/customizing-the-editor-part-3-inspectors-editors/

// TODO: Custom icon based on error count would be nice. See http://forum.unity3d.com/threads/custom-scriptableobject-icons-thumbnail.256246/

/// <summary>
/// An inspector override for C# files, running CodeWatchdog.
/// </summary>
[CustomEditor(typeof(MonoScript))]
public class CodeWatchdogInspector : Editor
{
    string lastFileViewed = "";
    
    string lastCheckErrors = "";
    
    string lastCheckSummary = "";
    
    string lastFileContent = "";
    
    /// <summary>
    /// Override the standard inspector GUI.
    /// </summary>
    public override void OnInspectorGUI()
    {
        // We really do *not* want to check the file on every OnInspectorGUI() call.
        //
        if (lastFileViewed != Selection.activeObject.name)
        {
            CamelCaseCSharpWatchdog cswd = new CamelCaseCSharpWatchdog();
            
            cswd.Init();
            
            lastCheckErrors = "";
            
            cswd.woff += (string message) => {lastCheckErrors += message + "\n";};
            
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            
            cswd.Check(path);
            
            lastCheckSummary = cswd.Summary();
            
            cswd = null;
            
            lastFileViewed = Selection.activeObject.name;
            
            lastFileContent = File.ReadAllText(AssetDatabase.GetAssetPath(Selection.activeObject));
        }
        
        GUILayout.Label("CodeWatchdog Results", EditorStyles.boldLabel);
        
        GUILayout.Label(lastCheckSummary);
        
        GUILayout.Label("File Content", EditorStyles.boldLabel);
        
        GUILayout.Label(lastFileContent);
        
        if (lastCheckErrors != "")
        {
            // TODO: Make clickable, opening MonoDevelop at the specific line.
            //
            GUILayout.Label("CodeWatchdog Errors", EditorStyles.boldLabel);
            
            GUILayout.Label(lastCheckErrors);
        }
        
        
        //        DrawDefaultInspector();
        
        return;
    }
}
