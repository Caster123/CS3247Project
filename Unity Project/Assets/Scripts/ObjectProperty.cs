using UnityEngine;
using System.Collections;

public class ObjectProperty : MonoBehaviour {

	public Transform camera;
	public Transform center;
	public Color originalColor = new Color(0.0f,0.0f,0.0f);
	public Color highlightColor = new Color(0.0f,0.0f,0.0f);
	private Vector3 direction;

	void Start() {
		MeshRenderer gameObjectRenderer = this.gameObject.GetComponent<MeshRenderer>();
		Material newMaterial = new Material(Shader.Find("Diffuse"));
		newMaterial.color = originalColor;
		gameObjectRenderer.material = newMaterial ;
	}

	// Update is called once per frame
	void Update () 	{
		//need to cache the touch index that started on the pad so others wont interfere
		direction = center.transform.position - camera.transform.position;
		//print ("hit!");
		RaycastHit hit;
		if (Physics.Raycast (camera.transform.position, direction, out hit)) {
			print ("hit!");
			if (hit.collider.gameObject == this.gameObject){
				print ("hit!");
				print (hit.collider.gameObject.tag);
				MeshRenderer gameObjectRenderer = this.GetComponent<MeshRenderer>();
				Material newMaterial = new Material(Shader.Find("Diffuse"));
				newMaterial.color = highlightColor;
				gameObjectRenderer.material = newMaterial ;
			}
			else {
				Start();
			}
		}
		
	}
}
