using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DestroyCountBoundary : MonoBehaviour {
    public float alpha = 0f;
    public float fadeSpeed = 100f;
    public int dead_enemies;
    public int max_dead;
    public Text enemyCounter;

	// Use this for initialization
	void Start () {
        dead_enemies = 0;
        displayText();
    }

    // Update is called once per frame
    void Update () {
        if (alpha == 0.0f)
            GameObject.Find("Collision_effect").transform.localScale = new Vector3(0, 0, 0);
        if (alpha > 0.0f)
        {
            // Reduce alpha by fadeSpeed amount.
            alpha -= fadeSpeed * Time.deltaTime;

            // Create a new color using original color RGB values combined
            // with new alpha value. We have to do this because we can't 
            // change the alpha value of the original color directly.
            GameObject.Find("Collision_effect").GetComponent<MeshRenderer>().material.color = new Color(GameObject.Find("Collision_effect").GetComponent<MeshRenderer>().material.color.r, GameObject.Find("Collision_effect").GetComponent<MeshRenderer>().material.color.g, GameObject.Find("Collision_effect").GetComponent<MeshRenderer>().material.color.b, alpha);
        }
    }

    void OnCollisionEnter2D(Collision2D monster)
    {
        if (monster.gameObject.tag == "enemy")
        {
            dead_enemies++;
            GameObject.Find("Happiness").GetComponent<happiness>().subtractHealth(1);
            alpha = 0.75f;
            GameObject.Find("Collision_effect").transform.localScale = new Vector3(25, 25, 1);
        }
        Destroy(monster.gameObject);
        displayText();
    }

    void displayText()
    {
        enemyCounter.text = "Enemies Passed: " + dead_enemies;
    }

}
