using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class currency : MonoBehaviour {

    public int bank;
    public int towerPrice;
    public int knockbackTowerPrice;
    public Text bankText;
    public GameObject newTower;
    public GameObject newKnockbackTower;
	// Use this for initialization
	void Start () {
        bank = 30;
        bankText.text = bank.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        addCurrency();
        subtractCurrency();
	}

    public void addToBank(int amount) {
        bank += amount;
        bankText.text = bank.ToString();
    }

    public void subtractFromBank(int amount) {
        bank -= amount;
        bankText.text = bank.ToString();
        
    }

    void addCurrency()
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
            }
        }
    }

    void subtractCurrency()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                //ADD MORE TOWER PRICES HERE

                if (hit.transform.name == "buyStandardTower")
                {
                    
                    if (bank < towerPrice)
                    {
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(towerPrice);
                        Instantiate(newTower);
                    }
                }

                if (hit.transform.name == "buyKnockbackTower")
                {
                    Debug.Log("In here");
                    if (bank < knockbackTowerPrice)
                    {
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(knockbackTowerPrice);
                        Instantiate(newKnockbackTower);
                    }
                }
            }
        }
    }
}
