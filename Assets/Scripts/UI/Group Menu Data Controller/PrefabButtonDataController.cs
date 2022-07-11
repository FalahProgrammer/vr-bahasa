using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PrefabButtonDataController : MonoBehaviour, IPointerEnterHandler
{
    /*[HideInInspector] public string UrlVideo;
    [HideInInspector] public string Description;
    
    [HideInInspector] public string UrlImage;*/
    [Space(40)]
    [SerializeField] private bool _debugMode;
    
    [HideInInspector] public int Duration;
    [HideInInspector] public int ScenarioNumber;
    [HideInInspector] public string SceneName;
    [HideInInspector] public string ID;
    [HideInInspector] public string MateriId;
    [HideInInspector] public string ChapterId;
    [HideInInspector] public string ContentAreaName;
    [HideInInspector] public string LocationtAreaName;
    [HideInInspector] public string ButtonName;
    [HideInInspector] public GameObject AreaPrefab;
    [HideInInspector] public Text TextButtonName;
    [HideInInspector] public Texture2D Logo;
    
    public ScriptableGameObjectDataController _scriptableGameObjectDataController;
    
    public enum Type
    {
        Unset,
        Location,
        Area
    }
    [HideInInspector]
    public Type SelectType;

    private void OnValidate()
    {
        _scriptableGameObjectDataController = Resources.Load<ScriptableGameObjectDataController>("ScriptableObjects/Game Object/Scriptable GameObject Data Controller");

        TextButtonName = this.transform.GetChild(0).GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_debugMode) Debug.Log("The cursor entered the selectable UI element.");
        
        _scriptableGameObjectDataController.ChapterButton = this.gameObject;
    }
}
