using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

public class iResetableManager : MonoBehaviour
{
    public List<Object> ToggleBehaviours = new List<Object>();
    //public List<ButtonController> ButtonControllers = new List<ButtonController>();

    private void Start()
    {
        //StartCoroutine(FindScript());
    }

    public void Reset()
    {
        //var iResetables = FindObjectsOfType<ButtonController>().OfType<iResetable>();
        //var buttonController = FindObjectsOfType<ButtonController>().OfType<iResetable>();
        var toggleBehaviour = FindObjectsOfType<ToggleBehaviour>();
        
        /*foreach(iResetable item in buttonController)
        {
            Debug.Log(item.ToString());

            if (!IResetables.Contains(item.ToString()))
            {
                IResetables.Add(item.ToString());
            }
            
            item.Reset();
        }*/
        
        foreach(ToggleBehaviour item in toggleBehaviour)
        {
            Debug.Log(item.ToString());

            if (!ToggleBehaviours.Contains(item))
            {
                ToggleBehaviours.Add(item);
            }
            
            //item.Reset();
        }
    }

    IEnumerator FindScript()
    {
        var toggleBehaviour = FindObjectsOfType<ToggleBehaviour>();
        
        var buttonController = FindObjectsOfType<ButtonController>();
        
        foreach(ButtonController item in buttonController)
        {
            //Debug.Log(item.ToString());

            if (!ToggleBehaviours.Contains(item))
            {
                ToggleBehaviours.Add(item);
            }
            
            //item.Reset();
        }
        
        foreach(ToggleBehaviour item in toggleBehaviour)
        {
            //Debug.Log(item.ToString());

            if (!ToggleBehaviours.Contains(item))
            {
                ToggleBehaviours.Add(item);
            }
            
            //item.Reset();
        }

        yield return null;
    }
}
