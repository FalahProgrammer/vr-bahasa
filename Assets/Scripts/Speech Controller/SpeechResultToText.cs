using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechResultToText : MonoBehaviour
{
    public VoskSpeechToText VoskSpeechToText;
    public TextMeshProUGUI ResultText;

    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
    }

    private void OnTranscriptionResult(string obj)
    {
        var result = new RecognitionResult(obj);
        
        //var confidance = 0f;
        var index = 0;
        
        /*for (int i = 0; i < result.Phrases.Length; i++)
        {
            if (confidance < result.Phrases[i].Confidence)
            {
                confidance = result.Phrases[i].Confidence;
                index = i;
            }
        }*/
        
        ResultText.text = result.Phrases[index].Text;
    }
}
