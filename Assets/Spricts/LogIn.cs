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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    public void Login()
    {
        print(UserName.text);
        print(PassWord.text);

        //★NCMBUserのインスタンス作成 
        NCMBUser user = new NCMBUser();

        // ★ユーザー名とパスワードでログイン
        NCMBUser.LogInAsync(UserName.text, PassWord.text, (NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("ログインに成功！");
                //画面遷移
                SceneManager.LoadScene("ChooseLevelScene");
            }
        });
    }
    public void SignUp()
    {
        print(UserName.text);
        print(PassWord.text);

        //NCMBUserのインスタンス作成 
        NCMBUser user = new NCMBUser();

        //ユーザ名とパスワードの設定
        user.UserName = UserName.text;
        user.Password = PassWord.text;

        //会員登録
        user.SignUpAsync((NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("新規登録に失敗: " + e.ErrorMessage);
                SceneManager.LoadScene(*); 
            }
            else
            {
                UnityEngine.Debug.Log("新規登録に成功");
                //★カレントユーザーを確認
                NCMBUser currentUser = NCMBUser.CurrentUser;
                if (currentUser != null)
                {
                    UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);
                    Login();
                }
                else
                {
                    UnityEngine.Debug.Log("未ログインまたは取得に失敗");
                }
                //画面遷移
                SceneManager.LoadScene(*);//遷移させたいシーンを指定
            }
        });
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
