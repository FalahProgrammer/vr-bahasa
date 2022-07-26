using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConversationPanelController : MonoBehaviour
{
    // scripts
    [SerializeField] private LogControllerBehaviour _logControllerBehaviour;
    
    
    [SerializeField] private GameObject _backgroundPanel_1;
    [SerializeField] private GameObject _backgroundPanel_2;
    [SerializeField] private GameObject _npcQuestion;

    [SerializeField] private GameObject _leftHandBackButton;

    [SerializeField] private RectTransform _rectAnswer;
    [SerializeField] private RectTransform _rectVoiceAnswer;
    [SerializeField] private RectTransform _rectMic;

    [SerializeField] private Vector3 _answerPosition_1;
    [SerializeField] private Vector3 _answerPosition_2;

    [SerializeField] private Vector3 _voiceAnswerPosition_1;
    [SerializeField] private Vector3 _voiceAnswerPosition_2;
    
    [SerializeField] private Vector3 _micPosition_1;
    [SerializeField] private Vector3 _micPosition_2;
    
    [Space(10)]
    public UnityEvent OnExerciseFinished;
    public UnityEvent OnExamFinished;

    public void EnableBackground()
    {
        switch (_logControllerBehaviour.Mode)
        {
            case LogControllerBehaviour.Type.Exercise:
                SetExercisePanel();
                break;
            
            case LogControllerBehaviour.Type.Exam:
                SetExamPanel();
                break;
        }
    }

    public void SetExercisePanel()
    {
        _rectAnswer.anchoredPosition = _answerPosition_1;
        _rectVoiceAnswer.anchoredPosition = _voiceAnswerPosition_1;
        _rectMic.anchoredPosition = _micPosition_1;
        
        _backgroundPanel_1.SetActive(true);
        _backgroundPanel_2.SetActive(false);
        

        _npcQuestion.SetActive(true);
    }

    public void SetExamPanel()
    {
        _rectAnswer.anchoredPosition = _answerPosition_2;
        _rectVoiceAnswer.anchoredPosition = _voiceAnswerPosition_2;
        _rectMic.anchoredPosition = _micPosition_2;
        
        _backgroundPanel_1.SetActive(false);
        _backgroundPanel_2.SetActive(true);
        _leftHandBackButton.SetActive(false);
        
        _npcQuestion.SetActive(false);
    }

    public void FinishedConverstation()
    {
        switch (_logControllerBehaviour.Mode)
        {
            case LogControllerBehaviour.Type.None:
                break;
            case LogControllerBehaviour.Type.Exercise:
                OnExerciseFinished?.Invoke();
                break;
            case LogControllerBehaviour.Type.Exam:
                OnExamFinished?.Invoke();
                break;
        }
    }

    public void TurnOnBackButton()
    {
        _leftHandBackButton.SetActive(true);
    }
}
