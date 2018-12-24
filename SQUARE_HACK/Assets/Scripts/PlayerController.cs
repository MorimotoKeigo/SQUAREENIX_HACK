using System.Collections;
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
	public int jumpCnt = 0;
	private float time= 0;
	private Rigidbody rigidBody;
	private float collisionFrame = 0;
	public AudioClip jumpSE;

	public AudioClip badSE;
	
	public AudioClip goodSE;

	public AudioClip parfectSE;


	public Animator playerAnim;
	public bool isDamage;
	public bool isStopDamage;
	public float justGauge = 0;

	public float CollisionFrame
	{
		get{return collisionFrame;}
	}

	public float badFrame = 1.0f;
	public float goodFrame = 0.5f;
	public float perfectFrame = 0.25f;

	private GameObject trailFX; 

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(justGauge < 0)
			justGauge = 0;
		else if(justGauge > 300)
			justGauge = 300;
		if(Input.GetKeyDown(KeyCode.Space)){
			if(jumpCnt == 0)
				JustResult();
			Jump();
		}

		// transform.position += new Vector3(Mathf.Cos(angle) * velocityX,Mathf.Sin(angle) * velocityY,0);
		// if(jumpCnt == 2)
		// 	rigidBody.velocity = new Vector3(Mathf.Cos(angle) * velocityX * speed,Mathf.Sin(angle)  * velocityY * speed,0);
		// else
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
		if(isStopDamage)
			return;
		switch(playerState){
			case PLAYER_STATE.RIGHT:
				if(jumpCnt == 0)
				{
					SetPlayerSpeed(-1f,1f,firstAngle);
					jumpCnt++;
					//SoundManager.instance.PlaySoundSE(jumpSE,5.0f);
					FirstJumpFX();
					playerAnim.SetInteger("State",4);

					
				}else if(jumpCnt == 1)
				{
					SoundManager.instance.PlaySoundSE(jumpSE,5.0f);
					secondJumpFX();
					velocityY = 1;
					// speed -= decreaseGaugeDouble;
					justGauge -= decreaseGaugeDouble;
					angle = secondAngle * Mathf.Deg2Rad;
					playerAnim.SetInteger("State",5);
					jumpCnt++;
				}
			break;
			case PLAYER_STATE.LEFT:
				if(jumpCnt == 0)
				{
					//SoundManager.instance.PlaySoundSE(jumpSE,5.0f);
					FirstJumpFX();
					SetPlayerSpeed(1f,1f,firstAngle);
					playerAnim.SetInteger("State",6);
					jumpCnt++;
				}else if(jumpCnt == 1)
				{
					SoundManager.instance.PlaySoundSE(jumpSE,5.0f);
					secondJumpFX();
					velocityY = 1;
					// speed -= decreaseGaugeDouble;
					justGauge -= decreaseGaugeDouble;
					angle = secondAngle * Mathf.Deg2Rad;
					playerAnim.SetInteger("State",7);
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
	public void FirstJumpFX(){
		ParticleManager.instance.PlayFX(transform.position,1);
		if(playerState == PLAYER_STATE.LEFT)
			ParticleManager.instance.PlayFX(transform.position,3);
		else if(playerState == PLAYER_STATE.RIGHT)
			ParticleManager.instance.PlayFX(transform.position,4);

			
	}
	public void secondJumpFX(){
		trailFX = ParticleManager.instance.PlayTrailFX(transform.position,1);
		ParticleManager.instance.PlayFX(transform.position,2);
	}
	public void JustResult()
	{	
		var nowTime = GameManager.instance.GetCurrentFrameTime();
		if(nowTime - collisionFrame >= badFrame)
		{
			Debug.Log("bad");
			SoundManager.instance.PlaySoundSE(badSE,10.0f);
			// speed -= decreaseGaugeBad;
			justGauge -= decreaseGaugeBad;
			justResult = JUST_RESULT.BAD;
			justGauge -= 6;
		}
		else if(nowTime - collisionFrame <= perfectFrame)
		{
			Debug.Log("perfect");
			SoundManager.instance.PlaySoundSE(parfectSE,10.0f);
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
			SoundManager.instance.PlaySoundSE(goodSE,10.0f);
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
			justGauge -= decreaseGaugeDamage;
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
		else if(other.gameObject.tag == "StopDamage" && !isStopDamage)
		{
			StartCoroutine(StopDamage());
			justGauge -= decreaseGaugeDamage;
			velocityX = 0;
			velocityY = 0;
			
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
					playerAnim.SetInteger("State",3);

					break;
				case PLAYER_STATE.LEFT:
					playerState = PLAYER_STATE.RIGHT;
					jumpCnt = 0;
					playerAnim.SetInteger("State",2);					
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
			justGauge -= decreaseGaugeDamage;
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

	IEnumerator StopDamage(){
		float start = GameManager.instance.GetCurrentFrameTime();
		 isStopDamage = true;
        while ( true ) {
			var now = GameManager.instance.GetCurrentFrameTime();
			if(now - start >= 1)
			{
            playerRender.enabled = true;
			isStopDamage = false;
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
			yield break;				
			}
            playerRender.enabled = !playerRender.enabled;
            yield return new WaitForSeconds(0.1f);
        }
	}

}
