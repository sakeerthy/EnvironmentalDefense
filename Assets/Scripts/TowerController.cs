using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {

    public float towerRange;
    public float delay;
    public bool isPlaced;
    public bool inDelay;

	// Use this for initialization
	void Start () {
        isPlaced = false;
        inDelay = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaced)
        {
            if (!inDelay)
            {
                detectEnemy();
            }
        }
	}

    void detectEnemy()
    {
        Collider2D enemyHit = null;

        enemyHit = Physics2D.OverlapCircle(transform.position, towerRange);

        if (enemyHit)
        {
            if (enemyHit.gameObject.CompareTag("enemy"))
            {
                fire(enemyHit.gameObject, enemyHit);
                inDelay = true;
                StartCoroutine(fireDelay());
            }
        }
    }

    void fire(GameObject enemy, Collider2D enemyHit)
    {
        enemy.GetComponent<EnemyMovement>().subtractHealth(5, enemyHit);
    }

    IEnumerator fireDelay()
    {

        yield return new WaitForSeconds(1 * delay);
        inDelay = false;
        
    }

}
