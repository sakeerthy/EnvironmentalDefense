using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies; //holds objects 



    public Vector3 spawnValues; //for locations 

    public float spawnWait;
    public float hardSpawnWait;
    public float veryHardSpawnWait;
    public int startWait;

    public float spawnMostWait;
    public float spawnLeastWait;

    int randEnemy;
    int timer = 0;

    public GameObject happiness;
    float health;
    float rangeMax = 1.0f;
    float rangeMin = -1.0f;

    void Start()
    {
        StartCoroutine(waitSpawner());
        spawnWait = 60;
    }

    void Update()
    { //updates every frame 

        timer++;
        if (timer > spawnWait)
        {
            StartCoroutine(waitSpawner());
            timer = 0;
        }
        health = happiness.GetComponent<happiness>().health;

        if (health > 80)
        {
            rangeMax = 3.0f;
            rangeMin = -3.0f;
            spawnWait = veryHardSpawnWait;
              
        } else if (health > 60) {
            rangeMax = 2.0f;
            rangeMin = -2.0f;
            spawnWait = hardSpawnWait;
        }
    }

    IEnumerator waitSpawner()
    {

        //yield return  new WaitForSeconds(startWait); //purpose: incriments time and holds function
        //argument = amount of time
        //while (true) {

        randEnemy = Random.Range(0, enemies.Length); //argument depends on which enemy you want to spawn

        Vector3 spawnPos = new Vector3(-13, Random.Range(rangeMin, rangeMax), 0); //x,y,z

        Instantiate(enemies[randEnemy], spawnPos, gameObject.transform.rotation);
        yield return 0;
        //}
    }
}
