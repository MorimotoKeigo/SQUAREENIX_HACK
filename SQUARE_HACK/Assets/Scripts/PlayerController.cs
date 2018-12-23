﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public enum PLAYER_STATE{
		RIGHT,
		LEFT,
		Air
	}
	public enum JUST_RESULT{
		BAD,
		GOOD,
		PERFECT
	}
	[SerializeField]
	PlayerBody playerBody;
	[SerializeField]
	PlayerHead playerHead;
	[SerializeField]
	float velocityX,velocityY;
	[SerializeField]
	private PLAYER_STATE playerState = PLAYER_STATE.RIGHT;
	public float angle = 30;
	public float firstAngle = 30;
	public float secondAngle = 60;
	public float speed = 10;
	public float secondSpeed;
	public float increaseGaugePerfect = 5;
	public float decreaseGaugeBad = 5;
	public float decreaseGaugeDamage = 10;
	public float decreaseGaugeDouble = 3;
	public JUST_RESULT justResult;
	public SpriteRenderer playerRender;
	// Use this for initialization
	private int jumpCnt = 0;
	private float time= 0;
	private Rigidbody rigidBody;
	private float collisionFrame = 0;
	public AudioClip jumpSE;
	public bool isDamage;
	public float justGauge = 0;

	public float CollisionFrame
	{
		get{return collisionFrame;}
	}

	const float badFrame = 1.0f;
	const float goodFrame = 0.5f;
	const float perfectFrame = 0.25f;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			if(jumpCnt == 0)
				JustResult();
			Jump();
			
		}
		// transform.position += new Vector3(Mathf.Cos(angle) * velocityX,Mathf.Sin(angle) * velocityY,0);
		if(jumpCnt == 2)
			rigidBody.velocity = new Vector3(Mathf.Cos(angle) * velocityX * speed,Mathf.Sin(angle)  * velocityY * speed,0);
		else
			rigidBody.velocity = new Vector3(Mathf.Cos(angle) * velocityX * speed,Mathf.Sin(angle)  * velocityY * speed,0);

		// Debug.Log(rigidBody.velocity);
		
		if(jumpCnt == 2)
		{
			time += Time.deltaTime;
			if(time >= 0.01f)
			{
				if(firstAngle <= angle * Mathf.Rad2Deg)
					angle -= 5 * Mathf.Deg2Rad;
				time = 0f;
			}
		}else
		{
			time = 0f;
		}

		if(playerHead.IsHitHead)
		{
			velocityY = -1;
			Debug.Log("head");
		}
		else if(playerBody.IsHitBody)
		{
			velocityX = 0;
			velocityY = 0;
			switch(playerState){
				case PLAYER_STATE.RIGHT:
					playerState = PLAYER_STATE.LEFT;
					break;
				case PLAYER_STATE.LEFT:
					playerState = PLAYER_STATE.RIGHT;
					break;
				case PLAYER_STATE.Air:
					break;
			}
		}

		
	}

	void Jump(){
		switch(playerState){
			case PLAYER_STATE.RIGHT:
				if(jumpCnt == 0)
				{
					SetPlayerSpeed(-1f,1f,firstAngle);
					jumpCnt++;
					SoundManager.instance.PlaySoundSE(jumpSE,5.0f);
				}else if(jumpCnt == 1)
				{
					SoundManager.instance.PlaySoundSE(jumpSE,5.0f);
					velocityY = 1;
					// speed -= decreaseGaugeDouble;
					justGauge -= decreaseGaugeDouble;
					angle = secondAngle * Mathf.Deg2Rad;
					jumpCnt++;
				}
			break;
			case PLAYER_STATE.LEFT:
				if(jumpCnt == 0)
				{
					SoundManager.instance.PlaySoundSE(jumpSE,5.0f);
					SetPlayerSpeed(1f,1f,firstAngle);
					jumpCnt++;
				}else if(jumpCnt == 1)
				{
					SoundManager.instance.PlaySoundSE(jumpSE,5.0f);
					velocityY = 1;
					// speed -= decreaseGaugeDouble;
					justGauge -= decreaseGaugeDouble;
					angle = secondAngle * Mathf.Deg2Rad;
					jumpCnt++;
				}
			break;
			case PLAYER_STATE.Air:
				velocityX = 0;
				velocityY = 0;
				jumpCnt = 0;
			break;

		}
	}
	public void JustResult()
	{	
		var nowTime = GameManager.instance.GetCurrentFrameTime();
		if(nowTime - collisionFrame >= badFrame)
		{
			Debug.Log("bad");
			// speed -= decreaseGaugeBad;
			justGauge -= decreaseGaugeBad;
			justResult = JUST_RESULT.BAD;
			justGauge -= 6;
		}
		else if(nowTime - collisionFrame <= perfectFrame)
		{
			Debug.Log("perfect");
			justResult = JUST_RESULT.PERFECT;	
			// speed += increaseGaugePerfect;		
			justGauge += increaseGaugePerfect;

		}
		else if(nowTime - collisionFrame <= goodFrame)
		{
			justResult = JUST_RESULT.GOOD;
			// speed += increaseGaugePerfect / 2;	
			justGauge += increaseGaugePerfect / 2;		
			Debug.Log("good");
		}
	}

	public JUST_RESULT GetJustResult(){
		return justResult;
	}


	void OnCollisionEnter(Collision other)
	{
		// if(!playerHead.IsHitHead)
		// {
		if(other.gameObject.tag == "Damage" && !isDamage)
		{
			StartCoroutine(Blink());
			speed -= decreaseGaugeDamage;
			velocityX = 0;
			velocityY = 0;
			switch(playerState){
				case PLAYER_STATE.RIGHT:
					playerState = PLAYER_STATE.LEFT;
					jumpCnt = 0;
					Jump();
					break;
				case PLAYER_STATE.LEFT:
					playerState = PLAYER_STATE.RIGHT;
					jumpCnt = 0;
					Jump();
					break;
				case PLAYER_STATE.Air:
					break;
			}
		}else if(other.gameObject.tag == "Head")
		{
			velocityY = -1;
		}else{
			rigidBody.useGravity = true;
			collisionFrame = GameManager.instance.GetCurrentFrameTime();
			velocityX = 0;
			velocityY = 0;
			switch(playerState){
				case PLAYER_STATE.RIGHT:
					playerState = PLAYER_STATE.LEFT;
					jumpCnt = 0;
					break;
				case PLAYER_STATE.LEFT:
					playerState = PLAYER_STATE.RIGHT;
					jumpCnt = 0;
					break;
				case PLAYER_STATE.Air:
					break;
			}
		
		}
	}

	private void OnCollisionStay(Collision other) {
		if(other.gameObject.tag == "Damage" && !isDamage)
		{
			StartCoroutine(Blink());
			// speed -= decreaseSpeedDamage;
			justGauge += decreaseGaugeDamage;
			velocityX = 0;
			velocityY = 0;
			switch(playerState){
				case PLAYER_STATE.RIGHT:
					playerState = PLAYER_STATE.LEFT;
					jumpCnt = 0;
					Jump();
					break;
				case PLAYER_STATE.LEFT:
					playerState = PLAYER_STATE.RIGHT;
					jumpCnt = 0;
					Jump();
					break;
				case PLAYER_STATE.Air:
					break;
			}
		}
	}
	private void OnCollisionExit(Collision other) {
		rigidBody.useGravity = false;
		
	}

	public Vector3 GetPlayerPosition(){
		return transform.position;
	}
	public PLAYER_STATE GetPlayerState(){
		return playerState;
	}

	void SetPlayerSpeed(float vx, float vy, float ang)
	{
		velocityX = vx;
		velocityY = vy;
		angle = ang * Mathf.Deg2Rad;
	}

	 IEnumerator Blink() {
		 float start = GameManager.instance.GetCurrentFrameTime();
		 isDamage = true;
        while ( true ) {
			var now = GameManager.instance.GetCurrentFrameTime();
			if(now - start >= 1)
			{
            playerRender.enabled = true;
			isDamage = false;
			yield break;				
			}
            playerRender.enabled = !playerRender.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }

}
