using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioEventBehaviour : MonoBehaviour
{
    public SequentialAnimation SequentialAnimation;
    [SerializeField] private DataVariable _dataVariable;
    //public List<ScenarioEvent> ScenarioEvents = new List<ScenarioEvent>();
    public ButtonController Microphone;
    [SerializeField] private FadingBehaviour _answerFading;
    [SerializeField] private MenuControllerBehaviour _menuScenario;
    [SerializeField] private CanvasGroup _panelScore;
    [SerializeField] private FadingBehaviour _panelQuestionFading;
    [SerializeField] private LogControllerBehaviour _logControllerBehaviour;
    [SerializeField] private SendScoreBehaviour _sendScoreBehaviour;
    [SerializeField] private TextMeshProUGUI _answerText;
    public int questionCounter;

    private void Start()
    {
        //SequentialAnimation = FindObjectOfType<SequentialAnimation>();

        /*for (int i = 0; i < SequentialAnimation.AnimationList.Count; i++)
        {
            ScenarioEvents.Add(new ScenarioEvent());
        }*/
        
        //ScenarioSubmiter();
    }

    public void ScenarioSubmiter()
    {
        questionCounter = Int32.Parse(_dataVariable.qustion_id);
        
        for (int i = 0; i < SequentialAnimation.AnimationList.Count; i++)
        {
            if (i == 0)
            {
                //Debug.Log((SequentialAnimation.AnimationList[i].AudioClip.name));
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(Microphone.SetButtonOn);
            }
            else if (i !=0 && i % 2 == 0 && i != SequentialAnimation.AnimationList.Count - 1 )
            {
                //Debug.Log("Even : "+i);
                
                for (int j = 0; j < SequentialAnimation.AnimationList[i].AnimationState.Count; j++)
                {
                    Debug.Log("Index : " + i + " Name : "+SequentialAnimation.AnimationList[i].AnimationState[j]);
                }
                
                
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed.AddListener(_answerFading.BeginFadingOut);
                
                SequentialAnimation.AnimationList[i].QuestionID = questionCounter += 1;

                _answerText.text = "";
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed.AddListener(()=>
                {
                    //questionCounter += 1;

                    for (int j = 0; j < SequentialAnimation.AnimationList.Count; j++)
                    {
                        if (j == SequentialAnimation._currentIteration)
                        {
                            _dataVariable.qustion_id = SequentialAnimation.AnimationList[j].QuestionID.ToString();
                            
                            Debug.Log(_dataVariable.qustion_id);
                        
                        }
                    }

                    

                    Debug.Log("Question ID is : " + i);
                    
                    
                    
                    
                });

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(Microphone.SetButtonOn);
            }
            
            if (i == SequentialAnimation.AnimationList.Count - 1)
            {
                //Debug.Log((SequentialAnimation.AnimationList[i].AnimationState));
                //Debug.Log("Last Index : "+i);
                
                for (int j = 0; j < SequentialAnimation.AnimationList[i].AnimationState.Count; j++)
                {
                    Debug.Log("Last Index is : " + i + " Name : "+ SequentialAnimation.AnimationList[i].AnimationState[j]);
                }

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(() => _menuScenario.ClickMyTargetMenu(_panelScore));

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_panelQuestionFading.BeginFadingOut);
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_logControllerBehaviour.CalculateLogScore);
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_sendScoreBehaviour.PostLogs);
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_sendScoreBehaviour.PostNilaiV2);
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_logControllerBehaviour.Reset);
            }
        }
    }
    
    
}
