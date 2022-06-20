using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DataController : IGetData
{
    private MonoBehaviour _monoBehaviour;
    
    private DataVariable _dataVariable;
    
    private RepositoryLocation _repositoryLocation;
    
    private RepositoryContentArea _repositoryContentArea;
    
    private ScriptableGameObjectDataController _scriptableGameObjectDataController;
    
    private Transform _prefabButton;
    
    private Transform _buttonParentLocation;

    private Transform _buttonParentContentArea;
    
    private UnityEvent _onClickChapter;

    public DataController(MonoBehaviour monoBehaviour,DataVariable dataVariable, RepositoryLocation repositoryLocation, RepositoryContentArea repositoryContentArea, 
        ScriptableGameObjectDataController scriptableGameObjectDataController, Transform prefabButton,Transform buttonParentLocation, Transform buttonParentContentArea, UnityEvent onClickChapter)
    {
        _monoBehaviour = monoBehaviour;
        
        _dataVariable = dataVariable;
        
        _repositoryLocation = repositoryLocation;
        
        _repositoryContentArea = repositoryContentArea;
        
        _scriptableGameObjectDataController = scriptableGameObjectDataController;
        
        _prefabButton = prefabButton;
        
        _buttonParentLocation = buttonParentLocation;

        _buttonParentContentArea = buttonParentContentArea;
        
        _onClickChapter = onClickChapter;
    }
    
    public void Init()
    {
        foreach(Transform child in _buttonParentContentArea)
        {
            Object.Destroy(child.gameObject);
        }
    }
    
    public void GetData()
    {
        _monoBehaviour.StartCoroutine(CoroutineGetData());
    }

    IEnumerator CoroutineGetData()
    {
        //ListContent.Clear();
        
        Init();
        
        /*for (int i = 0; i < _repositoryContent.Items.Count; i++)
        {
            if (_repositoryContent.Items[i].chapter_id.Equals(_dataVariable.chapter_id) &&
                _repositoryContent.Items[i].materi_id.Equals(_dataVariable.materi_id))
            {
                //ListContent.Add(ListAllArrayContent[i]);
            }
        }*/

        for (int i = 0; i < _repositoryContentArea.Items.Count; i++)
        {
            if (_repositoryContentArea.Items[i].chapter_id.Equals(_dataVariable.chapter_id) &&
                _repositoryContentArea.Items[i].materi_id.Equals(_dataVariable.materi_id))
            {
                /*GenerateContent(
                    _repositoryContentArea.Items[i].content, 
                    _repositoryContentArea.Items[i].conversation_topic,
                    _repositoryContentArea.Items[i].area_name,
                    _repositoryContentArea.Items[i].content_image
                    );*/
            }
        }
        
        for (int i = 0; i < _repositoryLocation.Items.Count; i++)
        {
            if (_repositoryLocation.Items[i].materi_id.Equals(_dataVariable.materi_id) &&
                _repositoryLocation.Items[i].menu_id.Equals(_dataVariable.menu_id))
            {
                //GenerateButtonChapter(_repositoryItems.ListChapter[i].no, _repositoryItems.ListChapter[i].judul);
                GenerateChapter(
                    _repositoryLocation.Items[i].id, 
                    _repositoryLocation.Items[i].judul
                    );
            }
        }
        
        yield return null;
    }


    public void GenerateChapter(string title, string name)
    {
        if (!name.Equals("Bibliography"))
        {
            _scriptableGameObjectDataController.ChapterButton = Object.Instantiate(_prefabButton.gameObject, _buttonParentLocation);
            PrefabButtonDataController prefabButtonDataController = _scriptableGameObjectDataController.ChapterButton.GetComponentInChildren<PrefabButtonDataController>();

            prefabButtonDataController.SelectType = PrefabButtonDataController.Type.Location;
            
            prefabButtonDataController.ContentAreaName = title;
            
            prefabButtonDataController.ButtonName = name;

            prefabButtonDataController.TextButtonName.text = name;

            /*ButtonChapter.ContentButton.transform.localScale = new Vector3(1, 1, 1);
            ButtonChapter.ContentButton.transform.localPosition = new Vector3(ButtonChapter.ContentButton.transform.localPosition.x, ButtonChapter.ContentButton.transform.localPosition.y, 0);
            ButtonChapter.ContentButton.transform.localRotation = new Quaternion(0, 0, 0, 0);*/
            _scriptableGameObjectDataController.ChapterButton.GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                Debug.Log(title);
                
                _dataVariable.chapter_id = title;
                
                _onClickChapter.Invoke();

            });
        }
    }

    public void GenerateContent(string urlVideo, string urlImage, string description, string name)
    {
        if (!description.Equals("Bibliography"))
        {
            _scriptableGameObjectDataController.ContentButton = Object.Instantiate(_prefabButton.gameObject, _buttonParentContentArea, true);
            
            PrefabButtonDataController prefabButtonDataController = _scriptableGameObjectDataController.ContentButton.GetComponentInChildren<PrefabButtonDataController>();

            prefabButtonDataController.SelectType = PrefabButtonDataController.Type.Area;
            
            /*prefabButtonDataController.UrlVideo = urlVideo;
            
            prefabButtonDataController.Description = description;
            
            prefabButtonDataController.ButtonName = name;
            
            prefabButtonDataController.UrlImage = urlImage;*/

            prefabButtonDataController.TextButtonName.text = name;

            _scriptableGameObjectDataController.ContentButton.transform.localScale = new Vector3(1, 1, 1);
            
            var localPosition = _scriptableGameObjectDataController.ContentButton.transform.localPosition;
            
            localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            
            _scriptableGameObjectDataController.ContentButton.transform.localPosition = localPosition;
            
            _scriptableGameObjectDataController.ContentButton.transform.localRotation = new Quaternion(0 , 0 , 0 , 0);
            
            description.Trim();
            
            _scriptableGameObjectDataController.ContentButton.GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                
                _onClickChapter.Invoke();
                
            });

        }
    }
}
