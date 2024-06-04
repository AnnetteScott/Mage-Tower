using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject keyPrefab;
    private bool keySpawned = false;

    void Update()
    {
        if (!keySpawned && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            SpawnKey();
            keySpawned = true;
        }
    }

    void SpawnKey()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0); // Set the desired spawn position
        Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Key spawned in the level");
    }
}
