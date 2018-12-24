using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GaugeController : MonoBehaviour {

	public Slider one;
	public Slider two;
	public Slider three;
	public Image max;
	public PlayerController playerController;
	public Image Number;
	public Sprite zero;
	public Sprite ichi;
	public Sprite ni;
	public Sprite san;
	public Sprite man;
	public GameObject effect;
	public GameObject effectOneTwo;
	public GameObject effectTwoThree;
	public GameObject effectMax;
	void Awake(){
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerController.justGauge >= 0 && playerController.justGauge < 100)
		{
			one.value = playerController.justGauge;
			two.value = 100;
			playerController.speed = 20f;
			Number.sprite = zero;
			effectOneTwo.SetActive(false);

		}
		else if(playerController.justGauge >= 100 && playerController.justGauge < 200)
		{
			one.value = 100;
			two.value = playerController.justGauge;
			three.value = 200;
			playerController.speed = 20f * 1.5f;
			Number.sprite = ichi;
			effectOneTwo.SetActive(true);
			effectTwoThree.SetActive(false);

			
		}
		else if(playerController.justGauge >= 200 && playerController.justGauge < 300)
		{
			two.value = 200;
			three.value = playerController.justGauge;
			playerController.speed = 20f * 2.0f;
			max.enabled = false;
			Number.sprite = san;
			effectTwoThree.SetActive(true);
			effectOneTwo.SetActive(false);
			effectMax.SetActive(false);


		}
		else if(playerController.justGauge >= 300)
		{
			max.enabled = true;
			Number.sprite = man;
			effectMax.SetActive(true);
			effectTwoThree.SetActive(false);


		}

	}
}
