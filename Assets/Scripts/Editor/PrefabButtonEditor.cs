using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    [CustomEditor(typeof(PrefabButtonDataController))]
    public class PrefabButtonEditor : UnityEditor.Editor
    {
        /*public UnityEvent OnCompleteMove;
    private SerializedObject serializedObject;
    private SerializedProperty property;*/
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PrefabButtonDataController prefabButtonDataController = (PrefabButtonDataController) target;

            EditorGUILayout.Space();

            EditorUtility.SetDirty(target);
            
            prefabButtonDataController.Logo = (Texture2D)Resources.Load("logo",typeof(Texture2D));
            
            GUI.DrawTexture(new Rect(20, 30, 560, 100), prefabButtonDataController.Logo, ScaleMode.StretchToFill, true, 10.0F);
            
            EditorGUILayout.Space(110);
            
            prefabButtonDataController.SelectType =
                (PrefabButtonDataController.Type) EditorGUILayout.EnumPopup("Select Type", prefabButtonDataController.SelectType);

            /*GUILayout.Label(doMoveBehaviour.m_Logo);*/
            switch (prefabButtonDataController.SelectType)
            {
                case PrefabButtonDataController.Type.Location:
                    
                    EditorGUILayout.Space();
                    
                    prefabButtonDataController.ID = EditorGUILayout.TextField("ID", prefabButtonDataController.ID);
                    
                    prefabButtonDataController.MateriId = EditorGUILayout.TextField("Materi Id", prefabButtonDataController.MateriId);
                    
                    //prefabButtonDataController.ChapterJudul = EditorGUILayout.TextField("Judul", prefabButtonDataController.ChapterJudul);
                    
                    //prefabButtonDataController.ButtonName = EditorGUILayout.TextField("Button Name", prefabButtonDataController.ButtonName);
                    prefabButtonDataController.LocationtAreaName = EditorGUILayout.TextField("Location Name", prefabButtonDataController.LocationtAreaName);

                    prefabButtonDataController.TextButtonName = EditorGUILayout.ObjectField("Text", prefabButtonDataController.TextButtonName, typeof(Text), true) as Text;

                    break;
                
                case PrefabButtonDataController.Type.Area:
                    
                    
                    
                    /*prefabButtonDataController.UrlVideo = EditorGUILayout.TextField("Url Video", prefabButtonDataController.UrlVideo);
                    
                    prefabButtonDataController.UrlImage = EditorGUILayout.TextField("Url Image", prefabButtonDataController.UrlImage);
                    
                    prefabButtonDataController.Description = EditorGUILayout.TextField("Description", prefabButtonDataController.Description);*/
                    
                    prefabButtonDataController.SceneName = EditorGUILayout.TextField("Scene Name", prefabButtonDataController.SceneName);
  
                    prefabButtonDataController.ID = EditorGUILayout.TextField("ID", prefabButtonDataController.ID);
                    
                    prefabButtonDataController.Duration = EditorGUILayout.IntField("Duration", prefabButtonDataController.Duration);
                    
                    prefabButtonDataController.ScenarioNumber = EditorGUILayout.IntField("Scenario Number", prefabButtonDataController.ScenarioNumber);
                    
                    prefabButtonDataController.ChapterId = EditorGUILayout.TextField("Chapter Id", prefabButtonDataController.ChapterId);
                    
                    prefabButtonDataController.ContentAreaName = EditorGUILayout.TextField("Area Name", prefabButtonDataController.ContentAreaName);
                    
                    prefabButtonDataController.AreaPrefab = EditorGUILayout.ObjectField("Area Prefab", prefabButtonDataController.AreaPrefab, typeof(GameObject), true) as GameObject;
                    
                    //prefabButtonDataController.ButtonName = EditorGUILayout.TextField("Button Name", prefabButtonDataController.ButtonName);
                    
                    prefabButtonDataController.TextButtonName = EditorGUILayout.ObjectField("Text", prefabButtonDataController.TextButtonName, typeof(Text), true) as Text;

                    break;
            }
            
            serializedObject.ApplyModifiedProperties();
            
        }
    }
}