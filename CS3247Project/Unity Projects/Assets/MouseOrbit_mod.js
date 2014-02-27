var target : Transform;
var distance = 5.0;

var xSpeed = 250.0;
var ySpeed = 120.0;

var yMinLimit = -20;
var yMaxLimit = 80;

private var x = 0.0;
private var y = 0.0;

var yPosition = Vector3(0.0,0.0,0.0);

@script AddComponentMenu("Camera-Control/Mouse Orbit")

function Start () {
    var angles = transform.eulerAngles;
    x = angles.y;
    y = angles.x;
	// Make the rigid body not change rotation
   	if (rigidbody)
		rigidbody.freezeRotation = true;
}

function LateUpdate () {
    if (target) {
    	if (Input.GetKey("left")){
    		x+=xSpeed*0.02;
    	}
    	if (Input.GetKey("right")){
    		x-=xSpeed*0.02;
    	}
    	
        if (Input.GetKey("up")){
    		y+=ySpeed*0.02;
    	}
    	if (Input.GetKey("down")){
    		y-=ySpeed*0.02;
    	}
    	
    	if (Input.GetKey("w")){
    	    yPosition.y+=0.1;
    	    //center.transform.y+=0.1;
    	}
    	if (Input.GetKey("s")){
    	    yPosition.y-=0.1;
    	    //center.transform.y+=0.1;
    	}
        //x += Input.GetAxis("Mouse X") * xSpeed * 0.02;
        //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02;
 		
 		//y = ClampAngle(y, yMinLimit, yMaxLimit);
 		       
        var rotation = Quaternion.Euler(y, x, 0);
        var position = rotation * Vector3(0.0, 0.0, -distance) + target.position + yPosition;
        
        transform.rotation = rotation;
        transform.position = position;
    }
}

static function ClampAngle (angle : float, min : float, max : float) {
	if (angle < -360)
		angle += 360;
	if (angle > 360)
		angle -= 360;
	return Mathf.Clamp (angle, min, max);
}