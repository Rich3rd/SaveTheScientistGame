using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour {
	
	public float destroyAfter = 2;
	private float timer = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer +=Time.deltaTime;
		if(destroyAfter <= timer)
			Destroy(gameObject);
	}
}
