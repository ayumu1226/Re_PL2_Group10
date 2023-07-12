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

public class LeaderBoard : MonoBehaviour
{
    private void Start()
    {
        NCMBUser user = new NCMBUser();
      
        String UserName="test";

        String Password = LogIn.HashPassword("Password1");


        NCMBUser.LogInAsync(UserName.text, Password, (NCMBException e) => {
            if (e != null)
            {
                error.text = "ログインに失敗: " + e.ErrorMessage;
                UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("ログインに成功！");
                SceneManager.LoadScene("ChooseModeScene");
            }
        });

        fetchTopRankers();
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
        NCMBUser currentUser = NCMBUser.CurrentUser;

        // 順位のカウント
        int count = 0;
        string tempScore = "";
        // データストアの「easyData」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("easyData");
        // CurrentUserのキーに対応するスコアのみを検索
        query.WhereEqualTo("UserName", currentUser.UserName);
        // Scoreフィールドの降順でデータを取得
        query.OrderByDescending("score");
        // 検索件数を10件に設定
        query.Limit = 10;
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

                // 値とインデックスのペアをループ処理
                foreach (NCMBObject obj in objList)
                {
                    count++;
                    // ユーザーネームとスコアを画面表示
                    tempScore += count.ToString() + "位：" + " スコア：" + obj["score"] + "\r\n";
                }

                userRanking.GetComponent<Text>().text = tempScore;
            }
        });
    }
    public Text userRankText;

    void FetchRank()
    {
        NCMBUser currentUser = NCMBUser.CurrentUser;

        // 順位のカウント
        int rank = 0;
        int userRank = -1; // ユーザーのランク (-1は未ランク付けを表す)

        // データストアの「easyData」クラスから検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("easyData");
        // Scoreフィールドの降順でデータを取得
        query.OrderByDescending("score");
        // 検索件数を1000件に設定
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

                // 値とインデックスのペアをループ処理
                foreach (NCMBObject obj in objList)
                {
                    rank++;

                    string userName = obj["UserName"] as string;

                    // ユーザーがCurrent Userと一致する場合、ランクを保存
                    if (userName == currentUser.UserName)
                    {
                        userRank = rank;
                        break;
                    }
                }

                // ランクが未設定の場合はランキング外を表示
                if (userRank == -1)
                {
                    userRank = rank + 1;
                }

                // ユーザーランクをテキストに格納
                string userRankTextString = "ユーザーランク: " + userRank.ToString();
                userRankText.text = userRankTextString;
            }
        });
    }

}