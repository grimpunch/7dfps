using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour {
	
	public float speed;
	public GameObject spawner;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAroundLocal(Vector3.up,speed);
	
	}
	public void OnDrawGizmos()
    {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(Vector3.zero,spawner.transform.position);
		
    }
}
