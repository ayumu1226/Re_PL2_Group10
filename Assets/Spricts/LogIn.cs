using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Collections;
using NCMB;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class LogIn : MonoBehaviour
{
    public Text error;

    public InputField UserName;
    public InputField PassWord;
    public InputField PassWord2;



    private string currentPlayerName;

    void Start()
    {

    }

    void Update()
    {

    }

    public void LogInButton()
    {
        NCMBUser user = new NCMBUser();

        String Password= HashPassword(PassWord.text);


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
    }

    public void SignUpButton()
    {
        NCMBUser user = new NCMBUser();
        user.UserName = UserName.text;
        user.Password = HashPassword(PassWord.text); ;

        if (!IsValidPassword(PassWord.text))
        {
            UnityEngine.Debug.Log("�p�X���[�h�͑啶���A�������A�������܂߂�K�v������܂�");
            error.text = "�V�K�o�^�Ɏ��s:�p�X���[�h�͑啶���A�������A�������܂߂�K�v������܂� ";
            return;
        }
       
        if (PassWord.text!=PassWord2.text)
        {
            UnityEngine.Debug.Log("�㉺�̓��͂���v���Ă��܂���");
            error.text = "�V�K�o�^�Ɏ��s:�㉺�̓��͂���v���Ă��܂���";
            return;
        }



        user.SignUpAsync((NCMBException e) => {
            if (e != null)
            {
                error.text = "�V�K�o�^�Ɏ��s: " + e.ErrorMessage;
                UnityEngine.Debug.Log("�V�K�o�^�Ɏ��s: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("�V�K�o�^�ɐ���");
                NCMBUser currentUser = NCMBUser.CurrentUser;
                if (currentUser != null)
                {
                    UnityEngine.Debug.Log("���O�C�����̃��[�U�[: " + currentUser.UserName);
                    LogInButton();
                }
                else
                {
                    UnityEngine.Debug.Log("�����O�C���܂��͎擾�Ɏ��s");
                }
  
            }
        });
    }
    private bool IsValidPassword(string password)
    {
        // �啶���A�������A���������ꂼ��܂ނ��m�F
        bool hasUpperCase = false;
        bool hasLowerCase = false;
        bool hasDigit = false;

        foreach (char c in password)
        {
            if (char.IsUpper(c))
            {
                hasUpperCase = true;
            }
            else if (char.IsLower(c))
            {
                hasLowerCase = true;
            }
            else if (char.IsDigit(c))
            {
                hasDigit = true;
            }
        }

        return hasUpperCase && hasLowerCase && hasDigit;
    }

    private string HashPassword(string password)
    {
        using (SHA1 sha1 = SHA1.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha1.ComputeHash(passwordBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }


    public void GoToSignUpScene()
    {
        SceneManager.LoadScene("SignUpScene");
    }


}
