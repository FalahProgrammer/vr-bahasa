using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DataLog
{
    public int question_id;
    
    public string question;

    public int answer_id;
    
    public string right_answer;
    
    public string user_answer;

    public double score;
    
    public string duration_taken;
    
    public bool answer_status;
}
