using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    [HideInInspector]
    public string objectID;
    public bool debug = false;

    public bool uniqueObject;

    void Awake()
    {
        objectID = name + transform.position.ToString();
    }

    void Start()
    {
        Scene scene = gameObject.scene;
        string sceneName;
        sceneName = scene.name;


        if (uniqueObject == true)
        {
            if (scene.buildIndex == SceneManager.GetActiveScene().buildIndex)
            {
                for (int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++)
                {
                    if (FindObjectsOfType<DontDestroy>()[i] != this)
                    {
                        if (FindObjectsOfType<DontDestroy>()[i].objectID == objectID)
                        {
                            Destroy(gameObject);
                        }
                    }
                }
                if (debug == true)
                {
                    Debug.Log("Added to Don't Destroy: " + gameObject.name);
                }
                DontDestroyOnLoad(gameObject);
            }
            if (scene.buildIndex != SceneManager.GetActiveScene().buildIndex)
            {
                Destroy(gameObject);
                if (debug == true)
                {
                    Debug.Log("Destroy " + name + " " + sceneName);
                }
            }
        }

        else if (uniqueObject == false)
        {
            for (int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++)
            {
                if (FindObjectsOfType<DontDestroy>()[i] != this)
                {
                    if (FindObjectsOfType<DontDestroy>()[i].objectID == objectID)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            DontDestroyOnLoad(gameObject);
        }
    }
}
