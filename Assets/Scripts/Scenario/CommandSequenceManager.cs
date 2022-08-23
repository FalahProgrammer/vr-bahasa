using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommandSequenceManager : MonoBehaviour, iResetable
{
    //[SerializeField] private IntegerVariable _integerVariable;
    //public List<SequentialAnimation> _commandSequences = new List<SequentialAnimation>();
    public SequentialAnimation _commandSequences;

    [SerializeField] private DataVariable _dataVariable;

    [SerializeField] private AnswerCheckerBehaviour _answerCheckerBehaviour;

    [SerializeField] private ScenarioQuizController _scenarioQuizController;

    [SerializeField] private SendScoreBehaviour _sendScoreBehaviour;
    
    [SerializeField] private TextMeshProUGUI _answerText;

    [Space(10)]
    [SerializeField] private UnityEvent OnBeginSkipToEnd;
    
    private int _counterOdd = 0;
    
    private int _counterEven = 0;

    private void Start()
    {
        //CallStartNextCommand();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CallContinueCommand();
        }
    }

    public void CallStartNextCommand()
    {
        /*for (int i = 0; i < _commandSequences.Count; i++)
        {
            _commandSequences[i].PlayAnimation();
        }*/
        
        _commandSequences.PlayAnimation();
    }
    
    public void CallContinueCommand()
    {
        /*for (int i = 0; i < _commandSequences.Count; i++)
        {
            _commandSequences[i].ContinueAnimation();
        }*/
        
        _commandSequences.ContinueAnimation();
    }
    
    

    public void SkipToEndCommand()
    {
        //OnBeginSkipToEnd.Invoke();
        
        Debug.Log("Skip To End!");
            
        CallContinueCommand();

        _counterOdd = _dataVariable.qustion_id;
        _counterEven = _counterOdd;
        
        for (_commandSequences._currentIteration = _commandSequences._currentIteration; _commandSequences._currentIteration < _commandSequences.AnimationList.Count; _commandSequences._currentIteration++)
        {
            //Debug.Log(_commandSequences._currentIteration);
            
            Debug.Log("total List : " + _commandSequences.AnimationList.Count);

            if (_commandSequences._currentIteration % 2 == 0 /*&& i != _commandSequences.AnimationList.Count - 1*/)
            {
                //Debug.Log("Ini genap kan? : " + _commandSequences._currentIteration);

                //_dataVariable.qustion_id = _counterEven.ToString();
                
                for (int j = 0; j < _commandSequences.AnimationList.Count; j++)
                {
                    if (j == _commandSequences._currentIteration)
                    {
                        _dataVariable.qustion_id = _commandSequences.AnimationList[j].QuestionID;
                            
                        Debug.Log(_dataVariable.qustion_id);
                        
                        _scenarioQuizController.GenerateQuizScenario();
                        
                        _answerText.text = "";
                    
                        _answerCheckerBehaviour.BeginCalculateStrinSimiliarity();
                    }
                }

                //_counterEven = Int32.Parse(_dataVariable.qustion_id); 
                
                /*_scenarioQuizController.GenerateQuizScenario();
                
                if (_counterEven < _commandSequences.AnimationList.Count / 2)
                {
                    _counterEven += 1;
                }

                _answerText.text = "-";
                    
                _answerCheckerBehaviour.BeginCalculateStrinSimiliarity();*/
            }

            /*if (_commandSequences._currentIteration != 0 && _commandSequences._currentIteration % 2 != 0 /*&& i != _commandSequences.AnimationList.Count - 1#1#)
            {
                /*if (_counterOdd < _commandSequences.AnimationList.Count / 2)
                {
                    _dataVariable.qustion_id = _counterOdd.ToString();
                    
                    //_counterOdd = Int32.Parse(_dataVariable.qustion_id); 
                
                    _scenarioQuizController.GenerateQuizScenario();
                    
                    _answerText.text = "-";
               
                    _answerCheckerBehaviour.BeginCalculateStrinSimiliarity();
                    
                    CallContinueCommand();
                }#1#
                
               
                
                if (_counterOdd < _commandSequences.AnimationList.Count / 2)
                {
                    _counterOdd += 1;
                }
                //_scenarioQuizController.GenerateQuizScenario();
            }*/
            /*else if (i == _commandSequences.AnimationList.Count - 1)
            {
                _microphone.onClick.Invoke();
                
                _scenarioQuizController.GenerateQuizScenario();
            
                //CallContinueCommand();
            }*/
        }
        
        /*_sendScoreBehaviour.PostLogs();
        
        _sendScoreBehaviour.PostNilai();*/
        
        _sendScoreBehaviour.PostNilaiV2();

        _commandSequences.StopCoroutine();
        //_microphone.onClick.Invoke();
        
        
    }

    public void Reset()
    {
        /*for (int i = 0; i < _commandSequences.Count; i++)
        {
            _commandSequences[i].Reset();
        }*/
        
        _commandSequences.Reset();
    }
}
