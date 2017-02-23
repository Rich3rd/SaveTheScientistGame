using UnityEngine;
using System.Collections;

public class SkiddingScript : MonoBehaviour {
	
	public GameObject skidSound;
	public float soundEmission = 15;
	public Material skidMaterial;
	
	private float skidAt = 0.8f;
	private float currentFrictionValue;
	private float soundWait;
	float markWidth = 0.2f;
	int skidding;
	private Vector3[] lastPos = new Vector3[2];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		WheelHit hit;
		WheelCollider wheel = (WheelCollider) transform.GetComponent(typeof(WheelCollider));
		wheel.GetGroundHit(out hit);
		currentFrictionValue = Mathf.Abs(hit.sidewaysSlip);
		if(skidAt<=currentFrictionValue && soundWait <=0)
		{
			Instantiate(skidSound,hit.point,Quaternion.identity);
			soundWait =1;
		}
		soundWait -= Time.deltaTime*soundEmission;
		if(skidAt<=currentFrictionValue)
		{
			SkidMesh();
		}
		else
			skidding=0;
	}
	
	void SkidMesh()
	{
		int i;
		WheelHit hit;
		WheelCollider wheel = (WheelCollider) transform.GetComponent(typeof(WheelCollider));
		wheel.GetGroundHit(out hit);
		GameObject mark = new GameObject("mark");
		MeshFilter filter  = (MeshFilter) mark.AddComponent<MeshFilter>();
		mark.AddComponent<MeshRenderer>();
		Mesh markMesh = new Mesh();
		Vector3[] vertices = new Vector3[4];
		int[] triangles;
		if(skidding ==0)
		{
			vertices[0] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*(new Vector3(markWidth,0.01f,0)); 
			vertices[1] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*(new Vector3(-markWidth,0.01f,0)); 
			vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*(new Vector3(-markWidth,0.01f,0)); 
			vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*(new Vector3(markWidth,0.01f,0)); 
			lastPos[0] = vertices[2];
			lastPos[1] = vertices[3];
			skidding=1;
		}
		else
		{
			vertices[1] = lastPos[0];
			vertices[0] = lastPos[1];
			vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*(new Vector3(-markWidth,0.01f,0)); 
			vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y,transform.eulerAngles.z)*(new Vector3(markWidth,0.01f,0)); 
			lastPos[0] = vertices[2];
			lastPos[1] = vertices[3];
		}
		triangles = new int[]{0,1,2,2,3,0};
		markMesh.vertices = vertices;
		markMesh.triangles = triangles;
		markMesh.RecalculateNormals();
		Vector2[] uvm = new Vector2[4];
		uvm[0] = new Vector2(1,0);
		uvm[1] = new Vector2(0,0);
		uvm[2] = new Vector2(0,1);
		uvm[3] = new Vector2(1,1);
		markMesh.uv = uvm;
		filter.mesh = markMesh;
		mark.GetComponent<Renderer>().material = skidMaterial;
		mark.AddComponent<DestroyTimer>();
	}
	
}
