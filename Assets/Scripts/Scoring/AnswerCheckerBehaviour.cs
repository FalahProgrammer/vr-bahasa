using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnswerCheckerBehaviour : MonoBehaviour, iResetable
{
    
    //public List<Text> InputOptions = new List<Text>();

    public string Input1;
    
    public TextMeshProUGUI Input2;

    private StringSimiliarity _stringSimiliarity;

    public ScenarioQuizController _scenarioQuizController;
    
    [SerializeField] private DataVariable _dataVariable;
    
    [SerializeField] private IntegerVariable _integerVariable;
    
    [SerializeField] private RepositoryContentArea _repositoryContentArea;
    
    //[SerializeField] private RepositoryPassingGrade _repositoryPassingGrade;
    
    //private int _count;
    
    private List<string> _escapedChars = new List<string>(){",",".","!","?","'"};
    
    private LogControllerBehaviour _logControllerBehaviour;
    
    [SerializeField] private UnityEvent OnCorrect;
    
    [SerializeField] private UnityEvent OnInCorrect;

    private SentenceChecker _sentenceChecker;
    
    int limit_score = 75;

    private void Start()
    {
        _stringSimiliarity = new StringSimiliarity();

        _sentenceChecker = FindObjectOfType<SentenceChecker>();

        _logControllerBehaviour = FindObjectOfType<LogControllerBehaviour>();

        _scenarioQuizController = FindObjectOfType<ScenarioQuizController>();

        //Input2.text = "ulala badai";

        //BeginCalculateStrinSimiliarity();
    }

    public void BeginCalculateStrinSimiliarity()
    {
        //Input1 = "saya suka kamu";
        //Input2.text = "saya suka anda";
        CalculateString(Input1,Input2.text);
        
        
        //kedepannya, nanti setelah calculate similiarity ini, cek lg calculate alternative untuk opsi 2
    }
    
    public string EscapedCharacter(string rightAnswer)
    {
        var mods = rightAnswer;
        foreach (var esChar in _escapedChars)
        {
            mods = mods.Replace(esChar, string.Empty).ToLower();
        }

        return mods;
    }
    
    public void GetDataQuiz(bool boolStatus)
    {
        var answer_Id =_scenarioQuizController.GetCurrentAnswerID();
        
        var question_Id =_scenarioQuizController.GetCurrentQuestionID();
        
        var logQuestion =_scenarioQuizController.GetCurrentQuestion();

        var rightAnswer = _scenarioQuizController.GetCurrentAnswer();

        var resultText = _sentenceChecker.SubmitResultText();

        var finalLogIndicatorIcon = _logControllerBehaviour.SubmitFinalLogIndicatorIcon();
        
        var logIndicatorIcon = _logControllerBehaviour.SubmitLogIndicatorIcon();
        
        var score = _stringSimiliarity.Similarity(EscapedCharacter(Input1), Input2.text)._score;

        _logControllerBehaviour.SubmitBoolAnswer(boolStatus);
        
        _logControllerBehaviour.SetLog(question_Id,answer_Id, logQuestion,rightAnswer,resultText, finalLogIndicatorIcon, logIndicatorIcon,score, _logControllerBehaviour._timerBehaviour._currentDuration,boolStatus);
    }

    private void CalculateString(String rightAnswer, String micAnswer)
    {
        //Debug.Log(EscapedCharacter(rightAnswer) + "\n" + micAnswer);
        
        Debug.Log(_stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score);
        
        for (int i = 0; i < _repositoryContentArea.Items.Count; i++)
        {
            if (_dataVariable.contentAreaIndex == i)
            {
                limit_score = _repositoryContentArea.Items[i]
                    .npc[_integerVariable.IntegerValue - 1].passing_grade;
            }
        }
        
        if (_stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score < limit_score)
        {
            Debug.Log("Kamu salah semua, ulangi !!");
            
            Debug.Log("Nilai Persamaannya : " + _stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score);
            
            _sentenceChecker.GetResult(EscapedCharacter(rightAnswer), micAnswer);

            GetDataQuiz(false);

            _logControllerBehaviour.CalculateCorrectInCorrectAnswer(score: _stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score,limit_score: limit_score);
            
            var finalLogindicatorIcon = _logControllerBehaviour.SubmitFinalLogIndicatorIcon();
            
            var logindicatorIcon = _logControllerBehaviour.SubmitLogIndicatorIcon();
            
            logindicatorIcon.GetComponent<Image>().color = Color.red;
                
            finalLogindicatorIcon.GetComponent<Image>().color = Color.red;
            
            if (OnInCorrect!=null)
                OnInCorrect.Invoke();
        }
        else
        {
            Debug.Log("Nilai Persamaannya : " + _stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score);    
            
            _sentenceChecker.GetResult(EscapedCharacter(rightAnswer), micAnswer);

            GetDataQuiz(true);
            
            _logControllerBehaviour.CalculateCorrectInCorrectAnswer(score: _stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score,limit_score: limit_score);
            
            var indicatorIcon = _logControllerBehaviour.SubmitFinalLogIndicatorIcon();
            
            var logindicatorIcon = _logControllerBehaviour.SubmitLogIndicatorIcon();
            
            logindicatorIcon.GetComponent<Image>().color = Color.green;
            
            indicatorIcon.GetComponent<Image>().color = Color.green;
            
            if (OnCorrect!=null)
                OnCorrect.Invoke();
        }
    }

    public void Reset()
    {
        Input2.text = "";
    }
}
