using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ConfigLoader : MonoBehaviour
{
    [SerializeField] private string configPath = Application.streamingAssetsPath + "/Config.json";
    [SerializeField] private RepositoryLoginData _repositoryLoginData;
    [SerializeField] private SendPOSTMethod _sendPostMethod;
    //public NetworkApiManager NetworkApiManager;
    
    public DataPostScenario _configFile;
    
    public string LastDate;

    public string Username;

    string[] args = System.Environment.GetCommandLineArgs ();
    
    private DataPostScenario ReadConfig()
    {
        using (StreamReader stream = new StreamReader(configPath))
        {
            string json = stream.ReadToEnd();
            
            var URL = "http://vr-bahasa.test/api/v1/report";
        
            string JSON = JsonUtility.ToJson(json);
        
            Debug.Log(json);
            
            _sendPostMethod.SendPOST(URL,json, (x) =>
            {
                        
                Debug.Log("Callback : " + x);
                        
            },_repositoryLoginData.Header.ToDictionary(x=>x.key, x=> x.value));
            
            return JsonUtility.FromJson<DataPostScenario>(json);
            
            
        }
    }

    private void WriteConfig(ConfigFile configFile)
    {
        string hasilJson = JsonUtility.ToJson(configFile);
        
        using (StreamWriter stream = new StreamWriter(configPath))
        {
            stream.Write(hasilJson);
        }
    }

    public void Awake()
    {
        configPath = Application.streamingAssetsPath + "/Config.json";
        
        _configFile = ReadConfig();

        //LastDate = _configFile.Date;
        
        //_configFile.Date = DateTime.Now.ToString();

        //_configFile.Username = System.Environment.GetCommandLineArgs()
        
        /*for (int i = 0; i < args.Length; i++) {
            Debug.Log ("ARG " + i + ": " + args [i]);
            if (args [i] == "-username") {
                Username = args [i + 1];
            }
        }*/

        //Tambahkan write untuk username dari raftels

        //WriteConfig(_configFile);
    }
    
    private void Start()
    {
        
            
    }
}
