using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOver : MonoBehaviour
{

    public int textChild;

    // Use this for initialization
    void Start()
    {
        //GameObject.Find("Canvas").transform.GetChild(textChild).gameObject.GetComponent<Text>().color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
       // GameObject.Find("Canvas").transform.GetChild(textChild).gameObject.GetComponent<Text>().color = Color.red;
    }

    private void OnMouseExit()
    {
        //GameObject.Find("Canvas").transform.GetChild(textChild).gameObject.GetComponent<Text>().color = Color.black;
    }
}
