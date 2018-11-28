using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour {

//public Canvas towerCanvas;
    public Button yourButton;
    public bool buttonisclicked;
// private var childCanvas : Transform;


    // Use this for initialization
    void Start () {
        Button btn = yourButton;
        btn.onClick.AddListener(OpenMenu);
        buttonisclicked = false;
        transform.GetChild(1).gameObject.SetActive(false);
        // childCanvas = transform.Find("TowerCanvas");
    }

    void OpenMenu()
    {
        Debug.Log("You have clicked the button!");
        if (buttonisclicked)
        {
            buttonisclicked = false;
            transform.GetChild(1).gameObject.SetActive(false);
//childCanvas.active = false;
        } else
        {
            buttonisclicked = true;
            transform.GetChild(1).gameObject.SetActive(true);
// childCanvas.active = true;
        }
        
    }


    private void OnGUI()
    {
        //check if our button click boolean is true
    }

    // Update is called once per frame
    void Update () {
		
	}
}
