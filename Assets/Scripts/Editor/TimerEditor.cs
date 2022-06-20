using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(TimerBehaviour))]
    public class TimerEditor : UnityEditor.Editor
    {
        /*public UnityEvent OnCompleteMove;
    private SerializedObject serializedObject;
    private SerializedProperty property;*/
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TimerBehaviour timerBehaviour = (TimerBehaviour) target;

            EditorGUILayout.Space();

            EditorUtility.SetDirty(target);
            
            timerBehaviour.Logo = (Texture2D)Resources.Load("logo",typeof(Texture2D));
            
            GUI.DrawTexture(new Rect(20, 30, 560, 100), timerBehaviour.Logo, ScaleMode.StretchToFill, true, 10.0F);
            
            EditorGUILayout.Space(110);
            
            timerBehaviour.SelectType =
                (TimerBehaviour.Type) EditorGUILayout.EnumPopup("Select Type", timerBehaviour.SelectType);
            
            SerializedProperty onTimerStart = serializedObject.FindProperty("onTimerStart");
            
            SerializedProperty onTickEvent = serializedObject.FindProperty("onTickEvent");
            
            SerializedProperty onTimerEnd = serializedObject.FindProperty("onTimerEnd");
            
            /*GUILayout.Label(doMoveBehaviour.m_Logo);*/
            switch (timerBehaviour.SelectType)
            {
                case TimerBehaviour.Type.Countdown:
                    
                    EditorGUILayout.Space();
                    
                    timerBehaviour._useCountdown = true;
                    
                    timerBehaviour._initialDuration = EditorGUILayout.Slider("Duration", timerBehaviour._initialDuration, 10000f, 0f);
                    
                    timerBehaviour._currentDuration = EditorGUILayout.FloatField("Current Duration", timerBehaviour._currentDuration);
                    
                    EditorGUILayout.PropertyField(onTimerStart, true);
                    
                    EditorGUILayout.PropertyField(onTickEvent, true);
                    
                    EditorGUILayout.PropertyField(onTimerEnd, true);
                    break;
                
                case TimerBehaviour.Type.Counting:

                    timerBehaviour._useCountdown = false;
                    
                    timerBehaviour._initialDuration = EditorGUILayout.Slider("Duration", timerBehaviour._initialDuration, 0f, 10000f);
                    
                    timerBehaviour._currentDuration = EditorGUILayout.FloatField("Current Duration", timerBehaviour._currentDuration);
                    
                    EditorGUILayout.PropertyField(onTimerStart, true);
                    
                    EditorGUILayout.PropertyField(onTickEvent, true);
                    
                    EditorGUILayout.PropertyField(onTimerEnd, true);
                    break;
            }

            
            EditorGUILayout.Space();

            EditorGUILayout.Space();

            
            
            
            
            
            
            
            serializedObject.ApplyModifiedProperties();
            
            EditorGUILayout.Space();
        }
    }
}