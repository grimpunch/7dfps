using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	transform.RotateAroundLocal(Vector3.left,3);
		transform.RotateAroundLocal(Vector3.down,6);
	}
}
