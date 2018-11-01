using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DestroyCountBoundary : MonoBehaviour {
    public int dead_enemies;
    public int max_dead;
    public Text enemyCounter;

	// Use this for initialization
	void Start () {
        dead_enemies = 0;
	}

    void OnCollisionEnter2D(Collision2D monster)
    {
        if (monster.gameObject.tag == "enemy")
        {
            dead_enemies++;
        }
        Destroy(monster.gameObject);
        Debug.Log("Object Destroyed!");
    }

    // Update is called once per frame
    void Update () {
        displayText();
    }

    void displayText()
    {
        enemyCounter.text = "Enemies Passed: " + dead_enemies;
    }
}
