using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Typing : MonoBehaviour
{

    [SerializeField] float time;
    [SerializeField] Text tText;
    // ��ʃe�L�X�g
    [SerializeField] Text fText;
    [SerializeField] Text qText;
    [SerializeField] Text aText;

    // �e�L�X�g�f�[�^��ǂݍ���
    [SerializeField] TextAsset _furigana;
    [SerializeField] TextAsset _question;
    //[SerializeField] TextAsset _answer;

    // �e�L�X�g�f�[�^���i�[���郊�X�g
    private List<string> _fList = new List<string>();
    private List<string> _qList = new List<string>();
    //private List<string> _aList = new List<string>();

    // ��ʕ\�������蕶
    private string _fString;
    private string _qString;
    private string _aString;

    // ���Ԗڂ̖�肩
    private int _qNum;

    // ���̉������ڂ�
    private int _aNum;

    //���딻�f
    bool isCorrect = false;

    private Dictionary dictionary;

    private List<string> _romSliceList = new List<string>();

    // �������ڂ̓��͂��������X�g :
    // _furiCountList[i] = 0 �� 1�����ڂ̓���
    private List<int> _furiCountList = new List<int>();

    // ���͂��镶�����A�����̃A���t�@�x�b�g������
    // _romNumList[2] = 0 �� 2�����ڂ�1��̓��͂̂�
    private List<int> _romNumList = new List<int>();

    private void Start()
    {
        Application.targetFrameRate = 60;

        dictionary = GetComponent<Dictionary>();

        // �e�L�X�g�f�[�^�����X�g�ɓ����
        SetList();

        // �����X�V
        NewQuestion();
    }



    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Check();
        }

        TimeCount();

    }

    void SetList()
    {
        string[] _fArray = _furigana.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        _fList.AddRange(_fArray);

        string[] _qArray = _question.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        _qList.AddRange(_qArray);

        //string[] _aArray = _answer.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        //_aList.AddRange(_aArray);
    }

    private void NewQuestion()
    {
        // 0�Ԗڂɖ߂�
        _aNum = 0;

        // �����_���Ȑ����𐶐�
        _qNum = Random.Range(0, _qList.Count);

        //_qNum = 1;

        _fString = _fList[_qNum];
        _qString = _qList[_qNum];

        CreateRomSliceList(_fString);

        _aString = string.Join("", _romSliceList);

        fText.text = _fString;
        qText.text = _qString;
        aText.text = _aString;
    }

    void CreateRomSliceList(string moji)
    {
        _romSliceList.Clear();
        _furiCountList.Clear();
        _romNumList.Clear();

        for (int i = 0; i < moji.Length; i++)
        {
            string a = dictionary.dic[moji[i].ToString()][0];

            if (moji[i].ToString() == "��" && i + 1 < moji.Length)
            {
                a = dictionary.dic[moji[i+1].ToString()][0].ToString();
            }

            _romSliceList.Add(a);

            for (int j = 0; j < a.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
        Debug.Log(string.Join(",", _romSliceList));
    }

    void ReCreateList(List<string> romList)
    {
        _furiCountList.Clear();
        _romNumList.Clear();

        for (int i = 0; i < romList.Count; i++)
        {
            string a = romList[i];

            for (int j = 0; j < a.Length; j++)
            {
                _furiCountList.Add(i);
                _romNumList.Add(j);
            }
        }
        //Debug.Log(string.Join(",", _romSliceList));
    }

    // ���͕����������̏ꍇ
    private void Correct()
    {
        Debug.Log((_aNum + 1) + "������:����");

        // ���̕������o��
        _aNum++;

        // ���������������D�F�ŕ\��
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>"
                     + _aString.Substring(_aNum);
    }

    // ���͕������s�����̏ꍇ
    private void Incorrect()
    {
        Debug.Log((_aNum + 1) + "������:�s����");

        // �ԈႦ��������Ԃ��\��
        aText.text = "<color=#6A6A6A>" + _aString.Substring(0, _aNum) + "</color>"
                     + "<color=#FF0000>" + _aString.Substring(_aNum, 1) + "</color>"
                     + _aString.Substring(_aNum + 1);
    }

    // ���͂��ꂽ�����̐��딻��
    private void Check()
    {
        // ���͒��̕��������Ԗڂ�
        int furiCount = _furiCountList[_aNum];

        isCorrect = false;

        if (Input.GetKeyDown(_aString[_aNum].ToString()))
        {
            isCorrect = true;

            // ����
            Correct();

            if (_aNum >= _aString.Length)
            {
                NewQuestion();
            }
        }
        else if (Input.GetKeyDown("n") && furiCount > 0 && _romSliceList[furiCount - 1] == "n")
        {
            _romSliceList[furiCount - 1] = "nn";
            _aString = string.Join("", _romSliceList);

            ReCreateList(_romSliceList);

            isCorrect = true;

            // ����
            Correct();

            if (_aNum >= _aString.Length)
            {
                NewQuestion();
            }
        }
        else
        {
            string currentFuri = _fString[furiCount].ToString();

            List<string> stringList = dictionary.dic[currentFuri];

            Debug.Log(string.Join(",", stringList));

            for (int i = 0; i < stringList.Count; i++)
            {
                string rom = stringList[i];
                int romNum = _romNumList[_aNum];

                if (Input.GetKeyDown(rom[romNum].ToString()))
                {
                    _romSliceList[furiCount] = rom;
                    _aString = string.Join("", _romSliceList);

                    ReCreateList(_romSliceList);

                    isCorrect = true;

                    // ����
                    Correct();

                    if (_aNum >= _aString.Length)
                    {
                        NewQuestion();
                    }
                    break;
                }
            }
        }
        if (!isCorrect)
        {
            // �s����
            Incorrect();
        }
    }

    void TimeCount()
    {
        time -= Time.deltaTime;
        tText.text = time.ToString("f1");

        GameObject timebar = GameObject.Find("TimebarDirector");
        timebar.GetComponent<TimebarDirector>().DecreaseTime();

        if (time <= 0)
        {
            tText.text = "�I��";
            SceneManager.LoadScene("ResultScene");
        }
    }
}
