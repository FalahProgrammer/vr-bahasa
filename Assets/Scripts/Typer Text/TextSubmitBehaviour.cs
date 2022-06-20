using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSubmitBehaviour : MonoBehaviour, iResetable
{
    [SerializeField] private Text _text;
    
    [SerializeField] private string _textToType;
    
    public void SubmitText()
    {
        _text.text = "";
            
        _text.text = _textToType;
    }

    public void Reset()
    {
        _text.text = "";
    }
}
