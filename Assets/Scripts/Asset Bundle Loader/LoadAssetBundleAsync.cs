using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoadAssetBundleAsync 
{
    public AssetBundle AssetBundle { get; set; }
    public List<string> UrlPath { get; }
    public Text ProgressText { get;}

    public LoadAssetBundleAsync(AssetBundle assetBundle, List<string> urlPath, Text progressText)
    {
        AssetBundle = assetBundle;
        
        UrlPath = urlPath;
        
        ProgressText = progressText;
    }

    public IEnumerator LoadBundleAsync(int index, Action<GameObject> onFinishedLoadAsset)
    {
        string path = Path.Combine(Application.streamingAssetsPath, UrlPath[index]);
        Debug.Log("Trying to load: " + path);
        AssetBundleCreateRequest assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(path);
            //AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, UrlPath[index]));
        
        AssetBundle = assetBundleCreateRequest.assetBundle;

        if (AssetBundle != null)
        {
            AssetBundleRequest assetBundleRequest = AssetBundle.LoadAllAssetsAsync();
        
            while (!assetBundleRequest.isDone)
            {
                ProgressText.text = "LoadAllAssetsAsync progress: " + assetBundleRequest.progress * 100.0f + "%";
            
                //Debug.Log(assetBundleRequest.progress* 100.0f);
            
                yield return ProgressText.text;
            }
            //text.text = "Loading completed";
            Debug.Log("AssetBundle.LoadAllAssetsAsync completed");
        
            GameObject obj = assetBundleRequest.asset as GameObject;

            //Debug.Log("Asset Bundle Name : " + obj.name);
            
            onFinishedLoadAsset?.Invoke(obj);
        }
        else
        {
            Debug.Log("Asset Bundle Null");
        }
        
        AssetBundle.Unload(false);
        
    }
    
    public void UnloadAssetBundle()
    {
        if (AssetBundle != null)
        {
            AssetBundle.Unload(true);
            Debug.Log(ReturnValue);
        }
    }

    public string ReturnValue
    {
        get
        {
            string x = "Asset Bundle Has Been Unloaded";
            
            return x;
        }
    }
}
