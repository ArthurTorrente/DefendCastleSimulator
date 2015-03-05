using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour 
{
    [SerializeField]
    private Transform m_transform;
    public Transform Transform
    {
        get { return m_transform; }
        set { m_transform = value; }
    }

    [SerializeField]
    private HealthManager m_healthManager;
    public HealthManager HealthManager
    {
        get { return m_healthManager; }
        set { m_healthManager = value; }
    }

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
