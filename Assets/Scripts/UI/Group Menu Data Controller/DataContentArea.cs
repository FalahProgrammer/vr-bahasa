using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class DataContentArea
{
    public string id;
    public string materi_id;
    public string chapter_id;
    public string area_name;
   //public string conversation_topic;
    public NpcList[] npc;
    //public int scenario_number;
    public int duration;
    public string path_area_prefab;
    public GameObject AreaPrefab;

    /*public string content_image;
    public string content;
    public string content_pdf;*/
}

[Serializable]
public class NpcList
{
    public string npc_name;
    public string conversation_topic;
}
