using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ButtonPress();
    }

    void ButtonPress()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Start")
                {
                    SceneManager.LoadScene(sceneName: "MainScene");
                }
                else if (hit.transform.name == "End")
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                }
            }
        }
    }
}
