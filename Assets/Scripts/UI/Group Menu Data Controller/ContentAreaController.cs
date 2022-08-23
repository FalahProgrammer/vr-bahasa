using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContentAreaController : MonoBehaviour
{
    public DataVariable DataVariable;

    [SerializeField] private IntegerVariable _integerVariable;
    
    [SerializeField] private LocationController _locationController;
    
    [SerializeField] private RepositoryLocation repositoryLocation;
    [SerializeField] private RepositoryContentArea repositoryContentArea;
    
    [SerializeField] private ScriptableGameObjectDataController _scriptableGameObjectDataController;
    
    [SerializeField] private ScriptableTransform _scriptableTransform;
    
    [SerializeField] private TimerBehaviour _timerBehaviour;
    
    [SerializeField] private TimerBehaviour _timerButton;

    [SerializeField] private SceneLoader _sceneLoader;
    
    [SerializeField] private Text _dateTime;
    
    [SerializeField] private Text _durationText;
    
    [SerializeField] private Text _finalDurationText;
    
    //[SerializeField] private Text[] _scenarioNameText;
    
    [SerializeField] private GameObject _prefabButtonContentArea;
    
    [SerializeField] private Transform _groupButtonContentArea;

    public int CurrentScenarioNumber;
    
    public List<DataContentArea> ListContent = new List<DataContentArea>();
    public Dictionary<int, int> DictContent = new Dictionary<int, int>();
    
    public UnityEvent OnClickContent;

    DateTime thisTime = DateTime.Now;
    
    private void Awake()
    {
        DataVariable = Resources.Load<DataVariable>("ScriptableObjects/Variable/String Variable");
        
        repositoryContentArea = Resources.Load<RepositoryContentArea>("ScriptableObjects/Repository/Repository Content Area");
        
        _scriptableTransform = Resources.Load<ScriptableTransform>("ScriptableObjects/Transform/Prefab Area");
        
        _scriptableGameObjectDataController = Resources.Load<ScriptableGameObjectDataController>("ScriptableObjects/Game Object/Scriptable GameObject Data Controller");
    }

    /*private void Start()
    {
        GenerateAreaPrefab();
    }*/

    public void Init()
    {
        foreach(Transform child in _groupButtonContentArea)
        {
            Destroy(child.gameObject);
        }
    }
    
    public void GetContentArea()
    {
        Init();
        
        ListContent.Clear();
        DictContent.Clear();
        
        for (int i = 0; i < repositoryContentArea.Items.Count; i++)
        {
            if (repositoryContentArea.Items[i].language_id.ToString() == DataVariable.materi_id && repositoryContentArea.Items[i].location_id.ToString() == DataVariable.chapter_id)
            //if (repositoryContentArea.Items[i].location_id.Equals(DataVariable.chapter_id) && repositoryContentArea.Items[i].language_id.Equals(DataVariable.materi_id))
            {
                ListContent.Add(repositoryContentArea.Items[i]);
                DictContent.Add(repositoryContentArea.Items[i].id, i);
            }
        }

        
        for (int i = 0; i < ListContent.Count; i++)
        {
            /*List<string> tempChecker = new List<string>();
            for (int p = 0; p < ListContent[i].npc.Length; p++)
            {
                if (tempChecker.Contains(ListContent[i].AreaPrefab.name))
                {
                    
                }

                
            }*/
            
            GenerateButtonContentArea(ListContent[i].npc[_integerVariable.IntegerValue - 1].duration * 60,ListContent[i].id/*,ListContent[i].scenario_number*/, ListContent[i].npc[0].conversation_topic, ListContent[i].area_name,ListContent[i].location_id.ToString(), ListContent[i].AreaPrefab, DictContent[ListContent[i].id]/*, _locationController.repositoryChapter[i].judul*/);

            //GenerateButtonContentArea(ListContent[i].duration * 60,ListContent[i].id/*,ListContent[i].scenario_number*/,
                 // ListContent[i].npc[_integerVariable.IntegerValue - 1].conversation_topic, ListContent[i].area_name,ListContent[i].chapter_id, ListContent[i].AreaPrefab/*, _locationController.repositoryChapter[i].judul*/);
            
            Debug.Log("Content Area: " + ListContent[i].area_name);
        }
    }
    
    public int GetCurrentScenarioNumber() => CurrentScenarioNumber;
	public void GenerateButtonContentArea(int sDuration, int sID/*, int sScenarioNumber*/, string sConversationTopic, string sContentAreaName, 
        string sChapterID, GameObject sAreaPrefab, int index/*, string sSceneName*/)
    {
        if (!sConversationTopic.Equals("Bibliography"))
        {
            if (sAreaPrefab)
            {
                //InitThis();
            _scriptableGameObjectDataController.ContentButton = Instantiate(_prefabButtonContentArea, _groupButtonContentArea, true);
            
            PrefabButtonDataController prefabButtonDataController = _scriptableGameObjectDataController.ContentButton.GetComponentInChildren<PrefabButtonDataController>();

            /*prefabButtonDataController.UrlVideo = sUrlVideo;
            
            prefabButtonDataController.Description = sDescription;
            
            prefabButtonDataController.UrlImage = sUrlImage;*/
            
            prefabButtonDataController.ID = sID.ToString();

            //prefabButtonDataController.SceneName = sSceneName;

            prefabButtonDataController.Duration = sDuration;
            
            //prefabButtonDataController.ScenarioNumber = sScenarioNumber;
            
            //prefabButtonChapterCs.textChapterName.text = sChapterName;
            
            prefabButtonDataController.ContentAreaName = sContentAreaName;

            prefabButtonDataController.ChapterId = sChapterID;
            
            prefabButtonDataController.AreaPrefab = sAreaPrefab;

            prefabButtonDataController.TextButtonName.text = sContentAreaName;

            /*PointerHandlerBehaviour pointerHandlerBehaviour = prefabButtonDataController.GetComponent<PointerHandlerBehaviour>();
            pointerHandlerBehaviour.OnPointerEnterEvent.AddListener(_timerButton.StartCounting);
            pointerHandlerBehaviour.OnPointerExitEvent.AddListener(_timerButton.Reset);*/

            _scriptableGameObjectDataController.ContentButton.transform.localScale = new Vector3(1, 1, 1);
            
            var localPosition = _scriptableGameObjectDataController.ContentButton.transform.localPosition;
            
            localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            
            _scriptableGameObjectDataController.ContentButton.transform.localPosition = localPosition;
            
            _scriptableGameObjectDataController.ContentButton.transform.localRotation = new Quaternion(0 , 0 , 0 , 0);
            
            sConversationTopic.Trim();

            Button btn = _scriptableGameObjectDataController.ContentButton.GetComponentInChildren<Button>();
            btn.onClick.AddListener(delegate
            {
                btn.interactable = false;
                //CurrentScenarioNumber = sScenarioNumber;
                
                _timerBehaviour._currentDuration = sDuration;
                
                _timerBehaviour._initialDuration = sDuration;

                /*for (int i = 0; i < _scenarioNameText.Length; i++)
                {
                    _scenarioNameText[i].text = sConversationTopic;
                }*/
                
                
                _durationText.text = sDuration / 60 + " " + "minutes";
                
                _finalDurationText.text = sDuration / 60 + " " + "minutes";

                _dateTime.text = thisTime.ToString("f");

                DataVariable.area_id = sID;

                if (_scriptableTransform != null)
                {
                    _scriptableTransform.MyTransform = null;
                    
                    _scriptableTransform.MyTransform = sAreaPrefab.transform;
                }

                DataVariable.contentAreaIndex = index;

                OnClickContent.Invoke();
                
            });
            }
            

        }
    }

    


}
