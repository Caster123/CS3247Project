using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shoot : TouchLogic {

	public Transform camera;
	public Transform center;
	public GameObject StateControl;
	public float hoverForce = 100.0f;
	private Vector3 direction;
	private GameObject[] pieces;
	private List<string> pieceOnGround = new List<string>();
	private string newPieceName = "";
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	public override void OnTouchBegan()
	{

		pieces = GameObject.FindGameObjectsWithTag("Piece");

		foreach (GameObject piece in pieces) 
			if (piece.GetComponent<ObjectProperty>().onGround()){
				pieceOnGround.Add(piece.name);
				print (piece.name);
			}
		//need to cache the touch index that started on the pad so others wont interfere
		direction = center.transform.position - camera.transform.position;
		RaycastHit hit;
		if (Physics.Raycast (camera.transform.position, direction, out hit)) {
			float distanceToGround = hit.distance;
			//print(hit.collider.gameObject.tag);
			if (hit.collider.gameObject.tag == "Piece"){
				hit.collider.gameObject.rigidbody.AddForce(direction * hoverForce, ForceMode.Acceleration);
				newPieceName = hit.collider.gameObject.name;
			}
		}
		//print ("fuck!");
		StartCoroutine (Awake());
	}

	public IEnumerator Awake() {
		//print(Time.time);
		yield return new WaitForSeconds(3.0f);
		foreach (GameObject piece in pieces) 
		if (piece.GetComponent<ObjectProperty>().onGround()) {
			print (piece.name);
			if (!pieceOnGround.Contains (piece.name) && piece.name != newPieceName) {
				print (piece.name);
				print ("Game End!");
				StartCoroutine (Fail());
			} else
				if (!pieceOnGround.Contains (piece.name) && piece.name == newPieceName){
					ObjectProperty op = piece.GetComponent<ObjectProperty>();
					op.remove ();
				}
		}
		//print(Time.time);
	}

	public IEnumerator Fail() {
		yield return new WaitForSeconds (1.5f);
		Application.LoadLevel("Fail");
	}
}

