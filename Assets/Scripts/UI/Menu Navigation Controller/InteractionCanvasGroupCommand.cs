using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCanvasGroupCommand
{
    public void DoAction(CanvasGroup canvasGroupTarget, Action<CanvasGroup> actionCanvasGroup)
    {
        actionCanvasGroup(canvasGroupTarget);
    }
}
