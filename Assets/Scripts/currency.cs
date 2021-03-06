﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class currency : MonoBehaviour {

    public int bank;
    public Text bankText;
    public GameObject newTower;
    public GameObject CannonTower;
    public GameObject WallTower;
    public GameObject knockbackTower;
    bool isMessage;
    new GUIStyle style;
    float timer = 0;
    public int basicPrice;
    public int knockbackPrice;
    public int cannonPrice;
    public int wallPrice;


    // Use this for initialization
    void Start () {
        bank = 30;
        bankText.text = string.Concat("$",bank.ToString());
	}

    // Update is called once per frame
    void Update()
    {
        GameButtons();
        timer++;
        if(timer > 60)
        {
            isMessage = false;
        }
    }

    public bool upgradeTower(int amount) {
        if ((bank - amount) < 0) {
            Debug.Log("Not enough money");
            isMessage = true;
            timer = 0;
            return false;
        } else {
            bank -= amount;
            bankText.text = string.Concat("$",bank.ToString());
            return true;
        }
    }

    public void addToBank(int amount) {
        bank += amount;
        bankText.text = string.Concat("$", bank.ToString());
    }

    public void subtractFromBank(int amount, string name) {
        bank -= amount;
        bankText.text = string.Concat("$", bank.ToString());
        if (name == "cannon")
        {
            Instantiate(CannonTower);
        } else if (name  == "coaltower")
        {
            
            Instantiate(newTower);
        } else if (name == "wall")
        {
            Instantiate(WallTower);
        } else if (name == "buyKnockbackTower")
        {
            Instantiate(knockbackTower);
        }
        
    }

    void GameButtons()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "coaltower")
                {
                    var amount = bank;
                    if (amount < basicPrice)
                    {
                        isMessage = true;
                        timer = 0;
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(basicPrice, hit.transform.name);
                    }
                }
                else if (hit.transform.name == "cannon")
                {
                    var amount = bank;
                    if (amount < cannonPrice)
                    {
                        isMessage = true;
                        timer = 0;
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(cannonPrice, hit.transform.name);
                    }
                }
                else if (hit.transform.name == "wall")
                {
                    var amount = bank;
                    if (amount < wallPrice)
                    {
                        isMessage = true;
                        timer = 0;
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(wallPrice, hit.transform.name);
                    }
                }
                else if (hit.transform.name == "buyKnockbackTower")
                {
                    var amount = bank;
                    if(amount < knockbackPrice)
                    {
                        isMessage = true;
                        timer = 0;
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(knockbackPrice, hit.transform.name);
                    }
                }

            }
        }
    }
    private void OnGUI()
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 20;

        // Load and set Font
        Font myFont = (Font)Resources.Load("Fonts/comic", typeof(Font));
        myButtonStyle.font = myFont;

        // Set color for selected and unselected buttons
        myButtonStyle.normal.textColor = Color.red;
        myButtonStyle.hover.textColor = Color.red;
        if (isMessage)
        {
            GUI.Label(new Rect(450, 250, 400, 50), "Not enough money", myButtonStyle);
        }
    }
}
