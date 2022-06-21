using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ButtonController))]
    public class ButtonControllerEditor : UnityEditor.Editor
    {
        /*public UnityEvent OnCompleteMove;
    private SerializedObject serializedObject;
    private SerializedProperty property;*/

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ButtonController buttonController = (ButtonController) target;

            EditorGUILayout.Space();

            EditorUtility.SetDirty(target);

            buttonController.MyTransform = buttonController.GetComponent<Transform>();

            buttonController.Logo = (Texture2D) Resources.Load("logo", typeof(Texture2D));

            GUI.DrawTexture(new Rect(20, 30, 560, 100), buttonController.Logo, ScaleMode.StretchToFill, true, 10.0F);

            EditorGUILayout.Space(115);

            EditorGUILayout.LabelField("Audio Settings", EditorStyles.miniButtonMid);

            EditorGUILayout.Space();

            buttonController._audioSource =
                EditorGUILayout.ObjectField("Audio Source", buttonController._audioSource, typeof(AudioSource), true) as
                    AudioSource;

            EditorGUILayout.Space();

            buttonController._onHoverSound =
                EditorGUILayout.ObjectField("On Hover Clip", buttonController._onHoverSound, typeof(AudioClip), true) as
                    AudioClip;

            EditorGUILayout.Space();
            buttonController._onClickSound =
                EditorGUILayout.ObjectField("On Click Clip", buttonController._onClickSound, typeof(AudioClip), true) as
                    AudioClip;

            EditorGUILayout.Space();
            buttonController._onExitSound =
                EditorGUILayout.ObjectField("On Exit Clip", buttonController._onExitSound, typeof(AudioClip), true) as
                    AudioClip;

            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("Button Settings", EditorStyles.miniButtonMid);

            EditorGUILayout.Space();

            buttonController.ImageMode =
                (ButtonController.ButtonMode) EditorGUILayout.EnumPopup("Mode", buttonController.ImageMode);
            
            EditorGUILayout.Space();
            
            buttonController._usePlaceHolderText =
                EditorGUILayout.Toggle("Use Place Holder Text?", buttonController._usePlaceHolderText);

            EditorGUILayout.Space();
            
            if (buttonController._usePlaceHolderText)
            {
                buttonController.PivotMode =
                    (ButtonController.MyMode) EditorGUILayout.EnumPopup("Pivot Mode", buttonController.PivotMode);
                
                buttonController._placeHolderPivot = EditorGUILayout.Slider("Range Pivot", buttonController._placeHolderPivot, -100f, 100f);
            }

            EditorGUILayout.Space();
            
            switch (buttonController.ImageMode)
            {
                case ButtonController.ButtonMode.Image:

                    buttonController._onHoverImage = EditorGUILayout.ObjectField("On Hover Sprite",
                        buttonController._onHoverImage, typeof(Sprite), true) as Sprite;

                    EditorGUILayout.Space();

                    buttonController._onClickImage = EditorGUILayout.ObjectField("On Click Sprite",
                        buttonController._onClickImage, typeof(Sprite), true) as Sprite;

                    EditorGUILayout.Space();

                    buttonController._onIdleImage = EditorGUILayout.ObjectField("On Idle Sprite",
                        buttonController._onIdleImage, typeof(Sprite), true) as Sprite;

                    EditorGUILayout.Space(10);


                    break;

                case ButtonController.ButtonMode.Color:

                    buttonController._hoverColor =
                        EditorGUILayout.ColorField("On Hover Color", buttonController._hoverColor);

                    EditorGUILayout.Space();

                    buttonController._clickColor =
                        EditorGUILayout.ColorField("On Click Color", buttonController._clickColor);

                    EditorGUILayout.Space();

                    buttonController._idleColor =
                        EditorGUILayout.ColorField("On Idle Color", buttonController._idleColor);

                    EditorGUILayout.Space(10);


                    break;
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Scaling Settings", EditorStyles.miniButtonMid);

            EditorGUILayout.Space();

            buttonController.MyTransform =
                EditorGUILayout.ObjectField("My Transform", buttonController.MyTransform, typeof(Transform), true) as
                    Transform;

            EditorGUILayout.Space();

            buttonController.SelectEase = (Ease) EditorGUILayout.EnumPopup("Select Ease", buttonController.SelectEase);

            EditorGUILayout.Space();

            buttonController.Speed = EditorGUILayout.Slider("Speed", buttonController.Speed, 0f, 10f);

            EditorGUILayout.Space();

            buttonController.ScaleValue = EditorGUILayout.Slider("Scale Value", buttonController.ScaleValue, 0f, 10f);

            EditorGUILayout.Space();
            
            buttonController.InitValue = EditorGUILayout.Slider("Init Value", buttonController.InitValue, 0f, 10f);

            EditorGUILayout.Space();

            buttonController.BoolDelayTime =
                EditorGUILayout.Toggle("Use Delayed Start?", buttonController.BoolDelayTime);

            if (buttonController.BoolDelayTime)
            {
                buttonController.DelayedTime =
                    EditorGUILayout.Slider("Delayed Time", buttonController.DelayedTime, 0f, 10f);
            }
            else
            {
                buttonController.DelayedTime = 0;
            }

            EditorGUILayout.Space();

            EditorGUILayout.Space();

            SerializedProperty SpesificTransform = serializedObject.FindProperty("OnCompleteScaling");

            EditorGUIUtility.labelWidth = 100;

            EditorGUIUtility.fieldWidth = 100;

            EditorGUILayout.PropertyField(SpesificTransform, true);

            serializedObject.ApplyModifiedProperties();


            EditorGUILayout.Space();
        }
    }
}