using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>
/// this script made for custom build setting enviorment
/// please put in this script to Asset/Editor folder
/// if KHSTool appear at unity editor upper tool box can be usable
/// 
/// How to Use
/// step1.choose your scene in popup window
///     exception -> if you don puls any scene will be build settings buttons appear in popup window
/// step2.choose build file path
/// step3.choose build file name
/// (if you dont follow step.2 & step3 it will be made by default setting)
/// </summary>
public class KHSBuildEditor : EditorWindow
{
    //scene choose popupwindow
    #region summary
    /// <summary>
    /// popup window for scenes array
    /// </summary>
    #endregion
    static CustomPopUpWindow popUpWindow = new();
    #region summary
    /// <summary>
    /// popup window rect position and size
    /// </summary>
    #endregion
    static Rect popUpRect = new(4f, 10f, 300f, 50f);
    #region summary
    /// <summary>
    /// current build setting scene string array
    /// </summary>
    #endregion
    static internal string[] Scenes = null;
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

    //path
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
    private void OnGUI()
    {
        #region popup window about plus scene to build
        GUILayout.Space(10f);
        GUILayout.Label("choose scene to build in buildsetting", EditorStyles.boldLabel);
        GUILayout.Space(10f);

        if (GUILayout.Button("choose scene"))
        {
            PopupWindow.Show(popUpRect, popUpWindow);
        }
        GUILayout.Space(10f);
        #endregion

        #region Button : choose path for making build file
        GUILayout.Space(10f);
        if (GUILayout.Button("Choose build Path"))
        {
            Path = EditorUtility.SaveFolderPanel("Choose build Path", "", "") + "/";
        }
        EditorGUILayout.TextField(Path);
        GUILayout.Space(10f);
        #endregion

        #region InputField : Setting Build File Name
        GUILayout.Space(10f);
        GUILayout.Label("input file name was created");
        FileName = EditorGUILayout.TextField(FileName.Split('.')[0]);
        GUILayout.Space(10f);
        #endregion

        if (GUILayout.Button("BuildStart")) PerformBuild();
    }

    [MenuItem("KHSTool/KHSBuildEditor", false, 1)]
    static void Init()
    {
        #region window setting
        KHSBuildEditor KBTwindow = (KHSBuildEditor)GetWindow<KHSBuildEditor>();
        KBTwindow.Show();

        KBTwindow.titleContent.text = "KHS Build Tester";

        KBTwindow.minSize = new Vector2(400f, 300f);
        KBTwindow.maxSize = new Vector2(400f, 1000f);

        #endregion

        #region initial path setting
        Path = Directory.GetCurrentDirectory() + "\\Build\\";
        FileName = "TestBuild";
        #endregion
    }
    static void PerformBuild()
    {
        #region path null exception handling
        if (Path == null || Path == "")
        {
            Path = Directory.GetCurrentDirectory() + "\\Build\\";
        }
        #endregion

        #region file name exception handling
        if (FileName == null || FileName == "")
        {
            FileName = "\\test" + ".exe";
        }
        else
        {
            FileName += ".exe";
        }
        #endregion

        #region scenelist null exception handling
        if (SceneList == null || SceneList.Count == 0)
        {
            Debug.Log("[##KHSBuild tool error : scene lost]");
        }
        #endregion

        BuildPipeline.BuildPlayer(SceneList.ToArray(), Path + FileName, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    public class CustomPopUpWindow : PopupWindowContent
    {
        Vector2 scrollPos = Vector2.zero;
        Vector2 minSize = new(395f, 100f);

        #region OnOpen : popup window initializing
        public override void OnOpen()
        {
            base.OnOpen();

            Scenes = EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
            SceneEnable = new bool[Scenes.Length];

            SceneList.Clear();
        }
        #endregion

        public override void OnGUI(Rect rect)//scroll and toggle window
        {
            editorWindow.minSize = minSize;

            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Width(395f), GUILayout.Height(100f));

            #region no scene execption 
            //when no scene in build settings open build settings
            if (Scenes == null || Scenes.Length == 0)
            {
                if (GUILayout.Button("Build Settings"))
                {
                    GetWindow(System.Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
                }
            }
            #endregion

            #region main scene stack
            else
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
            #endregion

            GUILayout.EndScrollView();
        }
    }
}
