using NCMB;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDummydata : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ユーザーリストの生成
        List<string> users = GenerateUserList();

        // ユーザーごとにデータを生成して保存
        foreach (string user in users)
        {
            NCMBObject data = new NCMBObject("careful");

            // 20組のランダムな点数データを生成して保存
            for (int i = 0; i < 20; i++)
            {
                int score = UnityEngine.Random.Range(800, 1201);
                data["UserName"] = user;
                data["score"] =score;
            }

            data.SaveAsync();
        }
    }

    // アルファベット順のユーザーリストを生成するメソッド
    private List<string> GenerateUserList()
    {
        List<string> users = new List<string>();

        for (char c = 'a'; c <= 'z'; c++)
        {
            users.Add(c.ToString());
        }

        return users;
    }
}