using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class VoskResultText : MonoBehaviour 
{
    [SerializeField] private VoskSpeechToText VoskSpeechToText;
    [SerializeField] private SpeechCheckerBehaviour speechCheckerBehaviour;
    
    public TextMeshProUGUI ResultText;
    public RecognizedPhrase[] Result;
    
    public List<double> nilai = new List<double>();

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += CheckVoiceAlternatives;
    }

    /*private void OnTranscriptionResult(string obj)
    {
        ResultText.text = "";
        Debug.Log(obj);
        var result = new RecognitionResult(obj);
        
        ResultText.text = result.Phrases[0].Text;
        
        /*for (int i = 0; i < result.Phrases.Length; i++)
        {
            if (i > 0)
            {
                ResultText.text += ", ";
            }

            ResultText.text = result.Phrases[i].Text;
        }#1#
    	//ResultText.text += "\n";
    }*/
    
    private void OnTranscriptionResult(string obj)
    {
        ResultText.text = "";
        Debug.Log(obj);
        var result = new RecognitionResult(obj);
        for (int i = 0; i < result.Phrases.Length; i++)
        {
            if (i > 0)
            {
                ResultText.text += ", ";
            }

            ResultText.text = result.Phrases[i].Text;
        }
        //ResultText.text += "\n";
    }
    
    private void CheckVoiceAlternatives (string obj)
    {
        Debug.Log("CheckVoiceAlternatives");
        
        // clear first
        ResultText.text = "";
        nilai.Clear();

        // get result
        var result = new RecognitionResult(obj);
        Result = result.Phrases;
        
        for (int i = 0; i < Result.Length; i++)
        {
            speechCheckerBehaviour.BeginCalculateStrinSimiliarity(Result[i].Text, AnswerCheckCallback);

            if ((int)nilai[i] == 100)
            {
                Debug.Log("<color=#00FFFF>Found perfect answer!</color>");
                break;
            }
        }

        double bestAnswer = 0;
        int index = 0;

        for (int i = 0; i < nilai.Count; i++)
        {
            if (nilai[i] > bestAnswer)
            {
                bestAnswer = nilai[i];
                index = i;
            }
        }

        // set to UI
        ResultText.text = result.Phrases[index].Text;
        
        Debug.Log("<color=#00FFFF>Best Answer Index: " + index + "</color>");
    }

    public void AnswerCheckCallback(double value)
    {
        nilai.Add(value);
    }
}
