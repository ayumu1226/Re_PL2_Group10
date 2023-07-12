using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine.SceneManagement;
using System.Text;

public class LeaderBoard : MonoBehaviour
{
    private void Start()
    {

        fetchTopRankers();
        fetchUserRanking();
    }

    public Text TopRankers;

    void fetchTopRankers()
    {
        // 順位のカウント
        int count = 0;
        List<string> usedUserNames = new List<string>(); // 重複したUserNameを格納するリスト

        // データストアの「data」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("data");
        // Scoreフィールドの降順でデータを取得
        query.OrderByDescending("score");
        // 検索件数を10件に設定
        query.Limit = 1000;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                UnityEngine.Debug.Log("ランキング取得失敗");
            }
            else
            {
                // 検索成功時の処理
                UnityEngine.Debug.Log("ランキング取得成功");

                string tempScore = "";

                // 値とインデックスのペアをループ処理
                for (int i = 0; i < objList.Count; i++)
                {
                    NCMBObject obj = objList[i];
                    string userName = obj["UserName"] as string;

                    // 重複したUserNameの場合はスキップ
                    if (usedUserNames.Contains(userName))
                    {
                        continue;
                    }

                    count++;
                    // ユーザーネームとスコアを画面表示
                    tempScore += count.ToString() + "位：" + userName + "　スコア：" + obj["score"] + "\r\n";
                    usedUserNames.Add(userName);

                    // 上位10件まで表示
                    if (count >= 10)
                    {
                        break;
                    }
                }

                TopRankers.GetComponent<Text>().text = tempScore;
            }
        });
    }
    public Text userRanking;

    void fetchUserRanking()
    { 
    // データストアの「data」クラスから検索
    NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("data");
    // ユーザー名で検索条件を指定
    query.WhereEqualTo("UserName", "a");
    // Scoreフィールドの降順でデータを取得
    query.OrderByDescending("score");
    // 検索件数を10件に設定
    query.Limit = 10;
    query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
    {
        if (e != null)
        {
            UnityEngine.Debug.Log("ユーザーランキング取得失敗");
        }
        else
{
    // 検索成功時の処理
    UnityEngine.Debug.Log("ユーザーランキング取得成功");

    string tempScore = "";

    // 値とインデックスのペアをループ処理
    for (int i = 0; i < objList.Count; i++)
    {
        NCMBObject obj = objList[i];
        string userName = obj["UserName"] as string;

        // ユーザーネームとスコアを画面表示
        tempScore += (i + 1).ToString() + "位：" + userName + "　スコア：" + obj["score"] + "\r\n";
    }

    userRanking.text = tempScore;
}
    });
}
}