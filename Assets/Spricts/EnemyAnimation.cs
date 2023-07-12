using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animation anim;

    void Start()
    {
        Debug.Log(anim.name);

        anim = gameObject.GetComponent<Animation>();
    }

    private void Update()
    {
        AnimOn();
    }

    static int beforepoint = 0;

    public  void AnimOn()
    {
        if(beforepoint < Typing.GetPoint())
        {
            anim.Play();
            beforepoint++;
        }
    }
}
