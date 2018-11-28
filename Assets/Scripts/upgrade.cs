using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrade : MonoBehaviour {

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
            if (GameObject.Find("CurrencyManager").GetComponent<currency>().upgradeTower(30)) {
                Debug.Log("Upgraded!!");
                transform.parent.gameObject.transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
            } else {
                Debug.Log("You poor");
            }
        }
    }
}
