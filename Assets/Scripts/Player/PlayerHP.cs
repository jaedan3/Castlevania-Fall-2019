using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Image[] HP_TICKS;
    public int health;
    private int maxHP = 10;
     

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxHP; i++)
        {
            HP_TICKS[i].enabled = i < health ? true : false;
        }   
    }
}
