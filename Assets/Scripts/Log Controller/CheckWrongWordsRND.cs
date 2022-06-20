using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Random = System.Random;

public class CheckWrongWordsRND : MonoBehaviour
{
    public string Input1;
    
    public string Input2;

    public List<string> RightAnswerList = new List<string>() { "a", "n", "a" ,"k"," ","a", "n", "a" ,"k"," ","m","a","i","n"};
    
    public List<string> UserAnswerList = new List<string>() { "a", "n", "a" ,"k"," ","siuu", "n", "a" ,"k"," ","a", "n", "a" ,"k"," ","siuu", "n", "a" ,"k"," ","m","a","i","n",};
    
    public TextMeshProUGUI TextMeshProUgui;
    private void Start()
    {
        //Compute(Input1, Input2);

        //Intersection();



        SentenceToList();
        
        CheckSentence();

        //GetResult();
    }
    
    
    public void SentenceToList()
    {
        for (int i = 0; i < Input1.Length; i++)
        {
            RightAnswerList.Add(Input1[i].ToString());
        }
        
        for (int i = 0; i < Input2.Length; i++)
        {
            UserAnswerList.Add(Input2[i].ToString());
        }
    }

    public void Check()
    {
        var al =new  List<string> { "a","b","c"};
        var bl = new List<string> { "da","a","c"};

        var res = (from a in al
            join b in bl
                on a equals b
            select new
            {
                Matched = a,
                Index = al.IndexOf(a)
            }).ToList();

        for (int i = 0; i < res.Count; i++)
        {
            Debug.Log(res[i]);
        }
        
        List<int> data1 = new List<int> {1,2,3,4,5};
        List<string> data2 = new List<string>{"6","3"};

        var newData = data1.Select(i => i.ToString()).Intersect(data2).ToString();
        
        Debug.Log(newData);
        
    }

    public string WrongColor(string list)
    {
        return "<color=#FF0000>"+list+"</color>";
    }
    
    public string RightColor(string list)
    {
        return "<color=#FFF000>"+list+"</color>";
    }
    public void CheckSentence()
    {
        List<string> wrong = new List<string>();
        
        List<string> wrongleft = new List<string>();
        
        /*/*string joinedl1 = l1.Aggregate((i, j) => i + j);
        
        string joinedl2 = l2.Aggregate((i, j) => i + j);#1#

        string joinedl1 =string.Join(null, l1);
        string joinedl2 =string.Join(null, l2);
        Debug.Log(joinedl1);
        Debug.Log(joinedl2);
        var firstSentence = joinedl1.Length < joinedl2.Length ? joinedl1 : joinedl2;
        var secondSentence = joinedl2.Length < joinedl1.Length ? joinedl2 : joinedl2;
        Debug.Log(firstSentence.Length);
        Debug.Log(secondSentence.Length);*/

        if (RightAnswerList.Count < UserAnswerList.Count)
        {
            Debug.Log("Kurang");
            
            for (int i = 0; i < RightAnswerList.Count; i++)
            {
                //Debug.Log(l1[i]);
                
                if (UserAnswerList[i]!=RightAnswerList[i])
                {
                    //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                    wrong.Add(RightColor(UserAnswerList[i]));

                }
                else
                {
                    //wrong.Add("<color=#FFF000>"+UserAnswerList[i]+"</color>");
                    wrong.Add(WrongColor(UserAnswerList[i]));
                }
            }
            for (int j = RightAnswerList.Count; j < UserAnswerList.Count; j++)
            {
                //wrong.Add("<color=#FF0000>"+UserAnswerList[j]+"</color>");
                wrong.Add(WrongColor(UserAnswerList[j]));
            }
        }
        else
        {
            Debug.Log("Lebih");
            //sisa string dari list di tambah terus di merahin
            for (int i = 0; i < UserAnswerList.Count; i++)
            {
                //Debug.Log(l2[i]);
                
                if (UserAnswerList[i]!=RightAnswerList[i])
                {
                    //wrong.Add("<color=#FF0000>"+UserAnswerList[i]+"</color>");
                    wrong.Add(WrongColor(UserAnswerList[i]));
                }
                else
                {
                    //wrong.Add("<color=#FFF000>"+UserAnswerList[i]+"</color>");
                    wrong.Add(RightColor(UserAnswerList[i]));
                }
            }
            for (int j = UserAnswerList.Count; j < RightAnswerList.Count; j++)
            {
                //wrong.Add("<color=#FF0000>"+RightAnswerList[j]+"</color>");
                wrong.Add(WrongColor(RightAnswerList[j]));
            }
        }
        
        
        Debug.Log(wrong.Count);

        string joinedWrong = wrong.Aggregate((i, j) => i + j);
        
        Debug.Log(joinedWrong);
/*        for (int i = 0; i < wrong.Count; i++)
        {
            
            Debug.Log( wrong[i]);
            
        }*/

    }

    public void Intersection()
    {
        List<string> l1 = new List<string>() { "a", "baa", "c" };
        List<string> l2 = new List<string>() { "c", "b", "as", "co" };

        foreach (string str in Intersect(l1,l2))
        {
            Debug.Log(str);
        }
        
        IEnumerable<T> Intersect<T>(IList<T> lhs, IList<T> rhs)
        {
            if (lhs == null) throw new ArgumentNullException("lhs");
            if (rhs == null) throw new ArgumentNullException("rhs");

            // build the dictionary from the shorter list
            if (lhs.Count > rhs.Count)
            {
                IList<T> tmp = rhs;
                rhs = lhs;
                lhs = tmp;
            }
            Dictionary<T, bool> lookup = new Dictionary<T, bool>();
            foreach (T item in lhs)
            {
                if (!lookup.ContainsKey(item)) lookup.Add(item, true);
            }
            foreach (T item in rhs)
            {
                if (lookup.ContainsKey(item))
                {
                    lookup.Remove(item); // prevent duplicates
                    //Debug.Log(item);
                    yield return item;

                }
            }
        }
        
        
    }
    
    public string GetWrongWords(string userAnswer, List<WrongWords> wrongWordses)
    {
        var richtext = userAnswer;
        var addedIndex = 0;
        foreach (var wrongword in wrongWordses)
        {
            //richtext = richtext.Insert(wrongword.StartIndex+addedIndex, "<color=Red>");
            richtext = richtext.Insert(wrongword.StartIndex+addedIndex, "<color=#FF0000>");
            addedIndex += 15;
            richtext = richtext.Insert(wrongword.EndIndex+addedIndex, "</color>");
            addedIndex += 8;
        }
        Debug.Log(richtext);
        return richtext;
    }

    public void GetResult(string supposedAnswer, string userAnswer)
    {
        //GetWrongWords(userAnswer, GetWrongWords(supposedAnswer,userAnswer));
    }
    
    public void Distinct()
    {
        var listoflist = new List<List<string>>
        {
            new List<string> {"N", "A", "M"},
            new List<string> {"N", "A", "N"}
        };
        var result = listoflist.SelectMany(l=>l).Distinct().ToList();
        Assert.AreEqual(result.Count, 3);

        for (int i = 0; i < result.Count; i++)
        {
            Debug.Log(result[i]);
        }
       
    }

    public int Compute(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            if (string.IsNullOrEmpty(t))
                return 0;
            return t.Length;
        }

        if (string.IsNullOrEmpty(t))
        {
            return s.Length;
        }

        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // initialize the top and right of the table to 0, 1, 2, ...
        for (int i = 0; i <= n; d[i, 0] = i++);
        for (int j = 1; j <= m; d[0, j] = j++);

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                int min1 = d[i - 1, j] + 1;
                int min2 = d[i, j - 1] + 1;
                int min3 = d[i - 1, j - 1] + cost;
                d[i, j] = Math.Min(Math.Min(min1, min2), min3);

                
            }
            //Debug.Log("<color=#FF0000>"+Input1[i]+"</color>");
            
            //TextMeshProUgui.text = "<color=#FF0000>" + Input1[i] + "</color>";
                
            
            
        }
        Debug.Log(d[n, m]);
        return d[n, m];
        
    }

}
