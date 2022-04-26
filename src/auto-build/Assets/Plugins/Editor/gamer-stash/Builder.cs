using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Plugins.Editor
{
    public static class Builder
    {
        [MenuItem("Build/Build Android")]
        public static void MyBuild()
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
                throw new Exception("Build failed");
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