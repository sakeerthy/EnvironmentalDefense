using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleKnockback : MonoBehaviour {

    

    public float force;
	// Update is called once per frame
	void FixedUpdate () {
     
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (gameObject.activeSelf == true)
        {

            Debug.Log("active in collision");
            foreach (ContactPoint2D contact in collision.contacts)
            {
               if(contact.otherCollider.gameObject.CompareTag("enemy"))
                {
                    contact.otherCollider.gameObject.GetComponent<Rigidbody2D>().AddForce(contact.normal * force);
                }

            }

            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Not active in collision");
        }
    }
}
