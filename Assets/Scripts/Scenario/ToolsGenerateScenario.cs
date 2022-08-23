using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ToolsGenerateScenario : MonoBehaviour
{
    public GenerateScenarioMode mode;
    [SerializeField] private IntegerVariable _integerVariable;
    
    [Space(10)]
    [HideInInspector] public GameObject scenarioPrefab;
    [HideInInspector] public string scenarioName;

    [Space(10)] 
    [SerializeField] private List<AudioClip> _audioClip;
    private SequentialAnimation ScenarioScript;

    //public bool replaceAudio = true;

    public void EditScenario()
    {
        ScenarioScript = scenarioPrefab.GetComponent<SequentialAnimation>();
        
        var replaceAudio = false;
        
        if (_audioClip.Count == 0)
        {
            var count = ScenarioScript.AnimationList.Count;
            replaceAudio = true;

            for (int i = 0; i < count; i++)
            {
                // even number is NPC
                if (i % 2 == 0)
                {
                    AnimInteraction animInteraction = ScenarioScript.AnimationList[i];
                    
                    _audioClip.Add(animInteraction.AudioClip);
                }
            }
        }
        
        ScenarioScript.AnimationList.Clear();
        
        int itemCount = Mathf.RoundToInt(_audioClip.Count * 2);

        int npcIndex = 0;

        for (int i = 0; i < itemCount; i++)
        {
            AnimInteraction animInteraction = new AnimInteraction();
            
            // even number is NPC
            if (i % 2 == 0)
            {
                animInteraction.name = "NPC " + (npcIndex + 1);
                animInteraction.AudioClip = _audioClip[npcIndex];
                animInteraction.QuestionID = npcIndex;
                animInteraction.AnimationState.Add("S+" + (npcIndex + 1));

                npcIndex += 1;
            }
            else
            {
                animInteraction.name = "USER";
                animInteraction.AudioClip = null;
                animInteraction.AnimationState.Add("IDLE");
            }
            
            ScenarioScript.AnimationList.Add(animInteraction);
        }

        if (replaceAudio)
        {
            _audioClip.Clear();
        }
    }

    public void NewScenario(GameObject prefab, GameObject go)
    {
        if (scenarioName == "")
        {
            Debug.LogError("Scenario name is empty!");
            ScenarioScript = null;
        }
        else
        {
            scenarioPrefab = prefab;
            int itemCount = Mathf.RoundToInt(_audioClip.Count * 2);

            SequentialAnimation ScenarioScript = prefab.AddComponent(typeof(SequentialAnimation)) as SequentialAnimation;
            ScenarioScript.AssignIntegerVariable = _integerVariable;
            
            int npcIndex = 0;

            for (int i = 0; i < itemCount; i++)
            {
                AnimInteraction animInteraction = new AnimInteraction();
            
                // even number is NPC
                if (i % 2 == 0)
                {
                    animInteraction.name = "NPC " + (npcIndex + 1);
                    animInteraction.AudioClip = _audioClip[npcIndex];

                    npcIndex += 1;
                }
                else
                {
                    animInteraction.name = "USER";
                    animInteraction.AudioClip = null;
                }
            
                ScenarioScript.AnimationList.Add(animInteraction);
            }

            DestroyImmediate(go);
        }
    }
    
    public enum GenerateScenarioMode
    {
        GenerateNewPrefab, EditExistingPrefab
    }

    public void ClearAudioClipList()
    {
        _audioClip.Clear();
    }
}
