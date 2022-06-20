using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(DoMoveBehaviour))]
    public class DoMoveEditor : UnityEditor.Editor
    {
        /*public UnityEvent OnCompleteMove;
    private SerializedObject serializedObject;
    private SerializedProperty property;*/
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DoMoveBehaviour doMoveBehaviour = (DoMoveBehaviour) target;

            EditorGUILayout.Space();

            EditorUtility.SetDirty(target);
            
            doMoveBehaviour.Logo = (Texture2D)Resources.Load("logo",typeof(Texture2D));
            
            GUI.DrawTexture(new Rect(20, 30, 560, 100), doMoveBehaviour.Logo, ScaleMode.StretchToFill, true, 10.0F);
            
            EditorGUILayout.Space(110);
            
            doMoveBehaviour.SelectType =
                (DoMoveBehaviour.Type) EditorGUILayout.EnumPopup("Select Type", doMoveBehaviour.SelectType);
            
            /*GUILayout.Label(doMoveBehaviour.m_Logo);*/
            
            switch (doMoveBehaviour.SelectType)
            {
                case DoMoveBehaviour.Type.SpesificTransform:

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Type of SpesificTransform :", EditorStyles.foldoutHeader);

                    EditorGUILayout.Space();
                    
                    doMoveBehaviour.MyTarget = EditorGUILayout.ObjectField("My Transform", doMoveBehaviour.MyTarget, typeof(Transform), true) as Transform;
                    
                    EditorGUILayout.Space();
                    
                    doMoveBehaviour.TargetLocation = EditorGUILayout.ObjectField("Target Location", doMoveBehaviour.TargetLocation, typeof(Transform), true) as Transform;
                    
                    EditorGUILayout.Space();

                    doMoveBehaviour._selectEase =
                        (Ease) EditorGUILayout.EnumPopup("Select Ease", doMoveBehaviour._selectEase);
                    doMoveBehaviour._speed = EditorGUILayout.Slider("Speed", doMoveBehaviour._speed, 0f, 10f);

                    doMoveBehaviour.BoolDelayTime = EditorGUILayout.Toggle("Use Delayed Start?", doMoveBehaviour.BoolDelayTime);
                    
                    if (doMoveBehaviour.BoolDelayTime)
                    {
                        doMoveBehaviour.DelayedTime = EditorGUILayout.Slider("Delayed Time", doMoveBehaviour.DelayedTime, 0f, 10f);
                    }
                    else
                    {
                        doMoveBehaviour.DelayedTime = 0;
                    }
                    EditorGUILayout.Space();

                    EditorGUILayout.Space();

                    SerializedProperty SpesificTransform = serializedObject.FindProperty("OnCompleteMove");

                    EditorGUIUtility.labelWidth = 100;

                    EditorGUIUtility.fieldWidth = 100;

                    EditorGUILayout.PropertyField(SpesificTransform, true);

                    serializedObject.ApplyModifiedProperties();
                    break;

                case DoMoveBehaviour.Type.XYZMode:

                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Type of XYZ Mode :", EditorStyles.foldoutHeader);

                    EditorGUILayout.Space();

                    doMoveBehaviour.MyTarget = EditorGUILayout.ObjectField("My Transform", doMoveBehaviour.MyTarget, typeof(Transform), true) as Transform;
                    
                    EditorGUILayout.Space();
                    
                    doMoveBehaviour.Mode =
                        (DoMoveBehaviour.MyMode) EditorGUILayout.EnumPopup("Mode", doMoveBehaviour.Mode);

                    doMoveBehaviour._selectEase =
                        (Ease) EditorGUILayout.EnumPopup("Select Ease", doMoveBehaviour._selectEase);

                    if (doMoveBehaviour.Mode == DoMoveBehaviour.MyMode.X)
                    {
                        doMoveBehaviour._speed = EditorGUILayout.Slider("Speed", doMoveBehaviour._speed, 0f, 10f);

                        doMoveBehaviour._targetValue =
                            EditorGUILayout.FloatField("Target Position (X/Y/Z)", doMoveBehaviour._targetValue);
                    }

                    /*serializedObject = new SerializedObject(this);
                property = serializedObject.FindProperty("OnCompleteMove");
                EditorGUILayout.PropertyField(property, true);*/
                    EditorGUILayout.Space();

                    EditorGUILayout.Space();

                    SerializedProperty XYZMode = serializedObject.FindProperty("OnCompleteMove");

                    EditorGUIUtility.labelWidth = 100;

                    EditorGUIUtility.fieldWidth = 100;

                    EditorGUILayout.PropertyField(XYZMode, true);

                    serializedObject.ApplyModifiedProperties();
                    break;
            }
            
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Please attach ''InitialPosRotBehaviour'' to MyTarget transform before run the script");
        }
    }
}