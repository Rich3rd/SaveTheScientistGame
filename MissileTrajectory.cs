using UnityEngine;
using System.Collections;

public class MissileTrajectory : MonoBehaviour {


	public GameObject explosion;
	Object thisExplosion;
	public AudioClip audioSource;
	AudioSource audio;
	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		GetComponent<Rigidbody>().AddForce (transform.TransformDirection (Vector3.forward) * 10.0f);
		audio = GetComponent<AudioSource> ();
	}

	public void OnCollisionEnter(Collision collision) {
	  
		audio.PlayOneShot (audioSource);
		ContactPoint contact = collision.contacts[0];
		thisExplosion = Instantiate(explosion, contact.point + (contact.normal) , Quaternion.identity);
		Destroy (thisExplosion, 2.0f);
		Destroy (gameObject);
	}

}






