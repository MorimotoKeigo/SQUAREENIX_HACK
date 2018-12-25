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
	bool stageSE = false;
	public AudioSource audioSource;
	public enum GAUGE_STATE{
		ZERO,
		ONE,
		TWO,
		Max,
	}
	public GAUGE_STATE prevGauge = GAUGE_STATE.ZERO;
	public GAUGE_STATE nowGauge = GAUGE_STATE.ZERO;

		void Awake(){
			audioSource = GetComponent<AudioSource>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerController.justGauge >= 0 && playerController.justGauge < 100)
		{	
			nowGauge = GAUGE_STATE.ZERO;
			playerController.trails[0].SetActive(true);
			playerController.trails[1].SetActive(false);
			
			one.value = playerController.justGauge;
			two.value = 100;
			playerController.speed = 15f;
			Number.sprite = zero;
			effectOneTwo.SetActive(false);
		}
		else if(playerController.justGauge >= 100 && playerController.justGauge < 200)
		{

			if(nowGauge == GAUGE_STATE.ZERO){
				audioSource.PlayOneShot(audioSource.clip,1f);
			}
			playerController.trails[0].SetActive(false);
			playerController.trails[1].SetActive(true);
			playerController.trails[2].SetActive(false);
			nowGauge = GAUGE_STATE.ONE;
			one.value = 100;
			two.value = playerController.justGauge;
			three.value = 200;
			playerController.speed = 15f * 1.3f;
			Number.sprite = ichi;
			effectOneTwo.SetActive(true);
			effectTwoThree.SetActive(false);
		}
		else if(playerController.justGauge >= 200 && playerController.justGauge < 300)
		{
			if(nowGauge == GAUGE_STATE.ONE){
				audioSource.PlayOneShot(audioSource.clip,1f);
				
			}
			playerController.trails[1].SetActive(false);
			playerController.trails[2].SetActive(true);
			playerController.trails[3].SetActive(false);//null

			nowGauge = GAUGE_STATE.TWO;
			two.value = 200;
			three.value = playerController.justGauge;
			playerController.speed = 15f * 1.5f;
			max.enabled = false;
			Number.sprite = san;
			effectTwoThree.SetActive(true);
			effectOneTwo.SetActive(false);
			effectMax.SetActive(false);


		}
		else if(playerController.justGauge >= 300)
		{
			if(nowGauge == GAUGE_STATE.TWO){
				audioSource.PlayOneShot(audioSource.clip,1f);
			}

			playerController.trails[2].SetActive(false);
			if(playerController.trails[3].activeSelf != false)
				playerController.trails[3].SetActive(true);


			playerController.speed = 15f * 1.7f;

			nowGauge = GAUGE_STATE.Max;
			max.enabled = true;

			Number.sprite = man;
			effectMax.SetActive(true);
			effectTwoThree.SetActive(false);


		}

	}
}
