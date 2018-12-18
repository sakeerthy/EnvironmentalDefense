using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseOver : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    private void OnMouseEnter()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }
}