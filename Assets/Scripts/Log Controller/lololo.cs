using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lololo : MonoBehaviour
{
    [SerializeField] private DataVariable _dataVariable;
    
    [SerializeField] private RepositoryContentArea _repositoryContentArea;
    
    [SerializeField] private RepositoryPassingGrade _repositoryPassingGrade;
    
    public List<DataContentArea> ListContent = new List<DataContentArea>();
    void Start()
    {
        /*for (int i = 0; i < _repositoryContentArea.Items.Count; i++)
        {
            if (_repositoryContentArea.Items[i].chapter_id.Equals(_dataVariable.chapter_id) && _repositoryContentArea.Items[i].materi_id.Equals(_dataVariable.materi_id))
            {
                ListContent.Add(_repositoryContentArea.Items[i]);
            }
        }*/

        for (int i = 0; i < _repositoryPassingGrade.Items.Count; i++)
        {
            if (_repositoryPassingGrade.Items[i].content_id==_dataVariable.exam_id)
            {
                Debug.Log(_repositoryPassingGrade.Items[i].pass_grade);
            }
        }

    }

}
