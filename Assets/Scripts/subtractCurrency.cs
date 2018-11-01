using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subtractCurrency : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "subtractCube")
                {
                    var amount = transform.parent.gameObject.GetComponent<currency>().bank;
                    if (amount < 10)
                    {
                        Debug.Log("Not enough money");
                    }
                    else
                    {
                        transform.parent.gameObject.GetComponent<currency>().subtractCurrency(10);
                    }
                }
            }
        }
        */
    }
}
