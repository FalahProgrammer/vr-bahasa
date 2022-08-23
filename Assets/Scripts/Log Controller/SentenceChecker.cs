using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SentenceChecker : MonoBehaviour
{
    /*public string Input1;
    public string Input2;
    public TextMeshProUGUI Result;
    */

    [SerializeField] private bool debugMode;
    
    [SerializeField] private List<string> RightAnswerList = new List<string>();

    [SerializeField] private List<string> UserAnswerList = new List<string>();

    [HideInInspector] public List<string> ListTambahan = new List<string>();

    List<string> CombinedList = new List<string>();

    private List<string> _escapedChars = new List<string>() {",", ".", "!", "?"};

    [SerializeField] public string ResultText;

    private ReturnColor _returnColor = new ReturnColor();

    public List<WrongWords> GetWrongWords(string supposedAnswer, string userAnswer)
    {
        List<WrongWords> words = new List<WrongWords>();

        //Clean Text
        supposedAnswer = supposedAnswer.ToLower();
        userAnswer = userAnswer.ToLower();

        //Separate into words
        var prevanswer = supposedAnswer;
        var supposedAnsWords = supposedAnswer.Split(' ');
        var userAnsWords = userAnswer.Split(' ');
        if (debugMode) Debug.Log(supposedAnsWords.Length);
        if (supposedAnsWords.Length == 1)
        {
            if (debugMode) Debug.Log("We need to use character-spaced method");
            var shorterSentence = supposedAnswer.Length < userAnswer.Length ? supposedAnswer : userAnswer;
            bool isEqualLength = supposedAnswer.Length == userAnswer.Length;

            for (int i = 0; i < shorterSentence.Length; i++)
            {
                if (userAnswer[i].Equals(supposedAnswer[i]))
                {
                }
                else
                {
                    var startIndex = userAnswer.IndexOf(userAnswer[i]);
                    var wrong = new WrongWords(startIndex, startIndex + 1);
                    words.Add(wrong);
                }
            }
        }
        else
        {
            var shorterSentence = supposedAnsWords.Length < userAnsWords.Length ? supposedAnsWords : userAnsWords;
            bool isEqualLength = supposedAnswer.Length == userAnswer.Length;

            for (int i = 0; i < shorterSentence.Length; i++)
            {
                if (userAnsWords[i].Equals(supposedAnsWords[i]))
                {
                }
                else
                {
                    var startIndex = userAnswer.IndexOf(userAnsWords[i]);
                    var wrong = new WrongWords(startIndex, startIndex + userAnsWords[i].Length);
                    words.Add(wrong);
                }
            }
        }


        return words;
    }

    public string GetWrongWords(string userAnswer, List<WrongWords> wrongWordses)
    {
        var richtext = userAnswer;
        var addedIndex = 0;
        foreach (var wrongword in wrongWordses)
        {
            //richtext = richtext.Insert(wrongword.StartIndex+addedIndex, "<color=Red>");
            richtext = richtext.Insert(wrongword.StartIndex + addedIndex, "<color=#FF0000>");
            addedIndex += 15;
            richtext = richtext.Insert(wrongword.EndIndex + addedIndex, "</color>");
            addedIndex += 8;
        }

        if (debugMode) Debug.Log(richtext);
        ResultText = richtext;
        return richtext;
    }

    /*private void Start()
    {
        GetResult(Input1, Input2);
    }*/

    public void GetResult(string supposedAnswer, string userAnswer)
    {
        //GetWrongWords(userAnswer, GetWrongWords(supposedAnswer,userAnswer));

        SentenceToList(EscapedCharacter(supposedAnswer), EscapedCharacter(userAnswer));
    }

    /*private void Start()
    {
        GetResult(Input1, Input2);
    }*/

    public string EscapedCharacter(string rightAnswer)
    {
        var mods = rightAnswer;
        foreach (var esChar in _escapedChars)
        {
            mods = mods.Replace(esChar, string.Empty).ToLower();
        }

        return mods;
    }

    public void SentenceToList(string supposedAnswer, string userAnswer)
    {
        for (int i = 0; i < supposedAnswer.Length; i++)
        {
            RightAnswerList.Add(supposedAnswer[i].ToString());
        }

        for (int i = 0; i < userAnswer.Length; i++)
        {
            UserAnswerList.Add(userAnswer[i].ToString());
        }

        CheckSentence();
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetResult(Input1, Input2);
        }
    }*/

    public void CheckSentence()
    {
        if (UserAnswerList.Count != 0)
        {
            if (RightAnswerList.Count < UserAnswerList.Count)
            {
                if (debugMode) Debug.Log("Kurang");

                for (int i = 0; i < RightAnswerList.Count; i++)
                {
                    //Debug.Log(l1[i]);

                    if (UserAnswerList[i] != RightAnswerList[i])
                    {
                        //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                        CombinedList.Add(_returnColor.WrongColor(UserAnswerList[i]));
                    }
                    else
                    {
                        //wrong.Add("<color=#FFF000>"+UserAnswerList[i]+"</color>");
                        CombinedList.Add(_returnColor.RightColor(UserAnswerList[i]));
                    }
                }

                for (int j = RightAnswerList.Count; j < UserAnswerList.Count; j++)
                {
                    //wrong.Add("<color=#FF0000>"+UserAnswerList[j]+"</color>");
                    CombinedList.Add(_returnColor.WrongColor(UserAnswerList[j]));
                }
            }
            else
            {
                if (debugMode) Debug.Log("Lebih");

                //sisa string dari list di tambah terus di merahin


                for (int i = 0; i < UserAnswerList.Count; i++)
                {
                    //Debug.Log(l2[i]);

                    if (UserAnswerList[i] != RightAnswerList[i])
                    {
                        //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                        CombinedList.Add(_returnColor.WrongColor(UserAnswerList[i]));
                    }
                    else
                    {
                        //wrong.Add("<color=#FFF000>"+UserAnswerList[i]+"</color>");
                        CombinedList.Add(_returnColor.RightColor(UserAnswerList[i]));
                    }
                }

                /*for (int j = UserAnswerList.Count - 1; j < RightAnswerList.Count; j++)
                {
                    //wrong.Add("<color=#FF0000>"+RightAnswerList[j]+"</color>");
                    CombinedList.Add(_returnColor.WrongColor(RightAnswerList[j]));
                }*/
            }


            //Debug.Log(CombinedList.Count);

            string joinedWrong = CombinedList.Aggregate((i, j) => i + j);

            if (debugMode) Debug.Log(joinedWrong);

            ResultText = joinedWrong;

            //Result.text = joinedWrong;

            CombinedList.Clear();

            RightAnswerList.Clear();

            UserAnswerList.Clear();
        }
        else
        {
            ResultText = "-";
        }
    }

    public void CheckSentence2()
    {
        if (RightAnswerList.Count < UserAnswerList.Count)
        {
            if (debugMode) Debug.Log("Kurang");

            for (int i = 0; i < RightAnswerList.Count; i++)
            {
                //Debug.Log(l1[i]);

                if (UserAnswerList[i] != RightAnswerList[i])
                {
                    //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                    CombinedList.Add(_returnColor.WrongColor(UserAnswerList[i]));
                }
                else
                {
                    //wrong.Add("<color=#FFF000>"+UserAnswerList[i]+"</color>");
                    CombinedList.Add(_returnColor.RightColor(UserAnswerList[i]));
                }
            }

            for (int j = RightAnswerList.Count; j < UserAnswerList.Count; j++)
            {
                //wrong.Add("<color=#FF0000>"+UserAnswerList[j]+"</color>");
                CombinedList.Add(_returnColor.WrongColor(UserAnswerList[j]));
            }
        }
        else
        {
            if (debugMode) Debug.Log("Lebih");


            //sisa string dari list di tambah terus di merahin


            if (UserAnswerList != null)
            {
                for (int i = RightAnswerList.Count - 1 - UserAnswerList.Count;
                    i < RightAnswerList.Count - UserAnswerList.Count;
                    i++)
                {
                    if (UserAnswerList[i] != RightAnswerList[i])
                    {
                        for (int j = 0; j < RightAnswerList.Count - UserAnswerList.Count; j++)
                        {
                            //wrong.Add("<color=#FF0000>"+RightAnswerList[j]+"</color>");
                            CombinedList.Add(_returnColor.WrongColor(RightAnswerList[j]));
                        }
                    }
                }
            }


            for (int i = 0; i < UserAnswerList.Count; i++)
            {
                //Debug.Log(l2[i]);


                if (UserAnswerList[i] != RightAnswerList[i])
                {
                    //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                    CombinedList.Add(_returnColor.WrongColor(UserAnswerList[i]));
                }
                else
                {
                    //wrong.Add("<color=#FFF000>"+UserAnswerList[i]+"</color>");
                    CombinedList.Add(_returnColor.RightColor(UserAnswerList[i]));
                }
            }

            /*Debug.Log(UserAnswerList.Count - 1);
            Debug.Log(RightAnswerList.Count - 1);*/

            /*for (int j = UserAnswerList.Count; j < RightAnswerList.Count; j++)
            {
                //wrong.Add("<color=#FF0000>"+RightAnswerList[j]+"</color>");
                CombinedList.Add(_returnColor.WrongColor(RightAnswerList[j]));
            }*/

            /*for (int j = UserAnswerList.Count - 1; j < RightAnswerList.Count; j++)
            {
                //wrong.Add("<color=#FF0000>"+RightAnswerList[j]+"</color>");
                CombinedList.Add(_returnColor.WrongColor(RightAnswerList[j]));

                
            }*/

            for (int i = UserAnswerList.Count - 1 - RightAnswerList.Count; i > -1; i--)
            {
                //Debug.Log(l2[i]);

                if (UserAnswerList[i] != RightAnswerList[i])
                {
                    //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                    for (int j = UserAnswerList.Count; j < RightAnswerList.Count; j++)
                    {
                        //wrong.Add("<color=#FF0000>"+RightAnswerList[j]+"</color>");
                        CombinedList.Add(_returnColor.WrongColor(RightAnswerList[j]));
                    }
                }
            }
        }


        //Debug.Log(CombinedList.Count);

        string joinedWrong = CombinedList.Aggregate((i, j) => i + j);

        if (debugMode) Debug.Log(joinedWrong);

        ResultText = joinedWrong;


        /*CombinedList.Clear();
        
        RightAnswerList.Clear();
        
        UserAnswerList.Clear();*/
    }

    public void CheckSentence3()
    {
        if (RightAnswerList.Count < UserAnswerList.Count)
        {
            if (debugMode) Debug.Log("Kurang");

            for (int i = 0; i < RightAnswerList.Count; i++)
            {
                //Debug.Log(l1[i]);

                if (UserAnswerList[i] != RightAnswerList[i])
                {
                    //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                    CombinedList.Add(_returnColor.WrongColor(UserAnswerList[i]));
                }
                else
                {
                    //wrong.Add("<color=#FFF000>"+UserAnswerList[i]+"</color>");
                    CombinedList.Add(_returnColor.RightColor(UserAnswerList[i]));
                }
            }

            for (int j = RightAnswerList.Count - 1; j < UserAnswerList.Count; j++)
            {
                //wrong.Add("<color=#FF0000>"+UserAnswerList[j]+"</color>");
                CombinedList.Add(_returnColor.WrongColor(UserAnswerList[j]));
            }
        }
        else
        {
            if (debugMode) Debug.Log("Lebih");

            //sisa string dari list di tambah terus di merahin
            if (debugMode) Debug.Log(UserAnswerList.Count);
            if (debugMode) Debug.Log(RightAnswerList.Count);

            for (int i = 0; i < RightAnswerList.Count - UserAnswerList.Count; i++)
            {
                CombinedList.Add(_returnColor.WrongColor(RightAnswerList[i]));
                ListTambahan.Add(RightAnswerList[i]);

                //UserAnswerList.Insert(0,RightAnswerList[i]);
            }

            for (int i = 0; i < UserAnswerList.Count; i++)
            {
                //ListTambahan.Add(RightAnswerList[i]);
                //ListTambahan.Add(_returnColor.WrongColor(UserAnswerList[i]));
                //UserAnswerList.Append(0,ListTambahan[i]);
                ListTambahan.Add(UserAnswerList[i]);

                //CombinedList.Add(_returnColor.WrongColor(ListTambahan[i]));
            }


            for (int i = RightAnswerList.Count - UserAnswerList.Count; i < ListTambahan.Count; i++)
            {
                //Debug.Log(l2[i]);

                if (ListTambahan[i] != RightAnswerList[i])
                {
                    //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                    CombinedList.Add(_returnColor.WrongColor(ListTambahan[i]));
                }
                else
                {
                    //wrong.Add("<color=#FFF000>"+UserAnswerList[i]+"</color>");
                    CombinedList.Add(_returnColor.RightColor(ListTambahan[i]));
                }
            }

            for (int j = ListTambahan.Count; j < RightAnswerList.Count; j++)
            {
                //wrong.Add("<color=#FF0000>"+RightAnswerList[j]+"</color>");
                CombinedList.Add(_returnColor.WrongColor(RightAnswerList[j]));
            }
        }


        //Debug.Log(CombinedList.Count);

        string joinedWrong = CombinedList.Aggregate((i, j) => i + j);

        if (debugMode) Debug.Log(joinedWrong);

        ResultText = joinedWrong;


        /*CombinedList.Clear();
        
        RightAnswerList.Clear();
        
        UserAnswerList.Clear();*/
    }

    public void CheckSentence4()
    {
        string rightAsnswer = string.Join("", RightAnswerList);
        string userAnswer = string.Join("", UserAnswerList);

        string[] rightAnswerInfo = rightAsnswer.Split();
        string[] userAnswerInfo = userAnswer.Split();


        for (int i = 0; i < userAnswerInfo.Length; i++)
        {
            if (rightAnswerInfo[i] != userAnswerInfo[i])
            {
                if (debugMode) Debug.Log(rightAnswerInfo[i]);
            }
        }

        /*foreach (string info in rightAnswerInfo)    
        {    
            Debug.Log(info);
            //Debug.Log(info.Substring(info.IndexOf(": ")));    
        }*/
    }

    public string SubmitResultText() => ResultText;
}

public class ReturnColor
{
    public string WrongColor(string list)
    {
        return "<color=#FF0000>" + list + "</color>";
    }

    public string RightColor(string list)
    {
        return "<color=#000000>" + list + "</color>";
    }
}