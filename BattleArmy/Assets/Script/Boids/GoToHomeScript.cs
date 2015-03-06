using UnityEngine;
using System.Collections;

public class GoToHomeScript : MonoBehaviour
{
    [SerializeField]
    public Transform m_base;

    [SerializeField]
    private Transform m_transform;

    void Update()
    {
		Vector3 dir = m_base.position - m_transform.position;
        m_transform.position += dir.normalized * Time.deltaTime;
		m_transform.LookAt (dir);
    }
}
