using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class Status : MonoBehaviour
{
    public Text Enemy;
    public Text TypeSpeed;
    public Text Accuracy;
    public Text InputSum;
    public Text UserName;

    // Start is called before the first frame update
    void Start()
    {
        FetchUserData();
        fetchUserTopRank();
    }

    void FetchUserData()
    {
        NCMBUser currentUser = NCMBUser.CurrentUser;

      
       if (currentUser != null)
        {
            UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);

            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserData");
            query.WhereEqualTo("UserName", currentUser.UserName);

            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    UnityEngine.Debug.Log("取得失敗: " + e.ErrorMessage);
                }
                else
                {
                    UnityEngine.Debug.Log("取得成功");

                    int enemyCount = 0;
                    int vaildSum = 0;
                    int missSum = 0;
                    float timeSum = 0;
                    foreach (NCMBObject obj in objList)
                    {
                        enemyCount = int.Parse(obj["enemy"].ToString());
                        missSum = int.Parse(obj["missSum"].ToString());
                        vaildSum = int.Parse(obj["validSum"].ToString());
                        timeSum = float.Parse(obj["timeSum"].ToString());
                    }

                    float typeSpeed = (float)Math.Round((float)vaildSum / timeSum, 1);
                    float accuracy = (float)Math.Round((float)vaildSum / (vaildSum + missSum)*100, 1);
                    int inputSum = vaildSum + missSum;

                    if (timeSum == 0)
                    {
                        Enemy.text = "0体";
                        TypeSpeed.text = "データがありません";
                        Accuracy.text = "データがありません";
                        InputSum.text = "データがありません";

                    }
                    else
                    {
                        Enemy.text = enemyCount.ToString() + "体";
                        TypeSpeed.text = typeSpeed.ToString() + "文字/s";
                        Accuracy.text = accuracy.ToString() + "%";
                        InputSum.text = inputSum.ToString() + "回";
                    }

                    UserName.text = currentUser.UserName;
                }
            });
        }
      else
    {
            UnityEngine.Debug.Log("未ログインまたは取得に失敗");
        }
    }

    public Text HighScore;


    void fetchUserTopRank()
    {
        // データストアの「data」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("nomal");


        NCMBUser currentUser = NCMBUser.CurrentUser;
        if (currentUser != null)
        {
            UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);
        }
        else
        {
            UnityEngine.Debug.Log("未ログインまたは取得に失敗");
        }

        // ユーザー名で検索条件を指定
        query.WhereEqualTo("UserName", currentUser.UserName);

        //query.WhereEqualTo("UserName", "a");//テスト用

        // Scoreフィールドの降順でデータを取得
        query.OrderByDescending("score");
        // 検索件数を10件に設定
        query.Limit = 10;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                UnityEngine.Debug.Log("ハイスコア取得失敗");
            }
            else
            {
                // 検索成功時の処理
                UnityEngine.Debug.Log("ハイスコア取得成功");
                string rank = "";

                // 値とインデックスのペアをループ処理
                for (int i = 0; i < objList.Count; i++)
                {
                    NCMBObject obj = objList[i];
                    if (i == 0)
                    {
                        // 一位のスコアをrank変数に格納
                        rank += obj["score"];
                    }
                }
                HighScore.text = rank;
            }
        });
    }

}
