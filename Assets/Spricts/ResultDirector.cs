using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.Threading.Tasks;

public class ResultDirector : MonoBehaviour
{
    private AudioSource audioSource = null;
    public AudioClip SE1;
    public AudioClip SE2;

    [SerializeField] Text iText;
    [SerializeField] Text mText;
    [SerializeField] Text pmText;
    [SerializeField] Text sText;

    private string iString;
    private string mString;
    private string perString;
    private string sString;

    private float perNum;
    private float sNum;
    private static float time;
    float t;

    public void Start()
    {
        Button button1 = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        Application.targetFrameRate = 60;

        if(Typing.GetInputNum() == 0)
        {
            perNum = 0;
        }
        else
        {
            perNum = (Typing.GetInputNum() - Typing.GetMiss()) * 100 / Typing.GetInputNum();
        }
        
        //Debug.Log(perNum);
        sNum = Typing.GetInputNum() * 5 - Typing.GetMiss() * 10;

        iString = Typing.GetInputNum().ToString() + "文字";
        mString = Typing.GetMiss().ToString() + "文字";
        perString = perNum.ToString() + "％";
        sString = sNum.ToString();

        time = 0;

        iText.text = "";
        mText.text = "";
        pmText.text = "";
        sText.text = "";

        //ここからサーバーへのデータストア処理
        NCMBUser currentUser = NCMBUser.CurrentUser;
        if (currentUser != null)
        {
            UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);
        }
        else
        {
            UnityEngine.Debug.Log("未ログインまたは取得に失敗");
        }

        int LevelFlag=ButtonDirector.GetLevel();
       
        
        switch (LevelFlag)
        {
            case 1:
                // easyに対する処理
                //NCMBObjectを作成
                NCMBObject easy = new NCMBObject("easyData");

                //UserNameとscoreをdataクラスに保存
                easy["score"] = sNum;
                easy["UserName"] = currentUser.UserName;
                
                easy.SaveAsync();
                break;
            case 2:
                // nomalに対する処理
                //NCMBObjectを作成
                NCMBObject nomal = new NCMBObject("nomalData");

                //UserNameとscoreをdataクラスに保存
                nomal["score"] = sNum;
                nomal["UserName"] = currentUser.UserName;
             
                nomal.SaveAsync();
                break;
            case 3:
                // hardに対する処理
                //NCMBObjectを作成
                NCMBObject hard = new NCMBObject("hardData");

                //UserNameとscoreをdataクラスに保存
                hard["score"] = sNum;
                hard["UserName"] = currentUser.UserName;
              
                hard.SaveAsync();
                break;
            default:
                break;
        }

    }

    private void Update()
    {
        ChangeText();
    }

    private void ChangeText()
    {
        t = ResultToTitle.TimeCounter(t);

        if(t > 1)
        {
            iText.text = iString;

            if (1 < t && t <= 1.017)
            {
                PlaySE(SE1);
            }
        }
        if(t > 2)
        {
            mText.text = mString;

            if (2 < t && t <= 2.017)
            {
                PlaySE(SE1);
            }
        }
        if(t > 3)
        {
            pmText.text = perString;

            if (3 < t && t <= 3.017)
            {
                PlaySE(SE1);
            }
        }
        if(t > 4.5)
        {
            sText.text = sString;

            if (4.5 < t && t <= 4.517)
            {
                PlaySE(SE2);
            }
        }
    }

    private float TimeCounter()
    {
        time += Time.deltaTime;
        return time;
    }

    private void PlaySE(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("効果音なし");
        }
    }
}
