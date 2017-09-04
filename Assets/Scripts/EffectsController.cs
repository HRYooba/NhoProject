using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{

    public GameObject[] Effects;
    private int EffectNum;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Effects.Length; i++)
        {
            if (Effects[i].active)
            {
                EffectNum = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            EffectNum++;
            if (EffectNum > Effects.Length - 1)
            {
                EffectNum = 0;
            }
            ActiveInit();
            Effects[EffectNum].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EffectNum--;
            if (EffectNum < 0)
            {
                EffectNum = Effects.Length - 1;
            }
            ActiveInit();
            Effects[EffectNum].SetActive(true);
        }
    }

    void ActiveInit()
    {
        for (int i = 0; i < Effects.Length; i++)
        {
            Effects[i].SetActive(false);
        }
    }
}
