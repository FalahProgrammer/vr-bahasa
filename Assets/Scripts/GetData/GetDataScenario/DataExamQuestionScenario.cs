using UnityEngine;

[System.Serializable]

public class DataExamQuestionScenario {

    /*public string materi_id;
    public string user_id;
    public string status_soal;
    public string text_soal;
    public string jawaban1;
    public string jawaban2;
    public string jawaban3;
    public string jawaban4;
    public string jawab_benar;

    public string DebugThis()
    {
        return "materi_id = " + materi_id + "\ninstruktur = " + user_id + "\nstatus_soal = " + status_soal;
    }*/

    public string author_id;
    public string name;
    public string materi_id;
    public string question_id;
    public string questions;
    public string right_answer_id;
    public DataAnswerScenario[] answers;
    public string status;
    
    [Header("User Defined")]
    public int IQuestionNo;
    public bool IsSkip;

    public string DebugThis()
    {
        string s_right_answer = "";

        foreach (DataAnswerScenario data_question_single in answers)
        {
            if (data_question_single.id.Equals(right_answer_id)) s_right_answer = data_question_single.text;
        }

        return "author_id = " + author_id + "\nname = " + name + "\nmateri_id = " + materi_id + "\nquestion_id = " + question_id + "\nquestions = " + questions + "\ns_right_answer = " + s_right_answer;
    }
    
    // [{"author_id":19,"name":"Supervisor B","materi_id":2,"question_id":18,"right_answer_id":69,"questions":"1","answers":[{"id":69,"text":"a"},{"id":70,"text":"b"},{"id":71,"text":"c"},{"id":72,"text":"d"}],"status":"Active"},{"author_id":19,"name":"Supervisor B","materi_id":2,"question_id":19,"right_answer_id":73,"questions":"2","answers":[{"id":73,"text":"a"},{"id":74,"text":"b"},{"id":75,"text":"c"},{"id":76,"text":"d"}],"status":"Active"},{"author_id":19,"name":"Supervisor B","materi_id":2,"question_id":20,"right_answer_id":77,"questions":"3","answers":[{"id":77,"text":"a"},{"id":78,"text":"b"},{"id":79,"text":"c"},{"id":80,"text":"d"}],"status":"Active"}]
}
