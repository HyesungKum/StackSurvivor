using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

public class KHSBuildTool : EditorWindow
{
    #region summary
    /// <summary>
    /// current build setting scene string array
    /// </summary>
    #endregion
    static string[] Scenes = null;
    #region summary
    /// <summary>
    /// this value make toggle button used by custumize add scene and build test
    /// </summary>
    #endregion
    static bool[] SceneEnable = null;
    #region summary
    /// <summary>
    /// this class was stacked scene about toggle in tool window
    /// must convert to array before this list used
    /// </summary>
    #endregion
    static List<string> SceneList = new List<string>();
    #region summary
    /// <summary>
    /// custom build path
    /// </summary>
    #endregion
    static string Path = null;
    #region summary
    /// <summary>
    /// custom build file name
    /// </summary>
    #endregion
    static string FileName = null;

    private void CreateGUI()
    {
        Init();
    }
    Rect buttonRect;
    private void OnGUI()
    {
        #region toggle about plus scene to build
        GUILayout.Space(10f);
        GUILayout.Label("choose scene to build in buildsetting", EditorStyles.boldLabel);
        GUILayout.Space(10f);

        if (GUILayout.Button("choose scene"))
        {
            buttonRect = GUILayoutUtility.GetLastRect();
            PopupWindow.Show(buttonRect, new customPopUpWindow());
        }
        GUILayout.Space(10f);
        #endregion

        #region Button : choose path for making build file
        GUILayout.Space(10f);
        if (GUILayout.Button("Choose build Path"))
        {
            Path = EditorUtility.SaveFolderPanel("Choose build Path", Path, "Build");
        }
        EditorGUILayout.TextField(Path);
        GUILayout.Space(10f);
        #endregion

        #region InputField : Setting Build File Name
        GUILayout.Space(10f);
        GUILayout.Label("input file name was created");
        FileName = EditorGUILayout.TextField(FileName);
        GUILayout.Space(10f);
        #endregion

        if (GUILayout.Button("BuildStart")) PerformBuild();
    }

    [MenuItem("KHSTool/KHSBuildTool", false, 1)]
    static void Init()
    {
        #region window setting
        KHSBuildTool KBTwindow = (KHSBuildTool)GetWindow<KHSBuildTool>();
        KBTwindow.Show();

        KBTwindow.titleContent.text = "KHS Build Tester";

        KBTwindow.minSize = new Vector2(400f, 300f);
        KBTwindow.maxSize = new Vector2(400f, 1000f);

        Scenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
        SceneEnable = new bool[Scenes.Length];
        #endregion

        #region initial path setting
        Path = Directory.GetCurrentDirectory() + "/Build/";
        FileName = "TestBuild";
        #endregion
    }
    static void PerformBuild()
    {
        if (Path == null)
        {
            Path = Directory.GetCurrentDirectory() + "/Build/";
        }

        if (FileName == null)
        {
            FileName = "/test" + ".exe";
        }
        else
        {
            FileName += ".exe";
        }

        if (SceneList == null || SceneList.Count == 0)
        {
            Debug.Log("[##KHSBuild tool error : scene lost]");
        }
 
        BuildPipeline.BuildPlayer(SceneList.ToArray(), Path + FileName, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    public class customPopUpWindow : PopupWindowContent
    {
        public override void OnGUI(Rect rect)
        {
            for (int i = 0; i < Scenes.Length; i++)
            {
                if (SceneEnable[i] = GUILayout.Toggle(SceneEnable[i], Scenes[i].ToString()))
                {
                    SceneList.Add(Scenes[i]);
                }
                else
                {
                    SceneList.Remove(Scenes[i]);
                }
            }
        }
    }
}
