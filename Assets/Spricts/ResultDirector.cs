using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System.Threading.Tasks;

public class ResultDirector : MonoBehaviour
{
    private AudioSource audioSource = null;
    public AudioClip SE1;
    public AudioClip SE2;

    [SerializeField] Text iText;
    [SerializeField] Text mText;
    [SerializeField] Text pmText;
    [SerializeField] Text sText;

    private string iString;
    private string mString;
    private string perString;
    private string sString;

    private float perNum;
    private float sNum;
    private static float time;
    float t;

    public void Start()
    {
        Button button1 = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        Application.targetFrameRate = 60;

        if(Typing.GetInputNum() == 0)
        {
            perNum = 0;
        }
        else
        {
            perNum = (Typing.GetInputNum() - Typing.GetMiss()) * 100 / Typing.GetInputNum();
        }
        
        //Debug.Log(perNum);
        sNum = Typing.GetInputNum() * 5 - Typing.GetMiss() * 10;

        iString = Typing.GetInputNum().ToString() + "����";
        mString = Typing.GetMiss().ToString() + "����";
        perString = perNum.ToString() + "��";
        sString = sNum.ToString();

        time = 0;

        iText.text = "";
        mText.text = "";
        pmText.text = "";
        sText.text = "";

        //��������T�[�o�[�ւ̃f�[�^�X�g�A����
        NCMBUser currentUser = NCMBUser.CurrentUser;
        if (currentUser != null)
        {
            UnityEngine.Debug.Log("���O�C�����̃��[�U�[: " + currentUser.UserName);
        }
        else
        {
            UnityEngine.Debug.Log("�����O�C���܂��͎擾�Ɏ��s");
        }

        int LevelFlag=ButtonDirector.GetLevel();
       
        
        switch (LevelFlag)
        {
            case 1:
                // easy�ɑ΂��鏈��
                //NCMBObject���쐬
                NCMBObject easy = new NCMBObject("easyData");

                //UserName��score��data�N���X�ɕۑ�
                easy["score"] = sNum;
                easy["UserName"] = currentUser.UserName;
                
                easy.SaveAsync();
                break;
            case 2:
                // nomal�ɑ΂��鏈��
                //NCMBObject���쐬
                NCMBObject nomal = new NCMBObject("nomalData");

                //UserName��score��data�N���X�ɕۑ�
                nomal["score"] = sNum;
                nomal["UserName"] = currentUser.UserName;
             
                nomal.SaveAsync();
                break;
            case 3:
                // hard�ɑ΂��鏈��
                //NCMBObject���쐬
                NCMBObject hard = new NCMBObject("hardData");

                //UserName��score��data�N���X�ɕۑ�
                hard["score"] = sNum;
                hard["UserName"] = currentUser.UserName;
              
                hard.SaveAsync();
                break;
            default:
                break;
        }

    }

    private void Update()
    {
        ChangeText();
    }

    private void ChangeText()
    {
        t = ResultToTitle.TimeCounter(t);

        if(t > 1)
        {
            iText.text = iString;

            if (1 < t && t <= 1.017)
            {
                PlaySE(SE1);
            }
        }
        if(t > 2)
        {
            mText.text = mString;

            if (2 < t && t <= 2.017)
            {
                PlaySE(SE1);
            }
        }
        if(t > 3)
        {
            pmText.text = perString;

            if (3 < t && t <= 3.017)
            {
                PlaySE(SE1);
            }
        }
        if(t > 4.5)
        {
            sText.text = sString;

            if (4.5 < t && t <= 4.517)
            {
                PlaySE(SE2);
            }
        }
    }

    private float TimeCounter()
    {
        time += Time.deltaTime;
        return time;
    }

    private void PlaySE(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("���ʉ��Ȃ�");
        }
    }
}
