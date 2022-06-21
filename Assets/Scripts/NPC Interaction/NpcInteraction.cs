using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NpcInteraction : MonoBehaviour
{
    //   [SerializeField] private GraspBehaviour _graspBehaviour;
    public int _id;

    /*[SerializeField] private NpcInteractionManager _npcInteractionManager;

    [SerializeField] private ScriptController _scriptController;

    public SequentialAnimation SequentialAnimation;*/
    
    /*[SerializeField] private RepositoryContentArea _repositoryContentArea;
    
    [SerializeField] private DataVariable _dataVariable;*/

    [SerializeField] private GraspBehaviour _graspBehaviour;
    
    public Animator Animator;
    
    [SerializeField] private NpcInteractionManager _npcInteractionManager;

    [SerializeField] private ScriptController _scriptController;
    
    [SerializeField] private GenerateScenarioBehaviour _generateScenarioBehaviour;

    public IntegerVariable _integerVariable;
    
    /*[SerializeField] private Text[] _scenarioNameText;
    
    [SerializeField] private Text[] _scenarioDescText;*/

    [SerializeField] private UnityEvent OnInteract;

    private void Start()
    {
        /*_scriptController = FindObjectOfType<ScriptController>();

        _npcInteractionManager = FindObjectOfType<NpcInteractionManager>();


        //_graspBehaviour = _npcInteractionManager._graspBehaviour;

        //OnInteract.AddListener(_scriptController.Reset);*/
    }

    private void OnEnable()
    {
        //_npcInteractionManager._npcInteractions.Add(this);

        _graspBehaviour = FindObjectOfType<GraspBehaviour>();

        _npcInteractionManager = FindObjectOfType<NpcInteractionManager>();
        
        _scriptController = FindObjectOfType<ScriptController>();

        _generateScenarioBehaviour = FindObjectOfType<GenerateScenarioBehaviour>();
        
        _npcInteractionManager._npcInteractions.Add(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnInteract.Invoke();
            
            /*for (int j = 0; j < _repositoryContentArea.Items.Count; j++)
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
            }*/

            /*int questid = Int32.Parse(_dataVariable.qustion_id);

            int tot = (SequentialAnimation.AnimationList.Count / 2) - 1;

            _dataVariable.qustion_id = (questid - tot).ToString();*/
            
            _scriptController.Reset();
            
            _generateScenarioBehaviour.GetScenario();
        }
    }

    public void BeginNPCInteraction()
    {
        if (_graspBehaviour._myTarget)
        {
            if (_graspBehaviour._myTarget.GetComponent<InitializeNpcInteraction>())
            {
                if (_integerVariable.IntegerValue /*- 1*/ == _id)
                {
                    //npcInteractionManager.ListEventNpcInteractor[npcInteractionManager.IncreamentInteraction].Invoke();
                    

                    OnInteract.Invoke();
                    
                    _scriptController.Reset();
                    /*for (int j = 0; j < _repositoryContentArea.Items.Count; j++)
                    {
                        if (_repositoryContentArea.Items[j].chapter_id.Equals(_dataVariable.chapter_id) &&
                            _repositoryContentArea.Items[j].materi_id.Equals(_dataVariable.materi_id) && 
                            _repositoryContentArea.Items[j].id.Equals(_dataVariable.exam_id.ToString()))
                        {
                            for (int i = 0; i < _scenarioNameText.Length; i++)
                            {
                                _scenarioNameText[i].text = _repositoryContentArea.Items[j].conversation_topic;
                            }
                        }
                    }*/

                    //npcInteractionManager.IncreamentInteraction++;
                }
            }
        }
    }
}