using System;
using System.Collections;
using System.Collections.Generic;
using CrazyMinnow.SALSA;
using UnityEngine;
using FIMSpace.FLook;

public class AnimatorLookoutController : MonoBehaviour
{
    [SerializeField] private ScriptableGameObjectDataController prefabArea;
    [SerializeField] private Transform playerBody;

    public void SetAnimatorLook()
    {
        Debug.LogWarning("Setting NPC Animator Look at");
        Transform characterContainer = prefabArea.ContentButton.GetComponent<AreaPrefab>().characterContainer;

        foreach (Transform npc in characterContainer)
        {
            Debug.Log(npc.name);
            
            var lookAnimator = npc.GetComponentInChildren<FLookAnimator>();
            if (lookAnimator != null)
            {
                lookAnimator.ObjectToFollow = playerBody;
                //lookAnimator.enabled = false;
            }

            /*var salsaEyes = npc.GetComponent<Eyes>();
            salsaEyes.lookTarget = playerBody;
            salsaEyes.useAffinity = true;*/
        }
    }
}
