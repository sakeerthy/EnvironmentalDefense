using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPosition : MonoBehaviour
{
     Vector3 offset;
    public bool placed;
    public Behaviour halo;
    public float towerRange;
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
        if(towerType == "Basic")
        {
            lRend = GetComponent<LineRenderer>();
            lRend.positionCount = 2;
            lRend.SetPosition(0, transform.position + offset);
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

        enemyHit = Physics2D.OverlapCircle(transform.position, towerRange);

        if (enemyHit)
        {
            if (enemyHit.gameObject.CompareTag("enemy"))
            {
                
            
                    lRend.SetPosition(1, enemyHit.transform.position);
                    lRend.SetPosition(0, transform.position + offset);
                    lRend.enabled = true;
                    StartCoroutine(effectDelay());
                    fire(enemyHit.gameObject, enemyHit, 5);
                
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
                fireLaser(target.transform.position);
            }

        }
        else
        {
            var dir = Vector3.left;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);

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

    void fireLaser(Vector3 target)
    {
        /*
        if (start == null)
        {
            start = Instantiate(laserStart) as GameObject;
            start.transform.parent = this.transform;
        }

        if (middle == null)
        {
            middle = Instantiate(laserMiddle) as GameObject;
            middle.transform.parent = this.transform;
            middle.transform.localPosition = Vector2.zero;
        }

        float maxLaserSize = 20f;
        float currentLaserSize = maxLaserSize;

        Vector2 direction = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, maxLaserSize);

        if(hit.collider != null)
        {
            currentLaserSize = Vector2.Distance(hit.point, this.transform.position);

            if (end == null)
            {
                end = Instantiate(laserEnd) as GameObject;
                end.transform.parent = this.transform;
                end.transform.localPosition = Vector2.zero;
            }
        }
        else
        {
            if(end != null)
            {
                Destroy(end);
            }
        }

        float startSpriteWidth = start.GetComponent<Renderer>().bounds.size.y;
        float endSpriteWidth = 0f;
        if (end != null) endSpriteWidth = end.GetComponent<Renderer>().bounds.size.x;

        middle.transform.localScale = new Vector3(middle.transform.localScale.x, currentLaserSize - startSpriteWidth, middle.transform.localScale.z);
        middle.transform.rotation = transform.rotation;
        middle.transform.localPosition = new Vector2(((target.x - transform.position.x)/ 2f), ((target.y - transform.position.y) / 2f));

       
        if (end != null)
        {
            end.transform.localPosition = new Vector2(((target.x - transform.position.x) - .1f), ((target.y - transform.position.y) - .1f));
        }
        */
    }
    
}
