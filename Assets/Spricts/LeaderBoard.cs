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
            // ���ʂ̃J�E���g
            int count = 0;
            List<string> usedUserNames = new List<string>(); // �d������UserName���i�[���郊�X�g

            int ModeFlag = RankingManager.GetFlag();

            // �f�[�^�X�g�A�́udata�v�N���X���猟��
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
                UnityEngine.Debug.Log("�����ȃ��[�h�t���O");
                return;
            }

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

                    // ���[�U�[�l�[�����i�[����ϐ��ƃX�R�A���i�[����ϐ�
                    string userNames = "";
                    string scores = "";
                    string oneToTen = "";

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
                        usedUserNames.Add(userName);

                        // ���[�U�[�l�[���ƃX�R�A�𕶎���ɒǉ�
                        userNames += userName + "\r\n";
                        scores += obj["score"] + "\r\n";
                        oneToTen += count.ToString() + "�ʁF" + "\r\n";

                        // ���10���܂ŕ\��
                        if (count >= 10)
                        {
                            break;
                        }
                    }

                    // ���������e�L�X�g�����ꂼ��̃e�L�X�g�R���|�[�l���g�ɐݒ�
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
            UnityEngine.Debug.Log("�����ȃ��[�h�t���O");
            return;
        }



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
                UnityEngine.Debug.Log("���[�U�[�����L���O�擾���s");
            }
            else
            {
                // �����������̏���
                UnityEngine.Debug.Log("���[�U�[�����L���O�擾����");

                string tempScore = "";
                string oneToTen = "";

                // �l�ƃC���f�b�N�X�̃y�A�����[�v����
                for (int i = 0; i < objList.Count; i++)
                {
                    NCMBObject obj = objList[i];
                    // ���[�U�[�l�[���ƃX�R�A����ʕ\��
                    tempScore += obj["score"] + "\r\n";
                    oneToTen += (i + 1).ToString() + "�ʁF" + "\r\n";
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
            UnityEngine.Debug.Log("���O�C�����̃��[�U�[: " + currentUser.UserName);

            string userName = currentUser.UserName;

            int userRank = -1;

            Dictionary<string, bool> usedUserNames = new Dictionary<string, bool>();

            // �f�[�^�X�g�A�́udata�v�N���X���猟��
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
                UnityEngine.Debug.Log("�����ȃ��[�h�t���O");
                return;
            }
            // Score�t�B�[���h�̍~���Ńf�[�^���擾
            query.OrderByDescending("score");
            // ����������10���ɐݒ�
            query.Limit = 10000;
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
                    for (int i = 0; i < objList.Count; i++)
                    {
                        NCMBObject obj = objList[i];
                        string objUserName = obj["UserName"] as string;

                        // �d������UserName�̏ꍇ�̓X�L�b�v
                        if (usedUserNames.ContainsKey(objUserName))
                        {
                            continue;
                        }

                        usedUserNames.Add(objUserName, true);

                        // ���[�U�[�l�[������v����ꍇ�A�����N��ݒ�
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
                            Rank.GetComponent<Text>().text = "�����L���O���Q���ł�";
                        }
                        else
                        {
                            if (userRank != objList.Count)
                            {
                                Rank.GetComponent<Text>().text = "���Ȃ���" + uniqueUserCount.ToString() + "�l��" + userRank.ToString() + "�ʂł�";

                            }
                            else
                            {
                                Rank.GetComponent<Text>().text = "���Ȃ���" + uniqueUserCount.ToString() + "�l�̒��ōŉ��ʂł�";
                            }
                        }
                    });
                }
            });
        }
        else
        {
            Rank.GetComponent<Text>().text = "�����O�C���܂��͎擾�Ɏ��s";
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
            UnityEngine.Debug.Log("�����ȃ��[�h�t���O");
            return;
        }

        query.Limit = 1000;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                UnityEngine.Debug.Log("�����L���O�擾���s");
                callback(0); // ���[�U�[����0�Ƃ��ăR�[���o�b�N����
            }
            else
            {
                // �����������̏���
                UnityEngine.Debug.Log("�����L���O�擾����");

                HashSet<string> uniqueUserNames = new HashSet<string>(); // �d���̂Ȃ����[�U�[�l�[�����i�[����HashSet

                // �l�ƃC���f�b�N�X�̃y�A�����[�v����
                for (int i = 0; i < objList.Count; i++)
                {
                    NCMBObject obj = objList[i];
                    string userName = obj["UserName"] as string;

                    // �d������UserName�̏ꍇ�̓X�L�b�v
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