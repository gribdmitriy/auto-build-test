#addin nuget:?package=Cake.Unity&version=0.8.1
//8111
var target = Argument("target", "Build-Android");
var unityEditor = FindUnityEditor(2021, 3) ?? throw new Exception("Cannot find Unity Editor");

Task("Clean-Artifacts")
    .Does(() =>
{
    CleanDirectory($"./artifacts");
});

Task("Build-Android")
    .IsDependentOn("Clean-Artifacts")
    .Does(() =>
{
    UnityEditor(unityEditor.Path,
        new UnityEditorArguments
        {
            BatchMode = true,
            Quit = true,
            ExecuteMethod = "Plugins.Editor.Builder.BuildAndroid",
            BuildTarget = BuildTarget.Android,
            LogFile = "./artifacts/unity.log"
        });
});


RunTarget(target);