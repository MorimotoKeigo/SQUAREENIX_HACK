using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {
	
    public static ParticleManager instance = null;

	[SerializeField]
	public GameObject FX_tap;
	
	private GameObject FX_tap_Inst;


	private GameObject FX_List_Inst;
	public GameObject[] FX_List;


	[SerializeField]
	public float DestoroyTime = 1.0f;

	// Use this for initialization
	void Start () {
			
	}

	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//このオブジェクトを破壊する
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayFX(Vector3 pos,int id){
		//FX_tap_Inst = Instantiate(FX_tap, pos,Quaternion.identity);		
		FX_List_Inst = Instantiate(FX_List[id], pos , Quaternion.identity);
		Destroy(FX_List_Inst,DestoroyTime);
		//Invoke("StopFX",1f);
	}

	public void StopFX(){
		FX_List_Inst.GetComponent<ParticleSystem>().Stop(true);
		//Destroy(FX_List_Inst);
		//FX_tap_Inst.GetComponent<ParticleSystem>().Stop(true);
	}

	
}
