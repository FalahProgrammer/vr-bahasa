using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TyperTextBehaviour : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float _speed; 
    
    [SerializeField] private Text _text;
    
    string _textToType;

    void Awake () 
    {
        //_text = GetComponent<Text>();
        
        /*_textToType = _text.text;
        
        _text.text = "";*/

        //StartCoroutine ("PlayTextCoroutine");
    }

    public void BeginPlayText()
    {
        _text.gameObject.SetActive(true);
        
        _textToType = _text.text;
        
        _text.text = "";
        
        StartCoroutine ("PlayTextCoroutine");
    }

    IEnumerator PlayTextCoroutine()
    {
        yield return new WaitForSeconds(3);
        foreach (char c in _textToType) 
        {
            _text.text += c;
            
            yield return new WaitForSeconds (_speed);
            
            
        }
    }

}
