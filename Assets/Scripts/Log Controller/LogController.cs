using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController
{
    private readonly GameObject _prefabLogController;
    
    private string _question;
    
    private string _rightAnswer;
    
    private string _fixAnswer;
    
    private Transform _groupLogContent;

    public LogController(GameObject prefabLogController, string question, string rightAnswer, string fixAnswer, Transform groupLogContent)
    {

        _prefabLogController = prefabLogController;
        
        _question = question;
        
        _rightAnswer = rightAnswer;
        
        _fixAnswer = fixAnswer;
        
        _groupLogContent = groupLogContent;
    }

    public void SetLog(string logQuestion, string logRightAnswer, string logFixAnswer)
    {
        GenerateLog(logQuestion, logRightAnswer,logFixAnswer);
    }
    
    public void GenerateLog(string sQuestion, string sRightAnswer, string sFixAnswer)
    {

        GameObject chapterButton = GameObject.Instantiate(_prefabLogController, _groupLogContent);
        
        PrefabLogController prefabButtonAnswerController = chapterButton.GetComponent<PrefabLogController>();

        prefabButtonAnswerController.Question = sQuestion;
        
        prefabButtonAnswerController.RightAnswer = sRightAnswer;
        
        prefabButtonAnswerController.YourAnswer = sFixAnswer;

        prefabButtonAnswerController.QuestionText.text = sQuestion;
        
        prefabButtonAnswerController.RightAnswerText.text = sRightAnswer;
        
        prefabButtonAnswerController.YourAnswerText.text = sFixAnswer;

    }
}
