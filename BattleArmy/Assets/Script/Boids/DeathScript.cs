using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DeathScript : MonoBehaviour 
{
    [SerializeField]
    private Collider m_collider;

    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private UnityEvent m_afterDeath;

    public UnityEvent AfterDeath
    {
        get { return m_afterDeath; }
        set { m_afterDeath = value; }
    }

    void OnEnable()
    {
        m_animator.SetBool("isFighting", false);
        m_animator.SetFloat("Speed", 0);
        m_animator.SetTrigger("Death");
        m_afterDeath.Invoke();
        m_collider.enabled = false;
        //StartCoroutine(death());
    }

    IEnumerator death()
    {
        m_animator.SetTrigger("Death");
		yield return new WaitForSeconds (3.35f);
        m_afterDeath.Invoke();
        m_collider.enabled = false;
		// Destroy (gameObject);
    }

}
