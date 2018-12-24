using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource efSource; //SoundEffect
    private AudioSource bgmSource; //Bgm
	private AudioSource bgmSource2; //Bgm

	//singleton
	public static SoundManager instance = null;


	// Use this for initialization
	void Start () {


        if(SoundManager.instance == null){
            instance = this;
        }else if(SoundManager.instance != this){
            Destroy(this);
        }

        //DontDestroyOnLoad(gameObject);

        efSource = GameObject.Find("SESource").GetComponent<AudioSource>();
        bgmSource = GameObject.Find("BGMSource").GetComponent<AudioSource>();
		bgmSource2 = GameObject.Find("BGMSource2").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySingleSound(AudioClip clip, float volume = 1.0f)
	{
        bgmSource.clip = clip;
		bgmSource.volume = volume;
		bgmSource.Play();
    }

    public void PlayRandomSound(params AudioClip[] clips){
        int ramdomIndex = Random.Range(0,clips.Length);

        efSource.clip = clips[ramdomIndex];
        efSource.Play();
    }

	public void PlaySingleSound2(AudioClip clip, float volume = 1.0f)
	{
		bgmSource2.clip = clip;
		bgmSource2.volume = volume;
		bgmSource2.Play();
	}

	public void PlayRandomSound2(params AudioClip[] clips)
	{
		int ramdomIndex = Random.Range(0, clips.Length);

		bgmSource2.clip = clips[ramdomIndex];
		bgmSource2.Play();
	}

	public void StopSingleSound()
    {
        bgmSource.Stop();
    }

	public void PlaySoundSE(AudioClip clip, float volume = 1.0f)
	{
		efSource.clip = clip;
		efSource.volume = volume;
		efSource.PlayOneShot(efSource.clip);
	}

	public void PlayRandomSoundSE(List<AudioClip> clips, float volume = 1.0f)
	{
		int ramdomIndex = Random.Range(0, clips.Count);

		efSource.clip = clips[ramdomIndex];
		efSource.volume = volume;
		efSource.PlayOneShot(efSource.clip);
	}
}
