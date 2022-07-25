using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpeechController : MonoBehaviour
{
    //[SerializeField] private VoskSpeechToText _voskSpeechToText;
    [SerializeField] private RepositoryLanguageURL _repositoryLanguageUrl;

    public void ChangeSpeechLanguage(int index)
    {
        //_voskSpeechToText.ModelPath = "SpeechRecognitionSystem\\model\\" + _repositoryLanguageUrl.LanguageURLList[index];
        
        //_voskSpeechToText.StartVoskStt();
    }

    public void StopVoskSpeech()
    {
        //_voskSpeechToText.StopVosk();
    }
}
