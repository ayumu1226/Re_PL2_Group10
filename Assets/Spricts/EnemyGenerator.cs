using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Image image;
    private Sprite sprite;

    static int beforeSum = 1;
    int sum = 0;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 1;

        image = this.GetComponent<Image>();
        //image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(time < 0.4)
        {
            time = ResultToTitle.TimeCounter(time);
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }

        sum = Typing.GetSum();

        if (sum > beforeSum)
        {
            ChangeEnemy();
        }

        beforeSum = sum;
    }

    private void ChangeEnemy()
    {
        time = 0;

        int randomNo = Random.Range(1, 5);

        string imgStr = randomNo.ToString();

        sprite = Resources.Load<Sprite>(imgStr);
        image = this.GetComponent<Image>();
        image.sprite = sprite;
    }
}

