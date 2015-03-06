using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Threading;

public class BoidScript : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private string m_winAnimName;

    [SerializeField]
    private Transform m_transform;
    public Transform Transform
    {
        get { return m_transform; }
        set { m_transform = value; }
    }

    [SerializeField]
    private Transform m_target;
    public Transform Target
    {
        get { return m_target; }
        set { m_target = value; }
    }

    [SerializeField]
    private Transform m_base;
    public Transform Base
    {
        get { return m_base; }
        set { m_base = value; }
    }

    [SerializeField]
    private Transform m_opposingBase;

    public Transform OpposingBase
    {
        get { return m_opposingBase; }
        set { m_opposingBase = value; }
    }
    

    public BoidScript m_fightRange;
    public BoidScript m_visionRange;
    public List<BoidScript> m_neighboors;

    [SerializeField]
    private float m_rangeVision;
    public float RangeVision
    {
        get { return m_rangeVision; }
        set { m_rangeVision = value; }
    }

    [SerializeField]
    private float m_fightVision;
    public float FightVision
    {
        get { return m_fightVision; }
        set { m_fightVision = value; }
    }

    [SerializeField]
    private float m_neighboorsVision;
    public float NeighboorsVision
    {
        get { return m_neighboorsVision; }
        set { m_neighboorsVision = value; }
    }

    [SerializeField]
    private HealthManager _healthManager;
    public HealthManager HealthManager
    {
        get { return _healthManager; }
        set { _healthManager = value; }
    }

    [SerializeField]
    private DeathScript m_deathScript;
    public DeathScript DeathScript
    {
        get { return m_deathScript; }
        set { m_deathScript = value; }
    }

    [SerializeField]
    private GoToHomeScript m_gotoHome;
    public GoToHomeScript GotoHome
    {
        get { return m_gotoHome; }
        set { m_gotoHome = value; }
    }

    [SerializeField]
    private float m_velocity = 1.0f;

    [SerializeField]
    private int m_attackDamage = 10;

    [SerializeField]
    private float m_attackSpeed = 0.5f;

    private float timestamp = 0;

    void Update()
    {
        if (m_fightRange != null)       // Je peux taper une cible
        {
            if (timestamp == 0 || Time.time >= timestamp + m_attackSpeed)
            {
                m_fightRange.HealthManager.takeDamage(m_attackDamage);
                timestamp = Time.time;
            }            
        }
        else if (m_visionRange != null) // Je vois qqun donc je me dirige vers elle
        {
            m_transform.position += (m_visionRange.Transform.position - m_transform.position).normalized * m_velocity * Time.deltaTime;
        }
        else                            // Je me déplace en mode boids
        {
            m_transform.position += (m_opposingBase.position - m_transform.position).normalized * m_velocity * Time.deltaTime;
        }

    }

    private Vector3 GetSeparationVector(Transform target)
    {
        var diff = m_transform.position - target.position;
        var diffLen = diff.magnitude;
        var scaler = Mathf.Clamp01(1.0f - diffLen / m_neighboorsVision);
        return diff * (scaler / diffLen);
    }

    public void launchWinAnim()
    {
        m_animator.SetBool(m_winAnimName, true);
    }

    /*
     * var currentPosition = m_transform.position;
            var currentRotation = m_transform.rotation;

            var separation = Vector3.zero;
            var alignment = -m_base.forward;
            var cohesion = m_base.position;

            foreach (BoidScript boid in m_neighboors)
            {
                // TODO: s'ignorer
                var t = boid.Transform;
                separation += GetSeparationVector(t);
                alignment += t.forward;
                cohesion += t.position;
            }

            //Division par le nombdre de boids afin de récupérer l'algnement et la cohesion
            var average = 1.0f / ((m_neighboors.Count > 0) ? m_neighboors.Count : 1.0f);
            alignment *= average;
            cohesion *= average;
            cohesion = (cohesion - currentPosition).normalized;

            // Calcule de la direction du boids
            var direction_tmp = separation + alignment + cohesion;
            var direction = new Vector3(direction_tmp.x, 0, direction_tmp.z);//separation + alignment + cohesion;
            var rotation = Quaternion.FromToRotation(Vector3.forward, direction.normalized);

            // Smooth du changement de direction
            if (rotation != currentRotation)
            {
                Quaternion rotation_tmp = Quaternion.Slerp(rotation, currentRotation, 0.8f);
                transform.rotation = new Quaternion(0, rotation_tmp.y, 0, rotation_tmp.w);

            }

            // Déplacement du boids.
            Vector3 transform_tmp = currentPosition + m_transform.forward * (m_velocity * Time.deltaTime);
            transform.position = new Vector3(transform_tmp.x, transform.position.y, transform_tmp.z);
     */
}
