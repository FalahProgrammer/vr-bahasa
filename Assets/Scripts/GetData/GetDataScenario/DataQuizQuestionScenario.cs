﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DataQuizQuestionScenario
{
    public string materi_id;
    
    public string chapter_id;
    
    public string question_id;
    
    public string questions;
    
    public string right_answer_id;
    
    public List<DataAnswerScenario> answers;
    
    public string status;
    
}
