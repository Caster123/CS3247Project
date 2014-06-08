// Used to enter multiplayer mode

using UnityEngine;
using System.Collections;

public class HitMulti : MonoBehaviour
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
		SharedBehaviour.current.isSingle = false;
		audio.PlayOneShot(hit);
        this.gameObject.rigidbody.isKinematic = false;
        this.gameObject.rigidbody.AddRelativeForce(-Vector3.forward * 1000);
        transform.Rotate(3f, 30f, 0);
        StartCoroutine(Wait(1.5F));
    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.LoadLevel("StructureChooseM");
    }    
}
