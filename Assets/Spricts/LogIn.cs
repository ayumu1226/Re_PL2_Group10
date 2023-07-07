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

        //��NCMBUser�̃C���X�^���X�쐬 
        NCMBUser user = new NCMBUser();

        // �����[�U�[���ƃp�X���[�h�Ń��O�C��
        NCMBUser.LogInAsync(UserName.text, PassWord.text, (NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("���O�C���Ɏ��s: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("���O�C���ɐ����I");
                //��ʑJ��
                SceneManager.LoadScene("ChooseLevelScene");
            }
        });
    }
    public void SignUp()
    {
        print(UserName.text);
        print(PassWord.text);

        //NCMBUser�̃C���X�^���X�쐬 
        NCMBUser user = new NCMBUser();

        //���[�U���ƃp�X���[�h�̐ݒ�
        user.UserName = UserName.text;
        user.Password = PassWord.text;

        //����o�^
        user.SignUpAsync((NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("�V�K�o�^�Ɏ��s: " + e.ErrorMessage);
                SceneManager.LoadScene(*); 
            }
            else
            {
                UnityEngine.Debug.Log("�V�K�o�^�ɐ���");
                //���J�����g���[�U�[���m�F
                NCMBUser currentUser = NCMBUser.CurrentUser;
                if (currentUser != null)
                {
                    UnityEngine.Debug.Log("���O�C�����̃��[�U�[: " + currentUser.UserName);
                    Login();
                }
                else
                {
                    UnityEngine.Debug.Log("�����O�C���܂��͎擾�Ɏ��s");
                }
                //��ʑJ��
                SceneManager.LoadScene(*);//�J�ڂ��������V�[�����w��
            }
        });
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
