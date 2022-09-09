using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ToolsGenerateScenario : MonoBehaviour
{
    public GenerateScenarioMode mode;
    [SerializeField] private IntegerVariable _integerVariable;

    public string scenarioId;
    public int npcId;
    
    
    [Space(10)]
    [HideInInspector] public GameObject scenarioPrefab;
    [HideInInspector] public string scenarioName;
    [HideInInspector] public string soundAssetsPath;

    [Space(10)] 
    public List<AudioClip> audioClip;
    private SequentialAnimation _scenarioScript;

    [FormerlySerializedAs("foldername")] public string[] scenarioArray;
    public string[] folderArray;
    public ScenarioLocation ScenarioLocation;
    
    
    //public bool replaceAudio = true;

    public void EditScenario()
    {
        _scenarioScript = scenarioPrefab.GetComponent<SequentialAnimation>();
        
        var replaceAudio = false;
        
        if (audioClip.Count == 0)
        {
            var count = _scenarioScript.AnimationList.Count;
            replaceAudio = true;

            for (int i = 0; i < count; i++)
            {
                // even number is NPC
                if (i % 2 == 0)
                {
                    AnimInteraction animInteraction = _scenarioScript.AnimationList[i];
                    
                    audioClip.Add(animInteraction.AudioClip);
                }
            }
        }
        
        _scenarioScript.AnimationList.Clear();
        
        int itemCount = Mathf.RoundToInt(audioClip.Count * 2);

        int npcIndex = 0;

        for (int i = 0; i < itemCount; i++)
        {
            AnimInteraction animInteraction = new AnimInteraction();
            
            // even number is NPC
            if (i % 2 == 0)
            {
                animInteraction.name = "NPC " + (npcIndex + 1);
                animInteraction.AudioClip = audioClip[npcIndex];
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
            
            _scenarioScript.AnimationList.Add(animInteraction);
        }

        if (replaceAudio)
        {
            audioClip.Clear();
        }
    }

    public void NewScenario(GameObject prefab, GameObject go)
    {
        if (scenarioName == "")
        {
            Debug.LogError("Scenario name is empty!");
            _scenarioScript = null;
        }
        else
        {
            scenarioPrefab = prefab;
            int itemCount = Mathf.RoundToInt(audioClip.Count * 2);

            SequentialAnimation scenarioScript = prefab.AddComponent(typeof(SequentialAnimation)) as SequentialAnimation;
            scenarioScript.AssignIntegerVariable = _integerVariable;
            scenarioScript.scenario_id = int.Parse(scenarioId);
            scenarioScript._id = npcId;
            
            int npcIndex = 0;

            for (int i = 0; i < itemCount; i++)
            {
                AnimInteraction animInteraction = new AnimInteraction();
            
                // even number is NPC
                if (i % 2 == 0)
                {
                    animInteraction.name = "NPC " + (npcIndex + 1);
                    animInteraction.AudioClip = audioClip[npcIndex];
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
            
                scenarioScript.AnimationList.Add(animInteraction);
            }

            DestroyImmediate(go);

            scenarioId = "";
        }
    }
    
    public enum GenerateScenarioMode
    {
        NewSinglePrefab, EditExistingPrefab, NewMultiplePrefabs
    }

    public void ClearAudioClipList()
    {
        audioClip.Clear();
    }
}
