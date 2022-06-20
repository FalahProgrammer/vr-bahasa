using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class LoadAsyncLanguageAssetBundle : MonoBehaviour
{
    [SerializeField] private DataVariable _dataVariable;

    [SerializeField] private RepositoryLanguageURL _repositoryLanguageUrl;
    
    [SerializeField] private TextMeshProUGUI _resultText;
    
    [SerializeField] private SpeechMicController _speechMicController;
    
/*    [SerializeField] private bool m_Spawning;
    public AssetBundleCreateRequest thebundlereequest;*/
    [SerializeField] private AssetBundle assetBundle;
    
    [SerializeField] private Text _progressText;

    public UnityEvent OnFinishLoadAsset;
    
    private SpeechRecognizer _speechRecognizer;

    private LoadAssetBundleAsync _loadAssetBundleAsync;

    private void Awake()
    {
        _loadAssetBundleAsync = new LoadAssetBundleAsync(assetBundle,_repositoryLanguageUrl.LanguageURLList, _progressText);
        
        _dataVariable = Resources.Load<DataVariable>("ScriptableObjects/Variable/String Variable");
        
        _repositoryLanguageUrl = Resources.Load<RepositoryLanguageURL>("ScriptableObjects/Repository/Repository Language URL");
    }

    private void Start()
    {
        Change();
    }

    public void Change()
    {
        Destroy(_speechMicController.GameObjectLanguage);

        StartCoroutine(_loadAssetBundleAsync.LoadBundleAsync(int.Parse(_dataVariable.materi_id) - 1, OnFinishedLoadAsset));
    }
    

    private bool Attach()
    {
        _speechRecognizer.ResultReceived.AddListener((x) => { _resultText.text = x;});
        
        return true;
    }
    
    private void OnFinishedLoadAsset(GameObject obj)
    {
        _speechMicController.GameObjectLanguage = Instantiate(obj);
        
        _speechRecognizer = _speechMicController.GameObjectLanguage.GetComponentInChildren<SpeechRecognizer>();
        
        _speechMicController.SetAudioRecorder(_speechMicController.GameObjectLanguage.GetComponentInChildren<AudioRecorder>());
            
        //_micCtrl.MuteMic();
        
        if(_speechRecognizer==null)
            Debug.LogWarning("Can't find SpeechRecognizer script");
        else
        {
            Attach();
            
            OnFinishLoadAsset.Invoke();
        }

        //assetBundle.Unload(true);
        
    }

    private void OnDestroy()
    {
        _loadAssetBundleAsync.UnloadAssetBundle();

        if (_speechMicController.GameObjectLanguage != null)
        {
            Destroy(_speechMicController.GameObjectLanguage);
            
            Debug.Log("Object Has Been Destroyed");
        }
        
    }

    /*IEnumerator LanguageBundle(int index)
    {
        thebundlereequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "english_large"));
        yield return thebundlereequest;

        TheBundle = thebundlereequest.assetBundle;
        if (TheBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }

        var assetLoadRequest = TheBundle.LoadAssetAsync<GameObject>(_listURL[index].gameObject.name);

        Debug.Log(assetLoadRequest.progress);
        
        yield return assetLoadRequest;
        
        
        Debug.Log("LOADING SELESAI");
        GameObject prefab = assetLoadRequest.asset as GameObject;
        
        _micCtrl.GameObjectLanguage = Instantiate(_listURL[index].gameObject);
        
        _srs = prefab.GetComponentInChildren<SpeechRecognizer>();
        
        _micCtrl.SetAudioRecorder(_micCtrl.GameObjectLanguage.GetComponentInChildren<AudioRecorder>());
            
        if(_srs==null)
            Debug.LogWarning("Can't find SpeechRecognizer script");
        else
        {
            Attach();
            //_micCtrl.MuteMic();
        }

        TheBundle.Unload(false);
    }
    

    IEnumerator Language(int index)
    {
        while (m_Spawning)
        {
            //_micCtrl.GameObjectLanguage = Instantiate(_listURL[index].gameObject);
            
            _srs = _micCtrl.GameObjectLanguage.GetComponentInChildren<SpeechRecognizer>();
            
            _micCtrl.SetAudioRecorder(_micCtrl.GameObjectLanguage.GetComponentInChildren<AudioRecorder>());
            
            if(_srs==null)
                Debug.LogWarning("Can't find SpeechRecognizer script");
            else
            {
                Attach();
                //_micCtrl.MuteMic();
            }

            m_Spawning = false;

        }
        
        yield return null;
    }
    
  
 
    IEnumerator RunTask(int index)
    {
        float waitTime;
        while (m_Spawning)
        {
            string key = _listURL[index].ToString();
            waitTime = UnityEngine.Random.Range(2f, 5f);
            //Addressables.InstantiateAsync("SpeechRecognitionSystem English Variant", _listURL[index].transform.position,
                //Quaternion.identity, transform, true).Completed += Spawn_Completed;
            yield return new WaitForSeconds(waitTime);
        }
    }
    
    /*private void Spawn_Completed(AsyncOperationHandle<GameObject> handle)
    {
        m_Spawning = false;
        
        Debug.Log("COMPLETED SPAWNING");
    }*/
 
   
}
