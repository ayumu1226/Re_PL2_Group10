using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NCMB;
using UnityEngine.SceneManagement;

public class LogIn : MonoBehaviour
{
    public InputField UserName;
    public InputField PassWord;
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
        NCMBUser.LogInAsync(UserName.text, PassWord.text, (NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("���O�C���Ɏ��s: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("���O�C���ɐ����I");
                SceneManager.LoadScene("ChooseLevelScene");
            }
        });
    }

    public void SignUpButton()
    {

        if (!IsValidPassword(PassWord.text))
        {
            UnityEngine.Debug.Log("�p�X���[�h�͑啶���A�������A�������܂߂�K�v������܂�");
            return;
        }

        NCMBUser user = new NCMBUser();
        user.UserName = UserName.text;
        user.Password = PassWord.text;

        user.SignUpAsync((NCMBException e) => {
            if (e != null)
            {
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



    public void GoToSignUpScene()
    {
        SceneManager.LoadScene("SignUpScene");
    }

    public void Userlogout()
    {
        NCMBUser.LogOutAsync((NCMBException e) => {
            if (e == null)
            {
                UnityEngine.Debug.Log("���O�A�E�g����");
            }
            else
            {
                UnityEngine.Debug.Log("���O�A�E�g�Ɏ��s: " + e.ErrorMessage);
            }
        });
    }
}
