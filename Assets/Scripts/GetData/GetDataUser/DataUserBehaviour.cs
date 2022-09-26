using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataUserBehaviour : MonoBehaviour
{
    [SerializeField] private RepositoryLoginData _repositoryLoginData;

    [SerializeField] private TextMeshProUGUI _usernameText;
    
    [SerializeField] private TextMeshProUGUI _loginDate;
    
    [SerializeField] private TextMeshProUGUI _dateNow;


    private void Awake()
    {
        _usernameText.text = "Welcome : " + _repositoryLoginData.data[0].username;
        
        _loginDate.text = "Last Login : " + _repositoryLoginData.data[0].login_date;

        _dateNow.text = "Date : " + DateTime.Now.ToString();
    }
}
