using UnityEngine;
using System.Collections;
using System;

public class Brick_Basic : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
        rigidbody.SetDensity((float)Convert.ToInt32(this.tag));
	}

    public AudioClip attack = Resources.Load("lazer") as AudioClip;

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        audio.pitch = (float)1.5;
        audio.PlayOneShot(attack);

        
        //Application.LoadLevel("basic");
        //Application.LoadLevel("1st");
        StartCoroutine(Wait(0.0F));
        //Application.LoadLevel("basic");
    }

    IEnumerator Wait(float waitTime)
    {
        for (int i=0; i< 10; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds((float)0.05);
            renderer.enabled = true;
            yield return new WaitForSeconds((float)0.05);
        }
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
		GameObject target = GameObject.Find("Counter");
		Counter c = target.GetComponent<Counter>();
		c.addRemove();
    }
}
