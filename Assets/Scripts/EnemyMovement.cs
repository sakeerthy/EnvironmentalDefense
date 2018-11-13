using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyMovement : MonoBehaviour {

    
     public Rigidbody2D rb;
    public float speed;
    public float acceleration;
    public int value;
    public float health;
    public Image healthBar;
    const int initialHealth = 10;
    Vector2 direction;

    public Vector3 destination;
	// Use this for initialization
	void Start () {
        health = initialHealth;
        //healthBar.fillAmount = health / initialHealth;
        
        rb.velocity = Vector2.zero;
       
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) {
            Destroy(this.gameObject);
        }

       
    }

    private void FixedUpdate()
    {
        move();
    }

    public void subtractHealth(int damage, Collider2D enemyHit) {
        health = health - damage;
        if(health <= 0){
            GameObject.Find("CurrencyManager").GetComponent<currency>().addToBank(enemyHit.gameObject.GetComponent<EnemyMovement>().value);
        }
        healthBar.fillAmount = health / initialHealth;
    }

    public void move()
    {
        Vector2 direction = new Vector2(destination.x - transform.position.x, destination.y - transform.position.y).normalized;

        if(rb.velocity.magnitude > speed)
        {
            rb.velocity = direction * speed;
        }
        else if(rb.velocity.magnitude < speed)
        {
            rb.AddForce(direction * acceleration);
        }

        



    }
}

