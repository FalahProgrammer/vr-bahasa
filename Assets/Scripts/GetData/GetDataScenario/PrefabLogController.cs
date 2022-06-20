using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrefabLogController : MonoBehaviour
{
    public string Id;
    public string Duration;
    public int Number;
    public string Question;
    public string RightAnswer;
    public string YourAnswer;
    public Text DurationText;
    public Text NumberText;
    public TextMeshProUGUI QuestionText;
    public Text TitleRightAnswerText;
    public TextMeshProUGUI RightAnswerText;
    public Text TitleYourAnswerText;
    public TextMeshProUGUI YourAnswerText;
    public Image IndicatorIcon;
}
