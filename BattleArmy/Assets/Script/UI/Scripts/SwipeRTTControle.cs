using UnityEngine;
using System.Collections;

public class SwipeRTTControle : MonoBehaviour {

    [SerializeField]
    GameObject[] m_arrayRTTCamera = new GameObject[3];
    int _idActiveRTT = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClickButton(int action) // -1: previous   1 : next
    {
        int id = _idActiveRTT + action;
        _idActiveRTT = (id < 0) ? (m_arrayRTTCamera.Length - 1) : ((id > m_arrayRTTCamera.Length - 1) ? 0 : id);
        for(int i = 0; i < m_arrayRTTCamera.Length; i++)
        {
            if (i == _idActiveRTT)
                m_arrayRTTCamera[i].SetActive(true);
            else
                m_arrayRTTCamera[i].SetActive(false);     
        }
    }
    public void OnClickButtonSet(int action) // -1: previous   1 : next
    {
        _idActiveRTT = action;
        for (int i = 0; i < m_arrayRTTCamera.Length; i++)
        {
            if (i == _idActiveRTT)
                m_arrayRTTCamera[i].SetActive(true);
            else
                m_arrayRTTCamera[i].SetActive(false);
        }
    }
}
