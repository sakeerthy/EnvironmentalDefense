using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class happiness : MonoBehaviour {

    public Image healthBar;
    public float health;
    const int initialHealth = 100;
    // Use this for initialization
    void Start () {
        health = 50;
        healthBar.fillAmount = health / initialHealth;
        //healthBar.color = new Color(255f, 255f, 0f, 1f);
    }

    // Update is called once per frame
    void Update () {
        if (health < 30) {
            healthBar.color = new Color(1f, 0f, 0f, 1f);
        } else if (health > 70) {
            healthBar.color = new Color(0f, 1f, 0f, 1f);
        } else {
            healthBar.color = new Color(1f, 1f, 0f, 1f);
        }
        if (health >= 100) {
            SceneManager.LoadScene(sceneName: "WinScene");
        }
        if (health <= 0) {
            SceneManager.LoadScene(sceneName: "LoseScene");
        }
    }

    public void subtractHealth(int damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / initialHealth;
    }

    public void addHealth(int damage)
    {
        health = health + damage;
        healthBar.fillAmount = health / initialHealth;
    }
}
