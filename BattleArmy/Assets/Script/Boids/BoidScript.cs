using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Threading;

public class BoidScript : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;

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


    [SerializeField]
    private DetonationTankScript m_detonation = null;

    private float timestamp = 0;

	private bool m_run = false;
    private float m_separationFactor = 1.0f;
    private float m_alignementFactor = 1.0f;
    private float m_cohesionFactor = 1.0f;

    void Start()
    {
        if(SettingsManager.Instance == null)
            return;

        m_separationFactor = SettingsManager.Instance.getSepationFactor();
        m_alignementFactor = SettingsManager.Instance.getAlignementFactor();
        m_cohesionFactor = SettingsManager.Instance.getCohesionFactor();
    }

    void Update()
    {
        if (m_fightRange != null)       // Je peux taper une cible
        {
			m_run = false;
			timestamp += Time.deltaTime;
            if (timestamp >= m_attackSpeed)
            {
                m_fightRange.HealthManager.takeDamage(m_attackDamage);
                m_animator.SetFloat("Speed", 0);
                m_animator.SetTrigger("StartFight");
				m_animator.SetBool("isFighting", true);
                if (m_detonation!=null)
                    m_detonation.SpawnExplosion(m_fightRange.m_transform.position);
				timestamp = 0;
            }            
        }
        else if (m_visionRange != null) // Je vois qqun donc je me dirige vers elle
        {
			Vector3 dir = m_visionRange.Transform.position - m_transform.position;
			dir.y = 0;
			if(!m_run)
			{
				m_run = true;
                m_animator.SetBool("isFighting", false);
                m_animator.SetFloat("Speed", m_velocity);
			}
				
            m_transform.position += dir.normalized * m_velocity * Time.deltaTime;
			m_transform.LookAt(m_visionRange.Transform.position);
        }
        else                            // Je me déplace en mode boids
        {
			/*Vector3 dir = (m_opposingBase.position - m_transform.position);
			dir.y = 0;
			if(!m_run)
			{
				m_run = true;
				m_animator.SetFloat("Velocity", 3);
			}

            m_transform.position += dir.normalized * m_velocity * Time.deltaTime;
			m_transform.LookAt(m_opposingBase.position);*/


            // Calcule la velocity avec une coefficiant random.
            var randomCoef = Random.Range(0, 1);
            var velocity = m_velocity * (1.0f + randomCoef);
            m_animator.SetBool("isFighting", false);
            m_animator.SetFloat("Speed", velocity);
            // Initialisation des vecteurs correspondantes aux 3 règles.
            var separation = Vector3.zero;
            var alignment = m_opposingBase.forward;
            var cohesion = m_opposingBase.position;

            foreach(BoidScript boid in m_neighboors)
            {
                if (boid == this)
                    continue;

                var t = boid.m_transform;
                separation += GetSeparationVector(t);
                alignment += t.forward;
                cohesion += t.position;
            }

            //Division par le nombdre de boids afin de récupérer l'algnement et la cohesion
            var average = 1.0f / m_neighboors.Count;
            alignment *= average;
            cohesion *= average;
            cohesion = (cohesion - m_transform.position).normalized;

            // Calcule de la direction du boids
            var direction_tmp = separation * m_separationFactor + alignment * m_alignementFactor + cohesion * m_cohesionFactor;
            var direction = new Vector3(direction_tmp.x, 0, direction_tmp.z);//separation + alignment + cohesion;
            var rotation = Quaternion.FromToRotation(Vector3.forward, direction.normalized);

            // Smooth du changement de direction
            if (rotation != m_transform.rotation)
            {
                Quaternion rotation_tmp = Quaternion.Slerp(rotation, m_transform.rotation, 0.8f);
                transform.rotation = new Quaternion(0, rotation_tmp.y, 0, rotation_tmp.w);

            }

            // Déplacement du boids.
            Vector3 transform_tmp = m_transform.position + m_transform.forward * (velocity * Time.deltaTime);
            transform.position = new Vector3(transform_tmp.x, transform.position.y, transform_tmp.z);
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
        m_animator.SetBool("Win", true);
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
