using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeknockback : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transform.parent.gameObject.GetComponent<knockbackTowerPosition>().upgrade == 0)
            {
                if (GameObject.Find("CurrencyManager").GetComponent<currency>().upgradeTower(20))
                {
                   
                    GameObject.Find("Happiness").GetComponent<happiness>().addHealth(5);
                    transform.parent.gameObject.GetComponent<knockbackTowerPosition>().upgradeTower();
                    //transform.parent.gameObject.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
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