using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
// & "C:\Program Files\Unity\Editor\Unity.exe" -batchmode -quit -executeMethod Plugins.Editor.Builder.BuildAndroid -buildTarget Android -logFile ./artifacts/unity.log
namespace Plugins.Editor
{
    public static class Builder
    {
        [MenuItem("Build/Build Android")]
        public static void BuildAndroid()
        {
            var buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = GetScenes(),
                target = BuildTarget.Android,
                locationPathName = "../../artifacts/build.apk"
            };
            
            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            var summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                throw new Exception("Failed to build Android package. See log for details.");
            }
        }
        
        private static string[] GetScenes()
        {
            var scenes = new List<string>();
            
            for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                if (EditorBuildSettings.scenes[i].enabled)
                    scenes.Add(EditorBuildSettings.scenes[i].path);
            }
            
            return scenes.ToArray();
        }
    }
}