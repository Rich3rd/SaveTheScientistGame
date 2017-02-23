using UnityEngine;
using System.Collections;


// add this script to an empty game object called Control



public class GUIControl : MonoBehaviour {
	

	// create GameObject variable

	GameObject carControl;
	public bool brakePressed = false;
	public bool accPressed = false;
	public bool handbrakePressed = false;
	public bool firePressed = false;

	AudioSource audio;
	public AudioClip missileLaunchSound;
	public GameObject missle;
	


	// Use this for initialization

	void Start () {
		
		// assign the GameObject called Car to the GameObject variable
		// this must exactly match the GameObject name in the Hierarchy

		carControl = GameObject.Find("Car"); 
		audio = gameObject.AddComponent<AudioSource> ();
		
	}



	
	public void OnGUI () {


		// create some GUI buttons
		
		
		if (GUI.RepeatButton (new Rect (400, 600, 300, 100), "Accelerate")) {
			
			// get the component (script) attached to the Car GameObject and
			// call its drive() method

			carControl.GetComponent<CarControlScript> ().drive ();
			accPressed = true; 
		}
			else 
				accPressed = false;
		

		if (GUI.RepeatButton (new Rect (10, 600, 300, 100), "Brake")) {
			
			
			// put your code here
			carControl.GetComponent<CarControlScript> ().brake ();
			brakePressed = true;
		} else
			brakePressed = false;

		if (GUI.RepeatButton (new Rect (800, 600, 300, 100), "Handbrake")) {
			
			
			// put your code here
			carControl.GetComponent<CarControlScript> ().HandBrake ();
			handbrakePressed = true;
		} else {
			handbrakePressed = false;
		}

		if (carControl.GetComponent<CarControlScript> ().cubepickup) {
			if (GUI.Button (new Rect (800, 400, 300, 100), "Fire")) {

					audio.PlayOneShot(missileLaunchSound);
					Vector3 position = new Vector3(0, 1f, 5);
					position = transform.TransformPoint (position);
					GameObject thisMissile = Instantiate (missle,position, transform.rotation) as GameObject;
					Physics.IgnoreCollision(thisMissile.GetComponent<Collider>(), GetComponent<Collider>());
			}
		}
		
			
		}



	}

	
	// Update is called once per frame
