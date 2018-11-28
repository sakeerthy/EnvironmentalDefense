using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPosition : MonoBehaviour
{

    public bool placed;
    public Behaviour halo;
    public float towerRange;
    public float delay;
    public bool inDelay;
    public Behaviour collide;
    public Image healthBar;
    public float health;
    public string towerType;
    public int initialHealth;

    // Use this for initialization
    void Start()
    {
        health = initialHealth;
        healthBar.fillAmount = health / initialHealth;
        inDelay = false;
        halo.enabled = false;
        placed = false;
        collide.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (placed == false)
        {
            Vector3 lTemp = transform.localPosition;
            lTemp.x = mousePosition.x;
            lTemp.y = mousePosition.y;
            transform.localPosition = lTemp;
        }
        if (Input.GetMouseButtonDown(0))
        {
            placed = true;
            collide.enabled = true;
        }
        if (placed)
        {
            if (!inDelay)
            {
                detectEnemy();
            }
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        this.gameObject.transform.GetChild(0).gameObject.SetActive(halo.enabled);

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (placed)
            {
                halo.enabled = !halo.enabled;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject.Destroy(this.gameObject);
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
                
                if (towerType == "Cannon")
                {
                    fire(enemyHit.gameObject, enemyHit, 10);
                    Collider2D[] nearbyEnemies;
                    nearbyEnemies = Physics2D.OverlapCircleAll(enemyHit.gameObject.transform.position, 1);
                    foreach(Collider2D enemy in nearbyEnemies)
                    {
                        fire(enemy.gameObject, enemy, 3);
                    }
                } else if (towerType == "Basic")
                {
                    fire(enemyHit.gameObject, enemyHit, 5);
                }
                inDelay = true;
                StartCoroutine(fireDelay());
            }
        }
    }

    void fire(GameObject enemy, Collider2D enemyHit, int damage)
    {
        enemy.GetComponent<EnemyMovement>().subtractHealth(damage, enemyHit);
    }

    IEnumerator fireDelay()
    {
        yield return new WaitForSeconds(1 * delay);
        inDelay = false;

    }

    public void subtractHealth(int damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / initialHealth;
    }

}
