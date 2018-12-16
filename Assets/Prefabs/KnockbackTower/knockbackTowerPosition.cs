using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbackTowerPosition : MonoBehaviour
{
    public float knockbackForce;

    public bool placed;
    public Behaviour halo;
    public float towerRange;
    public float delay;
    public bool inDelay;
    public Behaviour collider2D;

    public GameObject knockbackCircle;
    // Use this for initialization
    void Start()
    {
        inDelay = false;
        halo.enabled = false;
        placed = false;
        collider2D.enabled = false;
        
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
            placed = true;
            collider2D.enabled = true;
        }
        if (placed)
        {
            if (!inDelay)
            {
                fire();
            }
            
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
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Hi");
            Debug.Log(gameObject);
            GameObject.Destroy(this.gameObject);
        }
    }

    public void fire()
    {
        Debug.Log("fire");
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), towerRange);

        foreach (var collider2D in enemyColliders)
        {
            if(collider2D.gameObject.CompareTag("enemy"))
            {
                Debug.Log("Hit an enemy");
                collider2D.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                collider2D.GetComponent<Rigidbody2D>().AddForce(new Vector2(collider2D.GetComponent<Transform>().position.x - transform.position.x, collider2D.GetComponent<Transform>().position.y - transform.position.y).normalized * knockbackForce * 1/Vector3.Distance(collider2D.GetComponent<Transform>().position, transform.position));
            }
        }
        inDelay = true;
        StartCoroutine(fireDelay());
    }

    IEnumerator fireDelay()
    {
        yield return new WaitForSeconds(1 * delay);
        inDelay = false;

    }
   
}

