﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScenarioQuizController : MonoBehaviour
{
    [SerializeField] private  DataVariable DataVariable;

    [SerializeField] private RepositoryQuizQuestion _repositoryQuizQuestion;

    [SerializeField] private AnswerCheckerBehaviour answerCheckerBehaviour;
    //[SerializeField] private ScriptableGameObjectDataController _scriptableGameObjectDataController;

    [SerializeField] private Transform _groupButtonChapter;

    [SerializeField] private GameObject _prefabButtonChapter;
    
    public string CorrectAnswer;

    public string Question;

    [SerializeField] private UnityEvent OnFinishedGetScenarioQuiz;

    
    private void Awake()
    {
        DataVariable = Resources.Load<DataVariable>("ScriptableObjects/Variable/String Variable");

        _repositoryQuizQuestion =
            Resources.Load<RepositoryQuizQuestion>("ScriptableObjects/Repository/Repository Quiz Question");

        //_scriptableGameObjectDataController = Resources.Load<ScriptableGameObjectDataController>("ScriptableObjects/Game Object/Scriptable GameObject Data Controller");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GenerateQuizScenario();
        }
    }

    public void Init()
    {
        foreach (Transform child in _groupButtonChapter)
        {
            Destroy(child.gameObject);
        }
    }

    public void GenerateQuizScenario()
    {
        //Init();

        /*_repositoryQuizQuestion.Items.Clear();

        for (int i = 0; i < _repositoryQuizQuestion.Items.Count; i++)
        {
            /*if (_repositoryQuizQuestion.Items[i].materi_id.Equals(DataVariable.materi_id) &&
                _repositoryQuizQuestion.Items[i].chapter_id.Equals(DataVariable.chapter_id))
            {
                //repositoryChapter.ListChapter.Add(repositoryChapter.Items[i]);
            }#1#
        }*/

        //DataVariable.IncreamentQuestionID(Int32.Parse(DataVariable.qustion_id));
        
        for (int i = 0; i < _repositoryQuizQuestion.Items.Count; i++)
        {
            /*DataVariable.IncreamentQuestionID(Int32.Parse(DataVariable.qustion_id));*/
            
            
            
            if (_repositoryQuizQuestion.Items[i].question_id == DataVariable.qustion_id)
            {

                //Debug.Log(_repositoryQuizQuestion.Items[i].question_id);
                
                GenerateButtonChapter(
                    sMateri_id: _repositoryQuizQuestion.Items[i].materi_id,
                    sChapter_id: _repositoryQuizQuestion.Items[i].chapter_id,
                    _repositoryQuizQuestion.Items[i].question_id,
                    _repositoryQuizQuestion.Items[i].answers[0].text,
                    _repositoryQuizQuestion.Items[i].answers[1].text,
                    _repositoryQuizQuestion.Items[i].questions);
                
                for (int j = 0; j < _repositoryQuizQuestion.Items[i].answers.Count; j++)
                {
                    /*var iconassets = from asset in _repositoryQuizQuestion.Items[i].answers
                        where asset.id == _repositoryQuizQuestion.Items[i].right_answer_id
                        select asset;
                    var iconlist = iconassets.ToList();*/
                    
                    if (_repositoryQuizQuestion.Items[i].right_answer_id == _repositoryQuizQuestion.Items[i].answers[j].id)
                    {
                        //Debug.Log("Question : " + _repositoryQuizQuestion.Items[i].questions + "\n" + "Correct Answer : " + _repositoryQuizQuestion.Items[i].answers[j].text);
                        
                        CorrectAnswer = _repositoryQuizQuestion.Items[i].answers[j].text;

                        Question = _repositoryQuizQuestion.Items[i].questions;
                        
                        answerCheckerBehaviour.Input1 = CorrectAnswer;
                        
                        //answerCheckerBehaviour.GetDataQuiz(_repositoryQuizQuestion.Items[i].questions, CorrectAnswer, null);
                    }
                }
            }
            
        }
    }

    public string GetCurrentQuestion() => Question;

    public string GetCurrentAnswer() => CorrectAnswer;
    
    public void GenerateButtonChapter(string sMateri_id, string sChapter_id, string sQuestion_id, string sAnswerA, string sAnswerB, string sQuestion)
    {
        //InitThis();

        //GameObject chapterButton = Instantiate(_prefabButtonChapter, _groupButtonChapter);
        
        PrefabAnswerController prefabButtonAnswerController = _prefabButtonChapter.GetComponentInChildren<PrefabAnswerController>();

        DataVariable.chapter_id = sChapter_id;

        prefabButtonAnswerController.Materi_Id = sMateri_id;

        prefabButtonAnswerController.Chapter_Id = DataVariable.chapter_id;
        
        prefabButtonAnswerController.Question_Id = sQuestion_id;

        prefabButtonAnswerController.Question.text = sQuestion;
        
        prefabButtonAnswerController.AnswerA.text = sAnswerA;
        
        prefabButtonAnswerController.AnswerB.text = sAnswerB;
        
        //prefabButtonAnswerController.AnswerC.text = sAnswerC;

        /*chapterButton.GetComponentInChildren<Button>().onClick.AddListener(delegate
        {
            
        });*/
        
        //OnFinishedGetScenarioQuiz.Invoke();
    }
}