using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GetComponent<TextMesh>().renderer.sortingOrder = 10;

	}
	
	// Update is called once per frame
	void Update () {
		if (ItemText.textstatus == false)
        {
            Destroy(gameObject);
        }
	}
}
