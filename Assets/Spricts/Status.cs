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
            UnityEngine.Debug.Log("���O�C�����̃��[�U�[: " + currentUser.UserName);

            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("UserData");
            query.WhereEqualTo("UserName", currentUser.UserName);

            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                if (e != null)
                {
                    UnityEngine.Debug.Log("�擾���s: " + e.ErrorMessage);
                }
                else
                {
                    UnityEngine.Debug.Log("�擾����");

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
                        Enemy.text = "0��";
                        TypeSpeed.text = "�f�[�^������܂���";
                        Accuracy.text = "�f�[�^������܂���";
                        InputSum.text = "�f�[�^������܂���";

                    }
                    else
                    {
                        Enemy.text = enemyCount.ToString() + "��";
                        TypeSpeed.text = typeSpeed.ToString() + "����/s";
                        Accuracy.text = accuracy.ToString() + "%";
                        InputSum.text = inputSum.ToString() + "��";
                    }

                    UserName.text = currentUser.UserName;
                }
            });
        }
      else
    {
            UnityEngine.Debug.Log("�����O�C���܂��͎擾�Ɏ��s");
        }
    }

    public Text HighScore;


    void fetchUserTopRank()
    {
        // �f�[�^�X�g�A�́udata�v�N���X���猟��
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("nomal");


        NCMBUser currentUser = NCMBUser.CurrentUser;
        if (currentUser != null)
        {
            UnityEngine.Debug.Log("���O�C�����̃��[�U�[: " + currentUser.UserName);
        }
        else
        {
            UnityEngine.Debug.Log("�����O�C���܂��͎擾�Ɏ��s");
        }

        // ���[�U�[���Ō����������w��
        query.WhereEqualTo("UserName", currentUser.UserName);

        //query.WhereEqualTo("UserName", "a");//�e�X�g�p

        // Score�t�B�[���h�̍~���Ńf�[�^���擾
        query.OrderByDescending("score");
        // ����������10���ɐݒ�
        query.Limit = 10;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                UnityEngine.Debug.Log("�n�C�X�R�A�擾���s");
            }
            else
            {
                // �����������̏���
                UnityEngine.Debug.Log("�n�C�X�R�A�擾����");
                string rank = "";

                // �l�ƃC���f�b�N�X�̃y�A�����[�v����
                for (int i = 0; i < objList.Count; i++)
                {
                    NCMBObject obj = objList[i];
                    if (i == 0)
                    {
                        // ��ʂ̃X�R�A��rank�ϐ��Ɋi�[
                        rank += obj["score"];
                    }
                }
                HighScore.text = rank;
            }
        });
    }

}
