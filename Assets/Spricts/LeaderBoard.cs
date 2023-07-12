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
        // ���ʂ̃J�E���g
        int count = 0;
        List<string> usedUserNames = new List<string>(); // �d������UserName���i�[���郊�X�g

        // �f�[�^�X�g�A�́udata�v�N���X���猟��
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("data");
        // Score�t�B�[���h�̍~���Ńf�[�^���擾
        query.OrderByDescending("score");
        // ����������10���ɐݒ�
        query.Limit = 1000;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                UnityEngine.Debug.Log("�����L���O�擾���s");
            }
            else
            {
                // �����������̏���
                UnityEngine.Debug.Log("�����L���O�擾����");

                string tempScore = "";

                // �l�ƃC���f�b�N�X�̃y�A�����[�v����
                for (int i = 0; i < objList.Count; i++)
                {
                    NCMBObject obj = objList[i];
                    string userName = obj["UserName"] as string;

                    // �d������UserName�̏ꍇ�̓X�L�b�v
                    if (usedUserNames.Contains(userName))
                    {
                        continue;
                    }

                    count++;
                    // ���[�U�[�l�[���ƃX�R�A����ʕ\��
                    tempScore += count.ToString() + "�ʁF" + userName + "�@�X�R�A�F" + obj["score"] + "\r\n";
                    usedUserNames.Add(userName);

                    // ���10���܂ŕ\��
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
    // �f�[�^�X�g�A�́udata�v�N���X���猟��
    NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("data");
    // ���[�U�[���Ō����������w��
    query.WhereEqualTo("UserName", "a");
    // Score�t�B�[���h�̍~���Ńf�[�^���擾
    query.OrderByDescending("score");
    // ����������10���ɐݒ�
    query.Limit = 10;
    query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
    {
        if (e != null)
        {
            UnityEngine.Debug.Log("���[�U�[�����L���O�擾���s");
        }
        else
{
    // �����������̏���
    UnityEngine.Debug.Log("���[�U�[�����L���O�擾����");

    string tempScore = "";

    // �l�ƃC���f�b�N�X�̃y�A�����[�v����
    for (int i = 0; i < objList.Count; i++)
    {
        NCMBObject obj = objList[i];
        string userName = obj["UserName"] as string;

        // ���[�U�[�l�[���ƃX�R�A����ʕ\��
        tempScore += (i + 1).ToString() + "�ʁF" + userName + "�@�X�R�A�F" + obj["score"] + "\r\n";
    }

    userRanking.text = tempScore;
}
    });
}
}