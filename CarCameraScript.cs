using UnityEngine;
using System.Collections;

public class CarCameraScript : MonoBehaviour {
	public Transform car;
	public float distance = 2.0f;
	public float height = 0.5f;
	public float rotationDamping = 3.0f;
	public float heightDamping = 2.0f;
	public float zoomRatio = 0.5f;
	private Vector3 rotationVector;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		float wantedAngle = car.eulerAngles.y;
		float wantedHeight = car.position.y + height;
		float myAngle = transform.eulerAngles.y;
		float myHeight = transform.position.y;
		myAngle = Mathf.LerpAngle(myAngle,wantedAngle,rotationDamping*Time.deltaTime);
		myHeight = Mathf.Lerp (myHeight,wantedHeight,heightDamping*Time.deltaTime);
		Quaternion currentRotation = Quaternion.Euler(0,myAngle,0);
		transform.position = car.position;
		transform.position -= currentRotation*Vector3.forward*distance;
		Vector3 newPosition = transform.position;
		newPosition.y = myHeight;
		transform.position = newPosition;
		transform.LookAt(car);
	}
}
