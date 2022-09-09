using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Text.RegularExpressions;
using Leap.Unity;

[CustomEditor(typeof(ToolsGenerateScenario))]
public class NewScenarioGeneratorEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ToolsGenerateScenario script = (ToolsGenerateScenario) target;

        if (GUILayout.Button("Clear Audioclips"))
        {
            script.ClearAudioClipList();
        }

        GUILayout.Space(10);

        switch (script.mode)
        {
            case ToolsGenerateScenario.GenerateScenarioMode.NewSinglePrefab:
                script.scenarioName = EditorGUILayout.TextField("Scenario Name", script.scenarioName);

                GUILayout.Space(10);
                if (GUILayout.Button("New Single Scenario"))
                {
                    CreateSingleScenarioPrefab(script.scenarioName, script);
                }

                break;

            case ToolsGenerateScenario.GenerateScenarioMode.EditExistingPrefab:
                script.scenarioPrefab =
                    (GameObject) EditorGUILayout.ObjectField(script.scenarioPrefab, typeof(GameObject), true);

                GUILayout.Space(10);
                if (GUILayout.Button("Edit Scenario"))
                {
                    script.EditScenario();
                }

                break;

            case ToolsGenerateScenario.GenerateScenarioMode.NewMultiplePrefabs:
                script.soundAssetsPath = EditorGUILayout.TextField("Sound Asset Path", script.soundAssetsPath);
                if (GUILayout.Button("New Multiple Scenarios"))
                {
                    CreateMultipleScenarioPrefab(script);
                }

                break;
        }
    }

    public static void CreateSingleScenarioPrefab(string scenarioName, ToolsGenerateScenario script)
    {
        if (scenarioName == "")
        {
            Debug.LogError("No Scenario Name!");
            return;
        }

        GameObject go = new GameObject(scenarioName);

        string localPath = "Assets/Resources/" + scenarioName + ".prefab";

        // Make sure the file name is unique, in case an existing Prefab has the same name.
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        // Create the new Prefab and log whether Prefab was saved successfully.
        bool prefabSuccess;

        // create prefab
        GameObject prefabObject = PrefabUtility.SaveAsPrefabAsset(go, localPath, out prefabSuccess);

        if (prefabSuccess)
        {
            Debug.Log("Prefab was saved successfully saved at: Assets/Resources/" + scenarioName + ".prefab");
            script.NewScenario(prefabObject, go);
        }
        else
        {
            Debug.Log("Prefab failed to save" + prefabSuccess);
        }
    }

    public static void CreateMultipleScenarioPrefab(ToolsGenerateScenario script)
    {
        //script.foldername.Clear();
        // Item ScriptableObjects are in "Assets/Resources/ItemsFolder"

        DirectoryInfo dir = new DirectoryInfo(script.soundAssetsPath);
        DirectoryInfo[] info = dir.GetDirectories("*.*");
        int count = dir.GetDirectories().Length;

        script.scenarioArray = new string [Mathf.Max(121, count)];
        script.folderArray = new string [Mathf.Max(121, count)];
        
        string tempPath = script.soundAssetsPath.Remove(0,6);
        tempPath = Application.dataPath + "/" + tempPath;

        int directoryFound = 0;
        //int npcIndex = 1;
        
        for (int i = 0; i < count; i++)
        {
            string name = info[i].ToString().Remove(0, tempPath.Length);
            string newName = name.Remove(5, name.Length - 5);
            int index = int.Parse(Regex.Match(newName, @"\d+").Value) - 1;
            int toDelete = (index + 1).ToString().Length + 1;

            var x = name.Remove(0, toDelete).Replace("_", " ");
            
            script.scenarioArray[index] = x;
            script.folderArray[index] = info[i].ToString();

            AudioClip[] clips = Resources.LoadAll<AudioClip>(script.soundAssetsPath.Remove(0, 17) + "/" + name);
            
            //Debug.Log("ID: " + (index + 1) + ", sound count: " + clips.Length + ", Area: " + script.ScenarioLocation.ScenarioLocationList[index] + "/" + script.ScenarioLocation.ScenarioAreaList[index]);
            
            script.audioClip = new List<AudioClip>(clips);
            script.scenarioId = (index + 1).ToString();
            script.scenarioName = x;
            script.npcId = script.ScenarioLocation.npcId[index];

            GameObject go = new GameObject(x);
            string directory = 
                "Assets/Resources/Language/" + script.soundAssetsPath.Remove(0, 23) + "/" + 
                script.ScenarioLocation.ScenarioLocationList[index] + "/" + 
                script.ScenarioLocation.ScenarioAreaList[index];
            
            directory = directory.Replace("(", "").Replace(")", "").Replace(" ICONIC", "  ICONIC").Replace("k  ICONIC", "k   ICONIC");
            
            
            string prefabPath = directory + "/" + x + ".prefab";
                
            //Debug.LogWarning("ID: " + (index + 1) + ", Directory Path: " + directory);
            //Debug.LogWarning("ID: " + (index + 1) + ", Prefab Path: " + prefabPath);
                
            if (Directory.Exists(directory))
            {
                directoryFound++;
                    
                // Make sure the file name is unique, in case an existing Prefab has the same name.
                // prefabPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);

                // Create the new Prefab and log whether Prefab was saved successfully.
                bool prefabSuccess;

                // create prefab
                GameObject prefabObject = PrefabUtility.SaveAsPrefabAsset(go, prefabPath, out prefabSuccess);

                if (prefabSuccess)
                {
                    Debug.Log("Prefab was saved successfully saved");
                    script.NewScenario(prefabObject, go);
                }
                else
                { 
                    Debug.Log("Prefab failed to save" + prefabSuccess);
                }
            }
            else
            {
                Debug.LogWarning("Directory Doesn't Exist, ID: " + (index + 1) + ", Directory Path: " + directory);

                string localPath = "Assets/Resources/" + x + ".prefab";

                // Make sure the file name is unique, in case an existing Prefab has the same name.
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

                // Create the new Prefab and log whether Prefab was saved successfully.
                bool prefabSuccess;

                // create prefab
                GameObject prefabObject = PrefabUtility.SaveAsPrefabAsset(go, localPath, out prefabSuccess);

                if (prefabSuccess)
                { 
                    Debug.Log("Prefab was saved successfully saved at: Assets/Resources/" + x + ".prefab");
                    script.NewScenario(prefabObject, go);
                }
                else
                { 
                    Debug.Log("Prefab failed to save" + prefabSuccess);
                }
            }
        }
        
        Debug.LogWarning("Directory Found: " + directoryFound);
        
        script.scenarioArray.OrderByAlphaNumeric(s => s).ToList();
    }
}


public static class IEnumerableEx
{
    public static IOrderedEnumerable<T> OrderByAlphaNumeric<T>(this IEnumerable<T> source, Func<T, string> selector)
    {
        int max = source
                      .SelectMany(i => Regex.Matches(selector(i), @"\d+").Cast<Match>().Select(m => (int?)m.Value.Length))
                      .Max() ?? 0;

        return source.OrderBy(i => Regex.Replace(selector(i), @"\d+", m => m.Value.PadLeft(max, '0')));
    }
}