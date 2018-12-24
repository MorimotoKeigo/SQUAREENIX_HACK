using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {
	public GameObject cameraObject;
	private Vector3 prev;
	// Use this for initialization
	void Start () {
		prev = cameraObject.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position += Vector3.up * Mathf.Abs(cameraObject.transform.position.y - prev.y) / 2.0f;
		prev = cameraObject.transform.position;
	}
}
