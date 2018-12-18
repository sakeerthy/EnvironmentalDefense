using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPosition : MonoBehaviour
{
     Vector3 offset;
    public Vector3 offsetU;
    public bool placed;
    public Behaviour halo;
    public float towerRange;
    public float towerRangeU;
    public float basicDelay;
    public float cannonDelay;
    public bool inDelay;
    public Behaviour collide;
    public Image healthBar;
    public float health;
    public string towerType;
    public int initialHealth;
    public int upgrade;
    public Sprite initial;
    public Sprite upgraded;
    public ParticleSystem gas;
    public GameObject cannonBall;
    LineRenderer lRend;


    public GameObject laserEnd;
    private GameObject  end;
    public float laserTime;
    bool damageDelay = false;


    // Use this for initialization
    void Start()
    {
        offset = new Vector3(.2f,1, 0);
        upgrade = 0;
        health = initialHealth;
        healthBar.fillAmount = health / initialHealth;
        inDelay = false;
        halo.enabled = false;
        //placed = false;
        collide.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        InvokeRepeating("decreaseHappiness", 1.0f, 1.0f);
        GetComponent<SpriteRenderer>().sprite = initial;
        if (gas)
        {
            gas.Play();
            ParticleSystem.EmissionModule em = gas.emission;
            em.enabled = true;
        }
        if(towerType == "Basic" || towerType == "Cannon")
        {
            lRend = GetComponent<LineRenderer>();
            lRend.positionCount = 2;
            lRend.SetPosition(0, transform.position + offset);
            lRend.SetPosition(1, transform.position + offset);
            lRend.enabled = false;
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
        if (Input.GetMouseButtonDown(0) && mousePosition.x < 5)
        {
            placed = true;
            collide.enabled = true;
        }
        if(Input.GetMouseButtonDown(1) && !placed)
        {
            int price = 0;
            switch (towerType)
            {
                case "Basic":
                    price = GameObject.Find("CurrencyManager").GetComponent<currency>().basicPrice;
                    break;

                case "Cannon":
                    price = GameObject.Find("CurrencyManager").GetComponent<currency>().cannonPrice;
                    break;

                case "Wall":
                    price = GameObject.Find("CurrencyManager").GetComponent<currency>().wallPrice;
                    break;
            }
            GameObject.Find("CurrencyManager").GetComponent<currency>().addToBank(price);
            Destroy(this.gameObject);
        }
        if (placed)
        {
            if (!inDelay && towerType == "Basic")
            {

                detectEnemy();

            }

            if(towerType == "Cannon")
            {
                aim();
             
            }

           
        }
        if (health <= 0)
        {
            if (towerType == "Cannon" && upgrade != 0)
            {
                Destroy(end);
            }
            Destroy(this.gameObject);
        }
        transform.GetChild(0).gameObject.SetActive(halo.enabled);
        if (upgrade == 1) {
            gas.Stop();
        }
    }

    public void upgradeTower() {
        if (upgrade == 0) {
            upgrade++;
            GetComponent<SpriteRenderer>().sprite = upgraded;
        }

        if(towerType == "Wall")
        {
            initialHealth *= 2;
        }

        health = initialHealth;
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

    void decreaseHappiness() {
        if (upgrade == 0) {
            GameObject.Find("Happiness").GetComponent<happiness>().subtractHealth(0.1f);

        }
    }

    void detectEnemy()
    {
        Collider2D enemyHit = null;
        if (upgrade == 0)
        {
            enemyHit = Physics2D.OverlapCircle(transform.position, towerRange);
        }
        else
        {
            enemyHit = Physics2D.OverlapCircle(transform.position, towerRangeU);
        }

        

        if (enemyHit)
        {
            if (enemyHit.gameObject.CompareTag("enemy"))
            {
                
                if(upgrade == 0)
                {
                    lRend.SetPosition(0, transform.position + offset);
                }
                else
                {
                    lRend.SetPosition(0, transform.position + offsetU);
                }
                    lRend.SetPosition(1, enemyHit.transform.position);
                    
                    lRend.enabled = true;
                    StartCoroutine(effectDelay());

                if (upgrade == 0)
                {
                    fire(enemyHit.gameObject, enemyHit, 5);
                }
                else
                {
                    fire(enemyHit.gameObject, enemyHit, 10);
                }
                
                inDelay = true;
                StartCoroutine(fireDelay(basicDelay));
            }
        }
    }

    IEnumerator effectDelay()
    {
        yield return new WaitForSeconds(.3f);
        lRend.enabled = false;
    }

    void fire(GameObject enemy, Collider2D enemyHit, int damage)
    {
        enemy.GetComponent<EnemyMovement>().subtractHealth(damage, enemyHit);
    }

    IEnumerator fireDelay(float delay)
    {
        yield return new WaitForSeconds(1 * delay);
        inDelay = false;

    }



    public void subtractHealth(int damage)
    {
        health = health - damage;
        healthBar.fillAmount = health / initialHealth;
    }

    void aim()
    {
        Collider2D[] allHit;
        Collider2D target = null;
        float position = transform.position.x + towerRange;
        allHit = Physics2D.OverlapCircleAll(transform.position, towerRange);

        foreach(Collider2D hit in allHit)
        {
           if(hit.gameObject.CompareTag("enemy"))
            {
                if(hit.transform.position.x < position)
                {
                    target = hit;
                    position = target.transform.position.x;
                }
            }
              
        }

        if(target != null)
        {
            var dir = target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);

            if (upgrade == 0)
            {
                if (!inDelay)
                {
                    fireCannon(target.transform.position);
                }
            }
            else
            {
                fireLaser(target);
            }

        }
        else
        {
            var dir = Vector3.left;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            lRend.enabled = false;
            if (upgrade != 0)
            {
                if (end == null)
                {
                    end = Instantiate(laserEnd, transform.position, Quaternion.identity);
                }
                end.GetComponent<SpriteRenderer>().enabled = false;
            }

        }

       

    }

    void fireCannon(Vector3 target)
    {
      
        GameObject clone;
        clone = Instantiate(cannonBall, transform.position + transform.rotation * new Vector3(-1,0,0), Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().AddForce((target - transform.position).normalized * 300f);
        inDelay = true;
        StartCoroutine(fireDelay(cannonDelay));

    }

    void fireLaser(Collider2D target)
    {
      
        lRend.enabled = true;
        if(end == null)
        {
            end = Instantiate(laserEnd, target.transform.position, Quaternion.identity);
        }
        end.GetComponent<SpriteRenderer>().enabled = true;
        end.transform.rotation = transform.rotation;
        end.transform.position = target.transform.position - (target.transform.position - transform.position).normalized * .5f;
        lRend.SetPosition(1, target.transform.position - (target.transform.position - transform.position).normalized *.2f);
        lRend.SetPosition(0, transform.position + (target.transform.position - transform.position).normalized * .2f);
        if (!damageDelay)
        {
            target.gameObject.GetComponent<EnemyMovement>().subtractHealth(1, target);
            damageDelay = true;
        }
        StartCoroutine(laserDelay());
    }

    IEnumerator laserDelay()
    {
        yield return new WaitForSeconds(laserTime);
        damageDelay = false;
    }
    
}
