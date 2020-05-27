using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Diagnostics;

namespace WebXR.Editor
{
    public static class XRPackager
    {
        [MenuItem("XRPackage/Package Application")]
        public static void Build()
        {
            // string path = EditorUtility.SaveFolderPanel("Choose location for build...", "", "");

            // PlayerSettings.WebGL.template = "WebXR";
            // string[] editorScenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();

AssetDatabase.ExportPackage(new[] { "Assets/WebXR", "Assets/WebGLTemplates/WebXR" }, "WebXR-Assets.unitypackage", ExportPackageOptions.Recurse);

#if !UNITY_2018_4_OR_NEWER
			// There is no explicit api for setting the template as of 2018.4
			PlayerSettings.SetPropertyString("template", "PROJECT:WebXR", BuildTargetGroup.WebGL);
#else
			PlayerSettings.WebGL.template = "WebXR";
#endif
			BuildPipeline.BuildPlayer(new BuildPlayerOptions
			{
				target = BuildTarget.WebGL,
				locationPathName = "Build",
				scenes = new[] { "Assets/WebXR/Samples/Desert/WebXR.unity" },
			});

            System.OperatingSystem os = System.Environment.OSVersion;
            if (os.Platform.ToString() == "Win32NT" || os.Platform.ToString() == "Win64NT")
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.FileName = "cmd.exe";
                proc.WorkingDirectory = Application.dataPath;
                proc.Arguments = "/K package.cmd";
                Process.Start(proc);
                UnityEngine.Debug.Log("<color=green>Application packaged!</color>");
            } else
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.FileName = "/bin/sh";
                proc.WorkingDirectory = Application.dataPath;
                proc.Arguments = "./package.sh";
                // proc.Arguments = path;
                Process.Start(proc);
                UnityEngine.Debug.Log("<color=green>Application packaged!</color>");
            }
        }
    }
}
