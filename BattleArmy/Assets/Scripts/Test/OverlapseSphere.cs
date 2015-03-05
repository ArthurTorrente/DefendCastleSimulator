using UnityEngine;
using System.Collections;

public class OverlapseSphere : MonoBehaviour {

    [SerializeField]
    private LayerMask m_layer;

	void Start ()
    {
        if (Physics.OverlapSphere(transform.position, 1, m_layer).Length > 0)
            Debug.Log("Have collide");
        else
            Debug.Log("NO !!");
	}
}
