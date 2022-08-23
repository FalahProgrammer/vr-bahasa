using System;
using System.Collections;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private DataVariable _dataVariable;
    [SerializeField] private VoskSpeechToText _voskSpeechToText;
    [SerializeField] private RepositoryLanguageURL _repositoryLanguageUrl;

    [SerializeField] private DataSceneManagement _dataSceneManagement;

    private void OnEnable()
    {
        Debug.Log("Initializing Vosk Speech Recogniztion");
        
        _voskSpeechToText.ModelPath = "SpeechRecognitionSystem\\model\\" + _repositoryLanguageUrl.LanguageURLList[int.Parse(_dataVariable.materi_id) - 1];

        StartCoroutine(InitiateVosk());
    }

    IEnumerator InitiateVosk()
    {
        yield return new WaitUntil(() => !_dataSceneManagement.voskIsActive);
        
         _voskSpeechToText.StartVoskStt();
        // _voskSpeechToText.StartCoroutine(_voskSpeechToText.StartVosk());
    }

    /*private void OnDisable()
    {
        if (_dataSceneManagement.voskIsActive)
        {
            _voskSpeechToText.UnloadVosk();
        }
    }*/
}
