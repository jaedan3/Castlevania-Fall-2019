using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TempHealthDisplay : MonoBehaviour
{
    public GameObject player;
    private PlayerHurtbox PHB;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        PHB = player.GetComponent<PlayerHurtbox>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "" + PHB.health;   
    }
}
