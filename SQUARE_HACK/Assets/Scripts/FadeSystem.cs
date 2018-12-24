using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///------------------------------------------------------
///------------------------------------------------------
/// <summary>
/// ■Sprite画像を利用して,色々な演出を行うクラス.
/// </summary>
public class FadeSystem : MonoBehaviour
{
    //色のための変数-------------------------------------------
    public float alpha;             //アルファ値
    public float alphaControlTime;  //アルファ値の変化時間
    //--------------------------------------------------------

    //GameObjectの取得----------------------------------------
    public GameObject spriteObject; //2Dオブジェクト(Sprite画像)の読み込み
    //-------------------------------------------------------

    //列挙型の取得--------------------------------------------
    public StagingType stagingType; //演出の種類
    //-------------------------------------------------------

    public GameObject sprite;
    /// <summary>
    /// ●演出の種類
    /// </summary>
    public enum StagingType
    {
        /// 演出なし
        non,
        /// フェードイン演出
        fadein,
        /// フェードアウト演出
        fadeout
    };


    ///------------------------------------------------------
    /// <summary>
    /// ●初期化する関数.
    /// </summary>

    public static FadeSystem instance = null;
    

    void Start ()
    {
        if(SoundManager.instance == null ){
            instance = this;
        }else if(SoundManager.instance != this){
            Destroy(this);
        }

        
        //DontDestroyOnLoad(gameObject);
        
        StandingTypeChange ();  //演出の種類を切り替える関数の呼び出し
        
        
    }


    ///------------------------------------------------------
    /// <summary>
    /// ●初期化時に演出の種類によって対応を切り替える関数.
    /// </summary>
    public void StandingTypeChange ()
    {
        /*演出の種類が,*/
        switch (stagingType) {
        case StagingType.non:   /*演出なしの場合,*/
            /*何もしない.*/
            break;
        case StagingType.fadein:   /*フェードイン演出の場合,*/
            /*アルファ値を1(色を黒)にする.*/
            alpha = 1f;
            break;
        case StagingType.fadeout:  /*フェードアウト演出の場合,*/
            /*アルファ値を0(透明)にする.*/
            alpha = 0f;
            break;
        }
    }


    ///------------------------------------------------------
    /// <summary>
    /// ●演出の種類を切り替える関数.
    /// </summary>
    public void StandingTypeInvoke ()
    {
        /*演出の種類が,*/
        switch (stagingType)
        {
        case StagingType.non:   /*演出なしの場合,*/
            /*何もしない.*/
            break;
        case StagingType.fadein:   /*フェードイン演出の場合,*/
            /*フェードイン演出を行う.*/
            FadeIn ();
            break;
        case StagingType.fadeout:  /*フェードアウト演出の場合,*/
            /*フェードアウト演出を行う.*/
            FadeOut ();
            break;
        }
    }


    ///------------------------------------------------------
    /// <summary>
    /// ●フェードイン演出を行う関数.
    /// </summary>
    public void FadeIn ()
    {
        alpha -= Time.deltaTime * alphaControlTime; //アルファ値の変化

        /*Scene内にフェード(2Dオブジェクト)が存在するとき,*/
        if (GameObject.Find ("Fade") != null)
        {
            /*フェード(2Dオブジェクト)を発見し,取得する.*/
            spriteObject = GameObject.Find ("Fade");
        }else{
            spriteObject = sprite;
        }

        //フェード(2Dオブジェクト)の色の読み込み--------------------
        spriteObject.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, alpha);
        //-------------------------------------------------------

        /*アルファ値が0以下のとき,*/
        if (alpha <= 0f)
        {
            /*アルファ値は0になる.*/
            alpha = 0f;
        }
    }


    ///------------------------------------------------------
    /// <summary>
    /// ●フェードアウト演出を行う関数.
    /// </summary>
    public void FadeOut ()
    {
        alpha += Time.deltaTime * alphaControlTime; //アルファ値の変化

        /*Scene内にフェード(2Dオブジェクト)が存在するとき,*/
        if (GameObject.Find ("Fade") != null)
        {
            /*フェード(2Dオブジェクト)を発見し,取得する.*/
            spriteObject = GameObject.Find ("Fade");
        }else{
            spriteObject = sprite;
        }   
        //フェード(2Dオブジェクト)の色の読み込み--------------------
        spriteObject.GetComponent<SpriteRenderer> ().color = new Color (0, 0, 0, alpha);
        //-------------------------------------------------------

        /*アルファ値が1以上のとき,*/
        if (alpha >= 1f)
        {
            /*アルファ値は1になる.*/
            alpha = 1f;
        }
    }


    ///------------------------------------------------------
    /// <summary>
    /// ●1秒間に呼ばれる回数が一定で実行する関数.
    /// </summary>
    void FixedUpdate ()
    {
        StandingTypeInvoke ();  //演出の種類を切り替える関数の呼び出し
    }

	public void ChangeStagingType(int state){
		switch(state){
			case 0:
				stagingType = StagingType.non;
				//FadeIn();
				break;
			case 1:
				stagingType = StagingType.fadein;
				//FadeOut();
				break;
			case 2:
				stagingType = StagingType.fadeout;
				break;
				
		}
	}

}