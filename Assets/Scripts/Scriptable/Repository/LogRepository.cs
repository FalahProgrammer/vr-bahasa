using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Repository Log", menuName = "Repository/Repository Log")]
public class LogRepository : ScriptableObject
{
    public int content_id;
    //public Log logs;
    public List<Log> logs;
    
}

[Serializable]
public class Log
{
    //public List<DataLog> Items;
    
    public int question_id;
    
    public string question;

    public int answer_id;
    
    public string right_answer;
    
    public string user_answer;

    public double score;
    
    public string duration_taken;
    
    public bool answer_status;
}
