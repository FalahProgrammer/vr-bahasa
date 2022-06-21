using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GenerateScenarioBehaviour : MonoBehaviour
{
    //[SerializeField] private ContentAreaController _contentAreaController;

    [SerializeField] private IntegerVariable _integerVariable;
    
    [SerializeField] private RepositoryContentArea _repositoryContentArea;
    
    [SerializeField] private DataVariable _dataVariable;
    
    [SerializeField] private ScriptableGameObjectDataController _scriptableGameObjectDataController;
    
    [SerializeField] private Transform _groupButtonContentArea;
    
    [SerializeField] private TimerBehaviour _timerBehaviour;

    [SerializeField] private CommandSequenceManager _commandSequenceManager;

    [SerializeField] private ScenarioEventBehaviour _scenarioEventBehaviour;

    //public ScenarioAnimatorController _scenarioAnimatorController;
    
    //[SerializeField] private GraspBehaviour _graspBehaviour;
    
    [SerializeField] private Text _duration;
    
    [SerializeField] private Text _durationFinal;
    
    [SerializeField] private List<GameObject> _listScenario= new List<GameObject>();

    [SerializeField] private Text[] _scenarioNameText;
    
    [SerializeField] private Text[] _scenarioDescText;
    private void Awake()
    {

        //_contentAreaController = FindObjectOfType<ContentAreaController>();
        
        _integerVariable = Resources.Load<IntegerVariable>("ScriptableObjects/Variable/Integer Variable");
        
        _commandSequenceManager = FindObjectOfType<CommandSequenceManager>();

        _scenarioEventBehaviour = FindObjectOfType<ScenarioEventBehaviour>();
        
        _scriptableGameObjectDataController = Resources.Load<ScriptableGameObjectDataController>("ScriptableObjects/Game Object/Scriptable GameObject Data Controller");
    }
    
    public void Init()
    {
        foreach(Transform child in _groupButtonContentArea)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void GetScenario()
    {
        Init();

        //var index = _contentAreaController.GetCurrentScenarioNumber();

        var index = _integerVariable.IntegerValue - 1;
        
        Debug.Log(index);

        _durationFinal.text = _timerBehaviour.GetTime();
        
        GenerateScenario(index, OnFinishedLoadAsset);
    }

    private void Update()
    {
        _duration.text = _timerBehaviour.GetTime();
    }


    private void GenerateScenario(int index, Action<GameObject> onFinishedLoadAsset)
    {
        _scriptableGameObjectDataController.ContentButton = Instantiate(_listScenario[index], _groupButtonContentArea, true);
        
        //_commandSequenceManager._commandSequences.Clear();

        //_commandSequenceManager._commandSequences.Add(_scriptableGameObjectDataController.ContentButton.GetComponent<SequentialAnimation>());

        _commandSequenceManager._commandSequences =
            _scriptableGameObjectDataController.ContentButton.GetComponent<SequentialAnimation>();

        /*_scenarioAnimatorController =
            _scriptableGameObjectDataController.ContentButton.GetComponent<ScenarioAnimatorController>(); */
        
        _scenarioEventBehaviour.SequentialAnimation = _scriptableGameObjectDataController.ContentButton.GetComponent<SequentialAnimation>();
        
        _scriptableGameObjectDataController.ContentButton.GetComponent<SequentialAnimation>().AudioSource =
            Camera.main.GetComponent<AudioSource>();
            
        //_scriptableGameObjectDataController.ContentButton.transform.localScale = new Vector3(1, 1, 1);
            
        var localPosition = _scriptableGameObjectDataController.ContentButton.transform.localPosition;
            
        //localPosition = new Vector3(localPosition.x, localPosition.y, 0);
        
        localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z);
            
        _scriptableGameObjectDataController.ContentButton.transform.localPosition = localPosition;
            
        _scriptableGameObjectDataController.ContentButton.transform.localRotation = new Quaternion(0 , 0 , 0 , 0);
        
        for (int j = 0; j < _repositoryContentArea.Items.Count; j++)
        {
            if (_repositoryContentArea.Items[j].chapter_id.Equals(_dataVariable.chapter_id) &&
                _repositoryContentArea.Items[j].materi_id.Equals(_dataVariable.materi_id) && 
                _repositoryContentArea.Items[j].id.Equals(_dataVariable.exam_id.ToString()))
            {
                for (int i = 0; i < _scenarioNameText.Length; i++)
                {
                    _scenarioNameText[i].text = _repositoryContentArea.Items[j].conversation_topic;
                }
                    
                for (int k = 0; k < _scenarioDescText.Length; k++)
                {
                    _scenarioDescText[k].text = _repositoryContentArea.Items[j].npc_name;
                }
            }
        }
        onFinishedLoadAsset?.Invoke(_scriptableGameObjectDataController.ContentButton);
    }
    
    private void OnFinishedLoadAsset(GameObject obj)
    {
        //_commandSequenceManager.CallContinueCommand();
        _scenarioEventBehaviour.ScenarioSubmiter();
    }
}
