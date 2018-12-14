using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCanvasPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 1000, 0));
        Debug.Log(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
