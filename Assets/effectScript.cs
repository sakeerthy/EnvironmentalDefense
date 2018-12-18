using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Play();
        StartCoroutine(deathDelay());
	}

    IEnumerator deathDelay()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }
}
