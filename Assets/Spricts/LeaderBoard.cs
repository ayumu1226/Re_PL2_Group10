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
                error.text = "���O�C���Ɏ��s: " + e.ErrorMessage;
                UnityEngine.Debug.Log("���O�C���Ɏ��s: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("���O�C���ɐ����I");
                SceneManager.LoadScene("ChooseModeScene");
            }
        });

        fetchTopRankers();
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
        NCMBUser currentUser = NCMBUser.CurrentUser;

        // ���ʂ̃J�E���g
        int count = 0;
        string tempScore = "";
        // �f�[�^�X�g�A�́ueasyData�v�N���X���猟��
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("easyData");
        // CurrentUser�̃L�[�ɑΉ�����X�R�A�݂̂�����
        query.WhereEqualTo("UserName", currentUser.UserName);
        // Score�t�B�[���h�̍~���Ńf�[�^���擾
        query.OrderByDescending("score");
        // ����������10���ɐݒ�
        query.Limit = 10;
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

                // �l�ƃC���f�b�N�X�̃y�A�����[�v����
                foreach (NCMBObject obj in objList)
                {
                    count++;
                    // ���[�U�[�l�[���ƃX�R�A����ʕ\��
                    tempScore += count.ToString() + "�ʁF" + " �X�R�A�F" + obj["score"] + "\r\n";
                }

                userRanking.GetComponent<Text>().text = tempScore;
            }
        });
    }
    public Text userRankText;

    void FetchRank()
    {
        NCMBUser currentUser = NCMBUser.CurrentUser;

        // ���ʂ̃J�E���g
        int rank = 0;
        int userRank = -1; // ���[�U�[�̃����N (-1�͖������N�t����\��)

        // �f�[�^�X�g�A�́ueasyData�v�N���X���猟��
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("easyData");
        // Score�t�B�[���h�̍~���Ńf�[�^���擾
        query.OrderByDescending("score");
        // ����������1000���ɐݒ�
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

                // �l�ƃC���f�b�N�X�̃y�A�����[�v����
                foreach (NCMBObject obj in objList)
                {
                    rank++;

                    string userName = obj["UserName"] as string;

                    // ���[�U�[��Current User�ƈ�v����ꍇ�A�����N��ۑ�
                    if (userName == currentUser.UserName)
                    {
                        userRank = rank;
                        break;
                    }
                }

                // �����N�����ݒ�̏ꍇ�̓����L���O�O��\��
                if (userRank == -1)
                {
                    userRank = rank + 1;
                }

                // ���[�U�[�����N���e�L�X�g�Ɋi�[
                string userRankTextString = "���[�U�[�����N: " + userRank.ToString();
                userRankText.text = userRankTextString;
            }
        });
    }

}