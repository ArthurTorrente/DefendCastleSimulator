using UnityEngine;
using System.Collections;

public class GoToHomeScript : MonoBehaviour
{
    [SerializeField]
    public Transform m_base;

    [SerializeField]
    private Transform m_transform;

    [SerializeField]
    private Animator m_animator;


    void Update()
    {
        m_animator.SetBool("isFighting", false);
        m_animator.SetFloat("Speed", 3);
		Vector3 dir = m_base.position - m_transform.position;
        dir.y = 0;
        m_transform.position += dir.normalized * 3 * Time.deltaTime;
		m_transform.LookAt (dir);
    }
}
