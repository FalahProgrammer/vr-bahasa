using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "String Variable", menuName = "Variable/String Data Variable")]
public class DataVariable : ScriptableObject
{
    public string materi_id;
    public string menu_id;
    public string chapter_id;
    public int qustion_id;
    public int area_id;
    
    // repository content area index
    public int contentAreaIndex;

    public void Materi_Id(string materi_ID)
    {
        materi_id = materi_ID;
        Debug.Log("materi id kepannggil ");
    }

    public void Menu_Id(string menu_ID)
    {
        menu_id = menu_ID;
        Debug.Log("menu id kepannggil ");
    }

    public void Chapter_Id(string chapter_ID)
    {
        chapter_id = chapter_ID;
        Debug.Log("Chapter ID kepannggil ");
    }
    
    public void Question_Id(int question_ID)
    {
        qustion_id = question_ID;
        Debug.Log("Question ID kepannggil ");
    }
    
    public void Exam_Id(int exam_ID)
    {
        area_id = exam_ID;
        Debug.Log("Area ID kepannggil ");
    }

    public void IncreamentQuestionID(int startFrom)
    {
        startFrom = qustion_id; //Int32.Parse(qustion_id);

        startFrom += 1;

        qustion_id = startFrom; //.ToString();

    }
}