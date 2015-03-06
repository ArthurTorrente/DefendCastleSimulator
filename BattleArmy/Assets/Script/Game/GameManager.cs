using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_audioSource;

    [SerializeField]
    private ArmyManager m_a;

    [SerializeField]
    private ArmyManager m_b;

	void gameFinish()
    {

    }

    public void baseDead(int baseId)
    {
        if(baseId == 1)
        {
            m_b.winAnimAllBoid();
        }
        else if(baseId == 2)
        {
            m_a.winAnimAllBoid();
        }

        m_audioSource.Play();
    }
}
