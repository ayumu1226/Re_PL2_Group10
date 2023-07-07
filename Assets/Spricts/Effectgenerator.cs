using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effectgenerator : MonoBehaviour
{
    [SerializeField] ParticleSystem DamageEffect;
    float span = 3f;
    float delta = 0;

    static int beforePoint = 0;
    int point;

    void Update()
    {
        point = Typing.GetPoint();

        if(point > beforePoint)
        {
            ParticleSystem go = Instantiate(DamageEffect);
            int px = Random.Range(-3, 3);
            int py = Random.Range(-2, 3);
            go.transform.position = new Vector3(px, py, 0);
            go.Play();
        }
        beforePoint = point;
    }
}
