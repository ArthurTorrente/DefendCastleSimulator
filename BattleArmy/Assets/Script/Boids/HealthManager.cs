using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private int m_maxLife;
    public int MaxLife
    {
        get { return m_maxLife; }
        set { m_maxLife = value; }
    }

	[SerializeField]
    private int m_curLife;
    public int CurLife
    {
        get { return m_curLife; }
        set { m_curLife = value; }
    }


    [SerializeField]
    private UnityEvent m_deathEvent;

    void Start()
    {
        m_curLife = m_maxLife;
    }

    public void takeDamage(int value)
    {
        m_curLife = Mathf.Clamp(m_curLife - value, 0, m_maxLife);

        if (m_curLife == 0)
        {
            m_deathEvent.Invoke();
        }
    }

    /*
    public void regenLife(int value)
    {
        m_curLife = Mathf.Clamp(m_curLife - value, 0, m_maxLife);

        if(m_regenMode && m_curLife >= 50)
        {
            m_regenMode = false;
            m_regenModeDisable.Invoke();
        }
    }
    */
}
