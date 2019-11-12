using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Player;
    public float range;
    Rect rect;
    // Start is called before the first frame update
    void Start()
    {
        float xlength = GetComponent<BoxCollider2D>().size.x;
        float ylength = GetComponent<BoxCollider2D>().size.y;
        rect = new Rect(transform.position.x, transform.position.y, transform.position.x + xlength, transform.position.y + ylength);
    }
    // Update is called once per frame
    private void Update()
    {
        if (Mathf.Pow(Player.transform.position.x - transform.position.x,2) + Mathf.Pow(Player.transform.position.y - transform.position.y, 2) < range)
        {
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        foreach (Transform child in transform)
        {
            child.parent = null;
            child.gameObject.SetActive(true);
            child.GetComponent<Enemy>().target = Player;
            
        }
        Destroy(gameObject, 1);
    }
}
