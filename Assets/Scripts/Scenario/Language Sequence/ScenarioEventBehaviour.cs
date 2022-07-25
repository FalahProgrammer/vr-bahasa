using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScenarioEventBehaviour : MonoBehaviour
{
    public SequentialAnimation SequentialAnimation;

    [SerializeField] private DataVariable _dataVariable;
    [SerializeField] private ScenarioManager _scenarioManager;

    //public List<ScenarioEvent> ScenarioEvents = new List<ScenarioEvent>();

    public ButtonController Microphone;

    [SerializeField] private FadingBehaviour _answerFading;

    [SerializeField] private MenuControllerBehaviour _menuScenario;
    [SerializeField] private MenuControllerBehaviour _logScenario;

    [SerializeField] private CanvasGroup _panelScore;

    [SerializeField] private FadingBehaviour _panelQuestionFading;

    //[SerializeField] private TyperTextBehaviour _typerTextBehaviour;

    [SerializeField] private VoskSpeechToText _voskSpeechToText;

    [SerializeField] private SendScoreBehaviour _sendScoreBehaviour;

    [SerializeField] private NpcInteractionManager _npcInteractionManager;

    [SerializeField] private FadingBehaviour _requiredTextFadingBehaviour;
    [SerializeField] private TextMeshProUGUI _answerText;

    [SerializeField] private LogControllerBehaviour _logControllerBehaviour;
    [SerializeField] private ConversationPanelController _conversationPanelController;

    public int questionCounter;

    int counter;
    
    AudioClip clip;
        
    NpcSpeechSalsa npcSpeech;

    
    

    private void Start()
    {
        _npcInteractionManager = FindObjectOfType<NpcInteractionManager>();

        //SequentialAnimation = FindObjectOfType<SequentialAnimation>();

        /*for (int i = 0; i < SequentialAnimation.AnimationList.Count; i++)
        {
            ScenarioEvents.Add(new ScenarioEvent());
        }*/

        //ScenarioSubmiter();
    }

    public void ScenarioSubmiter()
    {
        SequentialAnimation.AudioSource = null;
        questionCounter = Int32.Parse(_dataVariable.qustion_id);

        Debug.Log("Scenario Submited");
        
        // dimaz
        counter = 0;
        // SequentialAnimation.AnimationList

        for (int i = 0; i < SequentialAnimation.AnimationList.Count; i++)
        {
            //SequentialAnimation.AnimationList[i].Animators.Add(_npcInteractionManager._npcInteractions[i].);
            
            SequentialAnimation.AnimationList[i].Animators.Clear();
            //Debug.Log("Animator Cleared! Current Animation List Index: " + i + ", Animator Count: " + SequentialAnimation.AnimationList[i].Animators.Count);

            SequentialAnimation.AnimationList[i].AnimationState.Clear();

            for (int j = 0; j < _npcInteractionManager._npcInteractions.Count; j++)
            {
                if (SequentialAnimation._id == _npcInteractionManager._npcInteractions[j]._id)
                {
                    SequentialAnimation.AnimationList[i].Animators.Add(_npcInteractionManager._npcInteractions[j].Animator);
                    
                    //Debug.Log("Animator Added! " + _npcInteractionManager._npcInteractions[j].Animator.name + ", Current Animation List Index: " + i + ", Animator Count: " + SequentialAnimation.AnimationList[i].Animators.Count);
                }
            }

            if (SequentialAnimation.AudioSource == null)
            {
                SequentialAnimation.AudioSource = SequentialAnimation.AnimationList[i].Animators[0].GetComponent<AudioSource>();
            }
            
            // NPC
            if (i == 0)
            {
                //Debug.Log((SequentialAnimation.AnimationList[i].AudioClip.name));

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(Microphone.SetButtonOn);
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(_requiredTextFadingBehaviour.BeginFadingIn);

                // new vosk test
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(_voskSpeechToText.StartRecording);

                counter += 1;
                
                SequentialAnimation.AnimationList[i].QuestionID = questionCounter += 1;
                
                SequentialAnimation.AnimationList[i].AnimationState.Add("S" + counter);

                /*for (int j = 0; j < SequentialAnimation.AnimationList[i].AnimationState.Count; j++)
                {
                    //Debug.Log("Ini counter : S + " + counter);

                    //SequentialAnimation.AnimationList[i].AnimationState.Add("");
                    
                    SequentialAnimation.AnimationList[i].AnimationState.Add("S+" + counter);

                    //Debug.Log("Index : " + i + " Name : " + SequentialAnimation.AnimationList[i].AnimationState[j]);
                }*/

                //clip = SequentialAnimation.AnimationList[i].AudioClip;
                //Debug.Log("Clip ID: " + clip);
                
                //npcSpeech = SequentialAnimation.AnimationList[i].Animators[0].GetComponent<NpcSpeechSalsa>();
                
                /*SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed
                    .AddListener(() => npcSpeech.PlaySpeech(SequentialAnimation.AnimationList[i].AudioClip));*/
            }

            // NPC
            else if (i != 0 && i % 2 == 0 && i != SequentialAnimation.AnimationList.Count - 1)
            {
                //Debug.Log("Even : "+i);

                counter += 1;
                
               

                SequentialAnimation.AnimationList[i].AnimationState.Add("S" + counter);
                
                /*for (int j = 0; j < SequentialAnimation.AnimationList[i].AnimationState.Count; j++)
                {
                    //Debug.Log("Ini counter : S + " + counter);

                    SequentialAnimation.AnimationList[i].AnimationState[j] = "S+" + counter;

                    //Debug.Log("Index : " + i + " Name : "+SequentialAnimation.AnimationList[i].AnimationState[j]);
                }*/

                
                //clip = ;
                
                //Debug.Log("Clip ID: " + clip);
                /*npcSpeech = SequentialAnimation.AnimationList[i].Animators[0].GetComponent<NpcSpeechSalsa>();

                SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed
                    .AddListener(() => npcSpeech.PlaySpeech(SequentialAnimation.AnimationList[i].AudioClip));*/

                SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed.AddListener(_answerFading.BeginFadingOut);
                //SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed.AddListener(_typerTextBehaviour.BeginPlayText);

                SequentialAnimation.AnimationList[i].QuestionID = questionCounter += 1;

                _answerText.text = "";

                SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed.AddListener(() =>
                {
                    //questionCounter += 1;

                    for (int j = 0; j < SequentialAnimation.AnimationList.Count; j++)
                    {
                        if (j == SequentialAnimation._currentIteration)
                        {
                            _dataVariable.qustion_id = SequentialAnimation.AnimationList[j].QuestionID.ToString();
                            Debug.Log("_currentIteration : "+ SequentialAnimation._currentIteration + ", Question ID: " + _dataVariable.qustion_id);
                        }
                    }


                    //Debug.Log("Question ID is : " + i);
                });

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(Microphone.SetButtonOn);
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(_requiredTextFadingBehaviour.BeginFadingIn);
                
                // new vosk test
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(_voskSpeechToText.StartRecording);
                
            }
            
            // USER
            else if (i != 0 && i % 2 == 1 && i != SequentialAnimation.AnimationList.Count - 1)
            {
                
                SequentialAnimation.AnimationList[i].AnimationState.Add("IDLE");
                SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed.AddListener(_requiredTextFadingBehaviour.BeginFadingOut);
                
                
                //SequentialAnimation.AnimationList[i].OnPartialAnimationFinished.AddListener(_typerTextBehaviour.BeginPlayText);
                
                /*for (int j = 0; j < SequentialAnimation.AnimationList[i].AnimationState.Count; j++)
                {
                    //Debug.Log("Index : " + i + " Name : "+SequentialAnimation.AnimationList[i].AnimationState[j]);

                    SequentialAnimation.AnimationList[i].AnimationState[j] = "IDLE";
                }*/
            }

            // LAST ARRAY (USER)
            if (i == SequentialAnimation.AnimationList.Count - 1)
            {
                //Debug.Log((SequentialAnimation.AnimationList[i].AnimationState));
                //Debug.Log("Last Index : "+i);

                /*for (int j = 0; j < SequentialAnimation.AnimationList[i].AnimationState.Count; j++)
                {
                    //Debug.Log("Last Index is : " + i + " Name : "+ SequentialAnimation.AnimationList[i].AnimationState[j]);
                }*/
                
                SequentialAnimation.AnimationList[i].AnimationState.Add("IDLE");
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationPlayed.AddListener(_requiredTextFadingBehaviour.BeginFadingOut);

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(() => _menuScenario.ClickMyTargetMenu(_panelScore));

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_panelQuestionFading.BeginFadingOut);

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_logControllerBehaviour.CalculateLogScore);

                // Logs is included in PostNilaiV2
                /*SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_sendScoreBehaviour.PostLogs);*/
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_sendScoreBehaviour.PostNilaiV2);

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_logControllerBehaviour.Reset);

                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(_conversationPanelController.FinishedConverstation);
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(() => _logScenario.ClickMyTargetMenu(_panelScore));
                
                SequentialAnimation.AnimationList[i].OnPartialAnimationFinished
                    .AddListener(() => _scenarioManager.ScenarioIsFinished());
            }
        }
        
        //SequentialAnimation.AudioSource = SequentialAnimation.AnimationList[0].Animators[0].GetComponent<AudioSource>();
    }
    
    
}