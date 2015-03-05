using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Threading;

public class BoidScript : MonoBehaviour
{
    [SerializeField]
    private Transform m_transform;
    public Transform Transform
    {
        get { return m_transform; }
        set { m_transform = value; }
    }

    private Transform m_target;
    public Transform Target
    {
        get { return m_target; }
        set { m_target = value; }
    }

    public List<BoidScript> m_fightRange;
    public List<BoidScript> m_visionRange;

    public List<BoidScript> m_neighboors;

    private float m_rangeVision;
    public float RangeVision
    {
        get { return m_rangeVision; }
        set { m_rangeVision = value; }
    }

    private float m_fightVision;
    public float FightVision
    {
        get { return m_fightVision; }
        set { m_fightVision = value; }
    }

    private float m_neighboorsVision;
    public float NeighboorsVision
    {
        get { return m_neighboorsVision; }
        set { m_neighboorsVision = value; }
    }
}
