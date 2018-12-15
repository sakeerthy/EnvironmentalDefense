using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitpos : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(transform.position);
        Debug.Log(Screen.width);
        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*.8f, Screen.height*.8f, 0)));
        //transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * .95f, Screen.height * .95f, 100));
	}
	
	// Update is called once per frame
	void Update () {
        GameButtons();
    }
    void GameButtons()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Quit")
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                }
            }
        }
    }
}
