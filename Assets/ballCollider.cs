using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCollider : MonoBehaviour {
    public float radius;
    public int damage;

    public GameObject effect;

	// Use this for initialization
	void Start () {
        StartCoroutine(death());
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("enemy"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
        else
        {
            explode();
        }
    }

    void explode()
    {
        Collider2D[] allHit;

        allHit = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(Collider2D hit in allHit)
        {
            if(hit.gameObject.CompareTag("enemy"))
            {
                hit.gameObject.GetComponent<EnemyMovement>().subtractHealth(damage, hit);
            }
        }
        Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
