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
        // ���[�U�[���X�g�̐���
        List<string> users = GenerateUserList();

        // ���[�U�[���ƂɃf�[�^�𐶐����ĕۑ�
        foreach (string user in users)
        {
            NCMBObject data = new NCMBObject("careful");

            // 20�g�̃����_���ȓ_���f�[�^�𐶐����ĕۑ�
            for (int i = 0; i < 20; i++)
            {
                int score = UnityEngine.Random.Range(800, 1201);
                data["UserName"] = user;
                data["score"] =score;
            }

            data.SaveAsync();
        }
    }

    // �A���t�@�x�b�g���̃��[�U�[���X�g�𐶐����郁�\�b�h
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