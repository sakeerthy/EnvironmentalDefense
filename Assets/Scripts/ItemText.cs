using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemText : MonoBehaviour {
    public Transform popuptext;
    public static bool textstatus = false;

    private void OnMouseEnter()
    {
        if (textstatus == false)
        {
            if (gameObject.name == "subtractCube")
            {
                popuptext.GetComponent<TextMesh>().text = "Basic tower\nLow health and damage\n$10";
            }
            else if (gameObject.name == "GasTower")
            {
                popuptext.GetComponent<TextMesh>().text = "Basic tower\nLow health and damage\n$10";
            }
            else if (gameObject.name == "cannon")
            {
                popuptext.GetComponent<TextMesh>().text = "Cannon with splash damage\nhigh damage\n$15";
            } 
            else if (gameObject.name == "wall")
            {
                popuptext.GetComponent<TextMesh>().text = "Wall does no damage\nVery high health\n$50";
            }
            else if (gameObject.name == "buyKnockbackTower")
            {
                popuptext.GetComponent<TextMesh>().text = "Knocks back enemies\n$40";
            }

            textstatus = true;
            Instantiate(popuptext, new Vector3(transform.position.x - 4, transform.position.y + 1, 0), popuptext.rotation);
        }

    }

    private void OnMouseExit()
    {
            textstatus = false;

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
