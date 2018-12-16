using System.Collections;
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


	// Use this for initialization
	void Start () {
        bank = 30;
        bankText.text = string.Concat("$",bank.ToString());
	}
	
	// Update is called once per frame
	void Update () {
        GameButtons();
	}

    public bool upgradeTower(int amount) {
        if ((bank - amount) < 0) {
            Debug.Log("Not enough coin");
            return false;
        } else {
            bank -= amount;
            bankText.text = string.Concat("$",bank.ToString());
            return true;
        }
    }

    public void addToBank(int amount) {
        bank += amount;
        bankText.text = bank.ToString();
    }

    public void subtractFromBank(int amount, string name) {
        bank -= amount;
        bankText.text = bank.ToString();
        if (name == "cannon")
        {
            Instantiate(CannonTower);
        } else if (name  == "subtractCube")
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
                if (hit.transform.name == "addCube")
                {
                    addToBank(10);
                }
                else if (hit.transform.name == "subtractCube")
                {
                    var amount = bank;
                    if (amount < 10)
                    {
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(10, hit.transform.name);
                    }
                }
                else if (hit.transform.name == "cannon")
                {
                    var amount = bank;
                    if (amount < 15)
                    {
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(15, hit.transform.name);
                    }
                }
                else if (hit.transform.name == "wall")
                {
                    var amount = bank;
                    if (amount < 50)
                    {
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(50, hit.transform.name);
                    }
                }
                else if (hit.transform.name == "buyKnockbackTower")
                {
                    var amount = bank;
                    if(amount < 40)
                    {
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(40, hit.transform.name);
                    }
                }

            }
        }
    }
}
