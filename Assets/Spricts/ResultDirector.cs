using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.Threading.Tasks;
using System.Net;
using System;
using System.Linq;

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

        if (ButtonDirector.GetMode() != 3)//練習モードでないときデータ格納処理はスキップする
        {


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

            int ModeFlag = ButtonDirector.GetMode();

            //IP格納をする
            string externalIpString = new WebClient().DownloadString("https://ipinfo.io/ip");
            var externalIp = IPAddress.Parse(externalIpString);

            string username = currentUser.UserName; // 更新したいユーザー名

            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserData");
            query.WhereEqualTo("UserName", currentUser.UserName);
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    // 検索失敗時の処理
                }
                else
                {
                    foreach (NCMBObject obj in objList)
                    {
                        obj.Increment("enemy", Typing.GetSum());
                        obj.Increment("missSum", Typing.GetMiss());
                        obj.Increment("validSum", Typing.GetInputNum() - Typing.GetMiss());
                        obj.Increment("timeSum", Typing.GetElapseTime());

                        // データを保存
                        obj.SaveAsync((NCMBException saveException) =>
                        {
                            if (saveException != null)
                            {
                                // 保存失敗時の処理
                            }
                            else
                            {
                                // 保存成功時の処理
                                Debug.Log("Data updated successfully.");
                            }
                        });
                    }
                }
            });



            switch (ModeFlag)
            {
                case 1:
                    // 通常モードに対する処理
                    //NCMBObjectを作成
                    NCMBObject Nomal = new NCMBObject("nomal");

                    //UserNameとscoreをdataクラスに保存
                    Nomal["score"] = sNum;
                    Nomal["UserName"] = currentUser.UserName;
                    Nomal["IP"] = externalIp.ToString();
                    Nomal.SaveAsync();
                    break;
                case 2:
                    // 慎重モードに対する処理
                    //NCMBObjectを作成
                    NCMBObject Careful = new NCMBObject("careful");

                    //UserNameとscoreをdataクラスに保存
                    Careful["score"] = sNum;
                    Careful["UserName"] = currentUser.UserName;
                    Careful["IP"] = externalIp.ToString();

                    Careful.SaveAsync();
                    break;
                default:
                    break;
            }
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
