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
    const int initialHealth = 10;
    // Use this for initialization
    void Start()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        //).renderer.material.color.a = 0.5f;


        //transform.renderer.material.color.a = 0.5f; // a is the alpha value.
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
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);

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
        if (halo.enabled) {
            transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
        } else {
            transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0f);
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

    public void subtractHealth(int damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / initialHealth;
    }

    void upgrade()
    {

    }
}
