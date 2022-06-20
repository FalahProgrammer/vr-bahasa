using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Networking;


public class SendPOSTMethod : MonoBehaviour
{
    //public bool RequestHeaderContentType = true;
    //public bool RequestHeaderAccept;
    public Action<string> _connectionFailed;
    public UnityEvent ConnectionFailed;
    
    /*public void SendPOST(string url, string json, Action<string> callback)
    {
        StartCoroutine(PostContentsCoroutine( url,json,callback,null));
    }*/
    
    public void SendPOST(string url, string json, Action<string> callback, Dictionary<string,string> requestheader)
    {
        StartCoroutine(PostContentsCoroutine( url,json,callback,requestheader));
    }
    private IEnumerator PostContentsCoroutine(string url, string data, Action<string> callback,Dictionary<string,string> requestheader)
    {
        using (UnityWebRequest www = UnityWebRequest.Put(url, data))
        {
            www.method="POST";
            if (requestheader != null)
            {
                foreach (var singleheader in requestheader)
                {
                    www.SetRequestHeader(singleheader.Key,singleheader.Value);
                }
            }
            
            yield return www.SendWebRequest();
            
            Debug.Log("Type of Key : " + www.GetRequestHeader("Content-Type"));
            Debug.Log("Type of Values : " + www.GetRequestHeader("Authorization"));

            //Error handling
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error connecting to the server");
                if(www.isHttpError)
                {
                    Debug.Log($"{www.responseCode}");
                    Debug.Log(www.error);
                    _connectionFailed?.Invoke(www.responseCode.ToString());
                }
                
                else
                {
                    Debug.Log($"{www.responseCode}");
                    _connectionFailed?.Invoke(www.responseCode.ToString());
                    ConnectionFailed?.Invoke();
                }
            }
            else
            {
               callback.Invoke(www.downloadHandler.text);
            }
        }
    }
}

[Serializable]
public class RequestHeader
{
    public string key;
    public string value;
}

