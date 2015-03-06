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
        m_animator.SetBool(m_deathAnim, true);
        yield return new WaitForSeconds(m_animator.GetCurrentAnimatorClipInfo(m_animator.GetLayerIndex(m_deathAnim))[0].clip.length);
        m_afterDeath.Invoke();
    }

}
