using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public enum PLAYER_STATE{
		RIGHT,
		LEFT,
		Air
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
	// Use this for initialization
	private int jumpCnt = 0;
	private float time= 0;

	private Rigidbody rigidBody;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			Jump();
		}
		// transform.position += new Vector3(Mathf.Cos(angle) * velocityX,Mathf.Sin(angle) * velocityY,0);
		rigidBody.velocity = new Vector3(Mathf.Cos(angle) * velocityX * speed,Mathf.Sin(angle)  * velocityY * speed,0);
		// Debug.Log(rigidBody.velocity);
		

		if(jumpCnt == 2)
		{
			time += Time.deltaTime;
			if(time >= 0.01f)
			{
				if(secondAngle >= angle * Mathf.Rad2Deg)
					angle += 10 * Mathf.Deg2Rad;
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
				}else if(jumpCnt == 1)
				{
					jumpCnt++;
				}
			break;
			case PLAYER_STATE.LEFT:
				if(jumpCnt == 0)
				{
					SetPlayerSpeed(1f,1f,firstAngle);
					jumpCnt++;
				}else if(jumpCnt == 1)
				{
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
	
	// void OnTriggerEnter(Collider other)
	// {
	// 	// if(other.gameObject.tag == "wall"){
	// 	if(!playerHead.IsHitHead)
	// 	{
	// 		velocityX = 0;
	// 		velocityY = 0;
	// 		switch(playerState){
	// 			case PLAYER_STATE.RIGHT:
	// 				playerState = PLAYER_STATE.LEFT;
	// 				jumpCnt = 0;
	// 				break;
	// 			case PLAYER_STATE.LEFT:
	// 				playerState = PLAYER_STATE.RIGHT;
	// 				jumpCnt = 0;
	// 				break;
	// 			case PLAYER_STATE.Air:
	// 				break;
	// 		}
	// 	}
	// 	// }
	// }

	void OnCollisionEnter(Collision other)
	{
		// if(!playerHead.IsHitHead)
		// {
		if(other.gameObject.tag == "Damage")
		{
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
		}else{
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

}
