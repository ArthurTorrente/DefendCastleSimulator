using UnityEngine;
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

        //Instancie tous les boids
        for(int i = 0; i < m_settings.unitCount; ++i)
        {
            //Instanciate
            m_units.Add(((Transform)Instantiate(m_prefabsBoid, m_base.position, m_base.rotation)).GetComponent<BoidScript>());
        }
    }

    void Update()
    {
        foreach(BoidScript boid in m_units)
        {
            //Calculs des targets

            getVision(boid);
            getNeighboors(boid);

            //Calcul des behavior
            /*if (false)                                  // Olol j'ai plus beaucoup de vie, faut que je rentre
            {
                boid.Target = m_base;
            }*/
            if (boid.m_fightRange.Count != 0)          // Test de je peux taper qqun -> plus de déplacement, je le défonce
            {
                boid.Target = getNearestEnemyInFightRange(boid);
            }
            else if (boid.m_visionRange.Count != 0)         // Test de je vois qqun -> il devient ma target
            {
                boid.Target = getNearestEnemyInVisionRange(boid);
            }
            else                                            // Sinon je fonce sur la base ennemie
            {
                boid.Target = m_opposing.m_base;
            }
        }
    }

    private void getVision(BoidScript boid)
    {
        var visionRange = Physics.OverlapSphere(boid.Transform.position, boid.RangeVision, m_opposingLayer);

        boid.m_visionRange.Clear();

        foreach (Collider boidVision in visionRange)
            boid.m_visionRange.Add(boidVision.GetComponent<BoidScript>());

        var fightRange = Physics.OverlapSphere(boid.Transform.position, boid.FightVision, m_opposingLayer);
        
        boid.m_fightRange.Clear();

        foreach (Collider boidVision in fightRange)
            boid.m_fightRange.Add(boidVision.GetComponent<BoidScript>());
    }

    private void getNeighboors(BoidScript boid)
    {
        var neighboors = Physics.OverlapSphere(boid.Transform.position, boid.NeighboorsVision, m_layer);

        boid.m_neighboors.Clear();

        foreach (Collider boidVision in neighboors)
            boid.m_neighboors.Add(boidVision.GetComponent<BoidScript>());
    }

    private Transform getNearestEnemyInFightRange(BoidScript bs)
    {
        Transform t = bs.Target;
        float currDistance = Vector3.Distance(bs.Transform.position, bs.Target.position), distance;
        foreach (BoidScript enemy in bs.m_fightRange)
        {
            distance = Vector3.Distance(bs.Transform.position, enemy.Transform.position);
            if (distance < currDistance)
            {
                t = enemy.Transform;
                currDistance = distance;
            }
        }

        return t;
    }

    private Transform getNearestEnemyInVisionRange(BoidScript bs)
    {
        Transform t = bs.Target;
        float currDistance = Vector3.Distance(bs.Transform.position, bs.Target.position), distance;
        foreach (BoidScript enemy in bs.m_visionRange)
        {
            distance = Vector3.Distance(bs.Transform.position, enemy.Transform.position);
            if (distance < currDistance)
            {
                t = enemy.Transform;
                currDistance = distance;
            }
        }

        return t;
    }
}
