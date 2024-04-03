using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawn : MonoBehaviour
{
    public GameObject[] titelPrefabs;
    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnPos = 0;
    private float tileLength = 100;
    private int startTiles = 6;
    private int z = 0;
    private int poy = 0;

    [SerializeField] private Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < startTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(3);
            }
            SpawnTile(Random.Range(0,titelPrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - 60 > spawnPos - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(0,titelPrefabs.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)

    {
        
        if (z==4)
        {
            Vector3 newPosition1 = new Vector3(0, poy, spawnPos);
            var transformPosition = transform.position;
            GameObject nextTile = Instantiate(titelPrefabs[tileIndex], newPosition1, transform.rotation);
            activeTiles.Add(nextTile);
            spawnPos += tileLength+25;
            poy += 4;
            z = 0;
        }
        else
        {
            Vector3 newPosition2 = new Vector3(0, poy, spawnPos);
            GameObject nextTile = Instantiate(titelPrefabs[tileIndex], newPosition2, transform.rotation);
            activeTiles.Add(nextTile);
            spawnPos += tileLength;
            z++;
        }
    }

    private void DeleteTile()
    {
       Destroy(activeTiles[0]);
       activeTiles.RemoveAt(0);
    }
}
