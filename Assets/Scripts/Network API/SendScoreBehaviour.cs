using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SendScoreBehaviour : MonoBehaviour
{
    [SerializeField] private bool _sendPost;
    [SerializeField] private string url;
    [SerializeField] private int UserId;
    [SerializeField] private SendPOSTMethod _sendPostMethod;
    [SerializeField] private DataVariable _dataVariable;
    [SerializeField] private RepositoryQuizPostData _repositoryQuizPostData;
    [SerializeField] private ContentAreaController _contentAreaController;
    [SerializeField]private LogControllerBehaviour _logControllerBehaviour;
    [SerializeField] private RepositoryLogAnswer _RepositoryLogAnswer;
    [SerializeField] private LogRepository _repositoryLog;
    [SerializeField] private RepositoryLoginData _repositoryLoginData;
    [SerializeField] private IntegerVariable _integerVariable;
    [Space(10)]
    public List<RequestHeader> Header = new List<RequestHeader>();
    /*public string Token;
    public List<InputField> Input = new List<InputField>(3);*/
    //[SerializeField] private  List<RequestHeader> Header = new List<RequestHeader>();
    
    
    private void Awake()
    {

        url = "http://" + _repositoryLoginData.API_URL;
        
        Header = _repositoryLoginData.Header;
        
        if(GetComponent<SendPOSTMethod>()==null)
        {
            _sendPostMethod = FindObjectOfType<SendPOSTMethod>();
            
            if (_sendPostMethod == null)
            {
                _sendPostMethod = gameObject.AddComponent<SendPOSTMethod>();
            }
        }
        else
        {
            Debug.Log("Script SendPostMethod Tidak Ada");
        }
    }

    private void Start()
    {
        //PostNilai();
        /*for (int i = 0; i < _RepositoryQuizPostData.Items.Count; i++)
        {
            var asnwer_id = from results in _RepositoryQuizPostData.Items[i].results
                select results.answer_id;
            
            var asnwer_idList = asnwer_id.ToList();
            
            var question_id = from _results in _RepositoryQuizPostData.Items[i].results
                select _results.question_id;
            var question_idList = question_id.ToList();


            var datatosend = _RepositoryQuizPostData.Items[i].kelas + _RepositoryQuizPostData.Items[i].user_id + asnwer_idList + question_idList 
                             +_RepositoryQuizPostData.Items[i].platform +_RepositoryQuizPostData.Items[i].chapter_id +_RepositoryQuizPostData.Items[i].waktu_pengerjaan 
                             + _RepositoryQuizPostData.Items[i].total_soal+ _RepositoryQuizPostData.Items[i].filename;
            Debug.Log(datatosend);
            string JSON = JsonUtility.ToJson(datatosend);
            _post.SendPOST(url,JSON,(x)=>{Debug.Log("Callback : " + x);},Header.ToDictionary(x=>x.key));
        }*/
    }
    
    /*public void PostNilai()
    {
        if (UserId.ToString() == "")
            Debug.Log("No UserID Found");

        var URL = "http://192.168.100.78/vr-bahasa/public/api/v1/report";
        
        for (int i = 0; i < _contentAreaController.ListContent.Count; i++)
        {
            if (_contentAreaController.ListContent[i].materi_id == _dataVariable.materi_id)
            {
                var datatosend = new DataPostQuizScenario 
                {
                    user_id = UserId, 
                    conversation_topic = _contentAreaController.ListContent[i].conversation_topic, 
                    waktu_pengerjaan = _contentAreaController.ListContent[i].duration, 
                    total_score = _RepositoryLogAnswer.TotalScore, 
                    total_correct = _RepositoryLogAnswer.CorrectAnswer, 
                    total_incorrect = _RepositoryLogAnswer.InCorrectAnswer
                };
                
                string JSON = JsonUtility.ToJson(datatosend);
                
                Debug.Log(JSON);

                if (_sendPost)
                    _sendPostMethod.SendPOST(url,JSON, (x) =>
                    {
                        
                        Debug.Log("Callback : " + x);
                        
                    },_repositoryLoginData.Header.ToDictionary(x=>x.key, x => x.value));
                
            }
        }
    }*/

    
    // dimaz, obsolete, Logs data is included in PostNilaiV2
    /*public void PostLogs()
    {
        //var URL = "http://"+ _repositoryLoginData.API_URL + "/vr-bahasa/public/api/v1/logs";
        //Header.Add(_repositoryLoginData.Header[0]);
        string JSON = JsonUtility.ToJson(_repositoryLog);
            
        Debug.LogWarning(JSON);
        
        if(_sendPost)
            _sendPostMethod.SendPOST(url + "/vr-bahasa/public/api/v1/logs",JSON, (x) =>
            {
                
                Debug.Log("Callback : " + x);
                
            },_repositoryLoginData.Header.ToDictionary(x=>x.key, x => x.value));

    }*/

    public void PostNilaiV2()
    {
        Debug.LogWarning("SENDING NILAI");
        
        StartCoroutine(CoroutinePostNilaiV2());
    }
    public IEnumerator CoroutinePostNilaiV2()
    {
        //yield return new WaitForSeconds(1);
        if (UserId.ToString() == "")
            Debug.Log("No UserID Found");

        //var URL = "http://192.168.101.32/vr-bahasa/public/api/v1/report";
        
        for (int i = 0; i < _contentAreaController.ListContent.Count; i++)
        {
            if (_contentAreaController.ListContent[i].language_id.ToString() == _dataVariable.materi_id)
            {
                Debug.Log("_contentAreaController.ListContent Index: " + i);
                Debug.Log("Available Duration : " + _contentAreaController.ListContent[i].npc[_integerVariable.IntegerValue -1].duration);
                Debug.Log("Remaining Duration : " + (int) _logControllerBehaviour._timerBehaviour._currentDuration);
                var dt = _contentAreaController.ListContent[i].npc[_integerVariable.IntegerValue -1].duration -
                         (int) _logControllerBehaviour._timerBehaviour._currentDuration;
                Debug.Log(dt);
                var datatosend = new DataPostScenario 
                {
                    user_id = _repositoryLoginData.data[0].id, 
                    scenario_id = _contentAreaController.ListContent[i].npc[_integerVariable.IntegerValue - 1].id,
                    language_id = _contentAreaController.ListContent[i].language_id,
                    
                    //conversation_topic = _contentAreaController.ListContent[i].npc[_integerVariable.IntegerValue - 1].conversation_topic, 
                     
                    total_score = _RepositoryLogAnswer.TotalScore, 
                    duration = _contentAreaController.ListContent[i].npc[_integerVariable.IntegerValue - 1].duration,
                    correct = _RepositoryLogAnswer.CorrectAnswer, 
                    incorrect = _RepositoryLogAnswer.InCorrectAnswer,
                    //duration_taken = _contentAreaController.ListContent[i].duration - Int32.Parse(_repositoryLog.logs.Items.Last().duration),_logControllerBehaviour._timerBehaviour._currentDuration
                    duration_taken = (int)_logControllerBehaviour._timerBehaviour._currentDuration - _contentAreaController.ListContent[i].npc[_integerVariable.IntegerValue - 1].duration,
                    //content_id = _repositoryLog.content_id,
                    //asnwer_status = _repositoryLog.logs.Items[i].answer_status,
                    logs = _repositoryLog.logs
                };
                
                string JSON = JsonUtility.ToJson(datatosend);
                
                Debug.Log(JSON);

                if (_sendPost)
                    _sendPostMethod.SendPOST(url + "/vr_bahasa_v2/public/api/v1/report",JSON, (x) =>
                    {
                        
                        Debug.Log("Callback : " + x);
                        
                    },Header.ToDictionary(x=>x.key, x => x.value));
                
            }
        }
        
        yield return null;
    }
}



[Serializable]
public class DataPostQuizScenario
{
    public int user_id;
    public string conversation_topic;
    public int waktu_pengerjaan;
    public double total_score;
    public int total_correct;
    public int total_incorrect;
}

[Serializable]
public class DataPostScenario
{
    public int user_id;
    public int scenario_id;
    public int language_id;
    //public string conversation_topic;
    public double total_score;
    public int duration;
    public int duration_taken;
    
    public int correct;
    public int incorrect;
    
    //public int content_id;
    //public bool asnwer_status;
    public List<Log> logs;
}

