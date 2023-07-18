using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effectgenerator : MonoBehaviour
{
    [SerializeField] ParticleSystem DamageEffect1;
    [SerializeField] ParticleSystem DamageEffect2;

    static int beforePoint = 0;
    static int beforeSum = 0;

    int point = 0;
    int sum = 0;

    void Update()
    {
        point = Typing.GetPoint();
        sum  = Typing.GetSum();

        if(point > beforePoint)
        {
            ParticleSystem go = Instantiate(DamageEffect1);
            int px = Random.Range(-3, 3);
            int py = Random.Range(-2, 3);
            go.transform.position = new Vector3(px, py, 0);
            go.Play();
        }

        if(sum > beforeSum)
        {
            ParticleSystem go2 = Instantiate(DamageEffect2);
            go2.transform.position = new Vector3(0, 0, 0);
            go2.Play();
        }
        beforePoint = point;
        beforeSum = sum;
    }
}
