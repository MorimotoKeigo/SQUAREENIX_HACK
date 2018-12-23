﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour {
	
	private bool isHitHead = false;

	public bool IsHitHead{
		get{return isHitHead;}
		set{isHitHead = value;}
	}

	void OnTriggerEnter(Collider other)
	{
		isHitHead = true;
		Debug.Log("enter");

	}
	void OnTriggerExit(Collider other) 
	{
		isHitHead = false;
		Debug.Log("exit");
	}

	void OnCollisionEnter(Collision other)
	{
		isHitHead = true;
		Debug.Log("enter");

	}

	void OnCollisionExit(Collision other) {
		isHitHead = false;
		Debug.Log("exit");
	}
}
