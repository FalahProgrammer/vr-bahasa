using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class GenerateScenarioBehaviour : MonoBehaviour
{
    //[SerializeField] private ContentAreaController _contentAreaController;
    [SerializeField] private ScenarioManager _scenarioManager;

    [SerializeField] private bool _usingVR;

    [SerializeField] private IntegerVariable _integerVariable;

    [SerializeField] private RepositoryContentArea _repositoryContentArea;

    [SerializeField] private DataVariable _dataVariable;

    [SerializeField] private ScriptableGameObjectDataController _scriptableGameObjectDataController;

    [SerializeField] private ScriptableTransform _prefabArea;

    [SerializeField] private Transform _groupButtonContentArea;

    [SerializeField] private TimerBehaviour _timerBehaviour;

    [SerializeField] private CommandSequenceManager _commandSequenceManager;

    [SerializeField] private ScenarioEventBehaviour _scenarioEventBehaviour;

    [SerializeField] private GraspBehaviour _graspBehaviour;

    //public ScenarioAnimatorController _scenarioAnimatorController;

    //[SerializeField] private GraspBehaviour _graspBehaviour;

    [SerializeField] private Text _durationPanelUser;
    
    [SerializeField] private Text _duration;

    [SerializeField] private Text _durationFinal;

    [SerializeField] private List<GameObject> _listScenario = new List<GameObject>();

    [SerializeField] private Text[] _scenarioNameText;

    [SerializeField] private Text[] _scenarioDescText;

    [SerializeField] private SendGETMethod _sendGetMethod;

    private void Awake()
    {
        //_contentAreaController = FindObjectOfType<ContentAreaController>();

        _integerVariable = Resources.Load<IntegerVariable>("ScriptableObjects/Variable/Integer Variable");

        _commandSequenceManager = FindObjectOfType<CommandSequenceManager>();

        _scenarioEventBehaviour = FindObjectOfType<ScenarioEventBehaviour>();

        _graspBehaviour = FindObjectOfType<GraspBehaviour>();

        _scriptableGameObjectDataController =
            Resources.Load<ScriptableGameObjectDataController>(
                "ScriptableObjects/Game Object/Scriptable GameObject Data Controller");

        _usingVR = XRGeneralSettings.Instance.Manager.activeLoader != null;
    }

    public void Init()
    {
        foreach (Transform child in _groupButtonContentArea)
        {
            Destroy(child.gameObject);
        }
    }

    public void GetScenario()
    {
        if (_usingVR)
        {
            if (_graspBehaviour._myTarget)
            {
                // bug!! _graspBehaviour._myTarget is not assigned when pressing tab
                if (_graspBehaviour._myTarget.GetComponent<InitializeNpcInteraction>())
                {
                    if (_graspBehaviour._myTarget.GetComponent<InitializeNpcInteraction>()._canInteract)
                    {
                        Init();

                        //var index = _contentAreaController.GetCurrentScenarioNumber();

                        var index = _integerVariable.IntegerValue - 1;

                        Debug.Log(index);

                        //_durationFinal.text = _durationPanelUser.text;

                        //GetScenarioSentences();

                        GenerateScenario(index, OnFinishedLoadAsset);
                    }
                }
            }
        }

        // if not using VR
        else
        {
            Init();

            //var index = _contentAreaController.GetCurrentScenarioNumber();

            var index = _integerVariable.IntegerValue - 1;

            //_durationFinal.text = _durationPanelUser.text;

            //GetScenarioSentences();

            GenerateScenario(index, OnFinishedLoadAsset);
        }
    }

    public void GetScenarioSentences()
    {
        _dataVariable.qustion_id = 0;
        _sendGetMethod.GetScenarioData(_dataVariable.materi_id,
            _scenarioEventBehaviour.SequentialAnimation.scenario_id.ToString());
    }

    private void Update()
    {
        _duration.text = _timerBehaviour.GetTime();
    }


    private void GenerateScenario(int index, Action<GameObject> onFinishedLoadAsset)
    {
        _scenarioManager.ScenarioIsActive = true;

        _scriptableGameObjectDataController.ContentButton =
            Instantiate(_listScenario[index], _groupButtonContentArea, true);

        //_commandSequenceManager._commandSequences.Clear();

        //_commandSequenceManager._commandSequences.Add(_scriptableGameObjectDataController.ContentButton.GetComponent<SequentialAnimation>());

        _commandSequenceManager._commandSequences =
            _scriptableGameObjectDataController.ContentButton.GetComponent<SequentialAnimation>();

        /*_scenarioAnimatorController =
            _scriptableGameObjectDataController.ContentButton.GetComponent<ScenarioAnimatorController>(); */

        _scenarioEventBehaviour.SequentialAnimation =
            _scriptableGameObjectDataController.ContentButton.GetComponent<SequentialAnimation>();

        //_commandSequenceManager._commandSequences.AudioSource = Camera.main.GetComponent<AudioSource>();

        // Debug.Log("Audio Source Object: " + _commandSequenceManager._commandSequences.AnimationList[0].Animators[0].name);

        //AudioSource tempAudio = _commandSequenceManager._commandSequences.AnimationList[0].Animators[0].GetComponent<AudioSource>();

        //Debug.Log("Audio Source Origin: " + tempAudio.gameObject.name + ", Assigned Audio Source: " + _commandSequenceManager._commandSequences.AudioSource.gameObject.name);

        //_scriptableGameObjectDataController.ContentButton.transform.localScale = new Vector3(1, 1, 1);

        var localPosition = _scriptableGameObjectDataController.ContentButton.transform.localPosition;

        //localPosition = new Vector3(localPosition.x, localPosition.y, 0);

        localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z);

        _scriptableGameObjectDataController.ContentButton.transform.localPosition = localPosition;

        _scriptableGameObjectDataController.ContentButton.transform.localRotation = new Quaternion(0, 0, 0, 0);

        Debug.Log("Assigning NPC Data");
        for (int j = 0; j < _repositoryContentArea.Items.Count; j++)
        {
            /*Debug.Log("Materi ID: " + _repositoryContentArea.Items[j].materi_id + ", Expected: " + _dataVariable.materi_id +
                               ", Chapter ID: " + _repositoryContentArea.Items[j].chapter_id + ", Expected: " + _dataVariable.chapter_id + 
                               ", ID: " + _repositoryContentArea.Items[j].id + ", Expected: " + _dataVariable.exam_id);*/

            if (_repositoryContentArea.Items[j].location_id == Int32.Parse(_dataVariable.chapter_id))
                if (_repositoryContentArea.Items[j].language_id == Int32.Parse(_dataVariable.materi_id)) /*&&)
                 
                _repositoryContentArea.Items[j].id.Equals(_dataVariable.area_id.ToString()))*/
                {
                    for (int i = 0; i < _scenarioNameText.Length; i++)
                    {
                        for (int k = 0; k < _repositoryContentArea.Items[j].npc.Length; k++)
                        {
                            if (_dataVariable.contentAreaIndex == j)
                            {
                                Debug.Log("PASSING GRADE : " + _repositoryContentArea.Items[j]
                                              .npc[_integerVariable.IntegerValue - 1].passing_grade);
                                
                                _durationPanelUser.text = _timerBehaviour.GetTime();
                                
                                _timerBehaviour._currentDuration = _repositoryContentArea.Items[j]
                                    .npc[_integerVariable.IntegerValue - 1].duration * 60;
                                
                                _scenarioNameText[i].text = _repositoryContentArea.Items[j]
                                    .npc[_integerVariable.IntegerValue - 1].npc_name;
                                
                                _durationFinal.text = _durationPanelUser.text;
                            } 
                            
                        }
                        
                        //_repositoryContentArea.Items[j].conversation_topic;
                    }

                    for (int i = 0; i < _scenarioDescText.Length; i++)
                    {
                        for (int k = 0; k < _repositoryContentArea.Items[j].npc.Length; k++)
                        {
                            if (_dataVariable.contentAreaIndex == j)
                            {
                                _scenarioDescText[i].text = _repositoryContentArea.Items[j]
                                    .npc[_integerVariable.IntegerValue - 1].conversation_topic;
                            } 
                            
                        }
                        
                        //_repositoryContentArea.Items[j].conversation_topic;
                    }
                }
        }

        onFinishedLoadAsset?.Invoke(_scriptableGameObjectDataController.ContentButton);
    }

    public UnityEvent OnScenarioGenerated;

    private void OnFinishedLoadAsset(GameObject obj)
    {
        //_commandSequenceManager.CallContinueCommand();
        _scenarioEventBehaviour.ScenarioSubmiter();
        OnScenarioGenerated?.Invoke();
        GetScenarioSentences();
    }

    // called when an area is instantiated
    public void AssignAreaScenario()
    {
        _listScenario.Clear();
        Transform container = _prefabArea.MyTransform.GetComponent<AreaPrefab>().characterContainer;
        foreach (Transform t in container)
        {
            _listScenario.Add(t.GetComponent<Interactor>().npcScenario);
        }
    }
}