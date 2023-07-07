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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        sum = Typing.GetSum();

        if (sum > beforeSum)
        {
            int randomNo = Random.Range(1, 5);

            string imgStr =randomNo.ToString();

            sprite = Resources.Load<Sprite>(imgStr);
            image = this.GetComponent<Image>();
            image.sprite = sprite;
        }
        beforeSum = sum;
    }

}
