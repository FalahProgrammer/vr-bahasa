using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Repository Log Answer", menuName = "Repository/Repository Log Answer")]
public class RepositoryLogAnswer : ScriptableObject
{
    public int CorrectAnswer;
    public int InCorrectAnswer;
    public double TotalScore;

}