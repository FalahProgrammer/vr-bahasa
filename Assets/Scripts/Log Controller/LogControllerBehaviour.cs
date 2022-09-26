using System;
using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LogControllerBehaviour : MonoBehaviour, iResetable
{

    private string _logQuestion;

    private string _logRightAnswer;

    private string _logFixAnswer;

    [SerializeField] private Transform _groupLogContent;
    
    [SerializeField] private Transform _groupFinalLogContent;

    [SerializeField] private GameObject _prefabLogController;
    
    [SerializeField] private GameObject _prefabScoreLogController;

    [SerializeField] private Text _finalScoreText;

    [SerializeField] private Text _correctText;

    [SerializeField] private Text _inCorrectText;

    [SerializeField] private ScrollRect _logScrollbar;
        
    public TimerBehaviour _timerBehaviour;

    [SerializeField] private ContentAreaController _contentAreaController;
        
    public LogRepository _logRepository;

    [SerializeField] private RepositoryLogAnswer _repositoryLogAnswer;

    [SerializeField] private DataVariable _dataVariable;
        
    private Image _finalLogIndicatorIcon;
    
    private Image _logIndicatorIcon;
    
    private int _logCounter;

    private bool _boolAnswer;
    
    public int _finalLogCounter;
    
    public int correctCounter = 0;
        
    public int incorrectCounter = 0;

    
    /*[HideInInspector]
    [Tooltip("Select your move direction")]*/
    [HideInInspector] 
    public Type Mode;
    
    public enum Type
    {
        None,
        Exercise,
        Exam
        
    }
    //private LogController _logController;
    
    private void Awake()
    {
        //_logController = new LogController(_prefabLogController,_logQuestion,_logRightAnswer,_logFixAnswer,_groupLogContent);
        
        _repositoryLogAnswer = Resources.Load<RepositoryLogAnswer>("ScriptableObjects/Repository/Repository Log Answer");
        
        ClearLog();
    }




    public void SetTypeLog(int index)
    {
        if (index == 1)
        {
            Mode = Type.Exercise;
        }
        else if (index == 2)
        {
            Mode = Type.Exam;
        }
        else
        {
            Mode = Type.None;
        }
    }
    public void SetLog(int questionId,int answerId, string logQuestion, string logRightAnswer, string logYourAnswer, Image finalLogIndicator, Image logIndicator, double score, float duration, bool answer_status)
    {
        GenerateLog(duration,logQuestion, logRightAnswer, logYourAnswer, logIndicator);
        
        GenerateFinalLog(duration,logQuestion, logRightAnswer, logYourAnswer, finalLogIndicator);

        FormatDuration(duration);
        
        Canvas.ForceUpdateCanvases();
        
        _logScrollbar.verticalNormalizedPosition = 0;
        
        Debug.Log(FormatDuration(duration));

        var durationFormat = FormatDuration(duration).ToString();
        
        for (int i = 0; i < _contentAreaController.ListContent.Count; i++)
        {
            if (_contentAreaController.ListContent[i].language_id.ToString() == _dataVariable.materi_id)
            {
                _logRepository.content_id = _contentAreaController.ListContent[i].id;
            }
        }
        
        _logRepository.logs.Add(new Log 
        { 
            question_id = questionId,
            answer_id = answerId,
            question = logQuestion, 
            right_answer = logRightAnswer, 
            user_answer = logYourAnswer,
            score = score, 
            duration_taken = durationFormat,
            answer_status = answer_status
        });
        
        
    }
    public void Init()
    {
        foreach(Transform child in _groupLogContent.transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach(Transform child in _groupFinalLogContent.transform)
        {
            Destroy(child.gameObject);
        }
    }
    TimeSpan FormatDuration(float duration)
    {
        float theTime = Mathf.Round(duration);
        
        TimeSpan _timeSpan = TimeSpan.FromSeconds(theTime);

        return _timeSpan;
    }
    public void CalculateLogScore()
    {
        double totalScore = 0;
        
        for (int i = 0; i < _logRepository.logs.Count; i++)
        {
            totalScore += _logRepository.logs[i].score;
        }

        totalScore = Math.Round(totalScore / _logRepository.logs.Count) * 1 / 1;

        _finalScoreText.text = totalScore.ToString();

        _repositoryLogAnswer.TotalScore = totalScore;
        
        Debug.Log("Your Score is : " + totalScore);
    }

    public void CalculateCorrectInCorrectAnswer(double score, int limit_score)
    {
        if (score <= limit_score)
        {
            _repositoryLogAnswer.InCorrectAnswer += 1;
            
            incorrectCounter += 1;
            
            _inCorrectText.text = incorrectCounter.ToString();
            
            Debug.Log("InCorrect");
            
            if (_inCorrectText.text != "")
            {
                _inCorrectText.text = incorrectCounter.ToString();
            }
            else
            {
                _inCorrectText.text = "0";
            }
        }
        else
        {
            _repositoryLogAnswer.CorrectAnswer += 1;

            correctCounter += 1;

            if (_correctText.text != null)
            {
                _correctText.text = correctCounter.ToString();
            }
            else
            {
                _correctText.text = "0";
            }
            
            
            Debug.Log("Correct");
        }
        
    }

    public void ClearLog()
    {
        _logRepository.logs.Clear();

        _repositoryLogAnswer.CorrectAnswer = 0;
        
        _repositoryLogAnswer.InCorrectAnswer = 0;
        
        _repositoryLogAnswer.TotalScore = 0;

        correctCounter = 0;

        incorrectCounter = 0;

        _finalLogCounter = 0;
    }

    public void GenerateLog(float sDuration, string sQuestion, string sRightAnswer, string sYourAnswer, Image IndicatorIcon)
    {
        switch (Mode)
        {
            case Type.Exercise:
                
                GameObject chapterButtonExercise = Instantiate(_prefabLogController, _groupLogContent);

                _logCounter += 1;
        
                PrefabLogController prefabButtonAnswerControllerExercise = chapterButtonExercise.GetComponent<PrefabLogController>();

                var exerciseDuration = FormatDuration(sDuration).ToString();
                    
                prefabButtonAnswerControllerExercise.Duration = exerciseDuration;
                
                prefabButtonAnswerControllerExercise.DurationText.text = exerciseDuration;
                
                prefabButtonAnswerControllerExercise.Number = _logCounter;
        
                prefabButtonAnswerControllerExercise.Question = sQuestion;
        
                prefabButtonAnswerControllerExercise.RightAnswer = sRightAnswer;
        
                prefabButtonAnswerControllerExercise.YourAnswer = sYourAnswer;
        
                prefabButtonAnswerControllerExercise.NumberText.text = _logCounter.ToString();

                prefabButtonAnswerControllerExercise.QuestionText.text = sQuestion;
        
                prefabButtonAnswerControllerExercise.RightAnswerText.gameObject.SetActive(false);
                
                prefabButtonAnswerControllerExercise.TitleRightAnswerText.gameObject.SetActive(false);

                prefabButtonAnswerControllerExercise.YourAnswerText.text = sYourAnswer;

                _logIndicatorIcon = prefabButtonAnswerControllerExercise.IndicatorIcon;
        
                prefabButtonAnswerControllerExercise.IndicatorIcon = IndicatorIcon;

                break;
            
            case Type.Exam:
                
                GameObject chapterButtonExam = Instantiate(_prefabLogController, _groupLogContent);

                _logCounter += 1;
        
                PrefabLogController prefabButtonAnswerControllerExam = chapterButtonExam.GetComponent<PrefabLogController>();

                var examDuration = FormatDuration(sDuration).ToString();
                
                prefabButtonAnswerControllerExam.Duration = examDuration;
                
                prefabButtonAnswerControllerExam.DurationText.text = examDuration;
                
                prefabButtonAnswerControllerExam.Number = _logCounter;
        
                prefabButtonAnswerControllerExam.Question = sQuestion;
                
                prefabButtonAnswerControllerExam.QuestionText.text = sQuestion;
                
                prefabButtonAnswerControllerExam.NumberText.text = _logCounter.ToString();

                _logIndicatorIcon = prefabButtonAnswerControllerExam.IndicatorIcon;
        
                prefabButtonAnswerControllerExam.IndicatorIcon = IndicatorIcon;
                
                prefabButtonAnswerControllerExam.TitleRightAnswerText.gameObject.SetActive(false);
        
                prefabButtonAnswerControllerExam.RightAnswerText.gameObject.SetActive(false);
                
                prefabButtonAnswerControllerExam.TitleYourAnswerText.gameObject.SetActive(false);
                
                prefabButtonAnswerControllerExam.YourAnswerText.gameObject.SetActive(false);

                break;
            
            default:
                Mode = Type.None;
                break;
        }
        

    }

    public void GenerateFinalLog(float sDuration,string sQuestion, string sRightAnswer, string sYourAnswer, Image IndicatorIcon)
    {
        GameObject chapterButtonExercise = Instantiate(_prefabScoreLogController, _groupFinalLogContent);

        _finalLogCounter += 1;
        
        PrefabLogController prefabButtonAnswerControllerFinal = chapterButtonExercise.GetComponent<PrefabLogController>();

        var finalDuration = FormatDuration(sDuration).ToString();
                
        prefabButtonAnswerControllerFinal.Duration = finalDuration;
                
        prefabButtonAnswerControllerFinal.DurationText.text = finalDuration;
        
        prefabButtonAnswerControllerFinal.Number = _finalLogCounter;
        
        prefabButtonAnswerControllerFinal.Question = sQuestion;
        
        prefabButtonAnswerControllerFinal.RightAnswer = sRightAnswer;
        
        prefabButtonAnswerControllerFinal.YourAnswer = sYourAnswer;
        
        prefabButtonAnswerControllerFinal.NumberText.text = _finalLogCounter.ToString();

        prefabButtonAnswerControllerFinal.QuestionText.text = sQuestion;
        
        prefabButtonAnswerControllerFinal.RightAnswerText.text = sRightAnswer;
        
        prefabButtonAnswerControllerFinal.YourAnswerText.text = sYourAnswer;

        _finalLogIndicatorIcon = prefabButtonAnswerControllerFinal.IndicatorIcon;
        
        prefabButtonAnswerControllerFinal.IndicatorIcon = IndicatorIcon;
    }

    public Image SubmitFinalLogIndicatorIcon() => _finalLogIndicatorIcon;
    
    public Image SubmitLogIndicatorIcon() => _logIndicatorIcon;

    public void SubmitBoolAnswer(bool status)
    {
        _boolAnswer = status;
    }

    public void Reset()
    {
        _logCounter = 0;

        _finalLogCounter = 0;
    }
}
