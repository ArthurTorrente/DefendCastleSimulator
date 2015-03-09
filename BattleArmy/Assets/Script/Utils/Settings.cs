using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour
{
    [SerializeField]
    public int typeUnit; //couleur team : 0 => blue      1 => red
    
    [SerializeField]
    public int unitCount;

    [SerializeField]
    public int numberRowsOfTank;

    [SerializeField]
    public int numberRowsOfRange;

    [SerializeField]
    public int numberRowsOfCAC;

    private int numberRows;
    public int NumberRows
    {
        get { return numberRows; }
        set { numberRows = value; }
    }

    private int numberUnitPerRow;
    public int NumberUnitPerRow
    {
        get { return numberUnitPerRow; }
        set { numberUnitPerRow = value; }
    }

    void Start()
    {
        if (typeUnit == 0 && SettingsManager.Instance != null)
        {
            unitCount = SettingsManager.Instance.getNbUnityA();
            numberRowsOfTank = SettingsManager.Instance.getRowTankA();
            numberRowsOfRange = SettingsManager.Instance.getRowRiffleA();
            numberRowsOfCAC = SettingsManager.Instance.getRowCacA();
        }
        else if (typeUnit == 1 && SettingsManager.Instance != null)
        {
            unitCount = SettingsManager.Instance.getNbUnityB();
            numberRowsOfTank = SettingsManager.Instance.getRowTankB();
            numberRowsOfRange = SettingsManager.Instance.getRowRiffleB();
            numberRowsOfCAC = SettingsManager.Instance.getRowCacB();
        }
        numberRows = numberRowsOfTank + numberRowsOfRange + numberRowsOfCAC;
        numberUnitPerRow = unitCount / numberRows;
    }

}
