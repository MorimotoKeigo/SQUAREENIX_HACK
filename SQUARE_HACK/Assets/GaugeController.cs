using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GaugeController : MonoBehaviour {

	public Slider one;
	public Slider two;
	public Slider three;
	public PlayerController playerController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerController.justGauge >= 0 && playerController.justGauge <= 100)
		{
			one.value = playerController.justGauge;
			playerController.speed = 20f;
		}
		else if(playerController.justGauge >= 100 && playerController.justGauge <= 200)
		{
			two.value = playerController.justGauge;
			playerController.speed = 20f * 1.5f;

		}
		else if(playerController.justGauge >= 200)
		{
			three.value = playerController.justGauge;
			playerController.speed = 20f * 2.0f;

		}
	}
}
