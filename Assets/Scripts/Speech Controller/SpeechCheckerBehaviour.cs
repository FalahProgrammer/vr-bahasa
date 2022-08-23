using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeechCheckerBehaviour : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField] private bool debugMode;
    
    //public List<double> nilai = new List<double>();
    [SerializeField] private ScenarioQuizController _scenarioQuizController;
    [SerializeField] private SentenceChecker _sentenceChecker;
    
    [Space(10)]
    [SerializeField] private DataVariable _dataVariable;
    [SerializeField] private RepositoryPassingGrade _repositoryPassingGrade;
    [SerializeField] private string requiredAnswer;
    
    private List<string> _escapedChars = new List<string>(){",",".","!","?","'"};
    private StringSimiliarity _stringSimiliarity;

    [Space(10)] 
    public UnityEvent OnAnswerChecked;
    public double score;

    public string RequiredAnswer
    {
        get { return requiredAnswer; }
        set { requiredAnswer = value; }
    }

    private void Start()
    {
        //nilai.Clear();
        _stringSimiliarity = new StringSimiliarity();
    }
    
    public void BeginCalculateStrinSimiliarity(string answer, Action<double> callback)
    {
        CalculateString(requiredAnswer, answer);

        callback(score);
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
    
    public void GetDataQuiz(bool boolStatus, string answer)
    {
        var logQuestion =_scenarioQuizController.GetCurrentQuestion();

        var rightAnswer = _scenarioQuizController.GetCurrentAnswer();

        var resultText = _sentenceChecker.SubmitResultText();

        var score = _stringSimiliarity.Similarity(EscapedCharacter(requiredAnswer), answer)._score;
    }

    private void CalculateString(string rightAnswer, string micAnswer)
    {
        //Debug.Log(EscapedCharacter(rightAnswer) + "\n" + micAnswer);
        
        if (debugMode) Debug.Log(_stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score);
        
        int limit_score = 75;
        
        for (int i = 0; i < _repositoryPassingGrade.Items.Count; i++)
        {
            if (_repositoryPassingGrade.Items[i].content_id==_dataVariable.area_id)
            {
                if (debugMode) Debug.Log(_repositoryPassingGrade.Items[i].pass_grade);

                limit_score = _repositoryPassingGrade.Items[i].pass_grade; 
            }
        }
        
        score = _stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score;
        
        /*if (score < limit_score)
        {
            if (debugMode) Debug.Log("Kamu salah semua, ulangi !!");
            
            //Debug.Log("Nilai Persamaannya : " + _stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score);
            //nilai.Add(_stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score);
            
            _sentenceChecker.GetResult(EscapedCharacter(rightAnswer), micAnswer);

            GetDataQuiz(false, micAnswer);
            
            /*if (OnInCorrect!=null)
                OnInCorrect.Invoke();#1#
        }
        else
        {
            //Debug.Log("Nilai Persamaannya : " + _stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score);  
            //nilai.Add(_stringSimiliarity.Similarity(EscapedCharacter(rightAnswer), micAnswer)._score);

            _sentenceChecker.GetResult(EscapedCharacter(rightAnswer), micAnswer);

            GetDataQuiz(true, micAnswer);
            
            /*if (OnCorrect!=null)
                OnCorrect.Invoke();#1#
        }*/
    }
}
