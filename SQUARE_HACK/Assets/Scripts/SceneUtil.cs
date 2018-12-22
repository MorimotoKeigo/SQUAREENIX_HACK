using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneUtil : MonoBehaviour {

    private static SceneUtil instance = null;
    public static SceneUtil Instance { get { return instance; } }

    [SerializeField]
    private GameObject fadeSystemPrefab;
    private GameObject fadeSystemInstance;
    private Image fadeFilter;

    public enum SceneID
    {
        INVALID = -1,
        TITLE,
        MAIN_GAME,
        RESULT,
        MAX,
    }
    private SceneID currentScene;
    private SceneID nextScene;

    public enum FadeState
    {
        WAIT,
        TO_IN,
        TO_OUT,
        MAX,
    }
    private FadeState fadeState = FadeState.WAIT;

    float fadeTimer;
    float fadeTimeMax;

    void SetupFadeFilter()
    {
        fadeFilter.color = new Color(0, 0, 0, 0);
        GameObject canvas = GameObject.Find("Canvas");
        fadeFilter.rectTransform.sizeDelta = new Vector3(1024,768,1);
    }

	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            fadeSystemInstance = Instantiate(fadeSystemPrefab);
            DontDestroyOnLoad(fadeSystemInstance);
            fadeFilter = GameObject.Find("FadeImage").GetComponent<Image>();
            SetupFadeFilter();
        }
	}
    private void Update()
    {
        switch(fadeState)
        {
            case FadeState.TO_IN:
                fadeTimer -= Time.deltaTime;
                fadeFilter.color = new Color(0, 0, 0, Mathf.Lerp(0, 1.0f, fadeTimer / fadeTimeMax));
                break;
            case FadeState.TO_OUT:
                fadeTimer -= Time.deltaTime;
                fadeFilter.color = new Color(0, 0, 0, Mathf.Lerp(0, 1.0f, 1.0f - fadeTimer / fadeTimeMax));
                break;
            default:
                break;
        }
        if(fadeTimer < 0.0f)
        {
            switch (fadeState)
            {
                case FadeState.TO_IN:
                    fadeState = FadeState.WAIT;
                    break;
                case FadeState.TO_OUT:
                    LoadScene();
                    FadeIn();
                    break;
                default:
                    break;
            }
        }
    }

    // 次のシーンの呼び出し手続き、これを呼べばフェードインアウトは勝手にやってくれる
    public void CallSceneMove(SceneID id, float fadeTime = 1.0f) {
        nextScene = id;
        fadeTimeMax = fadeTime;
        FadeOut();
	}

    public void LoadScene()
    {
        SceneManager.LoadScene((int)nextScene);
        fadeTimer = fadeTimeMax;
    }

    public void FadeOut()
    {
        if (fadeState == FadeState.WAIT)
        {
            fadeState = FadeState.TO_OUT;
            fadeTimer = fadeTimeMax;
        }
    }

    public void FadeIn()
    {
        if (fadeState == FadeState.TO_OUT)
        {
            fadeState = FadeState.TO_IN;
            fadeTimer = fadeTimeMax;
        }
    }

    public void MoveToTitle()
    {
        CallSceneMove(SceneID.TITLE);
    }

    public void MoveToMainGame()
    {
        CallSceneMove(SceneID.MAIN_GAME);
    }

    public void MoveToResult()
    {
        CallSceneMove(SceneID.RESULT);
    }
}
