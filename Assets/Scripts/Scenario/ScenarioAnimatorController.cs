using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioAnimatorController : MonoBehaviour
{
    public int id;
    
    public List<Animator> Animators = new List<Animator>();

    public Animator _selectedAnimator;
        
    private SequentialAnimation _sequentialAnimation;

    private void Start()
    {
        _sequentialAnimation = GetComponent<SequentialAnimation>();
        
        Animators.Add(this.GetComponent<Animator>());

        id = _sequentialAnimation._id;
    }

    public void AnimatorController()
    {
        for (int i = 0; i < Animators.Count; i++)
        {
            if (id == i)
            {
                _selectedAnimator = Animators[i];
            }
        }
    }
}
