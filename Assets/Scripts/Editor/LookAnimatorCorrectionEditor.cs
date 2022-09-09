using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(LookAnimatorOneClick))]
public class LookAnimatorCorrectionEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LookAnimatorOneClick script = (LookAnimatorOneClick)target;

        GUILayout.Space(10);
        
        if(GUILayout.Button("Apply"))
        {
            script.BoneCorrection();
        }
        
        GUILayout.Space(20);
        
        if(GUILayout.Button("Done"))
        {
            script.Finished();
        }
    }
}
