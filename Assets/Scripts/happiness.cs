using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class happiness : MonoBehaviour {

    public Image healthBar;
    public float health;
    const int initialHealth = 100;
    // Use this for initialization
    void Start () {
        health = initialHealth;
        healthBar.fillAmount = health / initialHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void subtractHealth(int damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / initialHealth;
    }
}
