using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPosition : MonoBehaviour
{

    public bool placed;
    public Behaviour halo;

    // Use this for initialization
    void Start()
    {
        halo.enabled = false;
        placed = false;
       

    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (placed == false)
        {
            Vector3 lTemp = transform.localPosition;
            lTemp.x = mousePosition.x;
            lTemp.y = mousePosition.y;
            transform.localPosition = lTemp;
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<TowerController>().isPlaced = true;
            placed = true;
        } else if (Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (placed)
            {
                halo.enabled = !halo.enabled;
            }
        }
    }
}
