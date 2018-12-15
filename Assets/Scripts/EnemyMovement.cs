using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyMovement : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public int value;
    public float health;
    public float initialHealth;
    public Image healthBar;

    private float coll_timer;
    public Collider2D tower;
    // Use this for initialization
    void Start () {
        health = initialHealth;
        healthBar.fillAmount = health / initialHealth;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1, 0) * speed;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            Destroy(this.gameObject);
        }

        if (Time.fixedTime - coll_timer >= 1.0f)
            rb.velocity = new Vector2(1, 0) * speed;
    }

    public void subtractHealth(int damage, Collider2D enemyHit) {
        health = health - damage;
        if(health <= 0){
            GameObject.Find("CurrencyManager").GetComponent<currency>().addToBank(enemyHit.gameObject.GetComponent<EnemyMovement>().value);
        }
        healthBar.fillAmount = health / initialHealth;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "tower"){
            other.gameObject.GetComponent<TowerPosition>().subtractHealth(1);
            coll_timer = Time.fixedTime;
        }
    }
}
