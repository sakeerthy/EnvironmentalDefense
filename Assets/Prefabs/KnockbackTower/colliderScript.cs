using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class colliderScript : MonoBehaviour
{
    public float delay;
    public float scalingFactor, maxSize;
    private bool inDelay = false;
    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    void Update()
    {

        if (!inDelay && gameObject.GetComponentInParent<knockbackTowerPosition>().placed)
        {
            GetComponent<Collider2D>().enabled = true;
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * scalingFactor * Time.deltaTime;
            if (transform.localScale.x > maxSize)
            {
                transform.localScale = new Vector3() * 0;
                inDelay = true;
              StartCoroutine(knockbackDelay());
            }
        }
    }

    IEnumerator knockbackDelay()
    {
        
        yield return new WaitForSeconds(delay);
        inDelay = false;

    }
}