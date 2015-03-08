using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour
{
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
        numberRows = numberRowsOfTank + numberRowsOfRange + numberRowsOfCAC;
        numberUnitPerRow = unitCount / numberRows;
    }

}
