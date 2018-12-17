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
    public int upgrade;
    public Sprite initial;
    public Sprite upgraded;

    // Use this for initialization
    void Start()
    {
        upgrade = 0;
        health = initialHealth;
        healthBar.fillAmount = health / initialHealth;
        inDelay = false;
        halo.enabled = false;
        placed = false;
        collide.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        InvokeRepeating("decreaseHappiness", 1.0f, 1.0f);
        GetComponent<SpriteRenderer>().sprite = initial;

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
        transform.GetChild(0).gameObject.SetActive(halo.enabled);

    }

    public void upgradeTower() {
        if (upgrade == 0) {
            upgrade++;
            GetComponent<SpriteRenderer>().sprite = upgraded;
        }
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

    void decreaseHappiness() {
        if (upgrade == 0) {
            GameObject.Find("Happiness").GetComponent<happiness>().subtractHealth(0.1f);

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
