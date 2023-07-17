using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using System.Linq;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using System.Text;
using System.Globalization;


public class LeaderBoard : MonoBehaviour
{
    private void Start()
    {

        fetchTopRankers();
        fetchUserRanking();
        fetchUserRank();
    }

    public Text TopRankers;
    public Text TopRankersScore;
    public Text TopOneToTen;

    void fetchTopRankers()
    {
            // 順位のカウント
            int count = 0;
            List<string> usedUserNames = new List<string>(); // 重複したUserNameを格納するリスト

            int ModeFlag = RankingManager.GetFlag();

            // データストアの「data」クラスから検索
            NCMBQuery<NCMBObject> query;
            if (ModeFlag == 1)
            {
                query = new NCMBQuery<NCMBObject>("nomal");
            }
            else if (ModeFlag == 2)
            {
                query = new NCMBQuery<NCMBObject>("careful");
            }
            else
            {
                UnityEngine.Debug.Log("無効なモードフラグ");
                return;
            }

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

                    // ユーザーネームを格納する変数とスコアを格納する変数
                    string userNames = "";
                    string scores = "";
                    string oneToTen = "";

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
                        usedUserNames.Add(userName);

                        // ユーザーネームとスコアを文字列に追加
                        userNames += userName + "\r\n";
                        scores += obj["score"] + "\r\n";
                        oneToTen += count.ToString() + "位：" + "\r\n";

                        // 上位10件まで表示
                        if (count >= 10)
                        {
                            break;
                        }
                    }

                    // 分割したテキストをそれぞれのテキストコンポーネントに設定
                    TopRankers.GetComponent<Text>().text = userNames;
                    TopRankersScore.GetComponent<Text>().text = scores;
                    TopOneToTen.GetComponent<Text>().text = oneToTen;
                }
            });
        }



    public Text userRanking;
    public Text userOneToTen;

    void fetchUserRanking()
    {

        int ModeFlag = RankingManager.GetFlag();
        NCMBQuery<NCMBObject> query;
        if (ModeFlag == 1)
        {
            query = new NCMBQuery<NCMBObject>("nomal");
        }
        else if (ModeFlag == 2)
        {
            query = new NCMBQuery<NCMBObject>("careful");
        }
        else
        {
            UnityEngine.Debug.Log("無効なモードフラグ");
            return;
        }



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
                UnityEngine.Debug.Log("ユーザーランキング取得失敗");
            }
            else
            {
                // 検索成功時の処理
                UnityEngine.Debug.Log("ユーザーランキング取得成功");

                string tempScore = "";
                string oneToTen = "";

                // 値とインデックスのペアをループ処理
                for (int i = 0; i < objList.Count; i++)
                {
                    NCMBObject obj = objList[i];
                    // ユーザーネームとスコアを画面表示
                    tempScore += obj["score"] + "\r\n";
                    oneToTen += (i + 1).ToString() + "位：" + "\r\n";
                }

                userRanking.text = tempScore;
                userOneToTen.text = oneToTen;
            }
        });
    }

    public Text Rank;

    void fetchUserRank()
    {
        NCMBUser currentUser = NCMBUser.CurrentUser;
        if (currentUser != null)
        {
            UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);

            string userName = currentUser.UserName;

            int userRank = -1;

            Dictionary<string, bool> usedUserNames = new Dictionary<string, bool>();

            // データストアの「data」クラスから検索
            int ModeFlag = RankingManager.GetFlag();
            NCMBQuery<NCMBObject> query;
            if (ModeFlag == 1)
            {
                query = new NCMBQuery<NCMBObject>("nomal");
            }
            else if (ModeFlag == 2)
            {
                query = new NCMBQuery<NCMBObject>("careful");
            }
            else
            {
                UnityEngine.Debug.Log("無効なモードフラグ");
                return;
            }
            // Scoreフィールドの降順でデータを取得
            query.OrderByDescending("score");
            // 検索件数を10件に設定
            query.Limit = 10000;
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
                    for (int i = 0; i < objList.Count; i++)
                    {
                        NCMBObject obj = objList[i];
                        string objUserName = obj["UserName"] as string;

                        // 重複したUserNameの場合はスキップ
                        if (usedUserNames.ContainsKey(objUserName))
                        {
                            continue;
                        }

                        usedUserNames.Add(objUserName, true);

                        // ユーザーネームが一致する場合、ランクを設定
                        if (objUserName == userName)
                        {
                            userRank = i + 1;
                            break;
                        }
                    }

                    CountUniqueUsers((uniqueUserCount) =>
                    {
                        if (userRank == -1)
                        {
                            Rank.GetComponent<Text>().text = "ランキング未参加です";
                        }
                        else
                        {
                            if (userRank != objList.Count)
                            {
                                Rank.GetComponent<Text>().text = "あなたは" + uniqueUserCount.ToString() + "人中" + userRank.ToString() + "位です";

                            }
                            else
                            {
                                Rank.GetComponent<Text>().text = "あなたは" + uniqueUserCount.ToString() + "人の中で最下位です";
                            }
                        }
                    });
                }
            });
        }
        else
        {
            Rank.GetComponent<Text>().text = "未ログインまたは取得に失敗";
        }
    }

    void CountUniqueUsers(Action<int> callback)
    {
        int ModeFlag = RankingManager.GetFlag();
        NCMBQuery<NCMBObject> query;
        if (ModeFlag == 1)
        {
            query = new NCMBQuery<NCMBObject>("nomal");
        }
        else if (ModeFlag == 2)
        {
            query = new NCMBQuery<NCMBObject>("careful");
        }
        else
        {
            UnityEngine.Debug.Log("無効なモードフラグ");
            return;
        }

        query.Limit = 1000;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                UnityEngine.Debug.Log("ランキング取得失敗");
                callback(0); // ユーザー数を0としてコールバックする
            }
            else
            {
                // 検索成功時の処理
                UnityEngine.Debug.Log("ランキング取得成功");

                HashSet<string> uniqueUserNames = new HashSet<string>(); // 重複のないユーザーネームを格納するHashSet

                // 値とインデックスのペアをループ処理
                for (int i = 0; i < objList.Count; i++)
                {
                    NCMBObject obj = objList[i];
                    string userName = obj["UserName"] as string;

                    // 重複したUserNameの場合はスキップ
                    if (uniqueUserNames.Contains(userName))
                    {
                        continue;
                    }

                    uniqueUserNames.Add(userName);
                }

                callback(uniqueUserNames.Count);
            }
        });
    }

}