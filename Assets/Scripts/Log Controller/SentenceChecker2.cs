using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SentenceChecker2 : MonoBehaviour
{
    public string firstSentence;
    public string secondSentence;

    public TextMeshProUGUI output;

    public string colorName;

    private void Start()
    {
        Check();
    }

    public string WordChecker(string first, string second, string colorName)
    {
        var maxCount = Math.Max(first.Length, second.Length);

        // create string to hold the result value
        var resultWord = string.Empty;
        
        for (int i = 0; i < maxCount; i++)
        {
            // if the index is out of second word range
            if (i >= second.Length)
            {
                resultWord += MarkWithColor(first[i].ToString(), colorName);
                continue;
            }
            
            // if the character doesn't match or the index is out of first word range
            if (i >= first.Length || first[i] != second[i])
            {
                resultWord += MarkWithColor(second[i].ToString(), colorName);
                continue;
            }

            resultWord += second[i];
        }

        return resultWord;
    }

    // Function to mark the string
    public string MarkWithColor(string str, string colorName)
    {
        return $"<color={colorName}>{str}</color>";
    }

    public string SentenceCheck(string firstSentence, string secondSentence)
    {
        // return if nothing to check for
        if (string.IsNullOrEmpty(firstSentence) || string.IsNullOrEmpty(secondSentence))
        {
            return "Please fill all text";
        }
        
        // split the array based on whitespace
        var firstSentenceArr = firstSentence.Split(' ');
        var secondSentenceArr = secondSentence.Split(' ');
        
        var maxCount = Math.Max(firstSentenceArr.Length, secondSentenceArr.Length);
        
        // create string array to hold the result value
        var resultStrings = new string[maxCount];
        
        for (int i = 0; i < maxCount; i++)
        {
            // if the index is out of second sentence range, get the first word and paint it
            if (i >= secondSentenceArr.Length)
            {
                resultStrings[i] = MarkWithColor(firstSentenceArr[i], colorName);
                continue;
            }

            // if the index is out of first sentence range, get the second word and paint it
            if (i >= firstSentenceArr.Length)
            {
                resultStrings[i] = MarkWithColor(secondSentenceArr[i], colorName);
                continue;
            }
            
            // if the word don't match
            if (firstSentenceArr[i] != secondSentenceArr[i])
            {
                resultStrings[i] = WordChecker(firstSentenceArr[i], secondSentenceArr[i], colorName);
                continue;
            }

            resultStrings[i] = secondSentenceArr[i];
        }

        // Join the string back
        return string.Join(" ", resultStrings);
    }

    public void Check()
    {
        output.text = SentenceCheck(firstSentence, secondSentence);
    }
}
