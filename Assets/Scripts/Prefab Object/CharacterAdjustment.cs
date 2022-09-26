using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAdjustment : MonoBehaviour
{
    [SerializeField] private bool debugMode;
    
    [Space(10)]
    public bool playerIsSitting;
    
    [Space(10)]
    //public bool adjustUIPosition;
    public Vector3 adjustmentUIPosition = new Vector3();

    [Space(5)]
    //public bool adjustPlayerPosition;
    public Vector3 adjustmentPlayerPosition = new Vector3();
    
    [Space(5)]
    //public bool adjustPlayerRotation;
    public Vector3 adjustmentPlayerRotation = new Vector3();
    
    [HideInInspector] public Transform characterPivot;
    [HideInInspector] public Transform uiPivot;

    [Space(10)]
    [Tooltip("Auto assign NPC layer, if you forget to assign NPC object's layer to NPC")]
    [SerializeField] private int NPCLayer = 12;
    
    [Tooltip("Maximum loop search for finding pivots before it's forcefully stopped, to avoid loop overload")]
    [SerializeField] private int maxLoop = 10; 
    
    private void Awake()
    {
        // Set NPC object's layer to NPC
        GameObject npcObject = GetComponentInChildren<Animator>().gameObject;
        if (npcObject.layer != NPCLayer)
        {
            SetLayerRecursively(npcObject, 12);
        }


        int loop = 0;
        
        if (characterPivot == null || characterPivot != null && characterPivot.name != "Character Pivot")
        {
            loop = 0;
            bool found = false; 
            
            if (debugMode)
            {
                Debug.Log("Finding Character Pivot");
            }

            FindTransfromByName("Character Pivot", transform, out characterPivot);
        }
        
        if (uiPivot == null || uiPivot != null && uiPivot.name != "UI Pivot")
        {
            loop = 0;
            bool found = false;
            
            if (debugMode)
            {
                Debug.Log("Finding UI Pivot");
            }

            FindTransfromByName("UI Pivot", transform, out uiPivot);
        }

        void FindTransfromByName(string target, Transform currentObj, out Transform foundObj)
        {
            bool found = false;
            foundObj = null;
            
            if (loop > maxLoop)
            {
                return;
            }
            loop += 1;

            foreach (Transform child in currentObj)
            {
                if (debugMode)
                {
                    Debug.Log("Current Search: " + child.name);
                }
                
                if (child.name == target)
                {
                    foundObj = child;
                    found = true;

                    if (debugMode)
                    {
                        Debug.Log("FOUND !!");
                    }

                    break;;
                }
            }

            if (!found)
            {
                if (loop < maxLoop)
                {
                    if (debugMode)
                    {
                        Debug.Log("Next Search");
                    }

                    foreach (Transform child in currentObj)
                    {
                        if (child.gameObject.layer == NPCLayer)
                        {
                            return;
                        }

                        FindTransfromByName(target, child, out foundObj);
                    }
                }
                else
                {
                    if (debugMode)
                    {
                        Debug.LogError("Loop Overload, NPC: " + currentObj.name);
                    }
                }
            }
        }
    }
    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }
       
        obj.layer = newLayer;
       
        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
