using UnityEngine;
using System.Collections;

public class BaseScript : MonoBehaviour 
{
    [SerializeField]
    public int typeUnit; //couleur team : 0 => blue      1 => red

    [SerializeField]
    private Transform m_transform;
    public Transform Transform
    {
        get { return m_transform; }
        set { m_transform = value; }
    }

    [SerializeField]
    private HealthManager m_healthManager;
    public HealthManager HealthManager
    {
        get { return m_healthManager; }
        set { m_healthManager = value; }
    }
    [SerializeField]
    private GameManager m_gameManager;

    [SerializeField]
    private ArmyManager m_armyManager;
    public void OnTriggerEnter(Collider collider)
    {
        m_gameManager.setScore(typeUnit);
        m_armyManager.deleteBoid(collider.gameObject.GetComponent<BoidScript>());
    }
}
