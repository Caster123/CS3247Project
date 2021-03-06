﻿// Used to enter Single player mode
using UnityEngine;
using System.Collections;

public class HitSingle : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    public AudioClip hit = Resources.Load("building crush") as AudioClip;

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
		SharedBehaviour.current.isSingle = true;
        audio.PlayOneShot(hit);
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
        Application.LoadLevel("StructureChooseS");
    }    
}
