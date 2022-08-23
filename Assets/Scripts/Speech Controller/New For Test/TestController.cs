using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    [SerializeField] private VoskSpeechToText _voskSpeechToText;
    
    [SerializeField] private Button _btnLoadEnglish;
    [SerializeField] private Button _btnLoadGerman;
    [SerializeField] private Button _btnUnload;

    private int languageIndex = 0;

    private bool _dataIsLoaded;

    public int LanguageIndex
    {
        get => languageIndex;
        set => languageIndex = value;
    }

    private void Awake()
    {
        CheckButton();
    }

    public void Load()
    {
        switch (languageIndex)
        {
            case 0:
                _voskSpeechToText.ModelPath = "SpeechRecognitionSystem\\model\\english_large";
                Debug.Log("Language English");
                break;
            case 1:
                _voskSpeechToText.ModelPath = "SpeechRecognitionSystem\\model\\german_large";
                Debug.Log("Language German");
                break;
        }
        
        _dataIsLoaded = true;
        CheckButton();
        
        _voskSpeechToText.StartVoskStt();
    }

    public void Unload()
    {
        _voskSpeechToText.UnloadVosk();

        _dataIsLoaded = false;

        //languageIndex = -1;
        CheckButton();
    }

    void CheckButton()
    {
        switch (_dataIsLoaded)
        {
            case true:
                switch (languageIndex)
                {
                    case 0:
                        _btnLoadEnglish.interactable = false;
                        _btnLoadGerman.interactable = false;
                        break;
                    case 1:
                        _btnLoadEnglish.interactable = false;
                        _btnLoadGerman.interactable = false;
                        break;
                }
                _btnUnload.interactable = true;
                break;
            case false:
                _btnLoadEnglish.interactable = true;
                _btnLoadGerman.interactable = true;
                
                _btnUnload.interactable = false;
                break;
        }
    }
}
