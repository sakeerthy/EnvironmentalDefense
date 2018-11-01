using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class currency : MonoBehaviour {

    public int bank;
    public Text bankText;
    public GameObject newTower;
	// Use this for initialization
	void Start () {
        bank = 100;
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
        Instantiate(newTower);
    }

    void addCurrency()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button down");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Got a hit");
                if (hit.transform.name == "addCube")
                {
                    Debug.Log("Addcube");
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
                if (hit.transform.name == "subtractCube")
                {
                    var amount = bank;
                    if (amount < 10)
                    {
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        subtractFromBank(10);
                    }
                }
            }
        }
    }
}
