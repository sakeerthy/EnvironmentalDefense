using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(1, 0) * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
