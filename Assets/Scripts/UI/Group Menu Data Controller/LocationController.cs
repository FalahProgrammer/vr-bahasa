using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LocationController : MonoBehaviour
{
    [SerializeField] private bool _debugMode;
    
    public DataVariable DataVariable;

    [SerializeField] private TimerBehaviour _timerBehaviour;
    
    [SerializeField] private RepositoryLocation repositoryLocation;
    [SerializeField] private RepositoryContentArea _repositoryContentArea;

    [SerializeField] private ScriptableGameObjectDataController _scriptableGameObjectDataController;

    [SerializeField] private Transform _groupButtonLocation;

    [SerializeField] private GameObject _prefabButtonLocation;
    
    public List<DataLocation> repositoryChapter = new List<DataLocation>();
    
    public UnityEvent OnClickChapter;
    
    private void Awake()
    {
        DataVariable = Resources.Load<DataVariable>("ScriptableObjects/Variable/String Variable");
        
        repositoryLocation = Resources.Load<RepositoryLocation>("ScriptableObjects/Repository/Repository Location");
        
        _scriptableGameObjectDataController = Resources.Load<ScriptableGameObjectDataController>("ScriptableObjects/Game Object/Scriptable GameObject Data Controller");
        
        //Invoke("GetLocation", 2f);
    }

    private void Start()
    {
        GetLocation();
    }

    public void Init()
    {
        foreach (Transform child in _groupButtonLocation)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void GetLocation()
    {
        Init();
        
        repositoryChapter.Clear();
        
        for (int i = 0; i < repositoryLocation.Items.Count; i++)
        {
            if (repositoryLocation.Items[i].materi_id.Equals(DataVariable.materi_id) && repositoryLocation.Items[i].menu_id.Equals(DataVariable.menu_id))
            {
                repositoryChapter.Add(repositoryLocation.Items[i]);
            }
        }
        for (int i = 0; i < repositoryChapter.Count; i++)
        {
            //GenerateButtonChapter(_repositoryItems.ListChapter[i].no, _repositoryItems.ListChapter[i].judul);

            for (int v = 0; v < _repositoryContentArea.Items.Count; v++)
            {
                if (_repositoryContentArea.Items[v].chapter_id != repositoryChapter[i].id) continue;
                if (_repositoryContentArea.Items[v].AreaPrefab == null) continue;
                
                Debug.Log("Location: " + repositoryChapter[i].judul);
                GenerateButtonLocation(repositoryChapter[i].id,repositoryChapter[i].materi_id, repositoryChapter[i].judul);
                break;
            }

            //GenerateButtonLocation(repositoryChapter[i].id,repositoryChapter[i].materi_id, repositoryChapter[i].judul);
        }
    }
    
    public void GenerateButtonLocation( string sID,string sMateriID, string sLocationName)
    {
        //InitThis();
        if (!sLocationName.Equals("Bibliography"))
        {
            _scriptableGameObjectDataController.ChapterButton = Instantiate(_prefabButtonLocation, _groupButtonLocation);
            PrefabButtonDataController prefabButtonDataController = _scriptableGameObjectDataController.ChapterButton.GetComponentInChildren<PrefabButtonDataController>();

            prefabButtonDataController.ID = sID;
            
            prefabButtonDataController.MateriId = sMateriID;

            prefabButtonDataController.LocationtAreaName = sLocationName;
            
            prefabButtonDataController.TextButtonName.text = sLocationName;

            prefabButtonDataController.GetComponent<PointerHandlerBehaviour>().OnPointerEnterEvent.AddListener(_timerBehaviour.StartCounting);
            
            prefabButtonDataController.GetComponent<PointerHandlerBehaviour>().OnPointerExitEvent.AddListener(_timerBehaviour.Reset);

            /*ButtonChapter.ContentButton.transform.localScale = new Vector3(1, 1, 1);
            ButtonChapter.ContentButton.transform.localPosition = new Vector3(ButtonChapter.ContentButton.transform.localPosition.x, ButtonChapter.ContentButton.transform.localPosition.y, 0);
            ButtonChapter.ContentButton.transform.localRotation = new Quaternion(0, 0, 0, 0);*/
            
            _scriptableGameObjectDataController.ChapterButton.GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                if (_debugMode) Debug.Log("Repository Chapter ID: " + sID);
                
                DataVariable.chapter_id = sID;
                
                OnClickChapter.Invoke();
                
                //OnClickChapter.AddListener(()=>DataVariable.chapter_id = sChapterID);

            });
        }
    }
}
