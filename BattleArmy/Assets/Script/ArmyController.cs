using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ArmyController : MonoBehaviour
{

    private List<GameObject> boidsArray = new List<GameObject>();
    
    public GameObject boidPrefab;

    public Text[] slidersText = new Text[4];
    public Slider[] slider = new Slider[4];

    public int spawnCount = 10;

    public float spawnRadius = 4.0f;

    [Range(0.0f, 6.0f)]
    public float velocity = 0.0f;

    [Range(0.0f, 0.9f)]
    public float velocityVariation = 0.5f;

    [Range(0.1f, 30.0f)]
    public float neighborDist = 2.0f;

    public LayerMask searchLayer;

    void Start()
    {

        /*slidersText[0].text = spawnCount.ToString();
        slidersText[1].text = spawnRadius.ToString();
        slidersText[2].text = velocity.ToString();
        slidersText[3].text = neighborDist.ToString();

        slider[0].value = spawnCount;
        slider[1].value = spawnRadius;
        slider[2].value = velocity;
        slider[3].value = neighborDist;*/

        //for (var i = 0; i < spawnCount; i++) Spawn();
        SpawnTest();
    }


    public void SpawnTest()
    {
        for (var i = 0; i < spawnCount; i++)
        {
            var randomRange = Random.Range(0, 30);
            var angle = i * Mathf.PI * 2 / spawnCount;
            var pos = transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * randomRange;
            GameObject boid = Instantiate(boidPrefab, pos, Quaternion.identity) as GameObject;
            boid.GetComponent<ArmyFollow>().controller = this;
            boidsArray.Add(boid);
        }
    }














    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
    public GameObject SpawnCircle(Vector3 position)
    {
        Vector3 pos = RandomCircle(position, 5.0f);
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, position - pos);
        var boid = Instantiate(boidPrefab, pos, rot) as GameObject;
        boid.GetComponent<ArmyFollow>().controller = this;
        boidsArray.Add(boid);
        return boid;
    }

    public GameObject Spawn()
    {
        return Spawn(transform.position + Random.insideUnitSphere * spawnRadius);
    }

    public GameObject Spawn(Vector3 position)
    {
        var rotation = Quaternion.Slerp(transform.rotation, Random.rotation, 0.3f);
        var boid = Instantiate(boidPrefab, position, rotation) as GameObject;
        boid.GetComponent<ArmyFollow>().controller = this;
        boidsArray.Add(boid);
        return boid;
    }
   
    public void setSpawnCountAndLabel()
    {
        spawnCount = (int)slider[0].value;
        slidersText[0].text = slider[0].value.ToString();
    }

    public void setSpawnRadiusAndLabel()
    {
        spawnRadius = slider[1].value;
        slidersText[1].text = slider[1].value.ToString();
    }

    public void setVelocityAndLabel()
    {
        velocity = slider[2].value;
        slidersText[2].text = slider[2].value.ToString();
    }

    public void setNeighborDistAndLabel()
    {
        neighborDist = slider[3].value; 
        slidersText[3].text = slider[3].value.ToString();
    }

    public void resetBoids()
    {
        for (int i = 0; i < boidsArray.Count; i++)
        {
            Destroy(boidsArray[i]);
        }
        boidsArray = new List<GameObject>();
        for (var i = 0; i < spawnCount; i++) Spawn();
    }

    public void leaveGame()
    {
        Application.Quit();
    }
}
