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
        print(UserName.text);
        print(PassWord.text);
        NCMBUser user = new NCMBUser();
        NCMBUser.LogInAsync(UserName.text, PassWord.text, (NCMBException e) => {
            if (e != null)
            {
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
        print(UserName.text);
        print(PassWord.text);

        NCMBUser user = new NCMBUser();
        user.UserName = UserName.text;
        user.Password = PassWord.text;

        user.SignUpAsync((NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("�V�K�o�^�Ɏ��s: " + e.ErrorMessage);
                SceneManager.LoadScene("�J�ڐ�̃V�[����");
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
                SceneManager.LoadScene("�J�ڐ�̃V�[����");
            }
        });
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
