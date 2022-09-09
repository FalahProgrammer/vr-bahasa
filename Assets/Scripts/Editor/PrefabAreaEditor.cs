using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AreaPrefab))]
public class PrefabAreaEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AreaPrefab script = (AreaPrefab)target;

        switch (script.skyboxType)
        {
            case SkyboxType.defaultSkybox:
                break;
            case SkyboxType.customSkybox:
                GUILayout.Space(5);
                script.skybox = (Material) EditorGUILayout.ObjectField(script.skybox , typeof(Material), true);
                break;
        }
    }
}
