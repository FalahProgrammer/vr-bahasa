using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(ToolsGenerateScenario))]
public class NewScenarioGeneratorEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ToolsGenerateScenario script = (ToolsGenerateScenario)target;

        if(GUILayout.Button("Clear Audioclips"))
        {
            script.ClearAudioClipList();
        }
        
        GUILayout.Space(10);
        
        switch (script.mode)
        {
            case ToolsGenerateScenario.GenerateScenarioMode.GenerateNewPrefab:
                script.scenarioName = EditorGUILayout.TextField("Scenario Name", script.scenarioName);
                
                GUILayout.Space(10);
                if(GUILayout.Button("New Scenario"))
                {
                    CreateScenarioPrefab(script.scenarioName, script);
                }
                
                break;
                
            case ToolsGenerateScenario.GenerateScenarioMode.EditExistingPrefab:
                script.scenarioPrefab = (GameObject) EditorGUILayout.ObjectField(script.scenarioPrefab , typeof(GameObject), true);
                    
                GUILayout.Space(10);
                if(GUILayout.Button("Edit Scenario"))
                {
                    script.EditScenario();
                }
                break;
        }
    }

    public static void CreateScenarioPrefab(string scenarioName, ToolsGenerateScenario script)
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
}