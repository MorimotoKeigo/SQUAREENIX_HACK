using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour {

	private bool isHitBody = false;

	public bool IsHitBody{
		get{return isHitBody;}
		set{isHitBody = value;}
	}

	void OnTriggerEnter(Collider other)
	{
		isHitBody = true;
		Debug.Log("enter");

	}
	void OnTriggerExit(Collider other) 
	{
		isHitBody = false;
		Debug.Log("exit");
		
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log("enter");
		isHitBody = true;
	}

	void OnCollisionExit(Collision other) {
		isHitBody = false;
		Debug.Log("exit");
	}
}
