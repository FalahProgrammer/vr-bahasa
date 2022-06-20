using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ScenarioEvent
{
    [SerializeField] public UnityEvent OnPartialAnimationPlayed;
    
    [SerializeField] public UnityEvent OnPartialAnimationFinished;
}
