	var c1 : Color = Color.yellow;
	var c2 : Color = Color.red;
	var center:GameObject;
	var lengthOfLineRenderer : int = 20;
	
	function Start() {
		 var lineRenderer : LineRenderer = gameObject.AddComponent(LineRenderer);
		 lineRenderer.material = new Material (Shader.Find("Particles/Additive"));
		 lineRenderer.SetColors(c1, c2);
		 lineRenderer.SetWidth(0.2,0.2);
		 lineRenderer.SetVertexCount(lengthOfLineRenderer);
	}
	function Update() {
		if (Input.GetMouseButtonDown(0)){
			var lineRenderer : LineRenderer = GetComponent(LineRenderer);
			var direction:Vector3;
			direction = center.transform.position - this.transform.position;
			var midpoint:Vector3;
			midpoint = center.transform.position + this.transform.position;
			var ERROR=Vector3(0,0.01,0);
			for(var i : int = 0; i < lengthOfLineRenderer; i++) {
				//this.transform.position;
				var pos : Vector3 = direction * i/1.0/lengthOfLineRenderer + this.transform.position-ERROR;
				lineRenderer.SetPosition(i, pos);
			}
		}
	}