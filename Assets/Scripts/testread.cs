using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class testread : MonoBehaviour
{
    public string filename;
    void Start()
    {
        byte[] file = File.ReadAllBytes(Application.streamingAssetsPath + "/Config.json");
        using (MemoryStream memory = new MemoryStream(file))  
        {  
            using (BinaryReader reader = new BinaryReader(memory))  
            {  
                for (int i = 0; i < file.Length; i++)  
                {  
                    byte result = reader.ReadByte();
                    filename = result.ToString();
                }  
            }  
        } 
        Debug.Log(filename);
        /*byte[] fileBytes = File.ReadAllBytes(Application.streamingAssetsPath + "/Config.json");
        StringBuilder sb = new StringBuilder();

        foreach(byte b in fileBytes)
        {
            sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));  
        }
        Debug.Log(sb);*/
    }

}
