using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        /*if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("here1");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("here2");
                if (hit.transform.name == "End")
                {
                    Debug.Log("here3");
                    GameObject.Find("End").transform.localScale = new Vector3(0, 0, 0);
                    //Application.Quit();
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            }
        }*/
	}

    void onMouseDown()
    {
        GameObject.Find("End").transform.localScale = new Vector3(0, 0, 0);
    }
}
