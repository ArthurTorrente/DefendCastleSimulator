using UnityEngine;
using System.Collections;

public class SettingsManager : MonoBehaviour {

    public static SettingsManager Instance { get; private set; }

    private float _separationFactor = 1.0f;
    private float _alignementFactor = 1.0f;
    private float _cohesionFactor = 1.0f;

    private int _nbUnityA = 100;
    private int _rowCacA = 1;
    private int _rowRiffleA = 1;
    private int _rowTankA = 1;

    private int _nbUnityB = 100;
    private int _rowCacB = 1;
    private int _rowRiffleB = 1;
    private int _rowTankB = 1;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
    }

	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLevel(string name)
    {
        Application.LoadLevel(name);
    }
    //Setters
    public void setSepationFactor(float value){_separationFactor = value;}
    public void setAlignementFactor(float value) { _alignementFactor = value; }
    public void setCohesionFactor(float value) { _cohesionFactor = value; }

    public void setNbUnityA(int value) { _nbUnityA = value; }
    public void setRowCacA(int value) { _rowCacA = value; }
    public void setRowRiffleA(int value) { _rowRiffleA = value; }
    public void setRowTankA(int value) { _rowTankA = value; }

    public void setNbUnityA(float value) { _nbUnityA = (int)value; }
    public void setRowCacA(float value) { _rowCacA = (int)value; }
    public void setRowRiffleA(float value) { _rowRiffleA = (int)value; }
    public void setRowTankA(float value) { _rowTankA = (int)value; }

    public void setNbUnityB(int value) { _nbUnityB = value; }
    public void setRowCacB(int value) { _rowCacB = value; }
    public void setRowRiffleB(int value) { _rowRiffleB = value; }
    public void setRowTankB(int value) { _rowTankB = value; }

    public void setNbUnityB(float value) { _nbUnityB = (int)value; }
    public void setRowCacB(float value) { _rowCacB = (int)value; }
    public void setRowRiffleB(float value) { _rowRiffleB = (int)value; }
    public void setRowTankB(float value) { _rowTankB = (int)value; }

    //Getters
    public float getSepationFactor() { return _separationFactor; }
    public float getAlignementFactor() { return _alignementFactor; }
    public float getCohesionFactor() { return _cohesionFactor; }

    public int getNbUnityA() { return _nbUnityA; }
    public int getRowCacA() { return _rowCacA; }
    public int getRowRiffleA() { return _rowRiffleA; }
    public int getRowTankA() { return _rowTankA; }

    public int getNbUnityB() { return _nbUnityB; }
    public int getRowCacB() { return _rowCacB; }
    public int getRowRiffleB() { return _rowRiffleB; }
    public int getRowTankB() { return _rowTankB; }
}
