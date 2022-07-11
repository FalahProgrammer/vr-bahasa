using System.Collections;
using UnityEngine;
using TMPro;

public class NewTyperText : MonoBehaviour
{
    [SerializeField] [Range(0,1)] private float _speed = 0.025f;
    
    [Space(10)]
    [SerializeField] private TextMeshProUGUI tmpText;

    private void Awake()
    {
        tmpText.text = "";
        if (textAnimation != null)
        {
            StopCoroutine(textAnimation);
        }
    }

    public void PlayText()
    {
        string text = tmpText.text;
        tmpText.maxVisibleCharacters = 0;

        if (textAnimation == null)
        {
            textAnimation = TextAnimation();
        }
        else
        {
            StopCoroutine(textAnimation);
            textAnimation = TextAnimation();
        }

        StartCoroutine(textAnimation);
    }

    private void OnDisable()
    {
        if (textAnimation != null)
        {
            StopCoroutine(textAnimation);
        }
    }

    private IEnumerator textAnimation;
    
    IEnumerator TextAnimation()
    {
        for (int i = 0; i < tmpText.text.Length; i++)
        {
            tmpText.maxVisibleCharacters = i + 1;
                
            yield return new WaitForSeconds(_speed);
        }
    }
}
