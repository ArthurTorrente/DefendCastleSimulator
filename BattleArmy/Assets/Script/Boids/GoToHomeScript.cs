using UnityEngine;
using System.Collections;

public class GoToHomeScript : MonoBehaviour
{
    [SerializeField]
    public Transform m_base;

    [SerializeField]
    private Transform m_transform;

    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private BoidScript m_boidScript;

    void Update()
    {
        m_animator.SetBool("isFighting", false);
        m_animator.SetFloat("Speed", 3);

         // Calcule la velocity avec une coefficiant random.
         var randomCoef = Random.Range(0, 1);
         var velocity = 3.0f * (1.0f + randomCoef);
         m_animator.SetBool("isFighting", false);
         m_animator.SetFloat("Speed", velocity);
         
        // Initialisation des vecteurs correspondantes aux 3 règles.
        var separation = Vector3.zero;
        var alignment = m_base.forward;
        var cohesion = m_base.position;

        foreach(BoidScript boid in m_boidScript.m_neighboors)
        {
            if ( (boid == this) || !(boid.GetComponent<GoToHomeScript>().enabled))
                continue;

            var t = boid.Transform;
            separation += GetSeparationVector(t);
            alignment += t.forward;
            cohesion += t.position;
        }

        //Division par le nombdre de boids afin de récupérer l'algnement et la cohesion
        var average = 1.0f / m_boidScript.m_neighboors.Count;
        alignment *= average;
        cohesion *= average;
        cohesion = (cohesion - m_transform.position).normalized;

        // Calcule de la direction du boids
        var direction_tmp = separation + alignment + cohesion;
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

        /*
		Vector3 dir = m_base.position - m_transform.position;
        dir.y = 0;
        m_transform.position += dir.normalized * 3 * Time.deltaTime;
		m_transform.LookAt (dir);
         */
    }

    private Vector3 GetSeparationVector(Transform target)
    {
        var diff = m_transform.position - target.position;
        var diffLen = diff.magnitude;
        var scaler = Mathf.Clamp01(1.0f - diffLen / m_boidScript.NeighboorsVision);
        return diff * (scaler / diffLen);
    }
}
