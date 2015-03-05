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
        m_transform.position += m_base.position.normalized * Time.deltaTime;
    }
}
