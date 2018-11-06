using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies; //holds objects 



    public Vector3 spawnValues; //for locations 

    public float spawnWait;
    public int startWait;

    public float spawnMostWait;
    public float spawnLeastWait;

    int randEnemy;
    int timer = 0;

    void Start()
    {

        StartCoroutine(waitSpawner());
    }

    void Update()
    { //updates every frame 

        spawnWait = 5;
        timer++;
        if (timer > 60)
        {
            StartCoroutine(waitSpawner());
            timer = 0;
        }
    }

    IEnumerator waitSpawner()
    {

        //yield return  new WaitForSeconds(startWait); //purpose: incriments time and holds function
        //argument = amount of time
        //while (true) {

        randEnemy = Random.Range(0, enemies.Length); //argument depends on which enemy you want to spawn

        Vector3 spawnPos = new Vector3(-10, Random.Range(-3.0f, 3.0f), 0); //x,y,z

        Instantiate(enemies[randEnemy], spawnPos, gameObject.transform.rotation);
        yield return 0;
        //}
    }
}
