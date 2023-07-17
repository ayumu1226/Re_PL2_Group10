using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NCMB;

public class LogInSceneManagement : MonoBehaviour
{
    public void GoToSignUpScene()
    {
        SceneManager.LoadScene("SignUpScene");
    }

    public void GoToLogInScene()
    {
        SceneManager.LoadScene("LogInScene");
    }
    public void Userlogout()
    {
        NCMBUser.LogOutAsync((NCMBException e) => {
            if (e == null)
            {
                UnityEngine.Debug.Log("���O�A�E�g����");
                SceneManager.LoadScene("TitleScene");
            }
            else
            {
                UnityEngine.Debug.Log("���O�A�E�g�Ɏ��s: " + e.ErrorMessage);
            }
        });
    }
    public void GoToStatusScene()
    {
        SceneManager.LoadScene("StatusScene");
    }
}
