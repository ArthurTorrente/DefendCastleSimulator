﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class ArmyManager : MonoBehaviour
{
    [SerializeField]
    private Settings m_settings;

    [SerializeField]
    private Transform m_prefabsBoid;

    [SerializeField]
    private Transform m_base;

    [SerializeField]
    private LayerMask m_layer;

    [SerializeField]
    private ArmyManager m_opposing;

    [SerializeField]
    private LayerMask m_opposingLayer;

    private List<BoidScript> m_units;

    void Start()
    {
        m_units = new List<BoidScript>();

        SpawnTest();
    }

    public void SpawnTest()
    {
        for (var i = 0; i < m_settings.unitCount; i++)
        {
            var randomRange = Random.Range(0, 30);
            var angle = i * Mathf.PI * 2 / m_settings.unitCount;
            var pos = m_base.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * randomRange;

            BoidScript boid = (Instantiate(m_prefabsBoid, pos, Quaternion.identity) as Transform).GetComponent<BoidScript>();

            boid.Base = m_base;
            boid.OpposingBase = m_opposing.m_base;
            boid.GotoHome.m_base = m_base;

            boid.DeathScript.AfterDeath.AddListener(
                delegate
                {
                    deleteBoid(boid);
                }
                );

            //Instanciate
            m_units.Add(boid);
        }
    }

    void Update()
    {
        foreach(BoidScript boid in m_units)
        {
            //Calculs des targets

            getVision(boid);

            if(boid.m_fightRange == null && boid.m_visionRange == null)
                getNeighboors(boid);
        }
    }

    private void getVision(BoidScript boid)
    {
        var fightRange = Physics.OverlapSphere(boid.Transform.position, boid.FightVision, m_opposingLayer);

        boid.m_fightRange = null;
        float minDist = float.MaxValue;

        foreach (Collider boidVision in fightRange)
        {
            BoidScript curBoid = boidVision.GetComponent<BoidScript>();
            float curDist = Vector3.Distance(curBoid.Transform.position, boid.Transform.position);

            if(curDist < minDist)
            {
                boid.m_fightRange = curBoid;
                minDist = curDist;
            }
            
        }

        if (boid.m_fightRange == null)
        {
            var visionRange = Physics.OverlapSphere(boid.Transform.position, boid.RangeVision, m_opposingLayer);

            boid.m_visionRange = null;
            minDist = float.MaxValue;

            foreach (Collider boidVision in visionRange)
            {
                BoidScript curBoid = boidVision.GetComponent<BoidScript>();
                float curDist = Vector3.Distance(curBoid.Transform.position, boid.Transform.position);

                if (curDist < minDist)
                {
                    boid.m_visionRange = curBoid;
                    minDist = curDist;
                }
            }
        }
    }

    private void getNeighboors(BoidScript boid)
    {
        var neighboors = Physics.OverlapSphere(boid.Transform.position, boid.NeighboorsVision, m_layer);

        boid.m_neighboors.Clear();

        foreach (Collider boidVision in neighboors)
            boid.m_neighboors.Add(boidVision.GetComponent<BoidScript>());
    }

    public void deleteBoid(BoidScript boid)
    {
        m_units.Remove(boid);
    }

    public void winAnimAllBoid()
    {
        foreach (BoidScript boid in m_units)
            boid.launchWinAnim();
    }
}
