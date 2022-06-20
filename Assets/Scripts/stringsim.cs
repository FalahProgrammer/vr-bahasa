using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class stringsim : MonoBehaviour
{
    public string inputa;
    public string inputb;

    private void Start()
    {
        Similarity(inputa, inputb);
    }

    public ResultSimiliaritya Similarity(String s1, String s2)
    {
        String longer = s1, shorter = s2;
        if (s1.Length < s2.Length)
        {
            // longer should always have greater length
            longer = s2;
            shorter = s1;
        }

        int longerLength = longer.Length;
        
        if (longerLength == 0)
        {
            return new ResultSimiliaritya(1.0,null); /* both strings are zero length */
            //return 1.0;
        }
        
        var score = (longerLength - EditDistance(longer, shorter)) / (double) longerLength * 100;
        
        Debug.Log(score);
        
        /* // If you have Apache Commons Text, you can use it to calculate the edit distance:
        LevenshteinDistance levenshteinDistance = new LevenshteinDistance();
        return (longerLength - levenshteinDistance.apply(longer, shorter)) / (double) longerLength; */
        var resultSimiliarity = new ResultSimiliaritya(score,null);
        return (resultSimiliarity);
        
    }

    // Example implementation of the Levenshtein Edit Distance
    // See http://rosettacode.org/wiki/Levenshtein_distance#Java
    private int EditDistance(String s1, String s2)
    {
        s1 = s1.ToLower();
        s2 = s2.ToLower();

        int[] costs = new int[s2.Length + 1];
        for (int i = 0; i <= s1.Length; i++)
        {
            int lastValue = i;
            for (int j = 0; j <= s2.Length; j++)
            {
                if (i == 0)
                    costs[j] = j;
                else
                {
                    if (j > 0)
                    {
                        int newValue = costs[j - 1];
                        if (s1.ElementAt(i - 1) != s2.ElementAt(j - 1))
                        {
                            newValue = Math.Min(Math.Min(newValue, lastValue), costs[j]) + 1;
                            //Debug.Log("Element S1 : "+s1);
                        }
                            
                        costs[j - 1] = lastValue;
                        
                        lastValue = newValue;
                        //Debug.Log(lastValue);
                    }
                }
            }

            if (i > 0)
                costs[s2.Length] = lastValue;
        }
        //Debug.Log(costs[s2.Length]);
        return costs[s2.Length];
    }
    
    private string Converter(string FixAnswer)
    {
        return FixAnswer;
    }

    
}

public class ResultSimiliaritya
{
    public List<WrongWorda> LisWrongWord { get; }
    
    public double _score;
    
    public ResultSimiliaritya(double score, List<WrongWorda> lisWrongWord)
    {
        LisWrongWord = lisWrongWord;
        
        _score = score;
        
    }
    
    
}

public class WrongWorda
{
    public int StartIndex { get; }
    public int EndIndex { get; }

    public WrongWorda(int startIndex, int endIndex)
    {
        StartIndex = startIndex;
        
        EndIndex = endIndex;
    }
}
