using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour {
    
    public Button yourButton;
    public bool buttonisclicked;
    

    // Use this for initialization
    void Start () {
        Button btn = yourButton;
        btn.onClick.AddListener(OpenMenu);
        buttonisclicked = false;
    }

    void OpenMenu()
    {
        Debug.Log("You have clicked the button!");
        buttonisclicked = true;
    }


    private void OnGUI()
    {
        //check if our button click boolean is true
        if (buttonisclicked)
        {
            //create gui menu, refer to linked documentation to learn how to create gui elements via script, and learn about various styling techniques
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
