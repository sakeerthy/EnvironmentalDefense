using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class knockbackTowerPosition : MonoBehaviour
{
    public float knockbackForce;

    public bool placed;
    public Behaviour halo;
    public float towerRange;
    public float delay;
    public bool inDelay;
    public Behaviour collider;
    public Sprite initial;
    public Sprite upgraded;
    public ParticleSystem gas;
    public GameObject knockbackCircle;
    public int upgrade;
    public Image healthBar;
    public float health;
    public int initialHealth;

    // Use this for initialization
    void Start()
    {
        upgrade = 0;
        health = initialHealth;
        healthBar.fillAmount = health / initialHealth;
        inDelay = false;
        halo.enabled = false;
        placed = false;
        collider.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        InvokeRepeating("decreaseHappiness", 1.0f, 1.0f);
        GetComponent<SpriteRenderer>().sprite = initial;
        if (gas)
        {
            gas.Play();
            ParticleSystem.EmissionModule em = gas.emission;
            em.enabled = true;
        }
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
            collider.enabled = true;
        }
        if (placed)
        {
            if (!inDelay)
            {
                fire();
            }
            
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        transform.GetChild(0).gameObject.SetActive(halo.enabled);
        if (upgrade == 1)
        {
            gas.Stop();
        }

    }

    public void upgradeTower()
    {
        if (upgrade == 0)
        {
            upgrade++;
            GetComponent<SpriteRenderer>().sprite = upgraded;
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
            GameObject.Destroy(this.gameObject);
        }
    }

    void decreaseHappiness()
    {
        if (upgrade == 0)
        {
            GameObject.Find("Happiness").GetComponent<happiness>().subtractHealth(0.1f);

        }
    }

    public void fire()
    {
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

    public void subtractHealth(int damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / initialHealth;
    }

}

