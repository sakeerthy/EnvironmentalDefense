﻿using System.Collections;
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
    public GameObject effect;

    private float coll_timer;
    public Collider2D tower;
    private bool damageDelay;
    // Use this for initialization
    void Start () {
        health = initialHealth;
        healthBar.fillAmount = health / initialHealth;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1, 0) * speed;
        damageDelay = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            GameObject.Find("Happiness").GetComponent<happiness>().addHealth(0.3f);
            death();
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
        if (other.gameObject.tag == "tower"){
            other.gameObject.GetComponent<TowerPosition>().subtractHealth(1);
            coll_timer = Time.fixedTime;
        }
        if (other.gameObject.tag == "knockback")
        {
            other.gameObject.GetComponent<knockbackTowerPosition>().subtractHealth(1);
            coll_timer = Time.fixedTime;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        
        if (!damageDelay)
        {
       
            if (other.gameObject.CompareTag("tower"))
            {
          
                other.gameObject.GetComponent<TowerPosition>().subtractHealth(1);
                coll_timer = Time.fixedTime;
                damageDelay = true;
                StartCoroutine(delay());
            }
            if (other.gameObject.name == "knockback")
            {
                other.gameObject.GetComponent<knockbackTowerPosition>().subtractHealth(1);
                coll_timer = Time.fixedTime;
                damageDelay = true;
                StartCoroutine(delay());
            }
            
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(.1f);
        damageDelay = false;
    }

    void death()
    {
        Instantiate(effect);
        effect.gameObject.transform.position = transform.position;
        
        Destroy(this.gameObject);
    }

    
}
