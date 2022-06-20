using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private Text _loadingText;
    
    public void BeginMoveScene(int _sceneNumber)
    {
        StartCoroutine(LoadScene(_sceneNumber));
    }
    IEnumerator LoadScene(int scenenumber)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenenumber, LoadSceneMode.Single);
        
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            float textprogress = progress * 100f - 10;

            if (textprogress > 0)
            {
                _loadingText.text = textprogress.ToString("0") + "%";
            }
            
            //Debug.Log(progress);
            
            yield return null;
        }
    }
}
