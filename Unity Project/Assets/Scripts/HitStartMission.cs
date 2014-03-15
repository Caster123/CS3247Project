using UnityEngine;
using System.Collections;

public class HitStartMission : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        this.gameObject.rigidbody.isKinematic = false;
        this.gameObject.rigidbody.AddRelativeForce(-Vector3.forward * 1000);
        transform.Rotate(3f, 30f, 0);
        //Destroy(this.gameObject);
        //Application.LoadLevel("basic");
        //Application.LoadLevel("1st");
        StartCoroutine(Wait(1.5F));
        //Application.LoadLevel("basic");
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel("ManIntroduction1");
    }    
}
