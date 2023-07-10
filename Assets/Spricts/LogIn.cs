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
                UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("ログインに成功！");
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
                UnityEngine.Debug.Log("新規登録に失敗: " + e.ErrorMessage);
                SceneManager.LoadScene("遷移先のシーン名");
            }
            else
            {
                UnityEngine.Debug.Log("新規登録に成功");
                NCMBUser currentUser = NCMBUser.CurrentUser;
                if (currentUser != null)
                {
                    UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);
                    LogInButton();
                }
                else
                {
                    UnityEngine.Debug.Log("未ログインまたは取得に失敗");
                }
                SceneManager.LoadScene("遷移先のシーン名");
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
                UnityEngine.Debug.Log("ログアウト成功");
            }
            else
            {
                UnityEngine.Debug.Log("ログアウトに失敗: " + e.ErrorMessage);
            }
        });
    }
}
