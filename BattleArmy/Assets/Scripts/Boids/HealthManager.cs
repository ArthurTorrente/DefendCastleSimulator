﻿using UnityEngine;
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

    private int m_curLife;
    public int CurLife
    {
        get { return m_curLife; }
        set { m_curLife = value; }
    }

    private bool m_regenMode;

    [SerializeField]
    private UnityEvent m_regenModeEnable;

    [SerializeField]
    private UnityEvent m_regenModeDisable;

    void Start()
    {
        m_curLife = m_maxLife;

        m_regenMode = false;
    }

    void takeDamage(int value)
    {
        m_curLife = Mathf.Clamp(m_curLife - value, 0, m_maxLife);

        if(!m_regenMode && m_curLife <= 25)
        {
            m_regenMode = true;
        }
    }

    void regenLife(int value)
    {
        m_curLife = Mathf.Clamp(m_curLife - value, 0, m_maxLife);

        if(m_regenMode && m_curLife >= 50)
        {
            m_regenMode = false;
        }
    }
}
