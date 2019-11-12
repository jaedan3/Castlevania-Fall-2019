using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject ToBeSpawned;
    private GameObject Player;
    public Vector2 spawnLocation;
    Rect rect;
    // Start is called before the first frame update
    void Start()
    {
        float xlength = GetComponent<BoxCollider2D>().size.x;
        float ylength = GetComponent<BoxCollider2D>().size.y;
        Player = GetComponent<SpawnSetup>().player;
        rect = new Rect(transform.position.x, transform.position.y, transform.position.x + xlength, transform.position.y + ylength);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("SDFSDF");
        if(collision.gameObject == Player)
        {
            SpawnEnemy(ToBeSpawned, spawnLocation);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame

    void SpawnEnemy(GameObject Type, Vector3 loc)
    {
        GameObject spawned = Instantiate(Type, loc, Quaternion.identity);
        spawned.GetComponent<Enemy>().target = Player;
    }
}
