using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrade : MonoBehaviour {

    public int basicUpgradePrice;
    public int cannonUpgradePrice;
    public int knockbackUpgradePrice;
    public int wallUpgradePrice;
    int price;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.parent.gameObject.GetComponent<TowerPosition>().upgrade == 0)
            {
                switch(transform.parent.gameObject.GetComponent<TowerPosition>().towerType)
                {
                    case "Basic":
                        price = basicUpgradePrice;
                        break;

                    case "Cannon":
                        price = cannonUpgradePrice;
                        break;

                    case "Wall":
                        price = wallUpgradePrice;
                        break;

                }
                Debug.Log(price);
                if (GameObject.Find("CurrencyManager").GetComponent<currency>().upgradeTower(price))
                {
                    GameObject.Find("Happiness").GetComponent<happiness>().addHealth(5);
                    transform.parent.gameObject.GetComponent<TowerPosition>().upgradeTower();
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("You poor");
                }
            }
            else
            {
                Debug.Log("Max Upgrade Reached!");
            }
        }
    }
}
