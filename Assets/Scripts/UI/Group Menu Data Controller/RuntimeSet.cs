// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//namespace RoboRyanTron.Unite2017.Sets
namespace RuntimeSets
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        public List<T> Items = new List<T>();

        public void Add(T thing)
        {
            if (!Items.Contains(thing))
                Items.Add(thing);
        }

        public void Remove(T thing)
        {
            if (Items.Contains(thing))
                Items.Remove(thing);
        }

        public void SetItems(string stringdata)
        {
            T[] arrData = JsonHelper.getJsonArray<T>(stringdata);
            Items = arrData.ToList();
        }
        
    }
}