using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_audioSource;
    [SerializeField]
    private AudioClip m_audioClip;
    [SerializeField]
    private ArmyManager m_a;

    [SerializeField]
    private ArmyManager m_b;

    [SerializeField]
    private int m_scoreA;

    [SerializeField]
    private int m_scoreB;

    [SerializeField]
    public bool m_isFinishA = false;

    [SerializeField]
    public bool m_isFinishB = false;

    [SerializeField]
    private GameObject m_panelWin;
    [SerializeField]
    private Text m_textWin;

    public void setScore(int value)
    {
        if (value == 0)
            m_scoreB++;
        else
            m_scoreA++;
    }
	public void gameFinish(int value)
    {
        if (value == 0)
            m_isFinishA = true;
        else
            m_isFinishB = true;

        if(m_isFinishA || m_isFinishB)
        {
            m_audioSource.clip = m_audioClip;
            m_audioSource.Play();
            m_panelWin.SetActive(true);
            if(m_scoreB>m_scoreA)
                m_textWin.text = "Win Army B";
            else if (m_scoreA > m_scoreB)
                m_textWin.text = "Win Army A";
            else if (m_scoreB == m_scoreA)
                m_textWin.text = "Egality";
        }
    }
    public void leaveGame()
    {
        Application.LoadLevel(0);
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
