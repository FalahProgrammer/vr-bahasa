using UnityEngine;

public class SpeechController : MonoBehaviour
{
    [SerializeField] private DataVariable _dataVariable;
    [SerializeField] private VoskSpeechToText _voskSpeechToText;
    [SerializeField] private RepositoryLanguageURL _repositoryLanguageUrl;

    private void Start()
    {
        _voskSpeechToText.ModelPath = "SpeechRecognitionSystem\\model\\" + _repositoryLanguageUrl.LanguageURLList[int.Parse(_dataVariable.materi_id) - 1];
        
        _voskSpeechToText.StartVoskStt();
    }
}
