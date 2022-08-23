using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class DataContentArea
{
    public int id;
    public int language_id;
    public int location_id;
    public string area_name;
    //public string conversation_topic;
    public NpcList[] npc;
    //public int scenario_number;
    //public int duration;
    public string path_area_prefab;
    
    public GameObject AreaPrefab;
}

[Serializable]
public class NpcList
{
    public string npc_name;
    public string conversation_topic;
    public int duration;
}
