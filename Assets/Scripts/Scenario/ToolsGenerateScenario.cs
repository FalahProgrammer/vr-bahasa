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

    public void EditScenario()
    {
        int itemCount = Mathf.RoundToInt(_audioClip.Count * 2);

        ScenarioScript = scenarioPrefab.GetComponent<SequentialAnimation>();
        
        ScenarioScript.AnimationList.Clear();

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
