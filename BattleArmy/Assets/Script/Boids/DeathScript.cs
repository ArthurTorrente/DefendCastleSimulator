using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DeathScript : MonoBehaviour 
{
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private string m_deathAnim;

    [SerializeField]
    private UnityEvent m_afterDeath;

    public UnityEvent AfterDeath
    {
        get { return m_afterDeath; }
        set { m_afterDeath = value; }
    }

    void OnEnable()
    {
        StartCoroutine(death());
    }

    IEnumerator death()
    {
        m_animator.SetTrigger(m_deathAnim);
		yield return new WaitForSeconds (3.35f);
        m_afterDeath.Invoke();
		Destroy (gameObject);
    }

}
