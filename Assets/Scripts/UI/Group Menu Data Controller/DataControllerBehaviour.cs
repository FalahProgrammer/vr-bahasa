using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DataControllerBehaviour : MonoBehaviour
{
    [SerializeField] private DataVariable _dataVariable;
    
    [SerializeField] private RepositoryLocation repositoryLocation;

    [SerializeField] private RepositoryContentArea repositoryContentArea;

    [SerializeField] private ScriptableGameObjectDataController _scriptableGameObjectDataController;

    [SerializeField] private Transform _buttonParentChapter;
    
    [SerializeField] private Transform _buttonParentContent;

    [SerializeField] private Transform _prefabButton;
    
    public UnityEvent OnClickLocation;
    
    public UnityEvent OnClickContentArea;

    private DataController _dataController;

    void OnValidate()
    {
        _dataVariable = Resources.Load<DataVariable>("ScriptableObjects/Variable/String Variable");
        
        repositoryLocation = Resources.Load<RepositoryLocation>("ScriptableObjects/Repository/Repository Chapter");
        
        repositoryContentArea = Resources.Load<RepositoryContentArea>("ScriptableObjects/Repository/Repository Content");
        
        _scriptableGameObjectDataController = Resources.Load<ScriptableGameObjectDataController>("ScriptableObjects/Game Object/Scriptable GameObject Data Controller");
    }

    void Awake()
    {
        _dataController = new DataController(
            monoBehaviour: this,
            dataVariable: _dataVariable,
            repositoryLocation: repositoryLocation,
            repositoryContentArea: repositoryContentArea,
            scriptableGameObjectDataController: _scriptableGameObjectDataController,
            prefabButton: _prefabButton,
            buttonParentLocation: _buttonParentChapter,
            buttonParentContentArea: _buttonParentContent, 
            onClickChapter: OnClickLocation);
        
        
    }

    public void GetDataChapter()
    {
        StartCoroutine(CoroutineGetDataChapter());
    }
    
    public void GetDataContent()
    {
        StartCoroutine(CoroutineGetDataContent());
    }

    

    IEnumerator CoroutineGetDataChapter()
    {
        foreach(Transform child in _buttonParentChapter)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < repositoryLocation.Items.Count; i++)
        {
            if (repositoryLocation.Items[i].language_id.Equals(_dataVariable.materi_id)) // &&
            //    repositoryLocation.Items[i].menu_id.Equals(_dataVariable.menu_id))
            {
                //GenerateButtonChapter(_repositoryItems.ListChapter[i].no, _repositoryItems.ListChapter[i].judul);
                GenerateChapter(
                    repositoryLocation.Items[i].id, 
                    repositoryLocation.Items[i].title
                );
            }
        }

        yield return null;
    }
    
    IEnumerator CoroutineGetDataContent()
    {
        //ListContent.Clear();
        
        foreach(Transform child in _buttonParentContent)
        {
            Destroy(child.gameObject);
        }
        
        /*for (int i = 0; i < _repositoryContent.Items.Count; i++)
        {
            if (_repositoryContent.Items[i].chapter_id.Equals(_dataVariable.chapter_id) &&
                _repositoryContent.Items[i].materi_id.Equals(_dataVariable.materi_id))
            {
                //ListContent.Add(ListAllArrayContent[i]);
            }
        }*/

        for (int i = 0; i < repositoryContentArea.Items.Count; i++)
        {
            if (repositoryContentArea.Items[i].location_id.Equals(_dataVariable.chapter_id) &&
                repositoryContentArea.Items[i].language_id.Equals(_dataVariable.materi_id))
            {
                /*GenerateContent(
                    repositoryContentArea.Items[i].content, 
                    repositoryContentArea.Items[i].content_image,
                    repositoryContentArea.Items[i].conversation_topic,
                    repositoryContentArea.Items[i].area_name
                );*/
            }
        }
        
        
        
        yield return null;
    }


    public void GenerateChapter(string title, string name)
    {
        if (!name.Equals("Bibliography"))
        {
            _scriptableGameObjectDataController.ChapterButton = Instantiate(_prefabButton.gameObject, _buttonParentChapter);
            PrefabButtonDataController prefabButtonDataController = _scriptableGameObjectDataController.ChapterButton.GetComponentInChildren<PrefabButtonDataController>();

            prefabButtonDataController.SelectType = PrefabButtonDataController.Type.Location;
            
            prefabButtonDataController.ContentAreaName = title;
            
            prefabButtonDataController.ButtonName = name;

            prefabButtonDataController.TextButtonName.text = prefabButtonDataController.ButtonName;

            /*ButtonChapter.ContentButton.transform.localScale = new Vector3(1, 1, 1);
            ButtonChapter.ContentButton.transform.localPosition = new Vector3(ButtonChapter.ContentButton.transform.localPosition.x, ButtonChapter.ContentButton.transform.localPosition.y, 0);
            ButtonChapter.ContentButton.transform.localRotation = new Quaternion(0, 0, 0, 0);*/
            _scriptableGameObjectDataController.ChapterButton.GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                Debug.Log(title);
                
                _dataVariable.chapter_id = title;
                
                OnClickLocation.Invoke();

            });
        }
    }

    public void GenerateContent(string urlVideo, string urlImage, string description, string name)
    {
        if (!description.Equals("Bibliography"))
        {
            _scriptableGameObjectDataController.ContentButton = Instantiate(_prefabButton.gameObject, _buttonParentContent, true);
            
            PrefabButtonDataController prefabButtonDataController = _scriptableGameObjectDataController.ContentButton.GetComponentInChildren<PrefabButtonDataController>();

            prefabButtonDataController.SelectType = PrefabButtonDataController.Type.Area;
            
            prefabButtonDataController.ButtonName = name;
            
            /*prefabButtonDataController.UrlVideo = urlVideo;
            
            prefabButtonDataController.Description = description;
            
            prefabButtonDataController.UrlImage = urlImage;*/

            prefabButtonDataController.TextButtonName.text = prefabButtonDataController.ButtonName;

            _scriptableGameObjectDataController.ContentButton.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            
            var localPosition = _scriptableGameObjectDataController.ContentButton.transform.localPosition;
            
            localPosition = new Vector3(localPosition.x, localPosition.y, 0);
            
            _scriptableGameObjectDataController.ContentButton.transform.localPosition = localPosition;
            
            _scriptableGameObjectDataController.ContentButton.transform.localRotation = new Quaternion(0 , 0 , 0 , 0);
            
            description.Trim();
            
            _scriptableGameObjectDataController.ContentButton.GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                
                OnClickContentArea.Invoke();
                
            });

        }
    }
}
