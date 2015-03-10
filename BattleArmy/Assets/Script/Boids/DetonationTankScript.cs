using UnityEngine;
using System.Collections;

public class DetonationTankScript : MonoBehaviour {

    public GameObject currentDetonator;
    private int _currentExpIdx = -1;
    public GameObject[] detonatorPrefabs;
    public float explosionLife = 10;
    public float timeScale = 1.0f;
    public float detailLevel = 1.0f;

    private void Start()
    {
        if (!currentDetonator) NextExplosion();
        else _currentExpIdx = 0;
    }

    

    private void NextExplosion()
    {
        if (_currentExpIdx >= detonatorPrefabs.Length - 1) _currentExpIdx = 0;
        else _currentExpIdx++;
        currentDetonator = detonatorPrefabs[_currentExpIdx];
    }

    private void Update()
    {

    }

    public void SpawnExplosion(Vector3 positionExplosion)
    {
            Detonator dTemp = (Detonator)currentDetonator.GetComponent("Detonator");

            float offsetSize = dTemp.size / 3;
            Vector3 hitPoint = positionExplosion;
                                     
            GameObject exp = (GameObject)Instantiate(currentDetonator, hitPoint, Quaternion.identity);
            dTemp = (Detonator)exp.GetComponent("Detonator");
            dTemp.detail = detailLevel;

            Destroy(exp, explosionLife);
    }
}
