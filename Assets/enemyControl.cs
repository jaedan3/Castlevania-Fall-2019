using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControl : MonoBehaviour
{
    public GameObject ToBeSpawned;
    public GameObject Player;
    public Vector2 spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            SpawnEnemy(ToBeSpawned, spawnLocation);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(GameObject Type, Vector3 loc)
    {
        GameObject spawned = Instantiate(Type, loc, Quaternion.identity);
        spawned.GetComponent<Enemy>().target = Player;
    }
}
