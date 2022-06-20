using UnityEngine;
using UnityEngine.UI;

public class LoadingExecution : MonoBehaviour
{
    public ScriptableTransform _scriptableTransform;

    private void Start()
    {
        _scriptableTransform = Resources.Load<ScriptableTransform>("ScriptableObjects/Transform/ScriptableTransform");
    }

    public void ExecuteButton()
    {
        if (_scriptableTransform.MyTransform)
        {
            if (_scriptableTransform.MyTransform.GetComponent<PointerHandlerBehaviour>())
            {
                _scriptableTransform.MyTransform.GetComponent<ButtonController>().OnClick();
                
                
                if (_scriptableTransform.MyTransform.GetComponent<Button>())
                {
                    _scriptableTransform.MyTransform.GetComponent<Button>().onClick.Invoke();
                }
                
                else if (_scriptableTransform.MyTransform.GetComponent<Toggle>())
                {
                    _scriptableTransform.MyTransform.GetComponent<Toggle>().onValueChanged.Invoke(false);
                }
            }
        }
    }
}
